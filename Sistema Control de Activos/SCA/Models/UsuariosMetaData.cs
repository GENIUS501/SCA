using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SCA.Models
{
    [MetadataType(typeof(UsuariosMetaData))]

    public partial class Usuarios
    {
        public class UsuariosMetaData
        {
            [Required(ErrorMessage = "Se Debe Ingresar una Cedula")]
            public int IdPersonal { get; set; }

            [Required(ErrorMessage = "Se Debe Ingresar un Perfil")]
            public int IdPerfiles { get; set; }

            [Display(Name = "Contraseña")]
            [Required(ErrorMessage = "La Contraseña es Requerida")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

        }
    }
}