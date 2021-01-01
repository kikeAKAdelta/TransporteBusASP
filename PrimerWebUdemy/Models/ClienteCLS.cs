using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrimerWebUdemy.Models
{
    public class ClienteCLS
    {
        [Display(Name = "Id Cliente")]
        [Required]
        public int iidcliente { get; set; }


        [Display(Name = "Nombre Cliente")]
        [Required]
        [StringLength(100, ErrorMessage = "La longitud maxima es de 100")]
        public string nombre { get; set; }


        [Display(Name = "Apellido Paterno")]
        [Required]
        [StringLength(150, ErrorMessage = "La longitud maxima es de 150")]
        public string apPaterno { get; set; }


        [Display(Name = "Apellido Materno")]
        [Required]
        [StringLength(150, ErrorMessage = "La longitud maxima es de 150")]
        public string apMaterno { get; set; }

        [Display(Name = "Email")]
        [Required]
        [StringLength(200, ErrorMessage = "La longitud maxima es de 200")]
        [EmailAddress(ErrorMessage = "Ingrese un email valido")]
        public string email { get; set; }

        [Display(Name = "Direccion")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string direccion { get; set; }

        [Display(Name = "Sexo")]
        [Required]
        public int iidsexo { get; set; }

        [Required]
        [Display(Name = "Telefono Fijo")]
        [StringLength(10, ErrorMessage = "La longitud maxima es de 10")]
        public string telefonoFijo { get; set; }

        [Display(Name = "Telefono Celular")]
        [Required]
        [StringLength(10, ErrorMessage = "La longitud maxima es de 10")]
        public string telefonoCelular { get; set; }

        public int bhabilitado { get; set; }


        //PROPIEDAD DE ERROR PARA NO REPETIR NOMBRE
        public string mensajeError { get; set; }
    }
}