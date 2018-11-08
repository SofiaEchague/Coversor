using System.Collections.Generic;
using System.Linq;
using Contexto;
using Modelo;
using ForexQuotes;
using System;
using System.IO;

namespace RepositorioCore
{
    public class Repositorio : IRepositorio
    {
        private readonly MonedaDb _contexto;
        //private readonly ForexDataClient client = new ForexDataClient("xtczkx5SyiottjHUHQavK1U5io8K7F9e"); 
        private readonly ForexDataClient client = new ForexDataClient("K5G9m0Hf5k6ZfbtSV3fbHGAL4f4jHGyY");

        public Repositorio(MonedaDb contexto)
        {
            _contexto = contexto;
            ListaMonedas = new List<Moneda>();
        }

        // U - UPDATE
        public void ActualizarMonedaNombre(string nombre,string identificador)
        {

            //Busca por l areferencia la información 
            // si la encuentra, la acyualiza con un objeto con la información
            // nueva y la anterior.

            var buscarMoneda = BuscarMonedaPorIdentificador(identificador);
            if (buscarMoneda != null)
            {
                buscarMoneda.Nombre =nombre;
                _contexto.SaveChanges();

            }
        }


        public void ActualizarMoneda(Moneda moneda)
        {

            //Busca por l areferencia la información 
            // si la encuentra, la acyualiza con un objeto con la información
            // nueva y la anterior.

            var buscarMoneda = BuscarMonedaPorId(moneda.Id);
            if (buscarMoneda != null)
            {
                buscarMoneda.Nombre = moneda.Nombre;
                buscarMoneda.IdentificadorMoneda = moneda.IdentificadorMoneda;
                _contexto.SaveChanges();

            }
        }

        // D - DELETE

        public void BorrarMoneda(Moneda moneda)
        {
            //Buscamos la referencia de la moneda por el id  
            // Si no la encuentrano borra nada, si la encuentra, la borra de la bbdd

            var buscarMoneda = BuscarMonedaPorId(moneda.Id);
            if (buscarMoneda == null) return;
            _contexto.Monedas.Remove(moneda);
            _contexto.SaveChanges();

        }

       

        public Moneda BuscarMonedaPorId(int IdMoneda)
        {

            //Busca la referencia de la moneda por el id y si la encuentra, devuelve el registro.
            return _contexto.Monedas.FirstOrDefault(
                p => p.Id == IdMoneda);
        }

        public Moneda BuscarMonedaPorIdentificador(string identificador)
        {

            //Busca la referencia de la moneda por el id y si la encuentra, devuelve el registro.
            return _contexto.Monedas.FirstOrDefault(
                p => p.IdentificadorMoneda == identificador);
        }

        // C - CREATE

        public void CrearMoneda(Moneda moneda)
        {

            var buscarMoneda = BuscarMonedaPorId(moneda.Id);
            // Comprueba si ha encontrado la moneda
            if (buscarMoneda != null)
            {
                // Ha encontrado la moneda y la actualizamos
                ActualizarMoneda(moneda);
            }
            else
            {
                // No ha encontrado la moneda y creamos la moneda
                _contexto.Monedas.Add(moneda);
                _contexto.SaveChanges();
            }
        }


        public List<Moneda> GetMonedas()
        {
            return new List<Moneda>();
        }

        public List<Moneda> ListaMonedas { get; set; }

        // R - RETRIEVE
        public List<Moneda> ObtenerMonedas()

        //Devolvemos listadas las monedas disponibles en la bbdd
        {
            return _contexto.Monedas.ToList();
        }

        public List<Pais> ObtenerPaises()

        //Devolvemos listadas las monedas disponibles en la bbdd
        {
            return _contexto.Paises.ToList();
        }
        public void CargarMonedas()
        {
            List<Moneda> lista = new List<Moneda>();
            List<string> monedas = new List<string>();

            var symbols = client.GetSymbols();
            foreach (var symbol in symbols)
            {
                string from_coin = symbol.Substring(0, 3);
                string to_coin = symbol.Substring(3, 3);

                monedas.Add(from_coin);
                monedas.Add(to_coin);

            }

            List<string> distinc_monedas = new List<string>();
            distinc_monedas.AddRange(monedas.Distinct());



            foreach (var money in distinc_monedas)
            {
                Moneda moned = new Moneda();
                moned.IdentificadorMoneda = money;
                moned.Nombre = money;
                lista.Add(moned);

            }

            var coins = ChargeCoinsName.CrearListaMonedas();

                foreach (var item in lista)
                {
                    foreach (var coin in coins)
                    {
                    if (item.IdentificadorMoneda == coin.IdentificadorMoneda)
                    {
                        item.Nombre = coin.Nombre;
                        _contexto.Monedas.Add(item);

                    }
                    else
                    {
                        _contexto.Monedas.Add(item);
                    }
                     
                    }
                    _contexto.SaveChanges();
                }


        }


        //Factores de conversion
        public void ActualizarFactor(FactorConversion factor)
        {

            //Busca por la referencia la información 
            // si la encuentra, la acyualiza con un objeto con la información
            // nueva y la anterior.

            var buscarFactor = BuscarFactorPorId(factor.Id);
            if (buscarFactor != null)
            {

                buscarFactor.IdMonedaDestino = factor.IdMonedaDestino;
                buscarFactor.IdMonedaOrigen = factor.IdMonedaOrigen;
                buscarFactor.denominacion = factor.denominacion;
                buscarFactor.Factor = factor.Factor;


                _contexto.SaveChanges();

            }
        }

        
        public void IntroducirFactores(FactorConversion factor)
        {

            var buscarFactor = BuscaFactorPorIdentificador(factor.denominacion);
            
            // Comprueba si ha encontrado la moneda
            if (buscarFactor != null)
            {

                // Ha encontrado la moneda y la actualizamos
                ActualizarFactor(factor);
               
            }
            else
            {
                // No ha encontrado la moneda y creamos la moneda
                _contexto.FactoresConversion.Add(factor);
                _contexto.SaveChanges();
            }
        }

