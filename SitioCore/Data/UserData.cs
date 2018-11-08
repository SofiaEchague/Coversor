using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace SitioCore.Data
{
    public class UserData:IdentityUser
    {
        public string Nombre { get; set; }
        public string FechaNacimiento { get; set; }
        public int Pais { get; set; }


    }
}
