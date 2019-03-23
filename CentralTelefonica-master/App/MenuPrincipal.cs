using System;
using System.Collections.Generic;
using static System.Console;
using CentralTelefonica.Entidades;
using CentralTelefonica.Util;
namespace CentralTelefonica.App

{
    public class MenuPrincipal
    {

        //constantes
        // a las constantes no se les puede cambiar su valor
        private const float precioUnoDepartamental = 0.65f;
        private const float precioDosDepartamental = 0.85f;

        private const float precioTresDepartamental = 0.98f;
        private const float precioLocal = 1.25f;
        //si la lista no se evalua o no se hace una validacion se puede hacer de esta forma:
        public List<Llamada> ListaDeLlamadas { get; set; } //no esta instanciado, no tiene direccion de memoria 
        public MenuPrincipal()
        {
            this.ListaDeLlamadas = new List<Llamada>();
        }
        /*
            public List<Llamada> ListaDeLlamadas = new List<Llamada>(); metodo 1
         */
        public void MostrarMenu()

        {
            var opcion = 100;

            do
            {
                try
                {
                    WriteLine("1. Registrar llamada local");
                    WriteLine("2. Registrar llamada departamental");
                    WriteLine("3. Costo total de las llamadas locales");
                    WriteLine("4. Costo total de las llamadas departamentales");
                    WriteLine("5. Costo total de las llamadas");
                    WriteLine("6. Mostrar Resumen");
                    WriteLine("0. Salir");
                    WriteLine("ingrese su opcion ==>");
                    string valor = Console.ReadLine();
                    //unboxing de un string a un entero
                    //clase convert tiene un metodo estatico que no es necesario instanciarlo. que se convierta en un entero y lo guarde en la variable
                    if (EsNumero(valor) == true)
                    {
                        opcion = Convert.ToInt16(valor);
                    }
                    if (opcion == 1)
                    {
                        RegistrarLlamada(opcion);
                    }
                    else if (opcion == 2)
                    {
                        RegistrarLlamada(opcion);
                    }
                    else if (opcion == 3)
                    {
                        MostrarCostoLlamadasLocales();
                    }
                    else if (opcion == 4)
                    {
                        MostrarCostoDeLlamdasDepto();
                    }
                    else if (opcion == 6)
                    {
                        MostrarDetalle();
                    }
                }

                catch (OpcionMenuException e)
                {
                    WriteLine(e.Message);
                }
            }
            while (opcion != 0);
        }
        //crear metodo para corregir la conversion y que no se salga del programa
        public Boolean EsNumero(string valor)
        {
            Boolean resultado = false;
            try
            {
                int numero = Convert.ToInt16(valor);
                resultado = true;
            }
            catch (Exception e)
            {
                throw new OpcionMenuException();
            }
            return resultado;
        }
        public void RegistrarLlamada(int opcion)
        {   /*  Llamada llamada = new Llamada(); no se puede debido a que es abstracta */
            string numeroOrigen = "";
            string numeroDestino = "";
            string duracion = "";
            Llamada llamada = null;
            WriteLine("Ingrese el numero de origen");
            numeroOrigen = ReadLine();
            WriteLine("Ingrese el numero de Destino");
            numeroDestino = ReadLine();
            WriteLine("Duracion de la llamada");
            duracion = ReadLine();
            if (opcion == 1)
            {
                llamada = new LlamadaLocal(numeroOrigen, numeroDestino, Convert.ToDouble(duracion));
                ((LlamadaLocal)llamada).Precio = precioLocal;
            }
            else if (opcion == 2)
            {
                llamada = new LlamadaDepartamental(numeroOrigen, numeroDestino, Convert.ToDouble(duracion));
                ((LlamadaDepartamental)llamada).PrecioUno = precioUnoDepartamental;
                ((LlamadaDepartamental)llamada).PrecioDos = precioDosDepartamental;
                ((LlamadaDepartamental)llamada).PrecioTres = precioTresDepartamental;
                ((LlamadaDepartamental)llamada).Franja = calcularFranja(DateTime.Now);
            }
            else
            {
                WriteLine("Tipo de llamada no registrada");
            }
            //se agrego el elemento en la lista
            this.ListaDeLlamadas.Add(llamada);
        }
        public void MostrarDetalleWhile()
        { //count =  cantidad de elementos que tiene la coleccion.
            int i = 0;
            while (this.ListaDeLlamadas.Count > i)
            {
                WriteLine(this.ListaDeLlamadas[i]);
                i++;
                //i+=1; o i+=2;
            }
        }
        /* public void MostrarDetalleDoWhile()
         {
             int i = 0;
             do
             {
                 //i++ no se imprimiria la variable 0 ya que se aumenta primero
                 WriteLine(this.ListaDeLlamadas[i]);
                 i++;
             }
             while (this.ListaDeLlamadas.Count > i);
         }
         public void MostrarDetalleFor()
         {
             for (int i = 0; i < this.ListaDeLlamadas.Count; i++)
             {
                 WriteLine(this.ListaDeLlamadas[i]);
             }
         }  */
        public void MostrarDetalle()
        { //usarlo en colecciones
            foreach (var llamada in this.ListaDeLlamadas)
            {
                WriteLine(llamada);
            }
        }
        public void MostrarCostoLlamadasLocales()
        {
            double tiempoLlamada = 0;
            double costoTotal = 0.0;
            foreach (var elemento in ListaDeLlamadas)
            {
                //type of verifica la instancia del objeto
                if (elemento.GetType() == typeof(LlamadaLocal))
                {
                    tiempoLlamada += elemento.Duracion;
                    costoTotal += /*costoTotal + */ elemento.CalcularPrecio();
                }
            }
            WriteLine($"Costo minuto: {precioLocal}");
            WriteLine($"Tiempo total de llamadas: {tiempoLlamada}");
            WriteLine($"Costo total: {costoTotal}");
        }
        public void MostrarCostoDeLlamdasDepto()
        {
            double tiempoLlamadaFranjaUno = 0.0;
            double tiempoLlamadaFranjaDos = 0.0;
            double tiempoLlamadaFranjaTres = 0.0;
            double costoTotalFranjaUno = 0.0;
            double costoTotalFranjaDos = 0.0;
            double costoTotalFranjaTres = 0.0;
            foreach (var elemento in ListaDeLlamadas)
            {
                if (elemento.GetType() == typeof(LlamadaDepartamental))
                {   //se convierte el objeto elemento en tipo departamental
                    //haciendo un unboxing por ello lleva .franja debido a que no esta en llamada solo en llamadaDepartamental
                    switch (((LlamadaDepartamental)elemento).Franja)
                    {
                        case 0:
                            tiempoLlamadaFranjaUno += elemento.Duracion;
                            costoTotalFranjaUno += elemento.CalcularPrecio();
                            break;

                        case 1:
                            tiempoLlamadaFranjaDos += elemento.Duracion;
                            costoTotalFranjaDos += elemento.CalcularPrecio();
                            break;

                        case 2:
                            tiempoLlamadaFranjaTres += elemento.Duracion;
                            costoTotalFranjaTres += elemento.CalcularPrecio();
                            break;
                    }
                }
                WriteLine("Franja: 1");
                WriteLine($"Costo minuto: {precioUnoDepartamental}");
                WriteLine($"Tiempo total de llamadas: {tiempoLlamadaFranjaUno}");
                WriteLine($"Costo total: {costoTotalFranjaUno}");

                WriteLine("Franja: 2");
                WriteLine($"Costo minuto: {precioDosDepartamental}");
                WriteLine($"Tiempo total de llamadas: {tiempoLlamadaFranjaDos}");
                WriteLine($"Costo total: {costoTotalFranjaDos}");

                WriteLine("Franja: 3");
                WriteLine($"Costo minuto: {precioTresDepartamental}");
                WriteLine($"Tiempo total de llamadas: {tiempoLlamadaFranjaTres}");
                WriteLine($"Costo total: {costoTotalFranjaTres}");
            }
        }
          //tarea
        public int calcularFranja(DateTime fecha)
        {   //0, 1 o 2 que devuleva las franjas dependiendo de la fecha del sistema 
            fecha = DateTime.Now;
            int franja = 0;
            int dia = Convert.ToInt32(fecha.DayOfWeek);
            int hora = fecha.Hour;
    
            if (dia >= 1 && dia <= 5) // verifica los dias de lunes a viernes 
            {
                if (hora >= 6 && hora < 22) // si la hora es mayor e igual a 6 am y menor a 22  la franja es 0 
                {
                    franja = 0;
                }
                else if (hora < 6 || hora >= 22) // si la franja es menor a 6 o mayor o igual que 22 la franja es 1 
                {
                    franja = 1;

                }
            }
            else if (dia == 0 || dia == 6) // verifica los dias domingo y sabado 
            {
                if (hora >= 22 || hora <= 6) // si es mayor o igual a 22 o menor o igual a 6 es franja 2
                {
                    franja = 2;

                }
                else if (hora > 6 && hora < 22) // // si es mayor a 6 o menor 22 es franja 2
                {
                    franja = 2;

                }

            }
            return franja;// que retorne el valor de la franja dependiendo de la hora del sistema 
        }
    }
}