using System.Security.Principal;
using System.ComponentModel.DataAnnotations;

namespace Modelo
{
    public class Moneda
    
    {
        [Key]
        public int Id { get; set; }
        public string IdentificadorMoneda { get; set; } // EUR
        public string Nombre { get; set; } // EUROS

    }
}