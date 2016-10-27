using DA2_SistemaEscolar2016.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA2_SistemaEscolar2016.Controllers
{
    [Authorize]
    public class AlumnoController : Controller
    {
        //Conectarnos a la BD
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult listar(String nombreBuscado)
        {
            var resultadoDeBusqueda = new List<Alumno>();
            if (!String.IsNullOrEmpty(nombreBuscado)) { 
                //Consultar la lista de alumnos
                resultadoDeBusqueda = db.alumnos.Where(a => a.apellidoP.Contains(nombreBuscado) ||
                    a.apellidoM.Contains(nombreBuscado) || a.nombre.Contains(nombreBuscado)).ToList();
            }
            else
            {
                //Arrojar todos los datos
                resultadoDeBusqueda = db.alumnos.ToList();
            }
            //Pedirle a la vista que muestra el resultado en pantalla
            return View(resultadoDeBusqueda);
        }

        [HttpGet]
        public ActionResult listar()
        {

            //Consultar la lista de alumnos
            var todosLosAlumnos = db.alumnos.ToList();

            //Pedirle a la vista que muestra el resultado en pantalla
            return View(todosLosAlumnos);
        }

        /*Este te muestra la paginac con la forma
         * para dar de alta un nuevo nuevo alumno*/
        [HttpGet]
        [Authorize(Roles="Admin, Capturista")]
        public ActionResult crear()
        {
            //Tomar los datos con los que rellenas la lista
            var grupos = db.grupos;
            //Crear la lista de seleccion
            SelectList grupoID = new SelectList(grupos, "grupoID", "nombreGrupo");
            //Se envia la lista de seleccion a la vista
            ViewBag.grupoID = grupoID;

            return View();
        }

        /*Este se encarga de recibir los datos del nuevo
         alumno y guardarlo*/
        [HttpPost]
        [Authorize(Roles = "Admin, Capturista")]
        public ActionResult crear(Alumno alumnoNuevo, bool enDetallesDeGrupo = false)
        {
            //Validar si el nuevo alumno es valido
            if(ModelState.IsValid){
                //Crear alumno
                db.alumnos.Add(alumnoNuevo);

                //Guardar cambios
                db.SaveChanges();

                //Regresar una vista, todo salio bien
                //Si estaba en pantalla de detalles de grupo
                if (enDetallesDeGrupo)
                {
                    return RedirectToAction("detalles", "grupos", new { id = alumnoNuevo.grupoID });
                }
                else
                {
                    return RedirectToAction("listar");
                }
            }
            //Si llegaste tan lejos, porque hay una bronca
            ViewBag.MensajeError = "Hubo un error, favor de verificar la informacion introducida";

            //Te devuelve a interntarlo de nuevo
            return View();
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "Admin")]
        public ActionResult eliminar(int id = 0)
        {
            //Se busca el alumno que se quiere eliminar
            var alumno = db.alumnos.Find(id);
            if (alumno == null)
            {
                return RedirectToAction("listar");
            }

            return View(alumno);
        }

        [HttpPost]
        [ActionName("eliminar")]
        [Authorize]
        [Authorize(Roles = "Admin")]
        public ActionResult confirmarEliminar(int noMatricula = 0)
        {
            //Se busca el alumno que se quiere eliminar
            Alumno alumno = db.alumnos.Find(noMatricula);

            if (alumno == null)
            {
                return RedirectToAction("listar");
            }

            //Elimina alumnos
            db.alumnos.Remove(alumno);

            //Ejecuta la lista de queries en la BD
            db.SaveChanges();

            return RedirectToAction("listar");
        }

        [HttpGet]
        [Authorize]
        [Authorize(Roles = "Admin, Capturista")]
        public ActionResult editar(int id=0)
        {
            var alumno = db.alumnos.Find(id);
            if (alumno == null)
            {
                return RedirectToAction("listar");
            }

            //Tomar los datos con los que rellenas la lista
            var grupos = db.grupos;
            //Crear la lista de seleccion
            SelectList grupoID = new SelectList(grupos, "grupoID", "nombreGrupo");
            //Se envia la lista de seleccion a la vista
            ViewBag.grupoID = grupoID;

            return View(alumno);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Capturista")]
        public ActionResult editar(Alumno alumnoEditado)
        {
            if (ModelState.IsValid) { 
                //Modificar el registro actual
                db.Entry(alumnoEditado).State = EntityState.Modified;
                db.SaveChanges();
                //Si llega tan lejos, es que todo salio bien
                return RedirectToAction("listar");
            }

            //Si llego hasta aca, es que algo es invalido
            //Lo regresas al formulario para corregir
            return View();
        }
    }
}