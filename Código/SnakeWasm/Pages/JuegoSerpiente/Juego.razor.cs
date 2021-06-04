using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;
using System.Timers;



namespace RedesNeuronales.Pages.JuegoSerpiente
{
       public class JuegoBase : ComponentBase,IDisposable
    {
        #region Campos, declaración de objetos y propiedades del Juego

        //Objetos del juego
        public ETipoPunto[,] Tablero;
        public Obstaculos ObstaculosPartida;
        public Manzana ManzanaPartida;
        public Serpiente SerpientePartida;

        //Configuración visualización tablero
        public int PixelsAnchoTablero { get; set; } = 30;
        public string TamañoPixelTablero { get; set; } = "15px";


        //Campos protegidos de estado juego
        protected Serpiente.Edireccion MovimientoActual;
        protected int Velocidad = 1;
        protected int VelocidaddMaxima = 20;
        protected int ContadorVelocidad = 0;
        public EestadosJuego estadoJuego = EestadosJuego.EsperandoIniciarPartida;

        //Campos privados
        protected Timer Reloj;
        protected ElementReference divTeclado;
        private bool pintandoEscena = false;

        //Campo Cerebro        
        [Parameter] public bool IniciaPartida { get; set; }
        public int Puntuación { get; set; }

        //Enumerados del juego
        public enum ETipoPunto
        {
            Libre = 0,
            Manzana = 1,
            Serpiente = 2,
            Obstaculo = 3
        }

        public enum EestadosJuego
        {
            EsperandoIniciarPartida = 0,
            IniciandoPartida = 1,
            Jugando = 2,
            GameOver = 3
        }
        #endregion

        #region Ciclo de vida componente Blazor

        protected override void OnInitialized()
        {
            ConfiguraTemporizador();
        }

        protected override void OnParametersSet()
        { 
            configuraJuego();        
           
        }
        protected override void OnAfterRender(bool firstRender)
        {
            divTeclado.FocusAsync();
        }        

        void IDisposable.Dispose()
        {
            Reloj.Dispose();
        }


        #endregion

        #region Interface publica de control del juego

        public void IniciarPartida()
        {
            configuraJuego();
            estadoJuego = EestadosJuego.IniciandoPartida;
        }

        public void MoverArriba()
        {
            MovimientoActual = (Serpiente.Edireccion.arriba);
        }
        public void MoverAbajo()
        {
            MovimientoActual = Serpiente.Edireccion.abajo;

        }
        public void MoverDerecha()
        {
            MovimientoActual = Serpiente.Edireccion.derecha;
        }
        public void MoverIzquierda()
        {
            MovimientoActual = Serpiente.Edireccion.izquierda;
        }

        #endregion

        #region Métodos privados de funcionamiento del Juego

        private void DibujaEscena()
        {
            //Vacío el tablero
            for (int i = 0; i < PixelsAnchoTablero; i++)
            {
                for (int j = 0; j < PixelsAnchoTablero; j++)
                {
                    Tablero[i, j] = ETipoPunto.Libre;
                }
            }

            //Pinto los obstaculos
            foreach (var punto in ObstaculosPartida.Puntos)
            {
                Tablero[punto.X, punto.Y] = ETipoPunto.Obstaculo;
            }

            //Pinto la manzana
            Tablero[ManzanaPartida.PosicionManzana.X, ManzanaPartida.PosicionManzana.Y] = ETipoPunto.Manzana;

            //Pinto la serpiente sin la cabeza
            for (int i = 0; i < SerpientePartida.CuerpoSerpiente.Count - 1; i++)
            {
                Tablero[SerpientePartida.CuerpoSerpiente[i].X, SerpientePartida.CuerpoSerpiente[i].Y] = ETipoPunto.Serpiente;
            }

            //Analizo si la cabeza colisiona con algún objeto de los que hay en el mapa, y según el objeto realizo las acciones pertinentes
            DetectarColision();

            //Pinto la cabeza de la serpiente tras haber analizado colisiones de la posición de la cabeza de la serpiente
            Tablero[SerpientePartida.CuerpoSerpiente[SerpientePartida.CuerpoSerpiente.Count - 1].X, SerpientePartida.CuerpoSerpiente[SerpientePartida.CuerpoSerpiente.Count - 1].Y] = ETipoPunto.Serpiente;

        }

