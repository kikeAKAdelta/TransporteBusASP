using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrimerWebUdemy.Models;

namespace PrimerWebUdemy.Controllers
{
    public class BusController : Controller
    {
        // GET: Bus
        public ActionResult BusView(BusCLS oBusCLS)
        {
            listarCombos();

            List<BusCLS> listaBus = null;
            List<BusCLS> listaRespuesta = null;
            using (var bd  = new BDPasajeEntities())
            {
                listaBus = (from bus in bd.Bus
                            join sucursal in bd.Sucursal
                            on bus.IIDSUCURSAL equals sucursal.IIDSUCURSAL
                            join tipobus in bd.TipoBus
                            on bus.IIDTIPOBUS equals tipobus.IIDTIPOBUS
                            join modelo in bd.Modelo
                            on bus.IIDMODELO equals modelo.IIDMODELO
                            where bus.BHABILITADO == 1
                            select new BusCLS
                            {
                                iidBus = bus.IIDBUS,
                                nombreModelo = modelo.NOMBRE,
                                placa = bus.PLACA,
                                nombreSucursal = sucursal.NOMBRE,
                                nombreTipoBus = tipobus.NOMBRE,
                                iidModelo = modelo.IIDMODELO,
                                iidTipoBus = tipobus.IIDTIPOBUS,
                                iidSucursal = sucursal.IIDSUCURSAL
                            }).ToList();

                //Validaciones para los filtros
                if (oBusCLS.iidBus == 0 && oBusCLS.placa == null && oBusCLS.iidModelo == 0 && oBusCLS.iidSucursal == 0 && oBusCLS.iidTipoBus == 0)
                    listaRespuesta = listaBus;
                else
                {
                    //-----------Filtros--------------
                    if (oBusCLS.iidBus != 0)
                    {
                        listaBus = listaBus.Where(p => p.iidBus.ToString().Contains(oBusCLS.iidBus.ToString())).ToList();
                    }

                    if (oBusCLS.placa != null)
                    {
                        listaBus = listaBus.Where(p => p.placa.ToString().Contains(oBusCLS.placa)).ToList();
                    }

                    if (oBusCLS.iidModelo != 0)
                    {
                        listaBus = listaBus.Where(p => p.iidModelo.ToString().Contains(oBusCLS.iidModelo.ToString())).ToList();
                    }

                    if (oBusCLS.iidSucursal != 0)
                    {
                        listaBus = listaBus.Where(p => p.iidSucursal.ToString().Contains(oBusCLS.iidSucursal.ToString())).ToList();
                    }

                    if (oBusCLS.iidTipoBus != 0)
                    {
                        listaBus = listaBus.Where(p => p.iidTipoBus.ToString().Contains(oBusCLS.iidTipoBus.ToString())).ToList();
                    }

                    listaRespuesta = listaBus;
                }
            }
                return View(listaRespuesta);
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            listarCombos();
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(BusCLS oBusCLS)
        {
            string placa = oBusCLS.placa;
            int numeroRegistrosEncontrados = 0;
            using (var bd = new BDPasajeEntities())
            {
                numeroRegistrosEncontrados = bd.Bus.Where(p =>p.PLACA.Equals(placa)).Count();
            }
                if (!ModelState.IsValid || numeroRegistrosEncontrados>0)
                {
                if (numeroRegistrosEncontrados > 0)
                    oBusCLS.mensajeError = "Este Bus ya existe!!";
                    listarCombos();
                    return View(oBusCLS);
                }
            using (var bd = new BDPasajeEntities())
            {
                Bus oBus = new Bus();
                oBus.BHABILITADO = 1;
                oBus.IIDBUS = oBusCLS.iidBus;
                oBus.IIDSUCURSAL = oBusCLS.iidSucursal;
                oBus.IIDMODELO = oBusCLS.iidModelo;
                oBus.IIDMARCA = oBusCLS.iidMarca;
                oBus.PLACA = oBusCLS.placa;
                oBus.NUMEROCOLUMNAS = oBusCLS.numeroColumnas;
                oBus.NUMEROFILAS = oBusCLS.numeroFilas;
                oBus.IIDTIPOBUS = oBusCLS.iidTipoBus;
                oBus.DESCRIPCION = oBusCLS.descripcion;
                oBus.OBSERVACION = oBusCLS.observacion;
                oBus.FECHACOMPRA = oBusCLS.fechaCompra;

                bd.Bus.Add(oBus);
                bd.SaveChanges();
            }

            return RedirectToAction("BusView");
        }

        public ActionResult Editar(int id)
        {
            BusCLS oBusCLS = new BusCLS();
            listarCombos();
            using (var bd = new BDPasajeEntities())
            {
                
                Bus oBus = bd.Bus.Find(id);
                oBusCLS.iidBus = oBus.IIDBUS;
                oBusCLS.iidSucursal = (int) oBus.IIDSUCURSAL;
                oBusCLS.iidModelo = (int)oBus.IIDMODELO;
                oBusCLS.iidMarca = (int)oBus.IIDMARCA;
                oBusCLS.iidTipoBus = (int)oBus.IIDTIPOBUS;
                oBusCLS.numeroColumnas = (int)oBus.NUMEROCOLUMNAS;
                oBusCLS.numeroFilas = (int)oBus.NUMEROFILAS;
                oBusCLS.fechaCompra = (DateTime)oBus.FECHACOMPRA;
                oBusCLS.observacion = oBus.OBSERVACION;
                oBusCLS.descripcion = oBus.DESCRIPCION;
                oBusCLS.placa = oBus.PLACA;

            }
                return View(oBusCLS);
        }


        [HttpPost]
        public ActionResult Editar(BusCLS oBusCLS)
        {
            int numeroRegistrosEncontrados = 0;
            string placa = oBusCLS.placa;
            int iidBus = oBusCLS.iidBus;
            using (var bd = new BDPasajeEntities())
            {
                numeroRegistrosEncontrados =
                    bd.Bus.Where(p => p.PLACA.Equals(placa) && !p.IIDBUS.Equals(iidBus)).Count();
            }
            if (!ModelState.IsValid || numeroRegistrosEncontrados>0)
            {
                if (numeroRegistrosEncontrados > 0)
                    oBusCLS.mensajeError = "Este Bus ya existe!!";
                listarCombos();
                return View(oBusCLS);
            }

            using (var bd = new BDPasajeEntities())
            {
                Bus oBus = bd.Bus.Find(oBusCLS.iidBus);
                oBus.IIDSUCURSAL = oBusCLS.iidSucursal;
                oBus.IIDMODELO = oBusCLS.iidModelo;
                oBus.IIDMARCA = oBusCLS.iidMarca;
                oBus.IIDTIPOBUS = oBusCLS.iidTipoBus;
                oBus.NUMEROCOLUMNAS = oBusCLS.numeroColumnas;
                oBus.NUMEROFILAS = oBusCLS.numeroFilas;
                oBus.FECHACOMPRA = oBusCLS.fechaCompra;
                oBus.OBSERVACION = oBusCLS.observacion;
                oBus.DESCRIPCION = oBusCLS.descripcion;
                oBus.PLACA = oBusCLS.placa;

                bd.SaveChanges();
            }

                return RedirectToAction("BusView");
        }

        public ActionResult EliminarBus(int id)
        {
            using (var bd = new BDPasajeEntities())
            {
                Bus oBus = bd.Bus.Find(id);
                oBus.BHABILITADO = 0;
                bd.SaveChanges();
            }
                return RedirectToAction("BusView");
        }



        /// <summary>
        /// Listado Combos
        /// </summary>
        public void listarCombos()
        {
            listarSucursal();
            listarMarca();
            listarModelo();
            listarTipoBus();
        }

        public void listarTipoBus()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from bus in bd.TipoBus
                         where bus.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = bus.NOMBRE,
                             Value = bus.IIDTIPOBUS.ToString()
                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "--SELECCIONE--", Value= "" });
                ViewBag.listaTipoBus = lista;
            }
        }

        public void listarMarca()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from marca in bd.Marca
                         where marca.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = marca.NOMBRE,
                             Value = marca.IIDMARCA.ToString()
                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "--SELECCIONE--", Value = "" });
                ViewBag.listaMarca = lista;
            }
        }

        public void listarModelo()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from modelo in bd.Modelo
                         where modelo.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = modelo.NOMBRE,
                             Value = modelo.IIDMODELO.ToString()
                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "--SELECCIONE--", Value = "" });
                ViewBag.listaModelo = lista;
            }
        }


        public void listarSucursal()
        {
            List<SelectListItem> lista;
            using (var bd = new BDPasajeEntities())
            {
                lista = (from sucursal in bd.Sucursal
                         where sucursal.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = sucursal.NOMBRE,
                             Value = sucursal.IIDSUCURSAL.ToString()
                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "--SELECCIONE--", Value ="" });
                ViewBag.listaSucursal = lista;
            }
        }
    }
}