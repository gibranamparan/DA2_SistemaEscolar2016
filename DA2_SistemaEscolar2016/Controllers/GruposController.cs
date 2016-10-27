using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DA2_SistemaEscolar2016.Models;

namespace DA2_SistemaEscolar2016.Controllers
{
    public class GruposController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Grupos
        public ActionResult Index()
        {
            return View(db.grupos.ToList());
        }

        // POST: Grupos
        [HttpPost]
        public ActionResult Index(String nombreBuscado)
        {
            var resultadoDeBusqueda = new List<Grupo>();
            if(!string.IsNullOrEmpty(nombreBuscado)){
                resultadoDeBusqueda = db.grupos.Where
                    (a => a.carrera.Contains(nombreBuscado)).
                    ToList();
            }
            else
            {
                resultadoDeBusqueda = db.grupos.ToList();
            }
            return View(resultadoDeBusqueda);
        }

        public ActionResult detalles(int id=0)
        {
            var grupo = db.grupos.Find(id);
            //Si el grupo no existe
            if (grupo == null)
            {
                return RedirectToAction("index");
            }
            //Si existe, se muestran los detalles en la vista
            return View(grupo);
        }

        // GET: Grupos/Create
        public ActionResult Create()
        {
            Object[] carreras = {"ELECTRONICA","TIC","MECATRONICA" };
            ViewBag.carrera = new SelectList(carreras);
            return View();
        }

        // POST: Grupos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "grupoID,nombreGrupo,carrera")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.grupos.Add(grupo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grupo);
        }

        // GET: Grupos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        // POST: Grupos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "grupoID,nombreGrupo,carrera")] Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grupo);
        }

        // GET: Grupos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        // POST: Grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grupo grupo = db.grupos.Find(id);
            db.grupos.Remove(grupo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
