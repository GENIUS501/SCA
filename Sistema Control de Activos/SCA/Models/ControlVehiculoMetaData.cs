using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SCA.Models
{
    [MetadataType(typeof(ControlVehiculoMetaData))]

    public partial class ControlVehiculo
    {
        public class ControlVehiculoMetaData
        {
            [Required(ErrorMessage = "Se Debe Ingresar una Placa")]
            public int IdFlotilla { get; set; }

            [Required(ErrorMessage = "Se Debe Ingresar una Cedula")]
            public int IdPersonal { get; set; }

            [Required(ErrorMessage = "Se Debe Seleccionar una Opcion")]
            public string EstadoVehiculo { get; set; }

            [Display(Name = "Fecha de Salida")]
            [Required(ErrorMessage = "La Fecha de Salida es Requerida")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime FechaSalida { get; set; }

            [Display(Name = "Kilometraje de Salida")]
            [Required(ErrorMessage = "El Kilometraje de Salida es Requerida")]
            public int KMSalida { get; set; }

            [Display(Name = "Fecha de Ingreso")]
            [Required(ErrorMessage = "La Fecha de Ingreso es Requerida")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime Fechaingresa { get; set; }

            [Display(Name = "Kilometraje de Ingreso")]
            [Required(ErrorMessage = "El Kilometraje de Ingreso es Requerida")]
            public int KMIngreso { get; set; }

            [Display(Name = "Anomalias")]
            public string Anomalias { get; set; }
        }
    }
}