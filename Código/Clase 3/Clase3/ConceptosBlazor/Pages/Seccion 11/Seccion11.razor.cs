using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConceptosBlazor.Pages.Seccion_11
{
    public partial class Seccion11
    {
        public List<Persona> Personas { get; set; }
        public List<Persona> PersonasFiltradas { get; set; } = new();
 
        protected override void OnInitialized()
        {
            Personas = new()
            {
                new Persona { Nombre = "Nombre 1", Apellido = "Apellido 1", Edad=1},
                new Persona { Nombre = "Nombre 2", Apellido = "Apellido 2", Edad = 2 },
                new Persona { Nombre = "Nombre 3", Apellido = "Apellido 3", Edad = 3 },
                new Persona { Nombre = "Nombre 4", Apellido = "Apellido 4", Edad = 4 },
                new Persona { Nombre = "Nombre 5", Apellido = "Apellido 5", Edad = 5 },
                new Persona { Nombre = "Nombre 6", Apellido = "Apellido 6", Edad = 6 },
                new Persona { Nombre = "Nombre 7", Apellido = "Apellido 7", Edad = 7 }
            };

            
        }
        /// <summary>
        /// Función que calcula el filtro de las personas a mostrar en el página
        /// </summary>
        //public void DamePersonas()
        //{
        //    PersonasFiltradas = new();

        //    foreach (var miPersona in Personas)
        //    {
        //        if (miPersona.Edad < 6 && (miPersona.Nombre == "Nombre 4" || miPersona.Apellido == "Apellido 2"))
        //        {
        //            PersonasFiltradas.Add(miPersona);
        //        }
        //    }
        //}
        public void DamePersonas()
        {
            PersonasFiltradas = Personas                
                .Where(miPersona=>miPersona.Edad<6 && miPersona.Nombre=="Nombre 4")                
                .OrderByDescending(miPersona=>miPersona.Edad).ToList();
        }

    }

    public class Persona
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
    }

}
