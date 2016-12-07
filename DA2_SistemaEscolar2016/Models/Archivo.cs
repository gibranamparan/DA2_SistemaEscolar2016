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
        /*
        public static bool guardarFoto(HttpPostedFileBase fotoUpload)
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
            return true;
        }
        public static bool editarFoto(HttpPostedFileBase fotoUpload)
        {
            Archivo fotoPerfil = db.archivos.Single(ar => ar.noMatricula ==
                        alumnoEditado.noMatricula);
            var reader = new System.IO.BinaryReader(fotoUpload.InputStream);
            fotoPerfil.contenido = reader.ReadBytes(fotoUpload.ContentLength);

            //Se modifica el registro de la foto
            db.Entry(fotoPerfil).State = EntityState.Modified;
            return true;
        }*/
    }
}