using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCA.Models
{
    public class ControlInventarioViewModel
    {
        [Display(Name = "Id del control de inventario")]
        public int IdControlInventario { get; set; }
        [Display(Name = "Inventario")]
        public Nullable<int> IdInventario { get; set; }
        [Display(Name = "Nombre de la persona")]
        public Nullable<int> IdPersonal { get; set; }
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El estado es Requerido")]
        public int EstadoActivo { get; set; }
        [Display(Name = "Fecha de salida ")]
        public System.DateTime FechaSalida { get; set; }
        [Display(Name = "Estado de ingreso")]
        public System.DateTime FechaIngresa { get; set; }
        [Display(Name = "Anomalias")]
        public string Anomalias { get; set; }
        public string ValorNuevo()
        {
            return "IdControlInventario:" + IdControlInventario + " |IdInventario:" + IdInventario + " |IdPersonal:" + IdPersonal + "|EstadoActivo:" + EstadoActivo.ToString() + " |FechaSalida:" + FechaSalida + " |FechaIngresa:" + FechaIngresa + " |Anomalias:" + Anomalias;
        }
        public string ValorAntiguo(ControlInventario Entidad)
        {
            return "IdControlInventario:" + Entidad.IdControlInventario + " |IdInventario:" + Entidad.IdInventario + " |IdPersonal:" + Entidad.IdPersonal + "|EstadoActivo:" + Entidad.EstadoActivo.ToString() + " |FechaSalida:" + Entidad.FechaSalida + " |FechaIngresa:" + Entidad.FechaIngresa + " |Anomalias:" + Entidad.Anomalias;
        }
    }
}