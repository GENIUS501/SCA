using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SCA.Models
{
    [MetadataType(typeof(ControlInventarioMetaData))]

    public partial class ControlInventario
    {
        public class ControlInventarioMetaData
        {
            [Required(ErrorMessage = "Se Debe Ingresar un Codigo")]
            public int IdInventario { get; set; }

            [Required(ErrorMessage = "Se Debe Ingresar una Cedula")]
            public int IdPersonal { get; set; }

            [Required(ErrorMessage = "Se Debe Seleccionar una Opcion")]
            public string EstadoActivo { get; set; }

            [Display(Name = "Fecha de Salida")]
            [Required(ErrorMessage = "La Fecha de Salida es Requerida")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime FechaSalida { get; set; }

            [Display(Name = "Fecha de Ingreso")]
            [Required(ErrorMessage = "La Fecha de Ingreso es Requerida")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime Fechaingresa { get; set; }

            [Display(Name = "Anomalias")]
            public string Anomalias { get; set; }
        }
    }
}