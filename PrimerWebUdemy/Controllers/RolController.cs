using PrimerWebUdemy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrimerWebUdemy.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult RolView()
        {
            List<RolCLS> listaRol = new List<RolCLS>();

            using (var bd = new BDPasajeEntities())
            {
                listaRol = bd.Rol
                            .Where(x => x.BHABILITADO == 1)
                            .Select(x => new RolCLS {
                                iidRol = x.IIDROL,
                                nombre = x.NOMBRE,
                                descripcion = x.DESCRIPCION
                            }).ToList();
            }

                return View(listaRol);
        }


        public ActionResult Filtro(string nombre)
        {
            List<RolCLS> listaRol = new List<RolCLS>();

            using (var bd = new BDPasajeEntities())
            {
                if (nombre == null) {
                    listaRol = bd.Rol
                                .Where(x => x.BHABILITADO == 1)
                                .Select(x => new RolCLS
                                {
                                    iidRol = x.IIDROL,
                                    nombre = x.NOMBRE,
                                    descripcion = x.DESCRIPCION
                                }).ToList();
                }else
                {
                    listaRol = bd.Rol
                                .Where(x => x.BHABILITADO == 1 && x.NOMBRE.Contains(nombre))
                                .Select(x => new RolCLS
                                {
                                    iidRol = x.IIDROL,
                                    nombre = x.NOMBRE,
                                    descripcion = x.DESCRIPCION
                                }).ToList();
                }

                return PartialView("_TablaRol", listaRol);
            }
        }

        public int Guardar(RolCLS oRolCLS, int titulo)
        {
            int rpta = 0;
            using (var bd = new BDPasajeEntities())
            {
                if (titulo.Equals(1))  //si titulo es igual a uno, es un agregar
                {
                    Rol oRol = new Rol();
                    oRol.NOMBRE = oRolCLS.nombre;
                    oRol.DESCRIPCION = oRolCLS.descripcion;
                    oRol.BHABILITADO = 1;

                    bd.Rol.Add(oRol);
                    rpta = bd.SaveChanges();  //Aparte de confirmar los cambios devuelve la cantidad de registros afectados
                }
            }

            return rpta;
        }
    }
}