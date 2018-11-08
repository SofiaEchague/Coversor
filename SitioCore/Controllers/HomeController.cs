using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modelo;
using RepositorioCore;
using SitioCore.Models;
using SitioCore.ViewModel;
using Microsoft.AspNetCore.Identity;
using SitioCore.Data;

namespace SitioCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositorio _repositorio;
        private readonly UserManager<UserData> userManager;

        public HomeController(IRepositorio repositorio,UserManager<UserData> userManager)
        {
            _repositorio = repositorio;
            this.userManager = userManager;
        }


        //Cogemos la información de lodding del usuario
        private Task<UserData> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        

        public IActionResult Listado() {

            var listaFactores = _repositorio.ObtenerFactores();
            var listaMonedas = _repositorio.ObtenerMonedas();

            var homeViewModel = new HomeViewModel
            {
                Titulo = "Listado",
                ListaFactor = listaFactores,
                ListaMonedas = listaMonedas

            };

            return View(homeViewModel);
        }

        public IActionResult DetalleMoneda(int id)
        {
            var moneda = _repositorio.BuscarMonedaPorId(id);
            if (moneda == null)
                return NotFound();
            return View(moneda);
        }


        [HttpPost]
        public IActionResult DetalleMoneda(Moneda moneda)
        {
            return View(moneda);
        }

        public IActionResult Conversor()
        {
           
            var listaMonedas = _repositorio.ObtenerMonedas();
            var conversorViewModel= new ConversorViewModel
            {
                Titulo = "Conversor divisas",
                ListaMonedas = listaMonedas
            };

            return View(conversorViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Resultado(ConversorViewModel conv)
        {
            var userInformation = await GetCurrentUserAsync();
            conv.Valor=_repositorio.CovertirMoneda(conv.idMonedaOrigen,conv.idMonedaDestino,conv.Cantidad);

            if (ModelState.IsValid)
            {
                DateTime hoy = DateTime.Now;
                string fecha_conv = hoy.ToString("dd/MM/yyyy");
                Operacion op = new Operacion
                {
                    Cantidad = conv.Cantidad,
                    IdMonedaOrigen = conv.idMonedaOrigen,
                    IdMonedaDestino = conv.idMonedaDestino,
                    FechaConversion = fecha_conv,
                    UsernameUsuario = userInformation.UserName,
                    Valor = conv.Valor
                };
                _repositorio.CrearOperacion(op);
            }
            return View(conv);
        }
        

        public async Task<IActionResult> Index()
        {

            var userInformation = await GetCurrentUserAsync();

            if (userInformation!=null)
            {
                return RedirectToAction("PerfilUsuario", "Account");
            }
            else{
                string[] options = new string[3];

                if (_repositorio.ObtenerMonedas().Count == 0)
                {
                    options[0] = "monedas";
                }
                if (_repositorio.ObtenerPaises().Count == 0)
                {
                    options[1] = "paises";
                }
                _repositorio.cargarDatos(options);

                return View();
                
            }
            
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
