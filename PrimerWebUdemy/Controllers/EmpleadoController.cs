using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrimerWebUdemy.Models;

namespace PrimerWebUdemy.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult EmpleadoView(EmpleadoCLS oEmpleadoCLS)
        {
            int iidTipoUsuario = oEmpleadoCLS.iidtipoUsuario;
            List<EmpleadoCLS> listaEmpleados = null;
            listarComboTipoUsuario();
            using (var bd = new BDPasajeEntities())
            {
                if (iidTipoUsuario == 0)
                {
                    listaEmpleados = (from empleado in bd.Empleado
                                      join tipousuario in bd.TipoUsuario
                                      on empleado.IIDTIPOUSUARIO equals tipousuario.IIDTIPOUSUARIO
                                      join tipocontrato in bd.TipoContrato
                                      on empleado.IIDTIPOCONTRATO equals tipocontrato.IIDTIPOCONTRATO
                                      where empleado.BHABILITADO == 1
                                      select new EmpleadoCLS
                                      {
                                          iidEmpleado = empleado.IIDEMPLEADO,
                                          nombre = empleado.NOMBRE,
                                          apMaterno = empleado.APMATERNO,
                                          apPaterno = empleado.APPATERNO,
                                          nombreTipoUsuario = tipousuario.NOMBRE,
                                          nombreTipoContrato = tipocontrato.NOMBRE
                                      }).ToList();
                }else
                {
                    listaEmpleados = (from empleado in bd.Empleado
                                      join tipousuario in bd.TipoUsuario
                                      on empleado.IIDTIPOUSUARIO equals tipousuario.IIDTIPOUSUARIO
                                      join tipocontrato in bd.TipoContrato
                                      on empleado.IIDTIPOCONTRATO equals tipocontrato.IIDTIPOCONTRATO
                                      where empleado.BHABILITADO == 1 && empleado.IIDTIPOUSUARIO == iidTipoUsuario
                                      select new EmpleadoCLS
                                      {
                                          iidEmpleado = empleado.IIDEMPLEADO,
                                          nombre = empleado.NOMBRE,
                                          apMaterno = empleado.APMATERNO,
                                          apPaterno = empleado.APPATERNO,
                                          nombreTipoUsuario = tipousuario.NOMBRE,
                                          nombreTipoContrato = tipocontrato.NOMBRE
                                      }).ToList();
                }
            }
                return View(listaEmpleados);
        }

        [HttpGet]
        public ActionResult EmpleadoAdd()
        {
            listarCombos();
            return View();
        }

        [HttpPost]
        public ActionResult EmpleadoAdd(EmpleadoCLS oEmpleadoCLS)
        {
            int numeroRegistrosEncontrados = 0;
            string nombre = oEmpleadoCLS.nombre;
            string apPaterno = oEmpleadoCLS.apPaterno;
            string apMaterno = oEmpleadoCLS.apMaterno;
            using (var bd = new BDPasajeEntities())
            {
                numeroRegistrosEncontrados =
                    bd.Empleado.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(apPaterno) && p.APMATERNO.Equals(apMaterno)).Count();
            }

            if (!ModelState.IsValid || numeroRegistrosEncontrados > 0)
            {
                if (numeroRegistrosEncontrados > 0)
                    oEmpleadoCLS.mensajeError = "Ya existe el registro!";

                listarCombos();
                return View(oEmpleadoCLS);
            }
            using (var bd = new BDPasajeEntities())
            {
                Empleado oEmpleado = new Empleado();
                oEmpleado.NOMBRE = oEmpleadoCLS.nombre;
                oEmpleado.APPATERNO = oEmpleadoCLS.apPaterno;
                oEmpleado.APMATERNO = oEmpleadoCLS.apMaterno;
                oEmpleado.FECHACONTRATO = oEmpleadoCLS.fechaContrato;
                oEmpleado.SUELDO = oEmpleadoCLS.sueldo;
                oEmpleado.IIDTIPOUSUARIO = oEmpleadoCLS.iidtipoUsuario;
                oEmpleado.IIDTIPOCONTRATO = oEmpleadoCLS.iidtipoContrato;
                oEmpleado.IIDSEXO = oEmpleadoCLS.iidSexo;
                oEmpleado.BHABILITADO = 1;
                bd.Empleado.Add(oEmpleado);
                bd.SaveChanges();
            }
                return RedirectToAction("EmpleadoView");
        }

        public ActionResult EditarEmpleado(int id)
        {
            listarCombos();
            EmpleadoCLS oEmpleadoCLS = new EmpleadoCLS();
            using (var bd = new BDPasajeEntities())
            {
                Empleado oEmpleado = bd.Empleado.Where(p=> p.IIDEMPLEADO == id).First();
                oEmpleadoCLS.iidEmpleado = oEmpleado.IIDEMPLEADO;
                oEmpleadoCLS.nombre = oEmpleado.NOMBRE;
                oEmpleadoCLS.apPaterno = oEmpleado.APPATERNO;
                oEmpleadoCLS.apMaterno = oEmpleado.APMATERNO;
                oEmpleadoCLS.fechaContrato = oEmpleado.FECHACONTRATO;
                oEmpleadoCLS.sueldo = (decimal) oEmpleado.SUELDO;
                oEmpleadoCLS.iidtipoUsuario = (int) oEmpleado.IIDTIPOUSUARIO;
                oEmpleadoCLS.iidtipoContrato = (int) oEmpleado.IIDTIPOCONTRATO;
                oEmpleadoCLS.iidSexo = (int) oEmpleado.IIDSEXO;
                oEmpleadoCLS.bhabilitado = 1;
            }

                return View(oEmpleadoCLS);
        }


        [HttpPost]
        public ActionResult EditarEmpleado(EmpleadoCLS oEmpleadoCLS)
        {
            int numeroRegistrosEncontrados = 0;
            int iidEmpleado = oEmpleadoCLS.iidEmpleado;
            string nombre = oEmpleadoCLS.nombre;
            string apPaterno = oEmpleadoCLS.apPaterno;
            string apMaterno = oEmpleadoCLS.apMaterno;
            using (var bd = new BDPasajeEntities())
            {
                numeroRegistrosEncontrados =
                    bd.Empleado.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(apPaterno) && p.APMATERNO.Equals(apMaterno) && !p.IIDEMPLEADO.Equals(iidEmpleado)).Count();
            }

            if (!ModelState.IsValid || numeroRegistrosEncontrados > 0)
            {
                listarCombos();

                if (numeroRegistrosEncontrados > 0)
                    oEmpleadoCLS.mensajeError = "Este Empleado ya existe!!";

                return View(oEmpleadoCLS);
            }

            using (var bd = new BDPasajeEntities())
            {
                Empleado oEmpleado = bd.Empleado.Find(oEmpleadoCLS.iidEmpleado);
                oEmpleado.NOMBRE = oEmpleadoCLS.nombre;
                oEmpleado.APPATERNO = oEmpleadoCLS.apPaterno;
                oEmpleado.APMATERNO = oEmpleadoCLS.apMaterno;
                oEmpleado.FECHACONTRATO = oEmpleadoCLS.fechaContrato;
                oEmpleado.SUELDO = oEmpleadoCLS.sueldo;
                oEmpleado.IIDTIPOUSUARIO = oEmpleadoCLS.iidtipoUsuario;
                oEmpleado.IIDTIPOCONTRATO = oEmpleadoCLS.iidtipoContrato;
                oEmpleado.IIDSEXO = oEmpleadoCLS.iidSexo;

                bd.SaveChanges();
            }

            return RedirectToAction("EmpleadoView");
        }

        public ActionResult EliminarEmpleado(int id)
        {
            using (var bd = new BDPasajeEntities())
            {
                Empleado oEmpleado = bd.Empleado.Find(id);
                oEmpleado.BHABILITADO = 0;
                bd.SaveChanges();
            }
                return RedirectToAction("EmpleadoView");
        }



        /*--------------INICIO: METODOS LLENAR COMBOBOX POR JOINS----------------*/
        public void listarComboSexo()
        {
            List<SelectListItem> listaSexo;

            using (var bd = new BDPasajeEntities())
            {
                listaSexo = (from sexo in bd.Sexo
                             where sexo.BHABILITADO == 1
                             select new SelectListItem
                             {
                                 Text = sexo.NOMBRE,
                                 Value = sexo.IIDSEXO.ToString()
                             }).ToList();

                listaSexo.Insert(0, new SelectListItem { Text = "-- Selecione --", Value=""});
                ViewBag.listaSexoViewBag = listaSexo;
            }

        }

        public void listarComboTipoContrato()
        {
            List<SelectListItem> listaTipoContrato;

            using (var bd = new BDPasajeEntities())
            {
                listaTipoContrato = (from tipocontrato in bd.TipoContrato
                                     where tipocontrato.BHABILITADO == 1
                                     select new SelectListItem
                                     {
                                         Text = tipocontrato.NOMBRE,
                                         Value = tipocontrato.IIDTIPOCONTRATO.ToString()
                                     }).ToList();

                listaTipoContrato.Insert(0, new SelectListItem { Text = "-- Seleccione --", Value=""});
                ViewBag.listaTipoContratoViewBag = listaTipoContrato;
            }
        }

        public void listarComboTipoUsuario()
        {
            List<SelectListItem> listaTipoUsuario;

            using (var bd = new BDPasajeEntities())
            {
                listaTipoUsuario = (from tipousuario in bd.TipoUsuario
                                     where tipousuario.BHABILITADO == 1
                                     select new SelectListItem
                                     {
                                         Text = tipousuario.NOMBRE,
                                         Value = tipousuario.IIDTIPOUSUARIO.ToString()
                                     }).ToList();

                listaTipoUsuario.Insert(0, new SelectListItem { Text = "-- Seleccione --", Value = "" });
                ViewBag.listaTipoUsuarioViewBag = listaTipoUsuario;
            }
        }

        public void listarCombos()
        {
            listarComboSexo();
            listarComboTipoContrato();
            listarComboTipoUsuario();
        }




        /*--------------FIN: METODOS LLENAR COMBOBOX POR JOINS----------------*/
    }
}