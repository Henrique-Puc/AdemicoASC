using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using AdemicoASC.Interfaces;
using System.Data;

namespace AdemicoASC.Classes
{
    public class Connection : IConnection, IDisposable
    {
//C:\Henrique\TrabalhoASC\AdemicoASC\BD\SQLiteAcademicoASC.db
        private SQLiteConnection conexao;

        public Connection()
        {
            String path = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            conexao = new SQLiteConnection("Data Source=" + path+@"\BD\SQLiteAcademicoASC.db" + ";Version=3;");
        }

        public SQLiteConnection Abrir()
        {
            if (conexao.State == ConnectionState.Closed)
            {
                conexao.Open();
            }

            return conexao;
        }

        public SQLiteConnection Buscar()
        {
            return this.Abrir();
        }

        public void Fechar()
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
            }
        }

        public void Dispose()
        {
            this.Fechar();
            GC.SuppressFinalize(this);
        }

    }
}