using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConceptosBlazor.Pages.Seccion_8
{
    public class Seccion8Moto : IVehiculo
    {
        public int DamePlazas()
        {
            return 2;
        }

        public int DameRuedas()
        {
            return 2;
        }
    }
}
