using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace ConceptosBlazor.Pages.Seccion_7
{
    public class Seccion7Base:ComponentBase
    {
        public Timer miTimer = new();
        public int contador;

        protected override void OnInitialized()
        {
            miTimer.Interval = 1000;
            miTimer.Elapsed += MetodoTimer;
            contador = 0;
        }

        protected void Arrancar()
        {
            miTimer.Start();
        }

        protected void Parrar()
        {
            miTimer.Stop();
        }

        private void MetodoTimer(object senter, ElapsedEventArgs e)
        {
            contador++;
            Console.WriteLine($"Valor del contador {contador}");
            InvokeAsync(StateHasChanged);
        }
        public void Dispose()
        {
            miTimer.Dispose();
        }

    }
}
