using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SCA.Models
{
    [MetadataType(typeof(PerfilesMetaData))]

    public partial class Perfiles
    {
        public class PerfilesMetaData
        {
            [Display(Name = "Nombre de Perfil")]
            [Required(ErrorMessage = "El Nombre del Perfil es Requerido")]
            public string NombrePerfil { get; set; }

            [Required(ErrorMessage = "Se Debe Seleccionar una Opcion")]
            public int IdPermiso { get; set; }


        }
    }
}