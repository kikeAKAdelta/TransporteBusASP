using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrimerWebUdemy.Models;

namespace PrimerWebUdemy.Controllers
{
    public class TipoUsuarioController : Controller
    {
        // GET: TipoUsuario
        public ActionResult TipoUsuarioView()
        {
            List<TipoUsuarioCLS> listaTipoUsuario = null;

            using (var bd = new BDPasajeEntities())
            {
                listaTipoUsuario = bd.TipoUsuario
                    .Where(x => x.BHABILITADO == 1)
                    .Select(x => new TipoUsuarioCLS {
                        iidTipoUsuario = x.IIDTIPOUSUARIO,
                        nombre = x.NOMBRE,
                        descripcion = x.DESCRIPCION
                    })
                    .ToList();
            }

                return View(listaTipoUsuario);
        }
    }
}