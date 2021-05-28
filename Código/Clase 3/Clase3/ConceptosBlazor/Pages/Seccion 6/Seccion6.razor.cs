using System;
using System.Timers;

namespace ConceptosBlazor.Pages.Seccion_6
{
    public partial class Seccion6 : IDisposable
    {
        public Timer miTimer = new();
        public int contador;

        protected override void OnInitialized()
        {
            miTimer.Interval = 1000;
            miTimer.Elapsed += MetodoTimer;
            contador = 0;
        }

        private void Arrancar()
        {
            miTimer.Start();
        }

        private void Parrar()
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
