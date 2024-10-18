
using Examen_Lenguajes1_.API.Constants;
using Examen_Lenguajes1_.API.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Examen_Lenguajes1_.API.Database
{
    public class Examen_Lenguajes1_Seeder
    {

        public static async Task LoadDataAsync(
            Examen_Lenguajes1_Context context,
            ILoggerFactory loggerFactory,
            UserManager<EmployeeEntity> userManager,
            RoleManager<IdentityRole> roleManager
            ) 
        {
            try
            {
                await LoadRolesAndUsersAsync(userManager, roleManager, loggerFactory);

            }
            catch (Exception e) 
            {
                var logger = loggerFactory.CreateLogger<Examen_Lenguajes1_Seeder>();
                logger.LogError(e, "Error inicializando la data del API");
            }
        }

        public static async Task LoadRolesAndUsersAsync(
    UserManager<EmployeeEntity> userManager,
    RoleManager<IdentityRole> roleManager,
    ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<Examen_Lenguajes1_Seeder>();
            try
            {
                if (!await roleManager.Roles.AnyAsync())
                {
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.ADMIN));
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.HR));
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.USER));
                }

                if (!await userManager.Users.AnyAsync())
                {
                    var userAdmin = new EmployeeEntity
                    {
                        Email = "PedroPedro@gmail.com",
                        UserName = "Pedro_Vasquez",
                        Position = "Administrador general",
                        StartDate = DateTime.Now,
                    };
                    var userHR = new EmployeeEntity
                    {
                        Email = "Olgamena@yahoo.com",
                        UserName = "Olga_Jimena",
                        Position = "Secretaria de RH",
                        StartDate = DateTime.Now
                    };


                     await userManager.CreateAsync(userAdmin, "12345678*Ww");
                    await userManager.AddToRoleAsync(userAdmin, RolesConstant.ADMIN);

                     await userManager.CreateAsync(userHR, "87654321*Mm");
                        await userManager.AddToRoleAsync(userHR, RolesConstant.HR);
                       
                    
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error en seeder de usuarios...");
            }
        }

        public static async Task LoadRequestsAsync(ILoggerFactory loggerFactory, Examen_Lenguajes1_Context context) 
        {
            try
            {
                var jsonFilePath = "SeedData/requests.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var requests = JsonConvert.DeserializeObject<List<RequestEntity>>(jsonContent);

                if (!await context.Requests.AnyAsync()) 
                {
                    var user = await context.Users.FirstOrDefaultAsync();

                    for (int i = 0; i < requests.Count; i++) 
                    {
                        requests[i].CreatedBy = user.Id;
                        requests[i].CreatedDate = DateTime.Now;
                        requests[i].UpdatedBy = user.Id;
                        requests[i].UpdatedDate = DateTime.Now;
                    }
                    
                    context.AddRange(requests);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e) 
            {
                var logger = loggerFactory.CreateLogger<Examen_Lenguajes1_Seeder>();
                logger.LogError(e, "Error al ejecutar el Seed de solicitudes");
            }
        }

       }
}
