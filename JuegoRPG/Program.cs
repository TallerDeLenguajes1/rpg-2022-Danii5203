using System;
namespace JuegoRPG
{
    class Program
    {
        static void Main(string[] args){
            int seguirJugando = 0;
            string nombrePJ1, nombrePJ2;
            int numPartidas = 0; 

            //DATOS DE LA PELEA PARA GUARDAR
            string hrDeLaPeleaInicio = DateTime.Now.ToString("hh:mm:ss");
            string fechaDeLaPeleaInicio = DateTime.Now.ToString("d/M/yyyy");
            string fechaYHrInicio = fechaDeLaPeleaInicio +" | "+hrDeLaPeleaInicio;
            string guardarArchivo = @"C:\juegoRPG\rpg-2022-Danii5203\JuegoRPG\ganadores.txt";
            StreamWriter writeStream = new StreamWriter(guardarArchivo);
            writeStream.WriteLine($"============================== INICIO DEL JUEGO {fechaYHrInicio} ==============================");
            writeStream.WriteLine($"===================================================================================================");

            //LLAMAMOS AL ARCHIVO DE LAS FUNCIONES
            funciones funciones = new funciones();

            //CREAMOS LA LISTA
            List<Personaje> jugadores = new List<Personaje>();
            List<Personaje> jugadoresQuePerdieron = new List<Personaje>();

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
                    i = funciones.ganadorKO(jugadores, jugadoresQuePerdieron, i); //Esta funcion controla el caso de que alguno gane sin haber terminado las 3 rondas, sino gano ninguno devuelve el valor actual de i, sino sale del for

                    if(jugadores.Count > 1){ //En el caso de que todavia tenga vida el PJ2 entrara al if
                        //ATAQUE Y DEFENSA
                        Console.WriteLine($"\n===== ATACA {jugadores[1].PjDatos.nombre} ({jugadores[1].PjDatos.apodo}) | DEFIENDE {jugadores[0].PjDatos.nombre} ({jugadores[0].PjDatos.apodo}) =====");
                        Console.WriteLine("============================================");
                        funciones.mecanicaDeCombate(jugadores, 1, 0);
                        
                        //CONTROL DE SALUD
                        i = funciones.ganadorKO(jugadores, jugadoresQuePerdieron, i); //Esta funcion controla el caso de que alguno gane sin haber terminado las 3 rondas, sino gano ninguno devuelve el valor actual de i, sino sale del for
                        Console.WriteLine("\n");
                    }
                }

                //CONTROL DE SALUD
                if(i < 5){
                    if(jugadores[0].PjDatos.salud < jugadores[1].PjDatos.salud){
                        if(jugadores[0].PjDatos.salud<0){ jugadores[0].PjDatos.salud = 0;}
                        jugadoresQuePerdieron.Add(jugadores[0]);
                        jugadores.RemoveAt(0);
                    }else{
                        if(jugadores[1].PjDatos.salud < jugadores[0].PjDatos.salud){
                            if(jugadores[1].PjDatos.salud<0){ jugadores[1].PjDatos.salud = 0;}
                            jugadoresQuePerdieron.Add(jugadores[1]);
                            jugadores.RemoveAt(1);
                        }
                    }
                }
                
                //MOSTRAR GANADOR Y DAR BONUS
                if(jugadores.Count == 1){
                    Console.WriteLine("\n\n=========================================================");
                    Console.WriteLine($"_GANADOR: FELICIDADES {jugadores[0].PjDatos.nombre} ({jugadores[0].PjDatos.apodo})");
                    funciones.aumentoCaracteristicasDelGanador(jugadores);
                    jugadores[0].PjDatos.partidasGanadas += 1;
                    Console.WriteLine("=========================================================\n");
                }else{
                    Console.WriteLine("\n\n=========================================================");
                    Console.WriteLine($"_EMPATE: {jugadores[0].PjDatos.nombre} ({jugadores[0].PjDatos.apodo}) Salud > {jugadores[0].PjDatos.salud} | {jugadores[1].PjDatos.nombre} ({jugadores[1].PjDatos.apodo}) Salud > {jugadores[1].PjDatos.salud}");
                    Console.WriteLine("=========================================================\n");
                }


                //CONTROLAMOS QUE NO HAYA EMPATES
                if(jugadores.Count == 1){
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
                }else{ //En el caso de que haya empate los haremos desempatar
                    seguirJugando = 1;
                    Console.WriteLine($"\n_Desempate entre: {jugadores[0].PjDatos.nombre} ({jugadores[0].PjDatos.apodo}) Salud > {jugadores[0].PjDatos.salud} | {jugadores[1].PjDatos.nombre} ({jugadores[1].PjDatos.apodo}) Salud > {jugadores[1].PjDatos.salud}");
                }

            } while (seguirJugando == 1);

            //DATOS DE LA PELEA PARA GUARDAR
            if(jugadores.Count == 1){ //en el caso de que ya no quieramos jugar mas partidas guardamos el ultimo ganador para mostrarlo
                jugadoresQuePerdieron.Add(jugadores[0]);
            }
            funciones.guardarDatosDelGanador(jugadoresQuePerdieron, writeStream);
            string hrDeLaPeleaFinal = DateTime.Now.ToString("hh:mm:ss");
            string fechaDeLaPeleaFinal = DateTime.Now.ToString("d/M/yyyy");
            string fechaYHrFinal = fechaDeLaPeleaFinal +" | "+hrDeLaPeleaFinal;
            writeStream.WriteLine($"============================== FIN DEL JUEGO {fechaYHrFinal} ==============================");
            writeStream.WriteLine($"===================================================================================================");
            writeStream.Close();

            //FIN DEL JUEGO
            Console.WriteLine("\n\n=========================================================");
            Console.WriteLine("============= GAME OVER =============");
            Console.WriteLine("=========================================================\n");
        }
    }
}

