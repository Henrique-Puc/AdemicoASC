using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AdemicoASC.Interfaces
{
    interface IDAO<T> : IDisposable
        where  T : class, new()
    {
        T inserir(T model);

        Collection<T> Listar();

        T ListarPorCodigo(int codigo);

        void ExcluirTodos();
    }
}
