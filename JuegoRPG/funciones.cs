using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JuegoRPG
{
    class funciones
    {
        public Personaje crearPJ(){ //CREAMOS PJ
            Personaje PJ = new Personaje(); //instanciamos el obj pj
            return PJ;
        }

        //FUNCIONES PARA MOSTRAR
        public void mostrarInformacion(List<Personaje> PJS){ //MOSTRAMOS LA INFO DE UN PJ
            Console.WriteLine("\n******************************************************");
            Console.WriteLine("***** DATOS Y CARACTERISTICAS DE LOS PERSONAJES *****");
            Console.WriteLine("******************************************************\n");
            int idPj = 1;
            foreach (var PJ in PJS) //RECORREMOS UNA LISTA DE OBJETOS Personajes
            {
                Console.WriteLine($"======== PJ{idPj} ({PJ.PjDatos.nombre}) ========");
                Console.WriteLine("=======================================================");
                Console.WriteLine("_DATOS: ");
                this.mostrarDatosPJ(PJ);
                Console.WriteLine("\n_CARACTERISTICAS: ");
                this.mostrarCaracteristicasPJ(PJ);
                Console.WriteLine("\n");
                idPj++;
            }
        }
        public void mostrarDatosPJ(Personaje _PJ){ //MOSTRAMOS LOS DATOS DEL PJ
            Console.WriteLine($"Nombre: {_PJ.PjDatos.nombre} | Apodo: {_PJ.PjDatos.apodo} | Raza: {_PJ.PjDatos.raza} | Fecha de Nac.: {_PJ.PjDatos.fechaDeNac.ToString("D")} | Edad: {_PJ.PjDatos.edad} años | Salud: {_PJ.PjDatos.salud} HP");
        }
        public void mostrarCaracteristicasPJ(Personaje _PJ){ //MOSTRAMOS LAS CARACTERISTICAS DEL PJ
            Console.WriteLine($"Velocidad: {Math.Round(_PJ.PjCaracteristicas.velocidad, 2)} | Destreza: {Math.Round(_PJ.PjCaracteristicas.destreza, 2)} | Fuerza: {Math.Round(_PJ.PjCaracteristicas.fuerza, 2)} | Nivel: {Math.Round(_PJ.PjCaracteristicas.nivel, 2)} | Armadura: {Math.Round(_PJ.PjCaracteristicas.armadura, 2)}");
        }

        //FUNCIOON DE COMBATE
        public void mecanicaDeCombate(List<Personaje> _jugadores, int _pjAtaque, int _pjDefensa){ //MECANICA DE COMBATE
            Random nRand = new Random();

            //VALORES DE ATAQUE
            double PD = ((_jugadores[_pjAtaque].PjCaracteristicas.destreza)/1.5) * _jugadores[_pjAtaque].PjCaracteristicas.fuerza * _jugadores[_pjAtaque].PjCaracteristicas.nivel; //Poder de Disparo
            double ED = nRand.Next(1, 101); //Efectividad de Disparo (valor aleatoria entre 1 y 100 [porcentual])
            double VA = (PD * ED)/100; //Obtenemos el daño real que realizara

            //VALORES DE DEFENSA
            double PDEF = _jugadores[_pjDefensa].PjCaracteristicas.destreza * _jugadores[_pjDefensa].PjCaracteristicas.armadura * _jugadores[_pjDefensa].PjCaracteristicas.nivel; //Poder de Defensa
            double EDEF = nRand.Next(1, 101); //Efectividad de Defensa (valor aleatoria entre 1 y 100 [porcentual])
            double VD = (PDEF * EDEF)/100; //Obtenemos la defensa que realizará
            
            //DAÑO PROVOCADO
            double DP=0;
            if(VD > VA){
                DP = 0; //NO LE SACA NADA DE VIDA
            }else{ //SINO
                DP = Math.Round(VA - VD);
            }


            //MOSTRAMOS LOS DATOS LUEGO DE UNA RONDA
            Console.WriteLine($"_Daño provocado por {_jugadores[_pjAtaque].PjDatos.nombre} ({_jugadores[_pjAtaque].PjDatos.apodo}): {DP} DP.");

            _jugadores[_pjDefensa].PjDatos.salud -= DP;
            if(_jugadores[_pjDefensa].PjDatos.salud < 0){
                _jugadores[_pjDefensa].PjDatos.salud = 0;
            }

            Console.WriteLine($"_Salud de {_jugadores[_pjDefensa].PjDatos.nombre} ({_jugadores[_pjDefensa].PjDatos.apodo}): {_jugadores[_pjDefensa].PjDatos.salud} HP.");
        }

        public int ganadorKO(List<Personaje> _jugadores, List<Personaje> _jugadoresQuePerdieron, int i){
            if(_jugadores[0].PjDatos.salud <= 0){ //si el PJ1 no tiene vida se lo debe sacar de la lista
                _jugadoresQuePerdieron.Add(_jugadores[0]); //guardamos a los jugadores que perdieron
                _jugadores.RemoveAt(0);
                return 5; //retornamos este valor para que salga del for
            }else{
                if(_jugadores[1].PjDatos.salud <= 0){ //si el PJ2 no tiene vida se lo debe sacar de la lista
                    _jugadoresQuePerdieron.Add(_jugadores[1]); //guardamos a los jugadores que perdieron
                    _jugadores.RemoveAt(1); //removemos al pj
                    return 5; //retornamos este valor para que salga del for
                }
            }
            return i; 
        }

        //BONUS
        public void aumentoCaracteristicasDelGanador(List<Personaje> _ganador){ //AUMENTO DE CARACTERISTICAS PARA EL GANADOR
            Random nRand = new Random();
            int caractRandom = nRand.Next(1, 7); //valor entre 1 y 6
            
            int cincoAdiez = nRand.Next(5, 11); //valor entre 5 y 10
            int dosAseis = nRand.Next(2, 7); //valor entre 2 y 6

            switch (caractRandom)
            {
                //En cada caso controlaremos si las caracteristicas no son las maximas
                case 1:
                    if(_ganador[0].PjCaracteristicas.armadura < 10){
                        _ganador[0].PjCaracteristicas.armadura += (_ganador[0].PjCaracteristicas.armadura*cincoAdiez)/100; //suma entre un 5% a 10%
                        Console.WriteLine($"-Se incremento un %{cincoAdiez} de Armadura.");
                    }else{Console.WriteLine("-No se incremento, nivel maximo de Armadura.");}
                    break;
                case 2:
                    if(_ganador[0].PjCaracteristicas.destreza < 5){
                        _ganador[0].PjCaracteristicas.destreza += (_ganador[0].PjCaracteristicas.destreza*dosAseis)/100; //suma entre un 2% y 6%
                        Console.WriteLine($"-Se incremento un %{dosAseis} de Destreza.");
                    }else{Console.WriteLine("-No se incremento, nivel maximo de Destreza.");}
                    break;
                case 3:
                    if(_ganador[0].PjCaracteristicas.fuerza < 10){
                        _ganador[0].PjCaracteristicas.fuerza += (_ganador[0].PjCaracteristicas.fuerza*cincoAdiez)/100; //suma entre un 5% a 10%
                        Console.WriteLine($"-Se incremento un %{cincoAdiez} de Fuerza.");
                    }else{Console.WriteLine("-No se incremento, nivel maximo de Fuerza.");}
                    break;
                case 4:
                    if(_ganador[0].PjCaracteristicas.nivel < 10){
                        _ganador[0].PjCaracteristicas.nivel += 1; //sumamos 1 nivel
                        Console.WriteLine($"-Se incremento 1 Nivel.");
                    }else{Console.WriteLine("-No se incremento el Nivel, nivel maximo.");}
                    break;
                case 5:
                    if(_ganador[0].PjCaracteristicas.velocidad <10){
                        _ganador[0].PjCaracteristicas.velocidad += (_ganador[0].PjCaracteristicas.velocidad*cincoAdiez)/100; //suma entre un 5% a 10%
                        Console.WriteLine($"-Se incremento un %{cincoAdiez} de Velocidad.");
                    }else{Console.WriteLine("-No se incremento, nivel maximo de Velocidad.");}
                    break;
                default:
                    _ganador[0].PjDatos.salud += 10; //sumamos 10 puntos de vida
                    Console.WriteLine($"-Se incremento 10 puntos de Salud.");
                    break;
            }
        }

        //GUARDAR Y MOSTRAR DATOS DE LOS GANADORES
        public void guardarDatosDelGanador(List<Personaje> _ganadores, StreamWriter _writeStream){ //guardamos los datos de los ganadores en un texto
            //la clase StreamWriter sirve para abrir un archivo y escribir sobre el mismo
            foreach (var jugador in _ganadores) //recorremos la lista con los ganadores
            {
                if(jugador.PjDatos.partidasGanadas >= 1){ //en el caso de que los perdedores tengan 1 o mas de 1 partida ganada se los guardara en el texto
                    _writeStream.WriteLine($"{jugador.PjDatos.nombre}, {jugador.PjDatos.apodo}, {jugador.PjDatos.raza}, {jugador.PjDatos.partidasGanadas}"); //escribimos los datos del ganador linea por linea
                }
            }
        }

        public void mostrarListaDeGanadores(){ //MOSTRAMOS LOS GANADORES DEL ARCHIVO DE TEXTO DONDE LOS GUARDAMOS
            string leerArchivo = @"C:\juegoRPG\rpg-2022-Danii5203\JuegoRPG\ganadores.csv";

            Console.WriteLine("\n********* GANADORES *********");
            List<string> ganadores = File.ReadAllLines(leerArchivo).ToList(); //transformamos en un string cada linea del texto y a cada linea la guardamos en una posicion de la lista
            foreach (var ganador in ganadores) //recorremos la lista y lo mostramos
            {
                Console.WriteLine("-"+ganador);
            }
        }

        //JSON
        public void guardarDatosDeJugadoresJson(List<Personaje> _jugadores){
            string pathJson = @"C:\juegoRPG\rpg-2022-Danii5203\JuegoRPG\jugadores.json";
            var options = new JsonSerializerOptions { WriteIndented = true }; //indentar el json
            string jugadoresJson = JsonSerializer.Serialize(_jugadores, options);
            File.WriteAllText(pathJson, jugadoresJson); //Escibimos todo el string en el .json
        }
        public Personaje crearPJAntiguo(){
            string jugadoresJson = File.ReadAllText("jugadores.json"); //Leemos
            var personajesDeserialize = JsonSerializer.Deserialize<List<Personaje>>(jugadoresJson);
            if(personajesDeserialize != null){ //controlamos que no vengan los datos del json vacio
                Random nRand = new Random();
                int nPj = nRand.Next(0, personajesDeserialize.Count); //numero random del 0 al numero de pjs que hayamos guardado
                personajesDeserialize[nPj].PjDatos.salud = 100; //le subimos la salud al maximo
                return personajesDeserialize[nPj]; //retornamos un pj aleatorio antiguo
            }else{
                return null;
            }
        }
    }
}

