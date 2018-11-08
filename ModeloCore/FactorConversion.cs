using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelo
{
    public class FactorConversion
    {
          [Key]
        public int Id { get; set; }
        public decimal Factor { get; set; }

        public int IdMonedaOrigen { get; set; }

        public int IdMonedaDestino { get; set; }
        public string denominacion { get; set; }

        // Propiedades de Navegación
        // [ForeignKey(nameof(IdMonedaOrigen))]
        //public Moneda MonedaOrigen { get; set; }

        //[ForeignKey(nameof(IdMonedaDestino))]
        //public Moneda MonedaDestino { get; set; }


    }
}
