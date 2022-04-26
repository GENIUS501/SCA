using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SCA.Models
{
    [MetadataType(typeof(InventarioMetaData))]
    public partial class Inventario
    {
        public class InventarioMetaData
        {
            [Display(Name = "Nombre del Equipo")]
            [Required(ErrorMessage = "El Nombre del Equipo es Requerido")]
            public string Nombre { get; set; }

            [Display(Name = "Modelo del Equipo")]
            [Required(ErrorMessage = "El Modelo del Equipo es Requerido")]
            public string Modelo { get; set; }

            [Display(Name = "Serie del Equipo")]
            [Required(ErrorMessage = "La Serie del Equipo es Requerido")]
            public string Serie { get; set; }

            [Display(Name = "Codigo del Equipo")]
            [Required(ErrorMessage = "El Codigo del Equipo es Requerido")]
            public string CodigoEmpresa { get; set; }

            [Display(Name = "Fabricante del Equipo")]
            [Required(ErrorMessage = "El Fabricante del Equipo es Requerido")]
            public string Fabricante { get; set; }

            [Display(Name = "Fecha de Compra")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime FechaCompra { get; set; }

            [Display(Name = "Costo del Equipo")]
            public decimal CostoEquipo { get; set; }

            [Required(ErrorMessage = "Se Debe Seleccionar una Opcion")]
            public string Garantia { get; set; }

            [Display(Name = "Vencimiento de Garantia")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime VenceGarantia { get; set; }

            [Required(ErrorMessage = "Se Debe Seleccionar una Opcion")]
            public int IdDepartamento { get; set; }

            public string MotivoDeshabilitar { get; set; }
        }

        public string ValorNuevo()
        {
            return "IdInventario:" + IdInventario.ToString() + " CodigoEmpresa:" + CodigoEmpresa + " Nombre:" + Nombre + " Modelo:" + Modelo + " Serie:" + Serie + " Fabricante:" + Fabricante + " FechaCompra" + FechaCompra.ToString() + " CostoEquipo:" + CostoEquipo + " Garantia:" + Garantia.ToString() + " VenceGarantia:" + VenceGarantia + " IdDepartamento:" + IdDepartamento;
        }
    }
}