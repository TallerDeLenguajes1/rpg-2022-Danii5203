using System;
namespace JuegoRPG
{
    class Program
    {
        static void Main(string[] args){
            int seguirJugando = 0;
            string nombrePJ1, nombrePJ2;

            //DATOS DE LA PELEA PARA GUARDAR
            string guardarArchivo = @"C:\juegoRPG\rpg-2022-Danii5203\JuegoRPG\ganadores.csv";
            StreamWriter writeStream = new StreamWriter(guardarArchivo); //abrimos un archivo con la clase StreamWriter para leer o escribir
            writeStream.WriteLine("Nombre, Apodo, Raza, Partidas Ganadas"); //Escribimos en el archivo

            //INSTANCIAMOS A LA CLASE DE LAS FUNCIONES
            funciones funciones = new funciones(); 

            //CREAMOS LA LISTA
            List<Personaje> jugadores = new List<Personaje>(); //inicialozamos la lista de tipo Personaje
            List<Personaje> jugadoresQuePerdieron = new List<Personaje>(); //inicializamos la lista para ir cargando los pj que van perdiendo y luego hacer la carga en el texto

            //PEDIMOS EL NOMBRE DE LOS PJ
            Console.WriteLine("\n******************************************************");
            Console.WriteLine("***** CREAR PERSONAJES *****");
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
                    Console.WriteLine($"\n*********************** RONDA {i+1} ***********************");
                    //ATAQUE Y DEFENSA
                    Console.WriteLine($"===== ATACA {jugadores[0].PjDatos.nombre} ({jugadores[0].PjDatos.apodo}) | DEFIENDE {jugadores[1].PjDatos.nombre} ({jugadores[1].PjDatos.apodo}) =====");
                    Console.WriteLine("=============================================================");
                    funciones.mecanicaDeCombate(jugadores, 0, 1); //COMBATE! 0=PJAtaca, 1=PJDefiende

                    //CONTROL DE SALUD
                    i = funciones.ganadorKO(jugadores, jugadoresQuePerdieron, i); //Esta funcion controla el caso de que alguno gane sin haber terminado las 3 rondas, sino gano ninguno devuelve el valor actual de i, sino sale del for

                    if(jugadores.Count > 1){ //En el caso de que todavia tenga vida el PJ2 entrara al if
                        //ATAQUE Y DEFENSA
                        Console.WriteLine($"\n===== ATACA {jugadores[1].PjDatos.nombre} ({jugadores[1].PjDatos.apodo}) | DEFIENDE {jugadores[0].PjDatos.nombre} ({jugadores[0].PjDatos.apodo}) =====");
                        Console.WriteLine("=============================================================");
                        funciones.mecanicaDeCombate(jugadores, 1, 0); //COMBATE! 1=PJAtaca, 0=PJDefiende
                        
                        //CONTROL DE SALUD
                        i = funciones.ganadorKO(jugadores, jugadoresQuePerdieron, i); //Esta funcion controla el caso de que alguno gane sin haber terminado las 3 rondas, sino gano ninguno devuelve el valor actual de i, sino sale del for
                    }
                    Console.WriteLine("\n");
                }

                //CONTROL DE SALUD
                if(i < 5){ //Controla que no hayan muerto dentro de las 3 rondas
                    if(jugadores[0].PjDatos.salud < jugadores[1].PjDatos.salud){ //Si el PJ1 tiene menos vida que el PJ2
                        if(jugadores[0].PjDatos.salud<0){ jugadores[0].PjDatos.salud = 0;} //Si el PJ1 tiene salud menor a 0 (es decir un numero negativo), le ponemos 0
                        jugadoresQuePerdieron.Add(jugadores[0]); //guardamos al PJ2 que perdio en la lista de los perdedores
                        jugadores.RemoveAt(0); //borramos al PJ2 de la lista jugadores
                    }else{ //Si el PJ2 tiene menos vida que el PJ1
                        if(jugadores[1].PjDatos.salud < jugadores[0].PjDatos.salud){
                            if(jugadores[1].PjDatos.salud<0){ jugadores[1].PjDatos.salud = 0;}
                            jugadoresQuePerdieron.Add(jugadores[1]);
                            jugadores.RemoveAt(1);
                        }
                    }
                }
                
