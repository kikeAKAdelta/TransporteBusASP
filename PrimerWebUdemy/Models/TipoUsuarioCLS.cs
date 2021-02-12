using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrimerWebUdemy.Models
{
    public class TipoUsuarioCLS
    {
        [Display(Name = "Id Tipo Usuario")]
        public int iidTipoUsuario { get; set; }

        [Display(Name = "Nombre tipo usuario")]
        [Required]
        [StringLength(150, ErrorMessage = "Longitud Maxima 150")]
        public string nombre { get; set; }

        [Display(Name = "Descripcion tipo usuario")]
        [Required]
        public string descripcion { get; set; }

        [Display(Name = "Habilitado")]
        public int bhabilitado { get; set; }
    }
}