        public FactorConversion BuscaFactorPorIdentificador(string denom)
        {

            //Busca la referencia de la moneda por el id y si la encuentra, devuelve el registro.
            return _contexto.FactoresConversion.FirstOrDefault(
                p => p.denominacion == denom);
        }

        public FactorConversion contrastarFactor(string denom, decimal factor)
        {
            return _contexto.FactoresConversion.FirstOrDefault(
                p => p.denominacion == denom && p.Factor == factor);
        }

        public FactorConversion BuscarFactorPorId(int IdFactor)
        {

            //Busca la referencia de la moneda por el id y si la encuentra, devuelve el registro.
            return _contexto.FactoresConversion.FirstOrDefault(
                p => p.Id == IdFactor);
        }




        public List<FactorConversion> ObtenerFactores()

        //Devolvemos listadas los factores disponibles en la bbdd
        {
            return _contexto.FactoresConversion.ToList();
        }

        public List<FactorConversion> ListaFactores { get; set; }


        //Forex


        public void CargarFactores()
        {

            List<FactorConversion> lista = new List<FactorConversion>();
            //List<string> factores = new List<string>();
            

            var symbols = client.GetSymbols();
            var factores = client.GetQuotes(symbols);

      

            foreach (var fact in factores)
            {
                               
                FactorConversion fact_help = new FactorConversion();

                string from_coin = fact.symbol.Substring(0, 3);
                string to_coin = fact.symbol.Substring(3, 3);
                Moneda Moneda_to = BuscarMonedaPorIdentificador(to_coin);
                Moneda Moneda_from = BuscarMonedaPorIdentificador(from_coin);

                fact_help.denominacion = fact.symbol;
                fact_help.IdMonedaDestino = Moneda_to.Id;
                fact_help.IdMonedaOrigen = Moneda_from.Id;
                fact_help.Factor = (decimal)fact.price;


               
                IntroducirFactores(fact_help);

            }

        }

 
        public void CargarPaises()
        {
            var paises=ChargeCountries.CrearListaPaises();

            foreach(var pais in paises)
            {
                InsertarPais(pais);
            }
        }

        public Pais BuscarPaisPorId(int idPais)
        {
            return _contexto.Paises.FirstOrDefault(
                p => p.Id == idPais);
        }

        public void CrearOperacion(Operacion Op)
        {
            _contexto.Operacion.Add(Op);
            _contexto.SaveChanges();

        }


        public List<Operacion> ObtenerOperaciones(string usernameUs)

        //Devolvemos las operaciones realizadas por el usuario
        {
            return _contexto.Operacion.Where(o => o.UsernameUsuario == usernameUs).ToList();
        }

        public decimal CovertirMoneda(string idMonedaOrigen, string idMonedaDestino, decimal cantidad)
        {

            //var origen=BuscaFactorPorIdentificador(idMonedaOrigen);
            //var destino = BuscaFactorPorIdentificador(idMonedaDestino);

            var denom = idMonedaOrigen + idMonedaDestino;
            var fact = BuscaFactorPorIdentificador(denom);
            var resultado = cantidad * fact.Factor;

            return resultado;
        }


        public void InsertarPais(Pais pais)
        {

                // No ha encontrado la moneda y creamos la moneda
                _contexto.Paises.Add(pais);
                _contexto.SaveChanges();
        }
               //Cargar datos 

        public void cargarDatos(string[] options)
        {
            bool monedas = options.Contains("monedas");
            bool paises = options.Contains("paises");

            bool[] states = new bool[3];

            if (monedas)
            {
                CargarMonedas();
            }
            if (paises)
            {
                CargarPaises();
            }
           
                //siempre cargamos los factores
                CargarFactores();

        }
    }

    public static class ChargeCountries
    {
        

        public static List<Pais> CrearListaPaises()
        {
            return ProcesarArchivo("Paises.csv");
        }
        private static List<Pais> ProcesarArchivo(string paisesCsv)
        {
            var query =

                File.ReadAllLines(paisesCsv)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .ToPais();

            return query.ToList();
            throw new NotImplementedException();
        }
        public static IEnumerable<Pais> ToPais(this IEnumerable<string> source)
        {
            

            foreach (var line in source)
            {
                var columns = line.Split(';');

                yield return new Pais
                {                    
                    NombrePais = columns[0]
                };
            }
        }
        
    }

    public static class ChargeCoinsName
    {


        public static List<Moneda> CrearListaMonedas()
        {
            return ProcesarArchivo("Coins.csv");
        }
        private static List<Moneda> ProcesarArchivo(string paisesCsv)
        {
            var query =

                File.ReadAllLines(paisesCsv)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .ToCoin();

            return query.ToList();
            throw new NotImplementedException();
        }
        public static IEnumerable<Moneda> ToCoin(this IEnumerable<string> source)
        {


            foreach (var line in source)
            {
                var columns = line.Split(';');

               
                yield return new Moneda {IdentificadorMoneda=columns[0],Nombre=columns[1]};
                
            }
        }

    }
}
