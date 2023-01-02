using Microsoft.EntityFrameworkCore;
using VuelosAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<sistem21_aeromexicoContext>(optionsBuilder =>

    optionsBuilder.UseMySql("server=sistemas19.com;database=sistem21_aeromexico;user=sistem21_aero;password=sistemas19_", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.17-mariadb")),
    ServiceLifetime.Transient
    );
var app = builder.Build();

app.UseRouting();

app.UseFileServer();

app.UseEndpoints(x => x.MapControllers());

app.Run();