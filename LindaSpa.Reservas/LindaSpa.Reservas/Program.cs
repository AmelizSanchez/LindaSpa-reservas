using LindaSpa.Reservas.Data;
using LindaSpa.Reservas.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<IClienteService, ClienteService>();
builder.Services.AddSingleton<IServicioService, ServicioService>();
builder.Services.AddSingleton<IEspecialistaService, EspecialistaService>();
builder.Services.AddSingleton<IReservaService, ReservaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();