using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SCA.Models
{
    [MetadataType(typeof(FlotillaMetaData))]

    public partial class Flotilla
    {
        public class FlotillaMetaData
        {
            [Display(Name = "Marca del Vehiculo")]
            [Required(ErrorMessage = "La Marca del Vehiculo es Requerido")]
            public string Marca { get; set; }

            [Display(Name = "Modelo del Vehiculo")]
            [Required(ErrorMessage = "El Modelo del Vehiculo es Requerido")]
            public string Modelo { get; set; }

            [Display(Name = "Placa del Vehiculo")]
            [Required(ErrorMessage = "La Placa del Vehiculo es Requerido")]
            public string Placa { get; set; }

            [Required(ErrorMessage = "Se Debe Seleccionar una Opcion")]
            public string Traccion { get; set; }

            [Display(Name = "Año del Vehiculo")]
            [Required(ErrorMessage = "El Año del Vehiculo es Requerido")]
            public string Anno { get; set; }

            [Required(ErrorMessage = "Se Debe Seleccionar una Opcion")]
            public string Combustible { get; set; }

            [Display(Name = "Fecha de Compra")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime FechaCompra { get; set; }

            [Display(Name = "Costo del Vehiculo")]
            public decimal CostoEquipo { get; set; }

            [Required(ErrorMessage = "Se Debe Seleccionar una Opcion")]
            public int IdDepartamento { get; set; }

            public string MotivoDeshabilitar { get; set; }
        }
    }
}