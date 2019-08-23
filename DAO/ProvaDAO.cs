using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdemicoASC.Interfaces;
using AdemicoASC.Models;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Data;

namespace AdemicoASC.DAO
{
    public class ProvaDAO : IDAO<Prova>, IDisposable
    {
        private IConnection conexao;

        public ProvaDAO(IConnection Conexao)
        {
            this.conexao = Conexao;
        }
        
        public Prova inserir(Prova model)
        {
            using (SQLiteCommand comando = conexao.Buscar().CreateCommand())
            {
                try
                {
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "insert into Prova (IDMateria, IDAluno, NotaProva1, NotaProva2, NotaProva3, MediaProva) "+
                        " values (@IDMateria, @IDAluno, @NotaProva1, @NotaProva2, @NotaProva3, @MediaProva)";
                    comando.Parameters.AddWithValue("@IDMateria", model.Materia.IDMateria);
                    comando.Parameters.AddWithValue("@IDAluno", model.Aluno.IDAluno);

                    comando.Parameters.AddWithValue("@NotaProva1", model.NotaProva1);
                    comando.Parameters.AddWithValue("@NotaProva2", model.NotaProva2);
                    comando.Parameters.AddWithValue("@NotaProva3", model.NotaProva3);
                    comando.Parameters.AddWithValue("@MediaProva", model.MediaProva);
                    comando.ExecuteNonQuery();

               }
                catch
                {
                    throw new NotImplementedException();
                }

            }

            return model;

        }

        public Collection<Prova> Listar()
        {
            Collection<Prova> colecao = new Collection<Prova>();

            using (SQLiteCommand comando = conexao.Buscar().CreateCommand())
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select idProva, t.IdTurma, m.IDMateria, a.idAluno, t.NomeTurma, a.NomeAluno,m.nomeMateria, p.NotaProva1, p.NotaProva2, " +
                    "p.NotaProva3, MediaProva from prova p inner join aluno a on a.idaluno = p.idAluno " +
                    "inner join turma t on t.IdTurma = a.IdTurma " +
                    "inner join materia m on m.idmateria = p.idmateria ";

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando))
                {
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);

                    foreach (DataRow row in tabela.Rows)
                    {
                        Prova prova = new Prova
                        {
                            IDProva = int.Parse(row["IDProva"].ToString()),
                            NotaProva1 = double.Parse(row["NotaProva1"].ToString()),
                            NotaProva2 = double.Parse(row["NotaProva2"].ToString()),
                            NotaProva3 = double.Parse(row["NotaProva3"].ToString()),
                            MediaProva = double.Parse(row["MediaProva"].ToString()),
                            IDAluno = int.Parse(row["IDAluno"].ToString()),
                            IDMateria = int.Parse(row["IDMateria"].ToString())
                        };

                        colecao.Add(prova);
                    }
                }
            }

            return colecao;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Prova ListarPorCodigo(int codigo)
        {
            throw new NotImplementedException();
        }

        public void ExcluirTodos()
        {
            using (SQLiteCommand comando = conexao.Buscar().CreateCommand())
            {
                try
                {
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "delete from Prova";
                    comando.ExecuteNonQuery();

                }
                catch
                {
                    throw new System.ArgumentException("Erro ao deletar Prova", "Erro");
                }
            }

        }
    }
}