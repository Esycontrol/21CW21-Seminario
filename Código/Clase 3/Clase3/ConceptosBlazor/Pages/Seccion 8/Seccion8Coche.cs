using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConceptosBlazor.Pages.Seccion_8
{
    public class Seccion8Coche : IVehiculo
    {
        public int DamePlazas()
        {
            return 5;
        }

        public int DameRuedas()
        {
            return 4;
        }
    }
}
