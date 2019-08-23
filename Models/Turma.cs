using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdemicoASC.Models
{
    public class Turma
    {
        public int IdTurma { get; set; }
        public String Nome { get; set; }
        public List<Aluno> Alunos { get; set; }
    }
}