using App.Infrastructure.DbConfigureManagement;
using DataAccess.DataAccessManagement.DbContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Получаем Кофигуратор БД
var dbConfigurator = new DbConfigurator(
    builder.Configuration,
    builder.Environment.ContentRootPath
);

// Внедряем контекст БД
builder.Services.AddDbContext<AppDbContext>(options  =>
    options.UseSqlite(dbConfigurator.ProcessedConnectionString)
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

app.Run();