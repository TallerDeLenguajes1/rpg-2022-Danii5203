using System;
namespace JuegoRPG
{
    class Program
    {
        static void Main(string[] args){
            int seguirJugando = 0;
            string nombrePJ1, nombrePJ2;
            funciones funciones = new funciones();

            List<Personaje> jugadores = new List<Personaje>(); //CREAMOS LA LISTA

            //PEDIMOS EL NOMBRE DE LOS PJ
            Console.WriteLine("\n******************************************************");
            Console.WriteLine("***** Crear Personajes *****");
            Console.WriteLine("******************************************************\n");
            //PJ 1
            Console.WriteLine("_Ingrese el nombre del PJ 1: ");
            nombrePJ1 = Console.ReadLine();
            Personaje PJ1 = funciones.crearPJ(nombrePJ1);
            //PJ 2
            Console.WriteLine("\n_Ingrese el nombre del PJ 2: ");
            nombrePJ2 = Console.ReadLine();
            Personaje PJ2 = funciones.crearPJ(nombrePJ2);

            //AGG LOS JUGADORES A LA LISTA
            jugadores.Add(PJ1);
            jugadores.Add(PJ2);

            //MOSTRAMOS LOS DATOS DE LOS PERSONAJES
            funciones.mostrarInformacion(jugadores);

            do
            {
                //PELEA
                Console.WriteLine("\n******************************************************");
                Console.WriteLine("***** FIGHT!! *****");
                Console.WriteLine("******************************************************\n");

                int rondas = 3, i;
                for (i=0; i<rondas; i++)
                {
                    Console.WriteLine($"\n********************* RONDA {i+1} ******************************************");
                    //ATAQUE Y DEFENSA
                    Console.WriteLine($"===== ATACA {jugadores[0].PjDatos.nombre} ({jugadores[0].PjDatos.apodo}) | DEFIENDE {jugadores[1].PjDatos.nombre} ({jugadores[1].PjDatos.apodo}) =====");
                    Console.WriteLine("============================================");
                    funciones.mecanicaDeCombate(jugadores, 0, 1);

                    //CONTROL DE SALUD
                    i = funciones.ganadorKO(jugadores, i); //Esta funcion controla el caso de que alguno gane sin haber terminado las 3 rondas, sino gano ninguno devuelve el valor actual de i, sino sale del for

                    if(jugadores.Count > 1){ //En el caso de que todavia tenga vida el PJ2 entrara al if
                        //ATAQUE Y DEFENSA
                        Console.WriteLine($"\n===== ATACA {jugadores[1].PjDatos.nombre} ({jugadores[1].PjDatos.apodo}) | DEFIENDE {jugadores[0].PjDatos.nombre} ({jugadores[0].PjDatos.apodo}) =====");
                        Console.WriteLine("============================================");
                        funciones.mecanicaDeCombate(jugadores, 1, 0);
                        
                        //CONTROL DE SALUD
                        i = funciones.ganadorKO(jugadores, i); //Esta funcion controla el caso de que alguno gane sin haber terminado las 3 rondas, sino gano ninguno devuelve el valor actual de i, sino sale del for
                        Console.WriteLine("\n");
                    }
                }

                //CONTROL DE SALUD
                if(i < 5){
                    if(jugadores[0].PjDatos.salud < jugadores[1].PjDatos.salud){
                        jugadores.RemoveAt(0);
                    }else{
                        if(jugadores[1].PjDatos.salud < jugadores[0].PjDatos.salud){
                            jugadores.RemoveAt(1);
                        }
                    }
                }
                
                //MOSTRAR GANADOR Y DAR BONUS
                if(jugadores.Count == 1){
                    Console.WriteLine("\n\n=========================================================");
                    Console.WriteLine($"_GANADOR: FELICIDADES {jugadores[0].PjDatos.nombre} ({jugadores[0].PjDatos.apodo})");
                    funciones.aumentoCaracteristicasDelGanador(jugadores);
                    Console.WriteLine("=========================================================\n");
                }else{
                    Console.WriteLine("\n\n=========================================================");
                    Console.WriteLine($"_EMPATE: {jugadores[0].PjDatos.nombre} ({jugadores[0].PjDatos.apodo}) Salud > {jugadores[0].PjDatos.salud} | {jugadores[1].PjDatos.nombre} ({jugadores[1].PjDatos.apodo}) Salud > {jugadores[1].PjDatos.salud}");
                    Console.WriteLine("=========================================================\n");
                }

                //DESEA SEGUIR JUGANDO?
                Console.WriteLine("Desea seguir jugando? (Si=1 | No=0)");
                seguirJugando = Convert.ToInt32(Console.ReadLine());

                if(seguirJugando == 1){
                    Console.WriteLine("\n_Ingrese el nombre del PJ nuevo: ");
                    string nombrePJNuevo = Console.ReadLine();
                    Personaje PJNuevo = funciones.crearPJ(nombrePJNuevo);
                    jugadores.Add(PJNuevo);
                    funciones.mostrarInformacion(jugadores);
                }

            PAPA while (seguirJugando == 1);

            Console.WriteLine("\n\n=========================================================");
            Console.WriteLine("============= GAME OVER =============");
            Console.WriteLine("=========================================================\n");
        }
    }
}

