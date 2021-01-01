using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrimerWebUdemy.Models
{
    public class BusCLS
    {
        [Display(Name = "Id Bus")]
        public int iidBus { get; set; }

        [Display(Name = "Placa")]
        [Required]
        [StringLength(100, ErrorMessage = "Longitud maxima de 100")]
        public string placa { get; set; }

        [Display(Name = "Nombre Sucursal")]
        [Required]
        public int iidSucursal { get; set; }

        [Display(Name = "Tipo Bus")]
        [Required]
        public int iidTipoBus { get; set; }

        [Display(Name = "Fecha Compra")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0}:yyyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime fechaCompra { get; set; }

        [Display(Name = "Nombre Modelo")]
        [Required]
        public int iidModelo { get; set; }

        [Display(Name = "Numero Filas")]
        [Required]
        public int numeroFilas { get; set; }

        [Display(Name = "Numero Columnas")]
        [Required]
        public int numeroColumnas { get; set; }

        [Display(Name = "Descripcion")]
        [Required]
        [StringLength(200, ErrorMessage = "Longitud maxima 200")]
        public string descripcion { get; set; }

        [Display(Name = "Observacion")]
        [StringLength(200, ErrorMessage = "Longitud maxima 200")]
        public string observacion { get; set; }

        [Display(Name = "Nombre Marca")]
        [Required]
        public int iidMarca { get; set; }

        public int bhabilitado { get; set; }

        //Propiedades adicionales por joins
        [Display(Name = "Nombre Sucursal")]
        public string nombreSucursal { get; set; }

        [Display(Name = "Nombre Tipo Bus")]
        public string nombreTipoBus { get; set; }

        [Display(Name = "Nombre Modelo")]
        public string nombreModelo { get; set; }


        //PROPIEDAD DE ERROR DE VALIDACION
        public string mensajeError { get; set; }
    }
}