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

        private TipoUsuarioCLS oTipoVal;

        // GET: TipoUsuario
        public ActionResult TipoUsuarioView(TipoUsuarioCLS oTipoUsuarioCLS)
        {
            oTipoVal = oTipoUsuarioCLS; //Guardamos modelo

            List<TipoUsuarioCLS> listaTipoUsuario = null;
            List<TipoUsuarioCLS> listaFiltrado;  //Mi variable para filtrar de acuerdo lo que ingrese el usuario en los texbobx

            using (var bd = new BDPasajeEntities())
            {
                //Obtenemos toda la data de la bd
                listaTipoUsuario = bd.TipoUsuario
                    .Where(x => x.BHABILITADO == 1)
                    .Select(x => new TipoUsuarioCLS {
                        iidTipoUsuario = x.IIDTIPOUSUARIO,
                        nombre = x.NOMBRE,
                        descripcion = x.DESCRIPCION
                    })
                    .ToList();

                //Validamos que tipo de filtro se ha realizado
                if (oTipoUsuarioCLS.iidTipoUsuario == 0 && oTipoUsuarioCLS.nombre == null && oTipoUsuarioCLS.descripcion == null)
                    listaFiltrado = listaTipoUsuario;
                else
                {
                    Predicate<TipoUsuarioCLS> pre = new Predicate<TipoUsuarioCLS>(buscarTipoUsuario); //Variable para hacer filtrado
                    listaFiltrado = listaTipoUsuario.FindAll(pre);
                }

            }

                return View(listaFiltrado);
        }


        //================ZONA DE METODOS ============================
        private bool buscarTipoUsuario(TipoUsuarioCLS oTipoUsuarioCLS)
        {
            bool busquedaId = true;
            bool busquedaNombre = true;
            bool busquedaDescripcion = true;

            if (oTipoVal.iidTipoUsuario > 0)
                busquedaId = oTipoUsuarioCLS.iidTipoUsuario.ToString().Contains(oTipoVal.iidTipoUsuario.ToString());

            if (oTipoVal.nombre != null)
                busquedaNombre = oTipoUsuarioCLS.nombre.ToString().Contains(oTipoVal.nombre.ToString());

            if (oTipoVal.descripcion != null)
                busquedaDescripcion = oTipoUsuarioCLS.descripcion.ToString().Contains(oTipoVal.descripcion.ToString());

            return (busquedaId && busquedaNombre && busquedaDescripcion);
        }
    }
}