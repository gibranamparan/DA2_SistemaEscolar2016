using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA2_SistemaEscolar2016.Models
{
    public class Archivo
    {
        public int archivoID { get; set; }
        public string nombre { get; set; }
        public string tipo { get; set; }
        public string formatoContenido { get; set; }
        public byte[] contenido { get; set; }


        //Un archivo corresponde a un alumno
        public int noMatricula { get; set; }
        public Alumno alumno { get; set; }
    }
}