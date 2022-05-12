//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCA.Models
{
    using System;
    using System.Collections.Generic;

    public partial class MantenimientoVehiculo
    {
        public int IdMantenimientoVehiculo { get; set; }
        public Nullable<int> IdFlotilla { get; set; }
        public int TipoMantenimiento { get; set; }
        public int KilometrajeActual { get; set; }
        public int ProximoKilometraje { get; set; }
        public int CostoMantenimiento { get; set; }
        public Nullable<System.DateTime> FechaMantenimiento { get; set; }
        public string DescripcionServicio { get; set; }
        public string ValorNuevo()
        {
            return "IdMantenimientoVehiculo:" + IdMantenimientoVehiculo + " |IdFlotilla:" + IdFlotilla + " |TipoMantenimiento:" + TipoMantenimiento + "|CostoMantenimiento:" + CostoMantenimiento.ToString() + " |FechaMantenimiento:" + FechaMantenimiento + " |KilometrajeActual:" + KilometrajeActual + " |DescripcionServicio:" + DescripcionServicio + " |ProximoKilometraje:" + ProximoKilometraje;
        }
        public string ValorAntiguo(MantenimientoVehiculo Entidad)
        {
            return "IdMantenimientoVehiculo:" + Entidad.IdMantenimientoVehiculo + " |IdFlotilla:" + Entidad.IdFlotilla + " |TipoMantenimiento:" + Entidad.TipoMantenimiento + "|CostoMantenimiento:" + Entidad.CostoMantenimiento.ToString() + " |FechaMantenimiento:" + Entidad.FechaMantenimiento + " |KilometrajeActual:" + Entidad.KilometrajeActual + " |DescripcionServicio:" + Entidad.DescripcionServicio + " |ProximoKilometraje:" + Entidad.ProximoKilometraje;
        }
        public virtual Flotilla Flotilla { get; set; }
    }
}
