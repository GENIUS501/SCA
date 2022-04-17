using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SCA.Models
{
    [MetadataType(typeof(PermisosMetaData))]

    public partial class Permisos
    {
        public class PermisosMetaData
        {
            [Display(Name = "Nombre de Permiso")]
            [Required(ErrorMessage = "El Nombre del Permiso es Requerido")]
            public string NombrePerfil { get; set; }
        }
    }
}