using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCA.Models
{
    public class DepartamentoViewModel:Departamento
    {
        [Display(Name = "Id del departamento")]
        public int IdDepartamento { get; set; }
        [Display(Name = "Nombre del departamento")]
        [Required(ErrorMessage = "El nombre del departamento es requerido")]
        public string Nombre { get; set; }
        public string ValorNuevo()
        {
            return "IdDepartamento:" + IdDepartamento.ToString() + " Nombre:" + Nombre;
        }
    }
}