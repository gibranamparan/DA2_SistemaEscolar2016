using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace DA2_SistemaEscolar2016.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        //Datos de profesor
        public String nombre { get; set; }
        public String apellidoP { get; set; }
        public String apellidoM { get; set; }
        public String especialidad { get; set; }
        public String grado { get; set; }

        //Un profesor podlria tener clases
        //public virtual ICollection<Clase> clases { get; set; }

        /// <summary>
        /// Contructor que crea un usuario a partir de los datos de registro
        /// </summary>
        /// <param name="model">Un modelo de vista que contiene los datos del nuevo usuario que se pretende registrar</param>
        public ApplicationUser(RegisterViewModel model)
        {
            this.UserName = model.Email;
            this.Email = model.Email;
            this.nombre = model.nombre;
            this.apellidoP = model.apellidoP;
            this.apellidoM = model.apellidoM;
            this.especialidad = model.especialidad;
            this.grado = model.grado;
        }

        /// <summary>
        /// Sobrecarga de constructor de usuario por defecto.
        /// </summary>
        public ApplicationUser() { }
    }

    //Un contexto en MVC representa la conexion y estructura de nuestra
    //base de datos.
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        //Se define tabla de alumnos a partir del modelo de alumno
        public DbSet<Alumno> alumnos { get; set; }
        public DbSet<Grupo> grupos { get; set; }
        public DbSet<Archivo> archivos { get; set; }
        //Constructor donde hace referencia a una conexion definida en 
        public ApplicationDbContext()
            : base("DefaultConnectionServer", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<DA2_SistemaEscolar2016.Models.Clase> Clases { get; set; }

        //public System.Data.Entity.DbSet<DA2_SistemaEscolar2016.Models.ApplicationUser> ApplicationUsers { get; set; }

    }
}