using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedesNeuronales.Pages.JuegoSerpiente
{
    public class Obstaculos
    {
        public List<Posicion> Puntos { get; set; } = new List<Posicion>();

        public Obstaculos(int tamañoTablero)
        {
            for (int i = 0; i < tamañoTablero; i++)
            {
                Puntos.Add(new Posicion { X = i, Y = 0 });
                Puntos.Add(new Posicion { X = i, Y = tamañoTablero - 1 }); ; ;
                Puntos.Add(new Posicion { X = 0, Y = i });
                Puntos.Add(new Posicion { X = tamañoTablero - 1, Y = i });
            }
        }

    }
}
