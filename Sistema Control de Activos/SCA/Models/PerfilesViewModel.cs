using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SCA.Models
{
    public class PerfilesViewModel : Perfiles_Acceso
    {
        [Display(Name = "Nombre del perfil")]
        [Required(ErrorMessage = "El Nombre del perfil es Requerido")]
        public string NombrePerfil { get; set; }
        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "La descripcion es Requerida")]
        public string Descripcion { get; set; }
        public List<Modulos> ModulosEscogidos { get; set; }
        public class Modulos
        {
            public string Modulo { get; set; }
            public string Checked { get; set; }
        }
        public string ValorNuevo()
        {
            return "Id_Perfil:" + Id_Perfil + " |NombrePerfil:" + NombrePerfil + " |Descripcion:" + Descripcion;
        }
        public string ValorAntiguo(Perfiles_Acceso Entidad)
        {
            return "Id_Perfil:" + Id_Perfil + " |NombrePerfil:" + NombrePerfil + " |Descripcion:" + Descripcion;
        }
    }
}