using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PrimerWebUdemy.Models
{
    public class EmpleadoCLS
    {
        [Display(Name = "Id Empleado")]
        public int iidEmpleado { get; set; }

        [Required]
        [Display(Name = "Nombre Empleado")]
        [StringLength(100, ErrorMessage = "Longitud maxima de 100")]
        public string nombre { get; set; }

        [Display(Name = "Apellido Paterno")]
        [Required]
        [StringLength(200, ErrorMessage = "Longitud maxima de 200")]
        public string apPaterno { get; set; }

        [Display(Name = "Apellido Materno")]
        [Required]
        [StringLength(200, ErrorMessage = "Longitud maxima de 200")]
        public string apMaterno { get; set; }

        [Display(Name = "Fecha Contrato")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ?fechaContrato { get; set; }

        [Display(Name = "Tipo Usuario")]
        [Required]
        public int iidtipoUsuario { get; set; }

        [Display(Name = "Tipo Contrato")]
        [Required]
        public int iidtipoContrato { get; set; }

        [Display(Name = "Sexo")]
        [Required]
        public int iidSexo { get; set; }


        [Display(Name = "Sueldo")]
        [Required]
        [Range(0, 1000000, ErrorMessage = "Fuera de Rango")]
        public decimal sueldo { get; set; }

        public int bhabilitado { get; set; }

        

        /*Campos adicionales por joins*/
        [Display(Name = "Tipo Contrato")]
        public string nombreTipoContrato { get; set; }

        [Display(Name = "Tipo Usuario")]
        public string nombreTipoUsuario { get; set; }

        //PROPIEDAD DE ERROR PARA NO REPETIR NOMBRE
        public string mensajeError { get; set; }



    }
}