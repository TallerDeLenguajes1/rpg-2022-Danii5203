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
        public void mostrarDatosPJ(Personaje _PJ){
            Console.WriteLine("Nombre: "+_PJ.PjDatos.nombre);
            Console.WriteLine("Apodo: "+_PJ.PjDatos.apodo);
            Console.WriteLine("Raza: "+_PJ.PjDatos.raza);
            Console.WriteLine("Fecha de Nacimiento: "+_PJ.PjDatos.fechaDeNac);
            Console.WriteLine("Edad: "+_PJ.PjDatos.edad+" años");
            Console.WriteLine("Salud: "+_PJ.PjDatos.salud);
        }
        public void mostrarCaracteristicasPJ(Personaje _PJ){
            Console.WriteLine("Velocidad: "+_PJ.PjCaracteristicas.velocidad);
            Console.WriteLine("Destreza: "+_PJ.PjCaracteristicas.destreza);
            Console.WriteLine("Fuerza: "+_PJ.PjCaracteristicas.fuerza);
            Console.WriteLine("Nivel: "+_PJ.PjCaracteristicas.nivel);
            Console.WriteLine("Armadura: "+_PJ.PjCaracteristicas.armadura);
        }
        public void mecanicaDeCombate(List<Personaje> _jugadores, int _pjAtaque, int _pjDefensa){
            Random nRand = new Random();

            //VALORES DE ATAQUE
            double PD = _jugadores[_pjAtaque].PjCaracteristicas.destreza * _jugadores[_pjAtaque].PjCaracteristicas.fuerza * _jugadores[_pjAtaque].PjCaracteristicas.nivel; //Poder de Disparo
            double ED = nRand.Next(1, 101); //Efectividad de Ataque (valor aleatoria entre 1 y 100 [porcentual])
            double VA = PD * ED;

            //VALORES DE DEFENSA
            double PDEF = _jugadores[_pjDefensa].PjCaracteristicas.armadura * _jugadores[_pjDefensa].PjCaracteristicas.velocidad; //Poder de Defensa
            double MDP = 50000; //Maximo daño Provocable
            double DP = Math.Round((((VA * ED) - PDEF) / MDP) * 100); //Daño Provocado

            Console.WriteLine($"_Daño provocado por {_jugadores[_pjAtaque].PjDatos.apodo}: {DP} DP.");
            _jugadores[_pjDefensa].PjDatos.salud -= DP;
            Console.WriteLine($"_Salud de {_jugadores[_pjDefensa].PjDatos.apodo}: {_jugadores[_pjDefensa].PjDatos.salud} HP.");
        }

        public int ganadorKO(List<Personaje> _jugadores, int i){
            if(_jugadores[0].PjDatos.salud <= 0){ //si el PJ1 no tiene vida se lo debe sacar de la lista
                _jugadores.RemoveAt(0);
                return 5; //retornamos este valor para que salga del for
            }else{
                if(_jugadores[1].PjDatos.salud <= 0){ //si el PJ2 no tiene vida se lo debe sacar de la lista
                    _jugadores.RemoveAt(1);
                    return 5; //retornamos este valor para que salga del for
                }
            }
            return i; //sino entra en ninguna condicion devolvera el valor actual de i para que siga controlando
        }
        public void aumentoCaracteristicasDelGanador(List<Personaje> _ganador){
            
        }
    }
}

