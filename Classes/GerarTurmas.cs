using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdemicoASC.Models;

namespace AdemicoASC.Classes
{
    public class GerarTurmas
    {
        public Util.Resultado Executar(int qtdTurmas)
        {

            Turma turma = new Turma();

            for (var i = 1; i <= qtdTurmas; i++)
            {
                 turma.Nome = Util.cTurma + i.ToString();
                 
            }

            return Util.Resultado.Ok;
        }

    }
}