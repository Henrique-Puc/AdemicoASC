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
    public class AlunoDAO : IDAO<Aluno>, IDisposable
    {
        private IConnection conexao;

        public AlunoDAO(IConnection Conexao)
        {
            this.conexao = Conexao;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Aluno inserir(Aluno model)
        {

            using (SQLiteCommand comando = conexao.Buscar().CreateCommand())
            {
                try
                {
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "insert into Aluno (NomeAluno, IDTurma) values (@NomeTurma, @IDTurma)";
                    comando.Parameters.AddWithValue("@NomeTurma", model.Nome);
                    comando.Parameters.AddWithValue("@IDTurma", model.Turma.IdTurma);
                    comando.ExecuteNonQuery();
                }
                catch
                {
                    throw new NotImplementedException();
                }

            }
            return model;
        }

        public Collection<Aluno> Listar()
        {
            Collection<Aluno> colecao = new Collection<Aluno>();

            using (SQLiteCommand comando = conexao.Buscar().CreateCommand())
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select * from Aluno where Simulado = 0";

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando))
                {
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);

                    foreach (DataRow row in tabela.Rows)
                    {
                        Aluno aluno = new Aluno
                        {
                            IDAluno = int.Parse(row["IDAluno"].ToString()),
                            Nome = row["IDAluno"].ToString()
                        };

                        colecao.Add(aluno);
                    }
                }
            }
            return colecao;
        }


        public List<Aluno> ListarAluno()
        {
            List<Aluno> colecao = new List<Aluno>();

            using (SQLiteCommand comando = conexao.Buscar().CreateCommand())
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select * from Aluno";

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando))
                {
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);

                    foreach (DataRow row in tabela.Rows)
                    {
                        Aluno aluno = new Aluno
                        {
                            IDAluno = int.Parse(row["IDAluno"].ToString()),
                            Nome = row["IDAluno"].ToString()
                        };

                        colecao.Add(aluno);
                    }
                }
            }
            return colecao;

        }

        public void alterarStatus(Aluno model)
        {
            using (SQLiteCommand comando = conexao.Buscar().CreateCommand())
            {
                try
                {
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "update Aluno set Simulado = 1 where IDAluno = @IDAluno";
                    comando.Parameters.AddWithValue("@IDAluno", model.IDAluno);
                    comando.ExecuteNonQuery();
                }
                catch
                {
                    throw new NotImplementedException();
                }
            }
        }

        public Aluno ListarPorCodigo(int codigo)
        {
            Aluno aluno = null;

            using (SQLiteCommand comando = conexao.Buscar().CreateCommand())
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select * from Aluno where IDAluno = @IDAluno";
                comando.Parameters.AddWithValue("@IDAluno", codigo);

                using (SQLiteDataReader  reader =  comando.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        aluno = new Aluno();
                        reader.Read();
                        aluno.IDAluno = reader.GetInt32(0);
                        aluno.Nome = reader.GetString(1);
                        aluno.IDTurma = reader.GetInt32(2);
                        
                    }

                }
            }
            return aluno;
        }

        public void ExcluirTodos()
        {
            using (SQLiteCommand comando = conexao.Buscar().CreateCommand())
            {
                try
                {
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "delete from Aluno";
                    comando.ExecuteNonQuery();

                }
                catch
                {
                    throw new System.ArgumentException("Erro ao deletar Aluno", "Erro");
                }
            }
        }

    }
    }