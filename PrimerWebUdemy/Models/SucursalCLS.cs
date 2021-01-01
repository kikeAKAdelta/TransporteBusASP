using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PrimerWebUdemy.Models
{
    public class SucursalCLS
    {
        [Display(Name = "Id Sucursal")]
        public int iidsucursal { get; set; }

        [Display(Name ="Nombre sucursal")]
        [Required]
        [StringLength(100, ErrorMessage ="Longitud maxima es de 100")]
        public string nombre { get; set; }

        [Display(Name = "Direccion")]
        [Required]
        [StringLength(200, ErrorMessage= "Longitud maxima es de 200")]
        public string direccion { get; set; }


        [Display(Name = "Telefono Sucursal")]
        [StringLength(10, ErrorMessage = "Longitud maxima es de 10")]
        [Required]
        public string telefono { get; set; }

        [Display(Name = "Email Sucursal")]
        [Required]
        [StringLength(100, ErrorMessage = "Longitud maxima es de 100")]
        [EmailAddress(ErrorMessage = "Ingrese un email valido")]
        public string email { get; set; }

        [Display(Name = "Fecha Apertura")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaApertura { get; set; }

        public int bhabilitado { get; set; }

        //PROPIEDAD ADICIONAL PARA ERRORES NOMBRES DUPLICADOS
        public string mensajeError { get; set; }

    }
}