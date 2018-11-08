using System;
using System.Collections.Generic;
using Modelo;
using ForexQuotes;
using System.Linq;

namespace RepositorioCore

{
    public class RepositorioFalso : IRepositorio {



        public void ActualizarMoneda(Moneda moneda)
        {
        }

        public void BorrarMoneda(Moneda moneda)
        {
        }

        public Moneda BuscarMonedaPorId(int IdMoneda)
        {
            throw new NotImplementedException();
        }

        public void CrearMoneda(Moneda moneda)
        {
        }

        public void CargarMonedas(){}

        public List<Moneda> GetMonedas()
        {
            throw new NotImplementedException();
        }


        public List<FactorConversion> GetFactores()
        {
            throw new NotImplementedException();
        }


        public List<Moneda> ListaMonedas { get; set; }
        /*public List<FactorConversion> ListFactores { get; set; }*/

        public List<Moneda> ObtenerMonedas() {

            throw new NotImplementedException();


        }

        //Factores de conversion

        public void ActualizarFactor(FactorConversion factor)
        {

        }


        public FactorConversion BuscaFactorPorIdentificador(string denom)
        {
            throw new NotImplementedException();
        }

        public FactorConversion BuscarFactorPorId(int IdFactor)
        {
            throw new NotImplementedException();
        }

        public void IntroducirFactores(FactorConversion factor)
        {


        }

        public void ActualizarMonedaNombre(string nombre, string identificador) { }


        public List<FactorConversion> ObtenerFactores() {
            throw new NotImplementedException();
        }

        public List<FactorConversion> ListaFactores { get; set; }

        public void CargarFactores() {

        }

        //Carga de operaciones

        public void CrearOperacion(Operacion op)
        {

        }

        public List<Operacion> ObtenerOperaciones(string usernameUs)
      
        {
            throw new NotImplementedException();
        }
        public decimal CovertirMoneda(string idMonedaOrigen, string idMonedaDestino, decimal valor)
        {
            throw new NotImplementedException();
        }
        //Gestion paises

        public void CargarPaises()
        { }

        public void InsertarPais(Pais pais)
        { }
        public FactorConversion contrastarFactor(string denom, decimal factor) { throw new NotImplementedException(); }

        public List<Pais> ObtenerPaises() { throw new NotImplementedException(); }
            public void cargarDatos(string[] options) { }
        public Pais BuscarPaisPorId(int idPais)
        {
            throw new NotImplementedException();
        }
    }
    


}