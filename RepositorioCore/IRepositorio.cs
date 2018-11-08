using System.Collections.Generic;
using Modelo;

namespace RepositorioCore
{
    public interface IRepositorio
    {
        List<Moneda> ListaMonedas { get; set; }
        List<FactorConversion> ListaFactores { get; set; }
        //List<FactorConversion> ListaFactores { get; set; }
        void ActualizarMoneda(Moneda moneda);
        void BorrarMoneda(Moneda moneda);
        Moneda BuscarMonedaPorId(int idMoneda);
        void CrearMoneda(Moneda moneda);
        List<Moneda> GetMonedas();
        List<Moneda> ObtenerMonedas();
        void CargarMonedas();
        void ActualizarMonedaNombre(string nombre, string identificador);
        

            //Factores de conversion
            void ActualizarFactor(FactorConversion factor);
        FactorConversion BuscaFactorPorIdentificador(string denom);
        FactorConversion BuscarFactorPorId(int IdFactor);
        void IntroducirFactores(FactorConversion factor);
        List<FactorConversion> ObtenerFactores();
        void CargarFactores();
        FactorConversion contrastarFactor(string denom, decimal factor);

        //Conversor

       decimal CovertirMoneda(string idMonedaOrigen,string idMonedaDestino, decimal valor);

        void CrearOperacion(Operacion op);
        List<Operacion> ObtenerOperaciones(string usernameUs);

        //Paises

        void CargarPaises();
        void InsertarPais(Pais pais);
        List<Pais> ObtenerPaises();
        Pais BuscarPaisPorId(int idPais);

        //Carga de datos

        void cargarDatos(string[] options);

    }
}