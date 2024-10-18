using Examen_Lenguajes1_.API;
using Examen_Lenguajes1_.API.Database;
using Examen_Lenguajes1_.API.Database.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = services.GetRequiredService<Examen_Lenguajes1_Context>();
        var userManager = services.GetRequiredService<UserManager<EmployeeEntity>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await Examen_Lenguajes1_Seeder.LoadDataAsync(context, loggerFactory, userManager, roleManager);
    }
    catch (Exception e)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "Error al ejecutar el Seed de datos");
    }

}

app.Run();
