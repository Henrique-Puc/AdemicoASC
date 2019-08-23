using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdemicoASC.Classes;
using AdemicoASC.Models;
using AdemicoASC.Interfaces;
using AdemicoASC.DAO;
using System.Collections.ObjectModel;

namespace AdemicoASC.Controllers
{
    public class MateriaController : Controller
    {
        // GET: Materia
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dados()
        {
            Collection<Materia> listaMateria;

            using (IConnection Conexao = new Connection())
            {
                IDAO<Materia> materiaDAO = new MateriaDAO(Conexao);
                listaMateria = new Collection<Materia>();
                listaMateria = materiaDAO.Listar();

            }

            return View(listaMateria);
        }


        [HttpPost]
        public JsonResult GravarMateria(Materia materia)
        {
            string resposta = Util.Resultado.Ok.ToString();

            using (IConnection Conexao = new Connection())
            {

                Conexao.Abrir();
                IDAO<Materia> materiaDAO = new MateriaDAO(Conexao);

                materiaDAO.inserir(materia);

            }

            return Json(resposta);
        }

    }
}