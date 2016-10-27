using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DA2_SistemaEscolar2016.Models
{
    public class Alumno
    {
        [Key] //Llave primaria con autoincremento 1 en 1
        public int noMatricula {get; set;}

        [Required]
        [MinLength(2)]
        [Display(Name = "Nombre")]
        public String nombre { get; set; }

        [Required]
        [Display(Name = "Apellido Paterno")]
        public String apellidoP { get; set; }

        [Required]
        [Display(Name = "Apellido Materno")]
        public String apellidoM { get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString="{0:yyyy-MM-dd}",ApplyFormatInEditMode=true)]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime fechaNac { get; set; }

        //Un alumno tiene un solo grupo
        public int grupoID { get; set; }
        virtual public Grupo grupo{ get; set; }
    }
}