using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SCA.Models
{
    public class PerfilesViewModel : Perfiles_Acceso
    {
        public List<Modulos> ModulosEscogidos { get; set; }
        public class Modulos
        {
            public string Modulo { get; set; }
            public string Checked { get; set; }
        }
    }
}