using System;
namespace JuegoRPG
{
    class Program
    {
        static void Main(string[] args){
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
            Console.WriteLine("\n******************************************************");
            Console.WriteLine("***** Datos y Caracteristicas de los Personajes *****");
            Console.WriteLine("******************************************************\n");
            int idPj = 1;
            foreach (var PJ in jugadores)
            {
                Console.WriteLine($"======== PJ{idPj} ({PJ.PjDatos.nombre}) ========");
                Console.WriteLine("=======================================================");
                Console.WriteLine("_DATOS: ");
                funciones.mostrarDatosPJ(PJ);
                Console.WriteLine("\n_CARACTERISTICAS: ");
                funciones.mostrarCaracteristicasPJ(PJ);
                Console.WriteLine("\n");
                idPj++;
            }

            //PELEA
            Console.WriteLine("\n******************************************************");
            Console.WriteLine("***** FIGHT!! *****");
            Console.WriteLine("******************************************************\n");

            int rondas = 3, i;
            for (i=0; i<rondas; i++)
            {
                Console.WriteLine($"\n********************* RONDA {i+1} *********************");
                //ATAQUE Y DEFENSA
                Console.WriteLine($"===== ATACA {jugadores[0].PjDatos.apodo} | DEFIENDE {jugadores[1].PjDatos.apodo} =====");
                Console.WriteLine("============================================");
                funciones.mecanicaDeCombate(jugadores, 0, 1);

                Console.WriteLine($"\n===== ATACA {jugadores[1].PjDatos.apodo} | DEFIENDE {jugadores[0].PjDatos.apodo} =====");
                Console.WriteLine("============================================");
                funciones.mecanicaDeCombate(jugadores, 1, 0);
                
                //CONTROL DE SALUD
                i = funciones.ganadorKO(jugadores, i); //Esta funcion controla el caso de que alguno gane sin haber terminado las 3 rondas, sino gano ninguno devuelve el valor actual de i, sino sale del for
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
            
            if(jugadores.Count == 1){
                Console.WriteLine("\n=========================================================");
                Console.WriteLine($"_GANADOR: FELICIDADES {jugadores[0].PjDatos.nombre} ({jugadores[0].PjDatos.apodo})");
                Console.WriteLine("=========================================================\n");
            }else{
                Console.WriteLine("\n=========================================================");
                Console.WriteLine($"_EMPATE: {jugadores[0].PjDatos.nombre} ({jugadores[0].PjDatos.apodo}) Salud > {jugadores[0].PjDatos.salud} | {jugadores[1].PjDatos.nombre} ({jugadores[1].PjDatos.apodo}) Salud > {jugadores[1].PjDatos.salud}");
                Console.WriteLine("=========================================================\n");
            }
        }
    }
}