                //MOSTRAR GANADOR Y DAR BONUS
                if(jugadores.Count == 1){ //Si hay 1 solo jugador en la lista, mostramos al ganador
                    Console.WriteLine("\n********************************************************\n=========================================================");
                    Console.WriteLine($"_GANADOR: FELICIDADES {jugadores[0].PjDatos.nombre} ({jugadores[0].PjDatos.apodo})");
                    funciones.aumentoCaracteristicasDelGanador(jugadores); //da bonus de alguna caracteristica aleatoria al ganador
                    jugadores[0].PjDatos.partidasGanadas += 1; //le sumamos una partida ganada para estadisticas
                    Console.WriteLine("********************************************************\n=========================================================\n");
                }else{ //En el caso de que haya mas de 1 jugador en la lista
                    Console.WriteLine("\n********************************************************\n=========================================================");
                    Console.WriteLine($"_EMPATE: {jugadores[0].PjDatos.nombre} ({jugadores[0].PjDatos.apodo}) Salud > {jugadores[0].PjDatos.salud} | {jugadores[1].PjDatos.nombre} ({jugadores[1].PjDatos.apodo}) Salud > {jugadores[1].PjDatos.salud}");
                    Console.WriteLine("********************************************************\n=========================================================\n");
                }


                //CONTROLAMOS QUE NO HAYA EMPATES
                if(jugadores.Count == 1){ //Si no hay empate, preguntamos:
                    //DESEA SEGUIR JUGANDO?
                    Console.WriteLine("Desea seguir jugando? (Si=1 | No=0)");
                    seguirJugando = Convert.ToInt32(Console.ReadLine());
                    if(seguirJugando == 1){ //Si desea seguir jugando, cargamos un PJ nuevo
                        Console.WriteLine("\n_Ingrese el nombre del PJ nuevo: ");
                        string nombrePJNuevo = Console.ReadLine();
                        Personaje PJNuevo = funciones.crearPJ(nombrePJNuevo);
                        jugadores.Add(PJNuevo);
                        funciones.mostrarInformacion(jugadores);
                    }
                }else{ //En el caso de que haya empate los haremos desempatar
                    seguirJugando = 1; //le damos a seguir jugando el valor de 1 por defecto para que vuelva a empezar el combate
                    Console.WriteLine($"\n_Desempate entre: {jugadores[0].PjDatos.nombre} ({jugadores[0].PjDatos.apodo}) Salud > {jugadores[0].PjDatos.salud} | {jugadores[1].PjDatos.nombre} ({jugadores[1].PjDatos.apodo}) Salud > {jugadores[1].PjDatos.salud}");
                }

            } while (seguirJugando == 1); //CONTROL DE PARTIDA

            //DATOS DE LA PELEA PARA GUARDAR
            if(jugadores.Count == 1){ //en el caso de que ya no quieramos jugar mas partidas guardamos el ultimo ganador para los controles que realizaremos despues
                jugadoresQuePerdieron.Add(jugadores[0]);
            }
            funciones.guardarDatosDelGanador(jugadoresQuePerdieron, writeStream); //mandamos la lista con todos los jugadores para hacer el control de los ganadores
            writeStream.Close(); //cerramos el archivo que abrimos al principio

            //LISTAR GANADORES
            Console.WriteLine("\nDesea mostrar la lista de ganadores? (Si=1 | No=0)");
            int verListaDeGanadores = Convert.ToInt32(Console.ReadLine());
            if(verListaDeGanadores == 1){
                funciones.mostrarListaDeGanadores(); //mostramos el txt con los ganadores
            }

            //FIN DEL JUEGO
            Console.WriteLine("\n\n=========================================================");
            Console.WriteLine("============= GAME OVER =============");
            Console.WriteLine("=========================================================\n");
        }
    }
}

