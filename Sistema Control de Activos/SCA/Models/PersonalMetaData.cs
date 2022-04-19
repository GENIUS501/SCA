using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SCA.Models
{
    [MetadataType(typeof(PersonalMetaData))]

    public partial class Personal
    {
        public class PersonalMetaData
        {
            [Display(Name = "Nombre del Empleado")]
            [Required(ErrorMessage = "El Nombre del Empleado es Requerido")]
            public string Nombre { get; set; }

            [Display(Name = "Primer Apellido del Empleado")]
            [Required(ErrorMessage = "El Apellido del Empleado es Requerido")]
            public string Apellido1 { get; set; }

            [Display(Name = "Segundo Apellido del Epleado")]
            public string Apellido2 { get; set; }

            [Display(Name = "Cedula de Empleado")]
            [Required(ErrorMessage = "La Cedula es Requerida")]
            [DataType(DataType.PhoneNumber)]
            [Phone]
            [StringLength(9, MinimumLength = 9, ErrorMessage = "La Cedula Debe ser Solo Numeros")]
            public string Cedula { get; set; }

            [Display(Name = "Telefono de Epleado")]
            [Required(ErrorMessage = "El Telefono del Empleado es Requerido")]
            [DataType(DataType.PhoneNumber)]
            [Phone]
            [StringLength(8, MinimumLength =8, ErrorMessage ="El Telefono Debe ser Solo Numeros")]
            public string Telefono { get; set; }

            [Display(Name = "Correo Electronico de Epleado")]
            [Required(ErrorMessage = "El Correo del Empleado es Requerido")]
            [DataType(DataType.EmailAddress)]
            [EmailAddress(ErrorMessage ="Se Necesita un Correo Valido")]
            public string Correo { get; set; }

            [Required(ErrorMessage = "Se Debe Seleccionar una Opcion")]
            public int IdLicencia { get; set; }

            [Required(ErrorMessage = "Se Debe Seleccionar una Opcion")]
            public string CarnetMS { get; set; }

            [Display(Name = "Fecha de Vencimiento")]
            [Required(ErrorMessage = "La Fecha de Vencimiento es Requerida")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
            public System.DateTime VenceCarnetMS { get; set; }

            [Required(ErrorMessage = "Se Debe Seleccionar una Opcion")]
            public int IdDepartamento { get; set; }

            public string MotivoDeshabilitar { get; set; }
        }
    }
}