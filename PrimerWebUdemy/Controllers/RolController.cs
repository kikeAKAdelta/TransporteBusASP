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
    }
}