        private void DetectarColision()
        {
            Posicion CabezaSerpiente = new Posicion();
            CabezaSerpiente = SerpientePartida.CuerpoSerpiente[SerpientePartida.CuerpoSerpiente.Count - 1];

            if (CabezaSerpiente is not null)
            {
                //Si coliosiono con un obstaculo,se acaba el juego
                if (Tablero[CabezaSerpiente.X, CabezaSerpiente.Y] == ETipoPunto.Obstaculo)
                {
                    Puntuación += 0;
                    estadoJuego = EestadosJuego.GameOver;
                }

                //Si colisiono con una parte del cuerpo de la serpiente se acaba el juego
                if (Tablero[CabezaSerpiente.X, CabezaSerpiente.Y] == ETipoPunto.Serpiente)
                {
                    Puntuación += 0;
                    estadoJuego = EestadosJuego.GameOver;
                }

                //Si colisiono con una manzana: aumento la puntuación, crezce el cuerpoo de la serpiente y se genera una nueva posición para la manzana
                if (Tablero[CabezaSerpiente.X, CabezaSerpiente.Y] == ETipoPunto.Manzana)
                {
                    Puntuación += 10;
                    ManzanaComida();
                }
                //Si se mueve por un espacio libre se suma 1 punto por tick
                if (Tablero[CabezaSerpiente.X, CabezaSerpiente.Y] == ETipoPunto.Libre)
                {
                    Puntuación += 1;
                }
            }
        }

        private void ManzanaComida()
        {
            SerpientePartida.CrecerSerpiente();
            GenerarNuevaPosicionManzana();
        }

        private void configuraJuego()
        {
            Tablero = new ETipoPunto[PixelsAnchoTablero + 1, PixelsAnchoTablero + 1];
            SerpientePartida = new Serpiente(PixelsAnchoTablero);
            ObstaculosPartida = new Obstaculos(PixelsAnchoTablero);
            ManzanaPartida = new Manzana(PixelsAnchoTablero);

            MovimientoActual = Serpiente.Edireccion.arriba;
            pintandoEscena = false;
            Puntuación = 0;
            Velocidad = 15;

        }

        private void ConfiguraTemporizador()
        {           
            Reloj = new Timer();
            Reloj.Interval = 10;
            Reloj.Elapsed += TicDeReloj; //Facilitamos el manejador del evento de reloj
            Reloj.Start();
        }

        private void GenerarNuevaPosicionManzana()
        {

            bool encontradaPosicionLibre = false;

            while (!encontradaPosicionLibre)
            {

                Posicion NuevaPosicionAleatoria = ManzanaPartida.calculaPosicionLibreAleatoria();

                if (Tablero[NuevaPosicionAleatoria.X, NuevaPosicionAleatoria.Y] == ETipoPunto.Libre)
                {
                    encontradaPosicionLibre = true;
                    ManzanaPartida.PosicionManzana = NuevaPosicionAleatoria;
                }

            }
        }

        private double[] EstadoTableroSerializado()
        {
            double[] tableroSerializado = new double[PixelsAnchoTablero * PixelsAnchoTablero+1];
            int indice = 0;
            for (int i = 0; i < PixelsAnchoTablero; i++)
            {
                for (int j = 0; j < PixelsAnchoTablero; j++)
                {
                    switch (Tablero[i, j])
                    {
                        case ETipoPunto.Libre:
                            tableroSerializado[indice] = 0.75;
                            break;
                        case ETipoPunto.Manzana:
                            tableroSerializado[indice] = 1;
                            break;
                        case ETipoPunto.Serpiente:
                            tableroSerializado[indice] = 0.5;
                            break;
                        case ETipoPunto.Obstaculo:
                            tableroSerializado[indice] = 0.25;
                            break;
                        default:
                            break;
                    }
                    indice++;
                }

            }
            tableroSerializado[indice] = Puntuación;
            return tableroSerializado;
        }
              

        private void TicDeReloj(object sender, ElapsedEventArgs e)
        {
            if (!pintandoEscena)
            {
                pintandoEscena = true;
                if (ContadorVelocidad > VelocidaddMaxima - Velocidad)
                {
                    switch (estadoJuego)
                    {
                        case EestadosJuego.EsperandoIniciarPartida:
                            if (IniciaPartida)
                            {
                                IniciaPartida = false;
                                IniciarPartida();
                            }
                            break;
                        case EestadosJuego.IniciandoPartida:
                            //TODO colocar aquí un temporizador que indique que va a comenzar la partida
                            estadoJuego = EestadosJuego.Jugando;
                            break;
                        case EestadosJuego.Jugando:
                            SerpientePartida.mover(MovimientoActual);
                            break;
                        case EestadosJuego.GameOver:
                            //Mostrar mensaje de GameOver un tiempo y volver a la pantalla de Inicio
                            estadoJuego = EestadosJuego.EsperandoIniciarPartida;
                            break;
                        default:
                            break;
                    }
                        ContadorVelocidad = 0;
                    
                    _ = InvokeAsync(() =>
                    {
                        DibujaEscena();
                        StateHasChanged();
                    });
                }               
                pintandoEscena = false;
            }
            ContadorVelocidad++;
     
        }

        protected void TeclaPulsada(KeyboardEventArgs e)
        {
            switch (e.Key)
            {
                case "ArrowDown":
                    MoverAbajo();
                    break;
                case "ArrowUp":
                    MoverArriba();
                    break;
                case "ArrowLeft":
                    MoverIzquierda();
                    break;
                case "ArrowRight":
                    MoverDerecha();
                    break;
                default:
                    break;
            }
        }

        #endregion

    }
}
