using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DA2_SistemaEscolar2016.Models;

namespace DA2_SistemaEscolar2016.Controllers
{
    public class ArchivoController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Archivo
        public FileResult download(int id =0)
        {
            var archivo = db.archivos.Find(id);
            return File(archivo.contenido, archivo.formatoContenido);
        }
    }
}