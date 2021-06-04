using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedesNeuronales.Pages.JuegoSerpiente
{
    public class Manzana
    {
        public Posicion PosicionManzana { get; set; } = new Posicion();
        private int _tamañoTablero;

        public Manzana(int tamañoTablero)
        {
            _tamañoTablero = tamañoTablero;

            PosicionManzana = calculaPosicionLibreAleatoria();
        }

        public Posicion calculaPosicionLibreAleatoria()
        {
            Posicion NuevaPosicionAleatoria = new Posicion();
            Random GeneradorAleatorio = new Random();

            NuevaPosicionAleatoria.X = GeneradorAleatorio.Next(1, _tamañoTablero - 1);
            NuevaPosicionAleatoria.Y = GeneradorAleatorio.Next(1, _tamañoTablero - 1);

            return NuevaPosicionAleatoria;
        }

    }
}
