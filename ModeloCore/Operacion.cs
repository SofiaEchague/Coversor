using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Modelo
{
    public class Operacion
    {

        [Key]
        public int Id { get; set; }
        public string UsernameUsuario { get; set; }
        public string FechaConversion { get; set; }
        public decimal Valor { get; set; }
        public decimal Cantidad { get; set; }
        public string IdMonedaOrigen { get; set; }
        public string IdMonedaDestino { get; set; }
    }


}
