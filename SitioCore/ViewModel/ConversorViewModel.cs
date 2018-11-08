using System.Collections.Generic;
using Modelo;

namespace SitioCore.ViewModel
{
    public class ConversorViewModel
    {
        public string Titulo { get; set; }
        public List<Moneda> ListaMonedas { get; set; }
        public string idMonedaDestino { get; set; }
        public string idMonedaOrigen { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Valor { get; set; }


    }
}