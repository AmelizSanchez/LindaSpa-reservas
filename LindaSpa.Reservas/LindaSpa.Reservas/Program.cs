using LindaSpa.Application.Services;
using LindaSpa.Infrastructure.Context;
using LindaSpa.Infrastructure.Core;
using LindaSpa.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<LindaSpaContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped(typeof(BaseRepository<>));
builder.Services.AddScoped<ClienteRepository>();
builder.Services.AddScoped<ReservaRepository>();

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ServicioService>();
builder.Services.AddScoped<EspecialistaService>();
builder.Services.AddScoped<ReservaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();