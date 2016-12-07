using DA2_SistemaEscolar2016.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

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
                resultadoDeBusqueda = db.alumnos.Include("archivos").Where(a => a.apellidoP.Contains(nombreBuscado) ||
                    a.apellidoM.Contains(nombreBuscado) || a.nombre.Contains(nombreBuscado)).ToList();
            }
            else
            {
                //Arrojar todos los datos
                resultadoDeBusqueda = db.alumnos.Include("archivos").ToList();
            }
            //Pedirle a la vista que muestra el resultado en pantalla
            return View(resultadoDeBusqueda);
        }

        [HttpGet]
        public ActionResult listar()
        {

            //Consultar la lista de alumnos
            var todosLosAlumnos = db.alumnos.Include("archivos").ToList();

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
        public ActionResult crear(Alumno alumnoNuevo, HttpPostedFileBase fotoUpload, bool enDetallesDeGrupo = false)
        {
            //Validar si el nuevo alumno es valido
            if(ModelState.IsValid){
                //Almacenar archivo

                //Si se subio una foto
                if (fotoUpload != null && fotoUpload.ContentLength > 0)
                {
                    //Crearemos un nuevo registro de archivo
                    Archivo ar = new Archivo();
                    ar.formatoContenido = fotoUpload.ContentType;
                    ar.nombre = fotoUpload.FileName;
                    ar.tipo = "Perfil";

                    //Se lee el archivo para descomponerlo 
                    var reader = new System.IO.BinaryReader(fotoUpload.InputStream);
                    ar.contenido = reader.ReadBytes(fotoUpload.ContentLength);

                    //Se crea al vuelo una nueva lista de archivos que
                    //contenga solamente el unico archivo que acabamos de crear
                    alumnoNuevo.archivos = new List<Archivo> { ar };
                }


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
            //Se busca el alumno con sus archivos incluidos
            //var alumno = db.alumnos.Include(f => f.archivos).SingleOrDefault(al => al.noMatricula == id);
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
        public ActionResult editar(Alumno alumnoEditado, HttpPostedFileBase fotoUpload)
        {
            if (ModelState.IsValid) { 
                
                //Si se recibio una foto
                if(fotoUpload!=null && fotoUpload.ContentLength>0){
                    Archivo fotoPerfil = new Archivo();
                    if (alumnoEditado.archivos!=null && alumnoEditado.archivos.Count > 0) { 
                        fotoPerfil = db.archivos.Single(ar => ar.noMatricula ==
                        alumnoEditado.noMatricula);

                        var reader = new System.IO.BinaryReader(fotoUpload.InputStream);
                        fotoPerfil.contenido = reader.ReadBytes(fotoUpload.ContentLength);

                        //Se modifica el registro de la foto
                        db.Entry(fotoPerfil).State = EntityState.Modified;
                    }
                    else
                    {
                        fotoPerfil.formatoContenido = fotoUpload.ContentType;
                        fotoPerfil.nombre = fotoUpload.FileName;
                        fotoPerfil.tipo = "Perfil";

                        //Se lee el archivo para descomponerlo 
                        var reader = new System.IO.BinaryReader(fotoUpload.InputStream);
                        fotoPerfil.contenido = reader.ReadBytes(fotoUpload.ContentLength);
                        //Se relaciona la nueva foto
                        fotoPerfil.noMatricula = alumnoEditado.noMatricula;
                        db.archivos.Add(fotoPerfil);
                    }
                }

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