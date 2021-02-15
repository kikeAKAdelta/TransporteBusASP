using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrimerWebUdemy.Models
{
    public class PaginaCLS
    {
        [Display(Name = "Id Pagina")]
        public int iidPagina { get; set; }

        [Required]
        [Display(Name = "Titulo de Link")]
        public string mensaje { get; set; }

        [Required]
        [Display(Name = "Nombre de la accion")]
        public string accion { get; set; }

        [Required]
        [Display(Name = "Nombre del controlador")]
        public string controlador { get; set; }

        public int bhabilitado { get; set; }
    }
}