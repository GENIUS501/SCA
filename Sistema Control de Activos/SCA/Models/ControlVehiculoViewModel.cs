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
        public int KilometrajeSalida { get; set; }
        public System.DateTime FechaIngresa { get; set; }
        public int KilometrajeIngresa { get; set; }
        public string Anomalias { get; set; }
    }
}