using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrimerWebUdemy.Models;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;

namespace PrimerWebUdemy.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        public ActionResult MarcaView()
        {
            List<MarcaCLS> listaMarca = null;
            using (var bd = new BDPasajeEntities())
            {
                listaMarca = (from d in bd.Marca
                              where d.BHABILITADO == 1
                              select new MarcaCLS
                              {
                                  iidmarca = d.IIDMARCA,
                                  nombre = d.NOMBRE,
                                  descripcion = d.DESCRIPCION
                              }).ToList();

                Session["listaMarca"] = listaMarca;  //Lo utilizamos para poder hacer el reporte pdf
            }
            return View(listaMarca);
        }

        /// <summary>
        /// Action que sirve para mostrar el modelo para
        /// agregar
        /// </summary>
        /// <returns></returns>
        public ActionResult MarcaAgregar()
        {
            return View();
        }


        /// <summary>
        /// Action que sirve para cuando se envie la informacion del modelo
        /// </summary>
        /// <param name="oMarcaCLS"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MarcaAgregar(MarcaCLS oMarcaCLS)
        {
            int numeroRegistrosEncontrados = 0;
            string nombreMarca = oMarcaCLS.nombre;

            //Realizamos conteo para ver si la marcaya existe
            using (var bd = new BDPasajeEntities())
            {
                numeroRegistrosEncontrados = bd.Marca.Where(p => p.NOMBRE.Equals(nombreMarca)).Count();
            }
            if (!ModelState.IsValid || numeroRegistrosEncontrados > 0)
            {
                if (numeroRegistrosEncontrados > 0)
                    oMarcaCLS.mensajeError = "El nombre de la marca ingresada ya esta registrada!!";

                return View(oMarcaCLS);
            }
            else
            {
                using (var bd = new BDPasajeEntities())
                {
                    Marca oMarca = new Marca();
                    oMarca.NOMBRE = oMarcaCLS.nombre;
                    oMarca.DESCRIPCION = oMarcaCLS.descripcion;
                    oMarca.BHABILITADO = 1;

                    bd.Marca.Add(oMarca);
                    bd.SaveChanges();
                }
            }
            return RedirectToAction("MarcaView");
        }

        /// <summary>
        /// Permite mostrar informacion de un modelo por paso de id
        /// y mostrarlo en la vista para su modificacion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult MarcaEditar(int id)
        {
            MarcaCLS oMarcaCLS = new MarcaCLS();
            using (var bd = new BDPasajeEntities())
            {
                Marca oMarca = bd.Marca.Where(p => p.IIDMARCA.Equals(id)).First();
                oMarcaCLS.iidmarca = oMarca.IIDMARCA;
                oMarcaCLS.nombre = oMarca.NOMBRE;
                oMarcaCLS.descripcion = oMarca.DESCRIPCION;
                oMarcaCLS.bhabilitado = oMarca.BHABILITADO;
            }
            return View(oMarcaCLS);
        }


        [HttpPost]
        public ActionResult MarcaEditar(MarcaCLS oMarcaCLS)
        {
            int numeroRegistrosEncontrados = 0;
            string nombreMarca = oMarcaCLS.nombre;
            int iidMarca = oMarcaCLS.iidmarca;
            using (var bd = new BDPasajeEntities())
            {
                numeroRegistrosEncontrados = bd.Marca.Where(p => p.NOMBRE.Equals(nombreMarca)  &&  !p.IIDMARCA.Equals(iidMarca)).Count();
            }

            if (!ModelState.IsValid || numeroRegistrosEncontrados>0)
            {
                if (numeroRegistrosEncontrados > 0) oMarcaCLS.mensajeError = "Esta marca ya esta registrada!";
                return View(oMarcaCLS);
            }
            int idMarca = oMarcaCLS.iidmarca;
            using (var bd = new BDPasajeEntities())
            {
                Marca oMarca = bd.Marca.Find(idMarca);
                oMarca.NOMBRE = oMarcaCLS.nombre;
                oMarca.DESCRIPCION = oMarcaCLS.descripcion;

                bd.SaveChanges();

            }

            return RedirectToAction("MarcaView");
        }

        [HttpGet]
        public ActionResult EliminarView(int id)
        {
            using (var bd = new BDPasajeEntities())
            {
                Marca oMarca = bd.Marca.Find(id);
                oMarca.BHABILITADO = 0;

                bd.SaveChanges();
            }
            return RedirectToAction("MarcaView");
        }



        public FileResult GenerarPdf()
        {
            Document doc = new Document();  //Creamos documento pdf
            byte[] buffer;  //Los bytes donde se almacenara el pdf

            using (MemoryStream ms = new MemoryStream())  //guardamos en memoria
            {
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                Paragraph title = new Paragraph("Lista Marca");  //titulo del pdf
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);  //Agregamos al documento

                Paragraph espacio = new Paragraph(" ");  //titulo del pdf
                espacio.Alignment = Element.ALIGN_CENTER;
                doc.Add(espacio);  //Agregamos al documento un espacio en blanco



                PdfPTable table = new PdfPTable(3);  //Creamos una tabla con sus # de columnas

                //Columnas
                float[] values = new float[3] { 30, 40, 80 };  //Definimos ancho de columnas de la tabla
                table.SetWidths(values);

                //Celdas
                PdfPCell celda1 = new PdfPCell(new Paragraph("Id Marca"));
                celda1.BackgroundColor = new BaseColor(130, 130, 130);
                celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda1);

                PdfPCell celda2 = new PdfPCell(new Paragraph("Nombre"));
                celda2.BackgroundColor = new BaseColor(130, 130, 130);
                celda2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda2);

                PdfPCell celda3 = new PdfPCell(new Paragraph("Descripcion"));
                celda3.BackgroundColor = new BaseColor(130, 130, 130);
                celda3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                table.AddCell(celda3);

                List<MarcaCLS> listaMarca = (List<MarcaCLS>)Session["listaMarca"]; //Objeto session que esta cuando se obtiene la lista de marca
                int numRegistros = listaMarca.Count();

                for (int i = 0; i < numRegistros; i++)
                {
                    table.AddCell(listaMarca[i].iidmarca.ToString());
                    table.AddCell(listaMarca[i].nombre.ToString());
                    table.AddCell(listaMarca[i].descripcion.ToString());
                }


                doc.Add(table);
                doc.Close();


                buffer = ms.ToArray();
            }

            return File(buffer, "application/pdf");
        }
    }
}