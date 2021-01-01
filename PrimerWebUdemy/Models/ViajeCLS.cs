using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrimerWebUdemy.Models
{
    public class ViajeCLS
    {
        [Display(Name = "id Viaje")]
        public int iidViaje { get; set; }

        [Display(Name = "Lugar Origen")]
        [Required]
        public int iidLugarOrigen { get; set; }

        [Display(Name = "Lugar Destino")]
        [Required]
        public int iidLugarDestino { get; set; }

        [Display(Name = "Precio")]
        [Required]
        public double precio { get; set; }

        [Display(Name = "Fecha Viaje")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaViaje { get; set; }

        [Display(Name = "Bus")]
        [Required]
        public int iidBus { get; set; }

        [Display(Name = "Numero Asientos Disponible")]
        [Required]
        public int numeroAsientosDisponibles { get; set; }


        //Propiedades adicionales por joins
        [Display(Name = "Lugar Destino")]
        public string nombreLugarDestino { get; set; }

        [Display(Name = "Lugar Origen")]
        public string nombreLugarOrigen { get; set; }

        [Display(Name = "Nombre Bus")]
        public string nombreBus { get; set; }

    }
}