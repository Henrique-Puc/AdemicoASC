using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdemicoASC.Models
{
    public class Prova
    {

        public int IDProva { get; set; }
        public Materia Materia { get; set; }
        public int IDMateria { get; set; }
        public Aluno Aluno { get; set; }
        public int IDAluno { get; set; }
        public Double NotaProva1 { get; set; }
        public double NotaProva2 { get; set; }
        public double NotaProva3 { get; set; }
        public double MediaProva { get; set; }
        public double NotaProvaFinal { get; set; }
        public double MediaFinal { get; set; }

        public Prova()
        {
            this.NotaProva1 = 0;
            this.NotaProva2 = 0;
            this.NotaProva3 = 0;
            this.MediaProva = 0;
            this.NotaProvaFinal = 0;
            this.MediaFinal = 0;
        }



    }
}