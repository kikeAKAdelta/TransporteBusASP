using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrimerWebUdemy.Models;

namespace PrimerWebUdemy.Controllers
{
    public class ViajeController : Controller
    {
        // GET: Viaje
        public ActionResult ViajeView()
        {
            List<ViajeCLS> listaViaje = null;
            using (var bd = new BDPasajeEntities())
            {
                listaViaje = (from viaje in bd.Viaje
                              join lugarOrigen in bd.Lugar
                              on viaje.IIDLUGARORIGEN equals lugarOrigen.IIDLUGAR
                              join lugarDestino in bd.Lugar
                              on viaje.IIDLUGARDESTINO equals lugarDestino.IIDLUGAR
                              join bus in bd.Bus
                              on viaje.IIDBUS equals bus.IIDBUS
                              join modelo in bd.Modelo
                              on bus.IIDMODELO equals modelo.IIDMODELO
                              select new ViajeCLS
                              {
                                  iidViaje = viaje.IIDVIAJE,
                                  nombreBus = modelo.NOMBRE,
                                  nombreLugarOrigen = lugarOrigen.NOMBRE,
                                  nombreLugarDestino = lugarDestino.NOMBRE,
                                  numeroAsientosDisponibles = (int)viaje.NUMEROASIENTOSDISPONIBLES,
                                  fechaViaje = (DateTime)viaje.FECHAVIAJE
                              }).ToList();
            }
                return View(listaViaje);
        }

        [HttpGet]
        public ActionResult ViajeAgregar()
        {
            listarCombos();
            return View();
        }

        [HttpPost]
        public ActionResult ViajeAgregar(ViajeCLS oViajeCLS)
        {
            if (!ModelState.IsValid)
            {
                listarCombos();
                return View(oViajeCLS);
            }

            using (var bd = new BDPasajeEntities())
            {
                Viaje oViaje = new Viaje();
                oViaje.IIDLUGARDESTINO = oViajeCLS.iidLugarDestino;
                oViaje.IIDLUGARORIGEN = oViajeCLS.iidLugarOrigen;
                oViaje.PRECIO = (decimal)oViajeCLS.precio;
                oViaje.FECHAVIAJE = oViajeCLS.fechaViaje;
                oViaje.IIDBUS = oViajeCLS.iidBus;
                oViaje.NUMEROASIENTOSDISPONIBLES = oViajeCLS.numeroAsientosDisponibles;
                oViaje.BHABILITADO = 1;

                bd.Viaje.Add(oViaje);
                bd.SaveChanges();
            }
            
            return RedirectToAction("ViajeView");
        }

        [HttpGet]
        public ActionResult ViajeEdit(int id)
        {
            ViajeCLS oViajeCLS = new ViajeCLS();
            listarCombos();
            using (var bd = new BDPasajeEntities())
            {
                Viaje oViaje = bd.Viaje.Find(id);

                oViajeCLS.iidViaje = oViaje.IIDVIAJE;
                oViajeCLS.iidLugarDestino = (int) oViaje.IIDLUGARDESTINO;
                oViajeCLS.iidLugarOrigen = (int)oViaje.IIDLUGARORIGEN;
                oViajeCLS.precio = (double) oViaje.PRECIO;
                oViajeCLS.fechaViaje = (DateTime)oViaje.FECHAVIAJE;
                oViajeCLS.iidBus = (int)oViaje.IIDBUS;
                oViajeCLS.numeroAsientosDisponibles = (int)oViaje.NUMEROASIENTOSDISPONIBLES;

            }

                return View(oViajeCLS);
        }

        [HttpPost]
        public ActionResult ViajeEdit(ViajeCLS oViajeCLS)
        {
            if(!ModelState.IsValid)
            {
                listarCombos();
                return View(oViajeCLS);
            }

            
            using (var bd = new BDPasajeEntities())
            {
                Viaje oViaje = bd.Viaje.Find(oViajeCLS.iidViaje);
                oViaje.IIDLUGARDESTINO = oViajeCLS.iidLugarDestino;
                oViaje.IIDLUGARORIGEN = oViajeCLS.iidLugarOrigen;
                oViaje.PRECIO = (decimal)oViajeCLS.precio;
                oViaje.FECHAVIAJE = oViajeCLS.fechaViaje;
                oViaje.IIDBUS = oViajeCLS.iidBus;
                oViaje.NUMEROASIENTOSDISPONIBLES = oViajeCLS.numeroAsientosDisponibles;

                bd.SaveChanges();
            }
                return RedirectToAction("ViajeView");
        }


        /*------------- Listado de ComboBox ---------------------*/

        public void listarCombos()
        {
            cmbLugarDestino();
            cmbLugarOrigen();
            cmbBus();
        }

        public void cmbLugarOrigen()
        {
            List<SelectListItem> listaLugarOrigen;
            using (var bd = new BDPasajeEntities())
            {
                listaLugarOrigen = (from lugarOrigen in bd.Lugar
                             where lugarOrigen.BHABILITADO == 1
                             select new SelectListItem
                             {
                                 Text = lugarOrigen.NOMBRE,
                                 Value = lugarOrigen.IIDLUGAR.ToString()
                             }).ToList();

                listaLugarOrigen.Insert(0, new SelectListItem { Text = "-- Selecione --", Value = "" });
                ViewBag.listaLugarOrigen = listaLugarOrigen;
            }
        }


        public void cmbLugarDestino()
        {
            List<SelectListItem> listaLugarDestino;
            using (var bd = new BDPasajeEntities())
            {
                listaLugarDestino = (from lugarDestino in bd.Lugar
                                    where lugarDestino.BHABILITADO == 1
                                    select new SelectListItem
                                    {
                                        Text = lugarDestino.NOMBRE,
                                        Value = lugarDestino.IIDLUGAR.ToString()
                                    }).ToList();

                listaLugarDestino.Insert(0, new SelectListItem { Text = "-- Selecione --", Value = "" });
                ViewBag.listaLugarDestino = listaLugarDestino;
            }
        }

        public void cmbBus()
        {
            List<SelectListItem> listaBus;
            using (var bd = new BDPasajeEntities())
            {
                listaBus = (from bus in bd.Bus
                                     where bus.BHABILITADO == 1
                                     select new SelectListItem
                                     {
                                         Text = bus.DESCRIPCION,
                                         Value = bus.IIDBUS.ToString()
                                     }).ToList();

                listaBus.Insert(0, new SelectListItem { Text = "-- Selecione --", Value = "" });
                ViewBag.listaBus = listaBus;
            }
        }
    }
}