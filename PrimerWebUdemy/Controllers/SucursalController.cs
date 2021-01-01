using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrimerWebUdemy.Models;

namespace PrimerWebUdemy.Controllers
{
    public class SucursalController : Controller
    {
        // GET: Sucursal
        public ActionResult SucursalView()
        {
            List<SucursalCLS> listaSucursal = null;
            using (var bd = new BDPasajeEntities())
            {
                listaSucursal = (from sucursal in bd.Sucursal
                                 where sucursal.BHABILITADO == 1
                                 select new SucursalCLS
                                 {
                                     iidsucursal = sucursal.IIDSUCURSAL,
                                     nombre = sucursal.NOMBRE,
                                     direccion = sucursal.NOMBRE,
                                     telefono = sucursal.TELEFONO,
                                     email = sucursal.EMAIL
                                 }).ToList();
            }
            return View(listaSucursal);
        }

        [HttpGet]
        public ActionResult SucursalAgregar()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SucursalAgregar(SucursalCLS model)
        {
            int numeroRegistrosEncontrados = 0;
            string nombreSucursal = model.nombre;
            using (var bd = new BDPasajeEntities())
            {
                numeroRegistrosEncontrados = bd.Sucursal.Where(p => p.NOMBRE.Equals(nombreSucursal)).Count();
            }
            if (!ModelState.IsValid || numeroRegistrosEncontrados > 0)
            {
                if (numeroRegistrosEncontrados > 0)
                    model.mensajeError = "Este registro ya existe!";

                return View(model);
            }
            else
            {
                using (var bd = new BDPasajeEntities())
                {
                    Sucursal oSucursal = new Sucursal();

                    oSucursal.NOMBRE = model.nombre;
                    oSucursal.DIRECCION = model.direccion;
                    oSucursal.TELEFONO = model.telefono;
                    oSucursal.EMAIL = model.email;
                    oSucursal.FECHAAPERTURA = model.fechaApertura;
                    oSucursal.BHABILITADO = 1;

                    bd.Sucursal.Add(oSucursal);
                    bd.SaveChanges();
                }
            }

            return RedirectToAction("SucursalView");
        }

        [HttpGet]
        public ActionResult EditarSucursal(int id)
        {
            SucursalCLS oSucursalCLS = new SucursalCLS();
            using (var bd = new BDPasajeEntities())
            {
                Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL == id).First();
                oSucursalCLS.iidsucursal = oSucursal.IIDSUCURSAL;
                oSucursalCLS.nombre = oSucursal.NOMBRE;
                oSucursalCLS.direccion = oSucursal.DIRECCION;
                oSucursalCLS.telefono = oSucursal.TELEFONO;
                oSucursalCLS.email = oSucursal.EMAIL;
                oSucursalCLS.fechaApertura = (DateTime)oSucursal.FECHAAPERTURA;
            }
            return View(oSucursalCLS);
        }

        [HttpPost]
        public ActionResult EditarSucursal(SucursalCLS oSucursalCLS)
        {
            int numeroRegistrosEncontrados = 0;
            string nombreSucursal = oSucursalCLS.nombre;
            int iidSucursal = oSucursalCLS.iidsucursal;
            using (var bd = new BDPasajeEntities())
            {
                numeroRegistrosEncontrados = bd.Sucursal.Where(p => p.NOMBRE.Equals(nombreSucursal) && !p.IIDSUCURSAL.Equals(iidSucursal)).Count();
            }

            if (!ModelState.IsValid || numeroRegistrosEncontrados>0)
            {
                if (numeroRegistrosEncontrados > 0) oSucursalCLS.mensajeError = "Este registro ya existe";
                return View(oSucursalCLS);
            }

            using (var bd = new BDPasajeEntities())
            {
                Sucursal oSucursal = bd.Sucursal.Find(oSucursalCLS.iidsucursal);
                oSucursal.NOMBRE = oSucursalCLS.nombre;
                oSucursal.DIRECCION = oSucursalCLS.direccion;
                oSucursal.EMAIL = oSucursalCLS.email;
                oSucursal.FECHAAPERTURA = oSucursalCLS.fechaApertura;

                bd.Entry(oSucursal).State = System.Data.Entity.EntityState.Modified;
                bd.SaveChanges();
            }
            return RedirectToAction("SucursalView");
        }

        [HttpGet]
        public ActionResult EliminarSucursal(int id)
        {
            using (var bd = new BDPasajeEntities())
            {
                Sucursal oSucursal = bd.Sucursal.Find(id);
                oSucursal.BHABILITADO = 0;
                bd.SaveChanges();
            }
            return RedirectToAction("SucursalView");
        }
    }
}