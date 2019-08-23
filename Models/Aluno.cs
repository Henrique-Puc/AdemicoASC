using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdemicoASC.Models
{
    public class Aluno
    {
        public int IDAluno { get; set; }
        public String Nome { get; set; }

        public Turma Turma { get; set; }
        public int IDTurma { get; set; }

    public int Simulado{ get; set; }

}
}