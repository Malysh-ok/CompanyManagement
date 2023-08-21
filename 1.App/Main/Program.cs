using App.AppInitializer;
using DataAccess.DataAccessManagement;
using DataAccess.DbConfigureManagement;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Получаем Кофигуратор БД
// var dbConfigurator = DbConfigurator.CreateDbConfigurator(
//     builder.Configuration,
//     builder.Environment.ContentRootPath
// );
var dbConfigurator = DbConfigurator.CreateDbConfiguratorWithAppData(true);
dbConfigurator.MigrationsAssemblyName = "Migrator";
// builder.Services.AddSingleton(dbConfigurator);   // внедряем конфигуратор

// Внедряем контекст БД
// builder.Services.AddDbContext<AppDbContext>(options  =>
//     options.UseSqlite(dbConfigurator.ProcessedConnectionString)
// );
builder.Services.AddDbContext<AppDbContext>(_  =>
    dbConfigurator.DbContextOptionsBuilderDelegate<AppDbContext>()
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var serviceScope = app.Services.CreateScope();

// Инициализируем приложение
var result = Initializer.Init(serviceScope.ServiceProvider);

app.Run();