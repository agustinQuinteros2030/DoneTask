using DoneTask.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoneTask.Data
{
    public static class DbInitializer
    {
        public static async Task SeedRolesAsync(
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            string[] roles = { "Admin", "Colaborador" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
        }

        public static async Task SeedAdminAsync(
            UserManager<Usuario> userManager)
        {
            const string adminEmail = "admin@admin.com";
            const string adminPassword = "Admin123!";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser != null)
                return; 

            var user = new Usuario
            {
                UserName = adminEmail,
                Email = adminEmail,
                Nombre = "Administrador",
                Apellido = "Sistema",
                FechaAlta = DateTime.UtcNow
            };
            var result = await userManager.CreateAsync(user, adminPassword);

            if (!result.Succeeded)
                throw new Exception("No se pudo crear el usuario admin");

            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}