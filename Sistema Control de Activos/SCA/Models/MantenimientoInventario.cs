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

    public partial class MantenimientoInventario
    {
        public int IdMantenimientoInventario { get; set; }
        public Nullable<int> IdInventario { get; set; }
        public int TipoMantenimiento { get; set; }
        public int CostoMantenimiento { get; set; }
        public Nullable<System.DateTime> FechaMantenimiento { get; set; }
        public Nullable<System.DateTime> FechaProximoMantenimiento { get; set; }
        public string DescripcionServicio { get; set; }
        public string ValorNuevo()
        {
            return "IdMantenimientoInventario:" + IdMantenimientoInventario + " |IdInventario:" + IdInventario + " |TipoMantenimiento:" + TipoMantenimiento + "|CostoMantenimiento:" + CostoMantenimiento.ToString() + " |FechaMantenimiento:" + FechaMantenimiento + " |FechaProximoMantenimiento:" + FechaProximoMantenimiento + " |DescripcionServicio:" + DescripcionServicio;
        }
        public string ValorAntiguo(MantenimientoInventario Entidad)
        {
            return "IdMantenimientoInventario:" + Entidad.IdMantenimientoInventario + " |IdInventario:" + Entidad.IdInventario + " |TipoMantenimiento:" + Entidad.TipoMantenimiento + "|CostoMantenimiento:" + Entidad.CostoMantenimiento.ToString() + " |FechaMantenimiento:" + Entidad.FechaMantenimiento + " |FechaProximoMantenimiento:" + Entidad.FechaProximoMantenimiento + " |DescripcionServicio:" + Entidad.DescripcionServicio;
        }
        public virtual Inventario Inventario { get; set; }
    }
}
