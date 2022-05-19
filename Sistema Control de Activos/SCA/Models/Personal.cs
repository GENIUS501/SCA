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
    using System.ComponentModel.DataAnnotations;

    public partial class Personal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Personal()
        {
            this.ControlInventario = new HashSet<ControlInventario>();
            this.ControlVehiculo = new HashSet<ControlVehiculo>();
            this.Usuario = new HashSet<Usuario>();
        }
        [Display(Name = "Id de la persona")]
        public int IdPersonal { get; set; }
        [Display(Name = "Cédula")]
        [Required(ErrorMessage = "La cédula es requerida")]
        public string Cedula { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Display(Name = "Primer apellido")]
        [Required(ErrorMessage = "El primer apellido es requerido")]
        public string Apellido1 { get; set; }
        [Display(Name = "Segundo apellido")]
        [Required(ErrorMessage = "El segundo apellido es requerido")]
        public string Apellido2 { get; set; }
        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "El telefono es requerido")]
        public string Telefono { get; set; }
        [Display(Name = "Correo electronico")]
        [Required(ErrorMessage = "El correo electronico es requerido")]
        [EmailAddress(ErrorMessage = "Debe escribir un correo electrónico válido")]
        [MaxLength(200, ErrorMessage = "200 caracteres como máximo")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        [Display(Name = "Tipo de licencia")]
        [Required(ErrorMessage = "El tipo de licencia es requerido")]
        public Nullable<int> IdLicencia { get; set; }
        [Display(Name = "Carnet MS")]
        [Required(ErrorMessage = "El carnet MS es requerido")]
        public Nullable<int> CarnetMS { get; set; }
        [Display(Name = "Vencimiento de carnet ms")]
        [Required(ErrorMessage = "El vencimiento de carnet ms es requerido")]
        public Nullable<System.DateTime> VenceCarnetMS { get; set; }
        [Display(Name = "Departamento")]
        public Nullable<int> IdDepartamento { get; set; }
        public Nullable<int> MotivoDeshabilitar { get; set; }
        public string ValorNuevo()
        {
            return "IdPersonal:" + IdPersonal + " |Cedula:" + Cedula + " |Nombre:" + Nombre + "|Apellido1:" + Apellido1.ToString() + " |Apellido2:" + Apellido2 + " |Telefono:" + Telefono + " |Correo:" + Correo + " |IdLicencia:" + IdLicencia + " |CarnetMS:" + CarnetMS + " |VenceCarnetMS:" + VenceCarnetMS + " |IdDepartamento:" + IdDepartamento + " |MotivoDeshabilitar:" + MotivoDeshabilitar;
        }
        public string ValorAntiguo(Personal Entidad)
        {
            return "IdPersonal:" + Entidad.IdPersonal + " |Cedula:" + Entidad.Cedula + " |Nombre:" + Entidad.Nombre + "|Apellido1:" + Entidad.Apellido1.ToString() + " |Apellido2:" + Entidad.Apellido2 + " |Telefono:" + Entidad.Telefono + " |Correo:" + Entidad.Correo + " |IdLicencia:" + Entidad.IdLicencia + " |CarnetMS:" + Entidad.CarnetMS + " |VenceCarnetMS:" + Entidad.VenceCarnetMS + " |IdDepartamento:" + Entidad.IdDepartamento + " |MotivoDeshabilitar:" + Entidad.MotivoDeshabilitar;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ControlInventario> ControlInventario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ControlVehiculo> ControlVehiculo { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual Licencia Licencia { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
