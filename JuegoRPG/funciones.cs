using System;

namespace JuegoRPG
{
    class funciones
    {
        public Personaje crearPJ(string _nombrePJ){
            Caracteristicas caracteristicasPJ = new Caracteristicas();
            Datos datosPJ = new Datos(_nombrePJ);
            Personaje PJ = new Personaje(caracteristicasPJ, datosPJ);
            return PJ;
        }
        public void mostrarInformacion(List<Personaje> PJS){
            Console.WriteLine("\n******************************************************");
            Console.WriteLine("***** Datos y Caracteristicas de los Personajes *****");
            Console.WriteLine("******************************************************\n");
            int idPj = 1;
            foreach (var PJ in PJS)
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
        public void mostrarDatosPJ(Personaje _PJ){
            Console.WriteLine("Nombre: "+_PJ.PjDatos.nombre);
            Console.WriteLine("Apodo: "+_PJ.PjDatos.apodo);
            Console.WriteLine("Raza: "+_PJ.PjDatos.raza);
            Console.WriteLine("Fecha de Nacimiento: "+_PJ.PjDatos.fechaDeNac);
            Console.WriteLine("Edad: "+_PJ.PjDatos.edad+" años");
            Console.WriteLine("Salud: "+_PJ.PjDatos.salud);
        }
        public void mostrarCaracteristicasPJ(Personaje _PJ){
            Console.WriteLine("Velocidad: "+Math.Round(_PJ.PjCaracteristicas.velocidad, 2));
            Console.WriteLine("Destreza: "+Math.Round(_PJ.PjCaracteristicas.destreza, 2));
            Console.WriteLine("Fuerza: "+Math.Round(_PJ.PjCaracteristicas.fuerza, 2));
            Console.WriteLine("Nivel: "+Math.Round(_PJ.PjCaracteristicas.nivel, 2));
            Console.WriteLine("Armadura: "+Math.Round(_PJ.PjCaracteristicas.armadura, 2));
        }
        public void mecanicaDeCombate(List<Personaje> _jugadores, int _pjAtaque, int _pjDefensa){
            Random nRand = new Random();

            //VALORES DE ATAQUE
            double PD = ((_jugadores[_pjAtaque].PjCaracteristicas.destreza)/1.5) * _jugadores[_pjAtaque].PjCaracteristicas.fuerza * _jugadores[_pjAtaque].PjCaracteristicas.nivel; //Poder de Disparo
            double ED = nRand.Next(1, 101); //Efectividad de Ataque (valor aleatoria entre 40 y 100 [porcentual])
            double VA = (PD * ED)/100; //Obtenemos el daño real que realizara

            //VALORES DE DEFENSA
            double PDEF = _jugadores[_pjDefensa].PjCaracteristicas.destreza * _jugadores[_pjDefensa].PjCaracteristicas.armadura * _jugadores[_pjDefensa].PjCaracteristicas.nivel; //Poder de Defensa
            double EDEF = nRand.Next(1, 101); //Efectividad de Defensa (valor aleatoria entre 1 y 50 [porcentual])
            double VD = (PDEF * EDEF)/100; //Obtenemos la defensa que realizará
            
            //DAÑO PROVOCADO
            double DP=0;
            if(VD > VA){
                DP = 0;
            }else{
                DP = Math.Round(VA - VD);
            }

            Console.WriteLine($"_Daño provocado por {_jugadores[_pjAtaque].PjDatos.nombre} ({_jugadores[_pjAtaque].PjDatos.apodo}): {DP} DP.");
            _jugadores[_pjDefensa].PjDatos.salud -= DP;
            Console.WriteLine($"_Salud de {_jugadores[_pjDefensa].PjDatos.nombre} ({_jugadores[_pjDefensa].PjDatos.apodo}): {_jugadores[_pjDefensa].PjDatos.salud} HP.");
        }

        public int ganadorKO(List<Personaje> _jugadores, List<Personaje> _jugadoresQuePerdieron, int i){
            if(_jugadores[0].PjDatos.salud <= 0){ //si el PJ1 no tiene vida se lo debe sacar de la lista
                if(_jugadores[0].PjDatos.salud<0){ _jugadores[0].PjDatos.salud = 0;} //para que no se vea salud en negativo
                _jugadoresQuePerdieron.Add(_jugadores[0]); //guardamos a los jugadores que perdieron
                _jugadores.RemoveAt(0);
                return 5; //retornamos este valor para que salga del for
            }else{
                if(_jugadores[1].PjDatos.salud <= 0){ //si el PJ2 no tiene vida se lo debe sacar de la lista
                    if(_jugadores[1].PjDatos.salud<0){ _jugadores[1].PjDatos.salud = 0;}
                    _jugadoresQuePerdieron.Add(_jugadores[1]); //guardamos a los jugadores que perdieron
                    _jugadores.RemoveAt(1);
                    return 5; //retornamos este valor para que salga del for
                }
            }
            return i; //sino entra en ninguna condicion devolvera el valor actual de i para que siga controlando
        }
        public void aumentoCaracteristicasDelGanador(List<Personaje> _ganador){
            Random nRand = new Random();
            int caractRandom = nRand.Next(1, 7);
            int cincoAdiez = nRand.Next(5, 11);
            int dosAseis = nRand.Next(2, 7);

            switch (caractRandom)
            {
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
                        _ganador[0].PjCaracteristicas.nivel += 1;
                        Console.WriteLine($"-Se incremento 1 Nivel."); //sumamos 1 nivel
                    }else{Console.WriteLine("-No se incremento el Nivel, nivel maximo.");}
                    break;
                case 5:
                    if(_ganador[0].PjCaracteristicas.velocidad <10){
                        _ganador[0].PjCaracteristicas.velocidad += (_ganador[0].PjCaracteristicas.velocidad*cincoAdiez)/100; //suma entre un 5% a 10%
                        Console.WriteLine($"-Se incremento un %{cincoAdiez} de Velocidad.");
                    }else{Console.WriteLine("-No se incremento, nivel maximo de Velocidad.");}
                    break;
                default:
                    _ganador[0].PjDatos.salud += 10;
                    Console.WriteLine($"-Se incremento 10 puntos de Salud.");
                    break;
            }
        }

        public void guardarDatosDelGanador(List<Personaje> _ganadores, StreamWriter _writeStream){
            string[] _participantes = new string[_ganadores.Count];
            int i=0;
            foreach (var participante in _ganadores)
            {
                _participantes[i] = participante.PjDatos.nombre;
                i++;
            }
            string guardarParticipantes = String.Join(", ", _participantes.ToArray());
            _writeStream.WriteLine($"\n***** Participantes: {guardarParticipantes} *****\n\n");
            _writeStream.WriteLine("***** _GANADORES_ *****");
            foreach (var jugador in _ganadores) //mostramos la info de los ganadores
            {
                if(jugador.PjDatos.partidasGanadas >= 1){ //en el caso de que los perdedores anteriores tengan mas de 1 partida ganada se los guardara en el texto
                    string fechaActual = DateTime.Now.ToString("d/M/yyyy");
                    string horaActual = DateTime.Now.ToString("hh:mm:ss");
                        
                    _writeStream.WriteLine($"_Ganador: {jugador.PjDatos.nombre} ({jugador.PjDatos.apodo})");
                    _writeStream.WriteLine($"_Raza: {jugador.PjDatos.raza}");
                    _writeStream.WriteLine($"_Salud: {jugador.PjDatos.salud}");
                    _writeStream.WriteLine($"_Partidas ganadas: {jugador.PjDatos.partidasGanadas}");
                    _writeStream.WriteLine("");
                }
            }
        }
    }
}

