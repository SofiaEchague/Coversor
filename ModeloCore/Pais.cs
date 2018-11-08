using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Modelo
{
    public class Pais
    {

        [Key]
        public int Id { get; set; }

        public string NombrePais { get; set; }

        
        // Propiedades de Navegación
        //public List<Usuario> Usuarios { get; set; }


    }
}