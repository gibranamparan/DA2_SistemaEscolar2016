using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DA2_SistemaEscolar2016.Models
{
    public class Grupo
    {
        //Llave primaria
        public int grupoID { get; set; }

        [Display(Name="Grupo")]
        public String nombreGrupo { get; set; }

        public String carrera { get; set; }

        //Un grupo tiene muchos alumnos
        virtual public ICollection<Alumno> alumnos { get; set; }
    }
}