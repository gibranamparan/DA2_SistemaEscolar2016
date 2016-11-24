using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DA2_SistemaEscolar2016.Models
{
    public class Clase
    {
        [Key]
        public int claseID { get; set; }

        public string asignatura { get; set; }
        /*
        //UNa clase se le imparte a un grupo
        public int grupoID { get; set; }
        virtual public Grupo grupos { get; set; }

        //Una clase es impartida por un usuario con rol profesor
        public string Id { get; set; }
        public virtual ApplicationUser profesor { get; set; }*/
    }
}