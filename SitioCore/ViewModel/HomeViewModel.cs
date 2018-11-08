using System.Collections.Generic;
using Modelo;

namespace SitioCore.ViewModel
{
    public class HomeViewModel
    {
        public List<Moneda> ListaMonedas { get; set; }

        public string Titulo { get; set; }

        public string ImagenMoneda { get; set; }

        public string ID { get; set; }

        public List<FactorConversion> ListaFactor { get; set; }

        public string TituloDenominacion { get; set; }

        public string Valor { get; set; }
    
}
}