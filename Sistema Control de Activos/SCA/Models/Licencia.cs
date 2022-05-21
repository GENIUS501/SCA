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

    public partial class Licencia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Licencia()
        {
            this.Personal = new HashSet<Personal>();
        }
        [Display(Name = "Id de la licencia")]
        public int IdLicencia { get; set; }
        [Display(Name = "Tipo de licencia")]
        [Required(ErrorMessage = "El tipo de licencia es requerido")]
        public string TipoLicencia { get; set; }
        [Display(Name = "Vencimiento de licencia")]
        [Required(ErrorMessage = "El Vencimiento de licencia es requerido")]
        public System.DateTime VenceLicencia { get; set; }
        public string ValorNuevo()
        {
            return "IdLicencia:" + IdLicencia + " |TipoLicencia:" + TipoLicencia + " |VenceLicencia:" + VenceLicencia;
        }
        public string ValorAntiguo(Licencia Entidad)
        {
            return "IdLicencia:" + Entidad.IdLicencia + " |TipoLicencia:" + Entidad.TipoLicencia + " |VenceLicencia:" + Entidad.VenceLicencia;
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Personal> Personal { get; set; }
    }
}
