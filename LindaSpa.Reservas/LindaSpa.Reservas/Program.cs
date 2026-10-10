using LindaSpa.Reservas.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IClienteService, ClienteService>();
builder.Services.AddSingleton<IServicioService, ServicioService>();
builder.Services.AddSingleton<IEspecialistaService, EspecialistaService>();
builder.Services.AddSingleton<IReservaService, ReservaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();