using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdemicoASC.Interfaces;
using AdemicoASC.Classes;
using AdemicoASC.Models;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data.SQLite;


namespace AdemicoASC.DAO
{
    public class TurmaDAO : IDAO<Turma>, IDisposable
    {

        private IConnection conexao;

        public TurmaDAO(IConnection Conexao)
        {
            this.conexao = Conexao;
        }

        public Turma inserir(Turma model)
        {
             using (SQLiteCommand comando = conexao.Buscar().CreateCommand())
            {
                try
                {
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "insert into Turma (NomeTurma) values (@NomeTurma)";
                    comando.Parameters.AddWithValue("@NomeTurma", model.Nome);
                    comando.ExecuteNonQuery();

                } catch
                {

                }

                comando.CommandText = "select max(IDTurma) from Turma ";
                model.IdTurma = int.Parse(comando.ExecuteScalar().ToString());
            }

            return model;
        }

        public Collection<Turma> Listar()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Turma ListarPorCodigo(int codigo)
        {
            Turma turma = null;

            using (SQLiteCommand comando = conexao.Buscar().CreateCommand())
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select * from Turma where IDTurma = @IDTurma";
                comando.Parameters.AddWithValue("@IDTurma", codigo);

                using (SQLiteDataReader reader = comando.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        turma = new Turma();
                        reader.Read();
                        turma.IdTurma = reader.GetInt32(0);
                        turma.Nome = reader.GetString(1);
                    }
                }
            }
            return turma;

        }

        public void ExcluirTodos()
        {
            using (SQLiteCommand comando = conexao.Buscar().CreateCommand())
            {
                try
                {
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "delete from Turma";
                    comando.ExecuteNonQuery();

                }
                catch
                {
                    throw new System.ArgumentException("Erro ao deletar a Turma", "Erro");
                }
            }

        }
    }
}