using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SCA.Models
{
    [MetadataType(typeof(DepartamentoMetaData))]

    public partial class Departamento
    {
        public class DepartamentoMetaData
        {
            [Display(Name ="Nombre de Departamento")]
            [Required(ErrorMessage ="El Nombre del Departamento es Requerido")]
            public string Nombre { get; set; }
        }
    }
}