using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ConceptosBlazor.Pages.Seccion_5
{
    public class Persona
    {
        [Required]
        public string Nombre { get; set; }
        [Range(0,Int32.MaxValue)]
        public int Edad { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime FechaNacimiento { get; set; }
    }
}
