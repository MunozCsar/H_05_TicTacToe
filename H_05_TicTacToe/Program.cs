using System;


namespace H_05_TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            int posY = 0, posX = 0, numeroTurno = 1, repetir = 0;
            char simbolo = 'O';
            string jugador = " ";
            char[,] tablero = new char[3, 3];
            bool bucle = true, reset = true, turno = true, pedirPosicion = false, isValid = false; 
            bool ticTacToe = menu();
            jugar(ticTacToe, bucle, reset, turno, tablero, jugador, simbolo, posY, posX, numeroTurno, isValid, repetir, pedirPosicion);
        }

        static bool menu() //Función del menu, permite entrar en el juego o salir,
        {
            int seleccion;
            bool isValid, jugar;
            Console.Write("_______________________________________________________________________________________________________________________");
            Console.WriteLine("\n\n                                                 TicTacToe (Tres en Raya)\n\n                                                        1.-Jugar\n\n                                                        2.-Salir\n\n\n\n\n");
            Console.Write("_______________________________________________________________________________________________________________________");
            Console.WriteLine();
            Console.Write("Elija una opción (1/2): ");
            do
            {
                isValid = Int32.TryParse(Console.ReadLine(), out seleccion);

                if (isValid == true && seleccion < 1 || seleccion > 2)
                {
                    Console.Write("Eliga una opción correcta (1/2): ");
                }
            } while (!isValid || 1 > seleccion || seleccion > 2);

            if(seleccion == 1)
            {
                jugar = true;
            }
            else
            {
                jugar = false;
            }
            return jugar;
        }

        static int PedirPosicion()
        {
            int posicion;
            bool isValid;
            do
            {
                isValid = Int32.TryParse(Console.ReadLine(), out posicion);
                if (posicion < 0 || posicion > 2)
                {
                    Console.WriteLine("Escriba un valor entre 0 y 2");
                }
            }
            while (!isValid || posicion < 0 || posicion > 2);

            return posicion;
        } //Funcion usada para pedirle al jugador la posición del simbolo en el tres en raya.

        static int IAPosicion() //Función que genera un número atleatorio entre 0 y 2 para que el enemigo ponga el símbolo.
        {
            int posicionIA;
            Random rnd = new Random();

            posicionIA = rnd.Next(0, 3);

            return posicionIA;
        }

        static void jugar(bool juego, bool bucle, bool reset, bool turno, char[,] tablero, string jugador, char simbolo, int posY, int posX, int numeroTurno, bool isValid, int repetir, bool pedirPosicion)
        {
            if(juego == true)
            {
                do
                { //Inicio bucle while

                    #region "Tablero"
                    while (reset == true) //Creación del tablero al iniciar o reiniciar el juego
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                tablero[i, j] = '-';
                                Console.Write(" ");
                                Console.Write(tablero[i, j]);
                            }
                            Console.WriteLine();
                        }

                        reset = false;
                    }

                    #endregion

                    #region "Asignar Valores"
                    if (turno == true) //Asignación de los valores de 'simbolo' y 'jugador'
                    {
                        simbolo = 'X';
                        Console.WriteLine("Turno del Jugador, simbolo X");
                        Console.WriteLine("Turno {0}", numeroTurno);

                        do //Pedir los numeros de X e Y al Jugador.
                        {
                            Console.Write("Escriba la posición Y: ");
                            posY = PedirPosicion(); //Se llama a la funcion PedirPosicion() 
                            Console.Write("Escriba la posición X: ");
                            posX = PedirPosicion(); //Se llama a la funcion PedirPosicion() 
                            pedirPosicion = false;
                            if (tablero[posY, posX].Equals('O') || tablero[posY, posX].Equals('X')) //Se comprueba que la posición elegida no esté ocupada. 
                            {
                                Console.WriteLine("Esta posición no es válida");
                                pedirPosicion = true;
                            }
                        }
                        while (pedirPosicion); //Se ejecutará este codigo mientras 'pedirPosicion' sea verdadero.
                    }
                    else if (turno == false) //Turno 2, IA, se asignan numeros atleatorios a la posicion X e Y usando un Random.
                    {
                        simbolo = 'O';
                        Console.WriteLine("Turno de la IA, simbolo O");
                        Console.WriteLine("Turno {0}", numeroTurno);

                        do //Asignar un valor atleatorio entre 0 y 2 a la IA.
                        {
                            posY = IAPosicion(); //Se llama a la funcion IAPosicion() 
                            posX = IAPosicion(); //Se llama a la funcion IAPosicion() 
                            pedirPosicion = false;
                            if (tablero[posY, posX].Equals('O') || tablero[posY, posX].Equals('X')) //Se comprueba que la posición elegida no esté ocupada. 
                            {
                                pedirPosicion = true;
                            }
                        }
                        while (pedirPosicion); //Se ejecutará este codigo mientras 'pedirPosicion' sea verdadero.
                    }

                    

                    numeroTurno++; //Se suma 1 al numero del turno

                        for (int i = 0; i < 3; i++)
                        { //Inicio bucle for i
                            for (int j = 0; j < 3; j++)
                            {// Inicio bucle for j

                                tablero[posY, posX] = simbolo; //Se sustituye el caracter del tablero por el simbolo apropiado.
                                Console.Write(" ");
                                Console.Write(tablero[i, j]); //Se dibuja el tablero en pantalla.
                                turno = !turno; //Se cambia de turno.


                            } //Fin bucle for j
                            Console.WriteLine();
                        } //Fin bucle for i

                    #endregion

                    #region "Comprobación" 
                    if (numeroTurno > 5) //Comprobación del tres en raya.
                    {
                        if (tablero[0, 0] != '-') //Comprobación desde el punto 0Y 0X.
                        {
                            //Se comprueban las lineas 00 01 02, 00 10 20, 00 11 22.
                            if (tablero[0, 0] == tablero[0, 1] && tablero[0, 1] == tablero[0, 2] || tablero[0, 0] == tablero[1, 0] && tablero[1, 0] == tablero[2, 0] || tablero[0, 0] == tablero[1, 1] && tablero[1, 1] == tablero[2, 2])
                            {
                                Console.WriteLine("Victoria");
                                bucle = false;
                            }

                        }
                        if (tablero[1, 1] != '-') //Comprobación desde el punto 1Y 1X.
                        {
                            //Se comprueban las lineas 01 11 21, 10 11 12, 02 11 20.
                            if (tablero[1, 1] == tablero[0, 1] && tablero[1, 1] == tablero[2, 1] || tablero[1, 1] == tablero[1, 0] && tablero[1, 1] == tablero[1, 2] || tablero[1, 1] == tablero[0, 2] && tablero[1, 1] == tablero[2, 0])
                            {
                                Console.WriteLine("Ganar");
                                bucle = false;
                            }
                        }

                        if (tablero[2, 2] != '-') //Comprobación desde el punto 2Y 2X.
                        {
                            //Se comprueban las lineas 22 12 02, 22 21 20.
                            if (tablero[2, 2] == tablero[1, 2] && tablero[1, 2] == tablero[0, 2] || tablero[2, 2] == tablero[2, 1] && tablero[2, 1] == tablero[2, 0])
                            {
                                Console.WriteLine("Ganado");
                                bucle = false;
                            }
                        }
                    }

                    #endregion

                    #region "Empate"
                    if (numeroTurno > 9) //Si se llega a 9 turnos el juego termina, debido a que se ha empatado.
                    {
                        Console.WriteLine("Empate, no gana nadie");
                        Console.WriteLine("Quieres volver a jugar?");
                        do
                        {
                            isValid = Int32.TryParse(Console.ReadLine(), out repetir);

                        } 
                        while (!isValid);

                        if(repetir == 1)
                        {
                            bucle = true;
                            reset = true;
                            numeroTurno = 1;
                            turno = true;
                        }
                        else
                        {
                            bucle = false;

                        }
                        
                    }

                    #endregion


                }
                while (bucle == true);


            }

            else
            {
                Console.Write("_______________________________________________________________________________________________________________________");
                Console.WriteLine("\n\n                                                 Salir del Juego                                               ");
                Console.Write("_______________________________________________________________________________________________________________________");
            }
        }
    }
}
