using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SCA.Models
{
    [MetadataType(typeof(LicenciaMetaData))]

    public partial class Licencia
    {
        public class LicenciaMetaData
        {
            [Required(ErrorMessage = "Se Debe Ingresar una Cedula")]
            public int IdPersonal { get; set; }

            [Required(ErrorMessage ="El Tipo de Licencia es Requerido")]
            public string TipoLicencia { get; set; }

            [Display(Name ="Fecha de Vencimiento")]
            [Required(ErrorMessage ="La Fecha de Vencimiento es Requerida")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime VencimientoLicencia { get; set; }
        }
    }
}