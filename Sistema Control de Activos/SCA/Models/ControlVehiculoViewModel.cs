using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCA.Models
{
    public class ControlVehiculoViewModel
    {
        [Display(Name = "Id del control del vehiculo")]
        public int IdControlVehiculo { get; set; }
        [Display(Name = "Flotilla")]
        public Nullable<int> IdFlotilla { get; set; }
        [Display(Name = "Personal")]
        public Nullable<int> IdPersonal { get; set; }
        [Display(Name = "Estado del vehiculo")]
        [Required(ErrorMessage = "El estado del vehiculo es Requerido")]
        public int EstadoVehiculo { get; set; }
        [Display(Name = "Fecha de salida")]
        public System.DateTime FechaSalida { get; set; }
        [Display(Name = "Kilometraje de salida")]
        [Required(ErrorMessage = "El kilometraje de salida es Requerido")]
        public int KilometrajeSalida { get; set; }
        [Display(Name = "Fecha de ingreso")]
        public System.DateTime FechaIngresa { get; set; }
        [Display(Name = "Kilometraje de ingreso")]
        public int KilometrajeIngresa { get; set; }
        [Display(Name = "Anomalias")]
        public string Anomalias { get; set; }
        public string ValorNuevo()
        {
            return "IdControlVehiculo:" + IdControlVehiculo + " |IdFlotilla:" + IdFlotilla + " |IdPersonal:" + IdPersonal + "| EstadoVehiculo:" + EstadoVehiculo + "| FechaSalida:" + FechaSalida + "| KilometrajeSalida:" + KilometrajeSalida + "| FechaIngresa:" + FechaIngresa + "| KilometrajeIngresa:" + KilometrajeIngresa + "| Anomalias:" + Anomalias;
        }
        public string ValorAntiguo(ControlVehiculo Entidad)
        {
            return "IdControlVehiculo:" + Entidad.IdControlVehiculo + " |IdFlotilla:" + Entidad.IdFlotilla + " |IdPersonal:" + Entidad.IdPersonal + "| EstadoVehiculo:" + Entidad.EstadoVehiculo + "| FechaSalida:" + Entidad.FechaSalida + "| KilometrajeSalida:" + Entidad.KilometrajeSalida + "| FechaIngresa:" + Entidad.FechaIngresa + "| KilometrajeIngresa:" + Entidad.KilometrajeIngresa + "| Anomalias:" + Entidad.Anomalias;
        }
    }
}