using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace SCA.Models
{
    [MetadataType(typeof(MantenimientoVehiculoMetaData))]

    public partial class MantenimientoVehiculo
    {
        public class MantenimientoVehiculoMetaData
        {
            [Required(ErrorMessage = "Se Debe Ingresar una Placa")]
            public int IdFlotilla { get; set; }

            [Required(ErrorMessage = "Se Debe Seleccionar una Opcion")]
            public string TipoMantenimiento { get; set; }

            [Display(Name = "Kilometraje Actual")]
            [Required(ErrorMessage = "Se debe Ingresar el Kilometraje Actual")]
            public int KMActual { get; set; }

            [Display(Name = "{Proximo Kilometraje")]
            [Required(ErrorMessage = "Se debe Ingresar el Kilometraje del Proximo Mantenimiento")]
            public int KMProximo { get; set; }

            [Display(Name = "Costo del Mantenimiento")]
            public decimal CostoMantenimiento { get; set; }

            [Display(Name = "Fecha de Mantenimiento")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime FechaMantenimiento { get; set; }

            [Display(Name = "Descripcion del Servicio")]
            public string DescripcionServicio { get; set; }
        }
    }
}