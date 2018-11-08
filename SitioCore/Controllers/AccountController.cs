using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SitioCore.Data;
using RepositorioCore;
using SitioCore.ViewModel;
using System;
using System.Globalization;

namespace SitioCore.Controllers
{
    public class AccountController:Controller
    {

        private readonly UserManager<UserData> userManager;
        private readonly SignInManager<UserData> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IRepositorio _repositorio;


        public AccountController(UserManager<UserData> userManager, SignInManager<UserData> signInManager, RoleManager<IdentityRole> roleManager, IRepositorio repositorio)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this._repositorio = repositorio;
        }

        private Task<UserData> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        public IActionResult AuthRegister()
        {

            var ListaPaises = _repositorio.ObtenerPaises();
            UserViewModel user = new UserViewModel
            {
                Titulo = "Registro",
                ListaPaises=ListaPaises
            };
           
     
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> AuthRegister(UserViewModel user)
        {
            if (ModelState.IsValid)
            {

                DateTime fech_reg = DateTime.ParseExact(user.FechaNacimiento, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                string fecha_b = fech_reg.ToString("dd/MM/yyyy");

                var user_data = new UserData()
                {
                    UserName = user.Username,
                    Nombre = user.Nombre,
                    Email = user.Email,
                    FechaNacimiento = fecha_b,
                    Pais = user.Pais
                };

                var result = await userManager.CreateAsync(
                   user_data, user.Password);

                foreach (var error in result.Errors)
                    ModelState.AddModelError("error", error.Description);

                return RedirectToAction("AuthLogin");
            }
            return View(user);
        }

        public IActionResult AuthLogin()
        {
            UserViewModel user = new UserViewModel
            {
                Titulo = "Login"
            };
            
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> AuthLogin(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result =  await signInManager.PasswordSignInAsync(user.Username, user.Password,true,
                        lockoutOnFailure: false);
                if (result.Succeeded)
                    return RedirectToAction("PerfilUsuario", user);
                else {
                    ViewData["Message"]= "Login incorrecto. Vuelva a intentarlo";

                    return RedirectToAction("AuthLogin");
                } 

                

                    
                
                
            }
                return View(user);
        }

        public async Task<IActionResult> PerfilUsuario()
        {
            
            var userInformation = await GetCurrentUserAsync();

            
                var ListaOperaciones = _repositorio.ObtenerOperaciones(userInformation.UserName);
                var OrigenPaisNombre = _repositorio.BuscarPaisPorId(userInformation.Pais);
                string p = OrigenPaisNombre.NombrePais;

                UserViewModel user = new UserViewModel
                {
                    Titulo = "Perfil de usuario",
                    HistorialOperaciones = ListaOperaciones,
                    Nombre = userInformation.Nombre,
                    FechaNacimiento = userInformation.FechaNacimiento,
                    OrigenPais = p,
                    Email = userInformation.Email,
                    Username=userInformation.UserName

                };

                return View(user);
            

           
        }

        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return View();
        }
    }

}
