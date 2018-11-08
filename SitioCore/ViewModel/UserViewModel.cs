using System.Collections.Generic;
using Modelo;


namespace SitioCore.ViewModel
{
    public class UserViewModel
    {
        public string Titulo { get; set; }
        public string Nombre { get; set; }
        public string Username { get; set; }
        public int Pais { get; set; }
        public string OrigenPais { get; set; }
        public string Email { get; set; }
        public string FechaNacimiento { get; set; }
        public string Password { get; set; }
        public List<Operacion> HistorialOperaciones;
        public List<Pais> ListaPaises;
    }
}
