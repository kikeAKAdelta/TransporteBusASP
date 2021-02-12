using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrimerWebUdemy.Models;

namespace PrimerWebUdemy.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult ClienteView(ClienteCLS oClienteCLS)
        {
            llenarSexo();
            ViewBag.lista = listaSexo;
            List<ClienteCLS> listaCLiente = null;
            using (var bd = new BDPasajeEntities())
            {
                if (oClienteCLS.iidsexo == 0)
                {
                    listaCLiente = (from cliente in bd.Cliente
                                    where cliente.BHABILITADO == 1
                                    select new ClienteCLS
                                    {
                                        iidcliente = cliente.IIDCLIENTE,
                                        nombre = cliente.NOMBRE,
                                        apPaterno = cliente.APPATERNO,
                                        apMaterno = cliente.APMATERNO,
                                        telefonoFijo = cliente.TELEFONOFIJO
                                    }).ToList();
                }else
                {
                    listaCLiente = (from cliente in bd.Cliente
                                    where cliente.BHABILITADO == 1 && cliente.IIDSEXO == oClienteCLS.iidsexo
                                    select new ClienteCLS
                                    {
                                        iidcliente = cliente.IIDCLIENTE,
                                        nombre = cliente.NOMBRE,
                                        apPaterno = cliente.APPATERNO,
                                        apMaterno = cliente.APMATERNO,
                                        telefonoFijo = cliente.TELEFONOFIJO
                                    }).ToList();
                }
            }
                return View(listaCLiente);
        }


        List<SelectListItem> listaSexo;

        private void llenarSexo()
        {
            using (var bd = new BDPasajeEntities())
            {
                listaSexo = (from sexo in bd.Sexo
                             where sexo.BHABILITADO == 1
                             select new SelectListItem
                             {
                                 Text = sexo.NOMBRE,
                                 Value = sexo.IIDSEXO.ToString()
                             }).ToList();

                listaSexo.Insert(0, new SelectListItem { Text = "-- Selecciones --", Value = "" });
            }
        }

        [HttpGet]
        public ActionResult ClienteAgregar()
        {
            llenarSexo();
            ViewBag.lista = listaSexo;
            return View();
        }

        [HttpPost]
        public ActionResult ClienteAgregar(ClienteCLS model)
        {

            int numeroRegistrosEncontrados = 0;
            string nombre = model.nombre;
            string apPaterno = model.apPaterno;
            string apMaterno = model.apMaterno;
            using (var bd = new BDPasajeEntities())
            {
                numeroRegistrosEncontrados = 
                    bd.Cliente.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(apPaterno) && p.APMATERNO.Equals(apMaterno)).Count();
            }


            if (!ModelState.IsValid || numeroRegistrosEncontrados>0)
            {

                if (numeroRegistrosEncontrados > 0)
                    model.mensajeError = "Ya existe el registro!";

                llenarSexo();
                ViewBag.lista = listaSexo;  //Asi no se cae el programa porque se vuelve a pintar
                return View(model);
            }
            using (var bd = new BDPasajeEntities())
            {
                Cliente ocliente = new Cliente();
                ocliente.NOMBRE = model.nombre;
                ocliente.APPATERNO = model.apPaterno;
                ocliente.APMATERNO = model.apMaterno;
                ocliente.EMAIL = model.email;
                ocliente.DIRECCION = model.direccion;
                ocliente.IIDSEXO = model.iidsexo;
                ocliente.TELEFONOCELULAR = model.telefonoCelular;
                ocliente.TELEFONOFIJO = model.telefonoFijo;
                ocliente.BHABILITADO = 1;

                bd.Cliente.Add(ocliente);
                bd.SaveChanges();
            }

            return RedirectToAction("ClienteView");
        }

        [HttpGet]
        public ActionResult ClienteEditar(int id)
        {
            llenarSexo();
            ViewBag.lista = listaSexo;  //Asi no se cae el programa porque se vuelve a pintar

            ClienteCLS oClienteCLS = new ClienteCLS();
            using (var bd= new BDPasajeEntities())
            {
                Cliente oCliente = bd.Cliente.Where(p=>p.IIDCLIENTE == id).First();
                oClienteCLS.iidcliente = oCliente.IIDCLIENTE;
                oClienteCLS.nombre = oCliente.NOMBRE;
                oClienteCLS.apPaterno = oCliente.APPATERNO;
                oClienteCLS.apMaterno = oCliente.APMATERNO;
                oClienteCLS.email = oCliente.EMAIL;
                oClienteCLS.direccion = oCliente.DIRECCION;
                oClienteCLS.iidsexo = (int) oCliente.IIDSEXO;
                oClienteCLS.telefonoCelular = oCliente.TELEFONOCELULAR;
                oClienteCLS.telefonoFijo = oCliente.TELEFONOFIJO;
                oClienteCLS.bhabilitado = 1;
            }

                return View(oClienteCLS);
        }


        public ActionResult ClienteEditar(ClienteCLS oClienteCLS)
        {
            
            int numeroRegistrosEncontrados = 0;
            int iidCliente = oClienteCLS.iidcliente;
            string nombre = oClienteCLS.nombre;
            string apPaterno = oClienteCLS.apPaterno;
            string apMaterno = oClienteCLS.apMaterno;
            using (var bd = new BDPasajeEntities())
            {
                numeroRegistrosEncontrados =
                    bd.Cliente.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(apPaterno) && p.APMATERNO.Equals(apMaterno) && !p.IIDCLIENTE.Equals(iidCliente)).Count();
            }

            if (!ModelState.IsValid || numeroRegistrosEncontrados>0)
            {
                llenarSexo();
                ViewBag.lista = listaSexo;  //Asi no se cae el programa porque se vuelve a pintar

                if (numeroRegistrosEncontrados > 0)
                    oClienteCLS.mensajeError = "Este cliente ya existe!!";
                return View(oClienteCLS);
            }

            int idCliente = oClienteCLS.iidcliente;

            using (var bd = new BDPasajeEntities())
            {
                Cliente oCliente = bd.Cliente.Find(idCliente);

                oCliente.NOMBRE = oClienteCLS.nombre;
                oCliente.APPATERNO = oClienteCLS.apPaterno;
                oCliente.APMATERNO = oClienteCLS.apMaterno;
                oCliente.DIRECCION = oClienteCLS.direccion;
                oCliente.EMAIL = oClienteCLS.email;
                oCliente.IIDSEXO = oClienteCLS.iidsexo;
                oCliente.TELEFONOCELULAR = oClienteCLS.telefonoCelular;
                oCliente.TELEFONOFIJO = oClienteCLS.telefonoFijo;

                bd.SaveChanges();
            }

                return RedirectToAction("ClienteView");
        }

        public ActionResult ClienteEliminar(int id)
        {
            using (var bd = new BDPasajeEntities())
            {
                Cliente oCliente = bd.Cliente.Find(id);
                oCliente.BHABILITADO = 0;
                bd.SaveChanges();
            }
                return RedirectToAction("ClienteView");
        }
    }
}