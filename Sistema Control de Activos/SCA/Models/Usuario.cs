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
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.BitacoraIngresoSalida = new HashSet<BitacoraIngresoSalida>();
            this.BitacoraMovimiento = new HashSet<BitacoraMovimiento>();
        }
    
        public int IdUsuario { get; set; }
        public Nullable<int> IdPersonal { get; set; }
        public Nullable<int> IdPerfiles { get; set; }
        public string Usuario1 { get; set; }
        public string Contraseña { get; set; }
        public string ValorNuevo()
        {
            return "IdUsuario:" + IdUsuario.ToString() + " |IdPersonal:" + IdPersonal.ToString() + " |IdPerfiles:" + IdPerfiles.ToString() + "|Usuario1:" + Usuario1 + " |Contraseña:" + Contraseña;
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BitacoraIngresoSalida> BitacoraIngresoSalida { get; set; }
        public virtual Perfiles_Acceso Perfiles_Acceso { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BitacoraMovimiento> BitacoraMovimiento { get; set; }
        public virtual Personal Personal { get; set; }
    }
}
