using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SCA.Models
{
    [MetadataType(typeof(MantenimientoInventarioMetaData))]

    public partial class MantenimientoInventario
    {
        public class MantenimientoInventarioMetaData
        {
            //[Required(ErrorMessage = "Se Debe Ingresar un Codigo")]
            //public int IdInventario { get; set; }

            //[Required(ErrorMessage = "Se Debe Seleccionar una Opcion")]
            //public string TipoMantenimiento { get; set; }

            //[Display(Name = "Costo del Mantenimiento")]
            //public decimal CostoMantenimiento { get; set; }

            //[Display(Name = "Fecha de Mantenimiento")]
            //[DataType(DataType.Date)]
            //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            //public System.DateTime FechaMantenimiento { get; set; }

            //[Display(Name = "Fecha Proximo Mantenimiento")]
            //[DataType(DataType.Date)]
            //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            //public System.DateTime ProximoMantenimiento { get; set; }

            //[Display(Name = "Descripcion del Servicio")]
            //public string DescripcionServicio { get; set; }
        }
    }
}