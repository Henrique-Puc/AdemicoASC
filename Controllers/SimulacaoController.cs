using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdemicoASC.Models;
using AdemicoASC.Interfaces;
using AdemicoASC.Classes;
using AdemicoASC.DAO;
using System.Collections.ObjectModel;

namespace AdemicoASC.Controllers
{
    public class SimulacaoController : Controller
    {
        // GET: Simulacao
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult GeraAlunos(int qtdTurmas, int qtdAlunos)
        {
            string resposta = Util.Resultado.Ok.ToString();

            using (IConnection Conexao = new Connection())
            {

                Conexao.Abrir();
                IDAO<Turma>  turmaDAO = new TurmaDAO(Conexao);
                IDAO<Aluno> alunoDAO = new AlunoDAO(Conexao);
                IDAO<Prova> provaDAO = new ProvaDAO(Conexao);
                IDAO<Materia> materiaDAO = new MateriaDAO(Conexao);

                provaDAO.ExcluirTodos();
                materiaDAO.ExcluirTodos();
                alunoDAO.ExcluirTodos();
                turmaDAO.ExcluirTodos();

                Turma turma = new Turma();
                Aluno aluno = new Aluno();

                //Gera as turmas automaticamente...
                for (var i = 1; i <= qtdTurmas; i++)
                {
                     turma.Nome = Util.cTurma + i.ToString();
                     turmaDAO.inserir(turma);
                    
                      for (var y=1; y <= qtdAlunos; y++) 
                     {
                          aluno.Nome = Util.cAluno + y.ToString();
                          aluno.Turma = turma;

                        alunoDAO.inserir(aluno);
                    }
                }
            }

            return Json(resposta);
        }


        public ActionResult Materia()
        {
            return View();
        }


        public JsonResult Simular()
        {
            using (IConnection Conexao = new Connection())
            {
                IDAO<Aluno> alunoDAO = new AlunoDAO(Conexao);
                IDAO<Materia> materiaDAO = new MateriaDAO(Conexao);
                IDAO<Prova> provaDAO = new ProvaDAO(Conexao);

                Collection<Aluno> listaAluno = new  Collection<Aluno>();
                listaAluno = alunoDAO.Listar();

                Collection<Materia> listaMateria = new Collection<Materia>();
                listaMateria = materiaDAO.Listar();


                foreach (Aluno aluno in listaAluno)
                {
                    
                    foreach (Materia materia in listaMateria)
                    {

                        Prova prova = new Prova();
                        prova.Materia = materia;
                        prova.Aluno = aluno;

                        Random random = new Random();
                        prova.NotaProva1 = Math.Round((random.NextDouble() * (10 - 4) + 4), 1); 
                        prova.NotaProva2 = Math.Round((random.NextDouble() * (10 - 4) + 4), 1); 
                        prova.NotaProva3 = Math.Round((random.NextDouble() * (10 - 4) + 4), 1); 

                        double p1 = prova.Materia.PesoProva1 * prova.NotaProva1;
                        double p2 = prova.Materia.PesoProva2 * prova.NotaProva2;
                        double p3 = prova.Materia.PesoProva3 * prova.NotaProva3;
                        double somaPeso = prova.Materia.PesoProva1 + prova.Materia.PesoProva2 + prova.Materia.PesoProva3;

                        double mediaPonderada = (p1 + p2 + p3) / (somaPeso);

                        prova.MediaProva = Math.Round(mediaPonderada, 1);

                        provaDAO.inserir(prova);

                        if (prova.MediaProva >= 6 || prova.MediaProva <= 4)
                        {
                            AlunoDAO alunoAlterar = new AlunoDAO(Conexao);
                            alunoAlterar.alterarStatus(aluno);
                        }
                    }
                }
            }

            return Json("Ok");
        }

        public ActionResult Resultado()
        {
            using (IConnection Conexao = new Connection())
            {
                IDAO<Aluno> alunoDAO = new AlunoDAO(Conexao);
                IDAO<Turma> turmaDAO = new TurmaDAO(Conexao);
                IDAO<Materia> materiaDAO = new MateriaDAO(Conexao);
                IDAO<Prova> provaDAO = new ProvaDAO(Conexao);
                Aluno aluno = new Aluno();

                Collection<Prova> prova = provaDAO.Listar();

                foreach (var item in prova)
                {
                    item.Aluno = alunoDAO.ListarPorCodigo(item.IDAluno);
                    item.Aluno.Turma = turmaDAO.ListarPorCodigo(item.Aluno.IDTurma);
                    item.Materia = materiaDAO.ListarPorCodigo(item.IDMateria);
                }

                return View(prova);
            }
        }


    }
}
