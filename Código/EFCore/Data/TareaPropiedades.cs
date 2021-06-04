using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Data
{
    public class TareaPropiedades
    {
        public int Id { get; set; }
        public int TareaId { get; set; }
        public string Comentarios { get; set; }
        public DateTime FechaLimite { get; set; }
    }
}
