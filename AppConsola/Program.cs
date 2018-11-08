using System;
using ForexQuotes;
using Microsoft.EntityFrameworkCore;
using RepositorioCore;
using Modelo;
using Contexto;
using System.Collections.Generic;

namespace AppConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ForexDataClient("xtczkx5SyiottjHUHQavK1U5io8K7F9e");
            List<FactorConversion> lista = new List<FactorConversion>();
            FactorConversion factor = new FactorConversion();
            int count = 0;

            var symbols = client.GetSymbols();
            foreach (var symbol in symbols)
            {

                string from_coin = symbol.Substring(0, 3);
                string to_coin = symbol.Substring(3,3);

                string[] stringArray = new string[1];


                stringArray[0] = symbol;
                var impreso = client.GetQuotes(stringArray);
                var valor = impreso[0].price;

                count = count++;

                factor.Id = count;
                factor.IdMonedaDestino = count++;
                factor.IdMonedaDestino = count++;
                factor.denominacion = symbol;
                factor.Factor = (decimal)valor;

                Console.WriteLine("Conversion:" + factor.denominacion + "/Valor:" + factor.Factor);

                
               
                

            }
            Console.Read();
        }
    }
}
