using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedesNeuronales.Pages.JuegoSerpiente
{
    public class Serpiente
    {
        public List<Posicion> CuerpoSerpiente = new List<Posicion>();
        public int LargoSerpiente = 4;

        public enum Edireccion
        {
            arriba = 0,
            abajo = 1,
            derecha = 2,
            izquierda = 3
        }

        public void mover(Edireccion direccion)
        {
            Posicion CabezaSerpiente = new Posicion();
            CabezaSerpiente.X = CuerpoSerpiente[CuerpoSerpiente.Count - 1].X;
            CabezaSerpiente.Y = CuerpoSerpiente[CuerpoSerpiente.Count - 1].Y;


            switch (direccion)
            {
                case Edireccion.arriba:
                    CabezaSerpiente.Y++;
                    break;
                case Edireccion.abajo:
                    CabezaSerpiente.Y--;
                    break;
                case Edireccion.derecha:
                    CabezaSerpiente.X++;
                    break;
                case Edireccion.izquierda:
                    CabezaSerpiente.X--;
                    break;
                default:
                    break;
            }

            CuerpoSerpiente.Add(CabezaSerpiente);
            CuerpoSerpiente.RemoveAt(0);

        }

        public void CrecerSerpiente()
        {
            Posicion ColaSerpiente = new Posicion();
            ColaSerpiente.X = CuerpoSerpiente[0].X;
            ColaSerpiente.Y = CuerpoSerpiente[0].Y;
            CuerpoSerpiente.Insert(0, ColaSerpiente);
        }

        public Serpiente(int PixelsAnchoTablero)
        {
            int CentroTablero = (int)(PixelsAnchoTablero / 2);

            CuerpoSerpiente.Clear();
            //CuerpoSerpiente.Add(new Posicion { X = CentroTablero, Y = CentroTablero });
            for (int i = 0; i < LargoSerpiente; i++)
            {

                CuerpoSerpiente.Insert(0, new Posicion { X = CentroTablero, Y = (CentroTablero - i) });
            }
        }


    }
}
