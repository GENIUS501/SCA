﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BaseDatosSCAEntities : DbContext
    {
        public BaseDatosSCAEntities()
            : base("name=BaseDatosSCAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<MantenimientoInventario> MantenimientoInventario { get; set; }
        public virtual DbSet<BitacoraIngresoSalida> BitacoraIngresoSalida { get; set; }
        public virtual DbSet<BitacoraMovimiento> BitacoraMovimiento { get; set; }
        public virtual DbSet<ControlInventario> ControlInventario { get; set; }
        public virtual DbSet<ControlVehiculo> ControlVehiculo { get; set; }
        public virtual DbSet<Departamento> Departamento { get; set; }
        public virtual DbSet<Flotilla> Flotilla { get; set; }
        public virtual DbSet<Inventario> Inventario { get; set; }
        public virtual DbSet<Licencia> Licencia { get; set; }
        public virtual DbSet<MantenimientoVehiculo> MantenimientoVehiculo { get; set; }
        public virtual DbSet<Perfiles_Acceso> Perfiles_Acceso { get; set; }
        public virtual DbSet<Personal> Personal { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Perfiles_Permisos> Perfiles_Permisos { get; set; }
    }
}
