using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Data
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Hecho { get; set; }
        public int PadreId { get; set; }  
        public Tarea()
        {
            Nombre = "Nuevo elemento";
            Hecho = false;
            PadreId = -1;
        }
       
    }  
}
