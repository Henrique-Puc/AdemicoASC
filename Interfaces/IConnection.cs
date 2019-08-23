using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace AdemicoASC.Interfaces
{
    public interface IConnection : IDisposable
    {

        //método para abrir conexão
        SQLiteConnection Abrir();


        //Quando aberta a conexão, faz a busca sem abrir mais a conexão
        SQLiteConnection Buscar();

        //Fecha a conexão
        void Fechar();

    }
}
