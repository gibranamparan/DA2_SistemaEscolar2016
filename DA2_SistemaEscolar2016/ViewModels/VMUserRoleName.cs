using DA2_SistemaEscolar2016.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA2_SistemaEscolar2016.ViewModels
{
    public class VMUserRoleName
    {
        public String Id { get; set; }
        public String nombreCompleto { get; set; }
        public String email { get; set; }
        public String rolID { get; set; }

        public VMUserRoleName(ApplicationUser usr)
        {
            this.Id = usr.Id;
            this.nombreCompleto = usr.nombre + " " + usr.apellidoP + " " + usr.apellidoM;
            this.email = usr.Email;
            if (usr.Roles.Count > 0) {
                this.rolID = usr.Roles.First().RoleId;
            }
            
        }

        public VMUserRoleName(){}
    }
}