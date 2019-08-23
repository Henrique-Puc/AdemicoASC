using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdemicoASC.Interfaces;
using AdemicoASC.Classes;
using AdemicoASC.Models;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Data;


namespace AdemicoASC.DAO
{
    public class MateriaDAO : IDAO<Materia>, IDisposable
    {

        private IConnection conexao;

        public MateriaDAO(IConnection Conexao)
        {
            this.conexao = Conexao;
        }

        public Materia inserir(Materia model)
        {
            using (SQLiteCommand comando = conexao.Buscar().CreateCommand())
            {
                try
                {
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "insert into Materia (NomeMateria, PesoProva1, PesoProva2, PesoProva3) "+
                                                             "values (@NomeMateria, @PesoProva1, @PesoProva2, @PesoProva3)";
                    comando.Parameters.AddWithValue("@NomeMateria", model.Nome);
                    comando.Parameters.AddWithValue("@PesoProva1", model.PesoProva1);
                    comando.Parameters.AddWithValue("@PesoProva2", model.PesoProva2);
                    comando.Parameters.AddWithValue("@PesoProva3", model.PesoProva3);
                    comando.ExecuteNonQuery();

                }
                catch
                {
                    throw new System.ArgumentException("Erro ao gravar Materia", "Erro");
                }
            }

            return model;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Collection<Materia> Listar()
        {
            Collection<Materia> colecao = new Collection<Materia>();

            using (SQLiteCommand comando = conexao.Buscar().CreateCommand())
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select * from Materia";

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(comando))
                {
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);

                    foreach (DataRow row in tabela.Rows)
                    {
                        Materia materia = new Materia
                        {
                            IDMateria = int.Parse(row["IDMateria"].ToString()),
                            Nome = row["NomeMateria"].ToString(),
                            PesoProva1 = Double.Parse(row["PesoProva1"].ToString()),
                            PesoProva2 = Double.Parse(row["PesoProva2"].ToString()),
                            PesoProva3 = Double.Parse(row["PesoProva3"].ToString())
                        };

                        colecao.Add(materia);
                    }
                }
            }
            return colecao;

        }

        public Materia ListarPorCodigo(int codigo)
        {
            Materia materia = null;

            using (SQLiteCommand comando = conexao.Buscar().CreateCommand())
            {
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "Select * from Materia where IDMateria = @IDMateria";
                comando.Parameters.AddWithValue("@IDMateria", codigo);

                using (SQLiteDataReader reader = comando.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        materia = new Materia();
                        reader.Read();
                        materia.IDMateria = reader.GetInt32(0);
                        materia.Nome = reader.GetString(1);
                        materia.PesoProva1 = reader.GetDouble(2);
                        materia.PesoProva2 = reader.GetDouble(3);
                        materia.PesoProva3 = reader.GetDouble(4);
                    }
                }
            }
            return materia;

        }

        public void ExcluirTodos()
        {
            using (SQLiteCommand comando = conexao.Buscar().CreateCommand())
            {
                try
                {
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "delete from Materia";
                    comando.ExecuteNonQuery();

                }
                catch
                {
                    throw new System.ArgumentException("Erro ao deletar Materia", "Erro");
                }
            }

        }
    }
}