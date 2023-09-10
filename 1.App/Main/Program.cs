using System.Reflection;
using System.Text.Json.Serialization;
using App.AppInitializer;
using DataAccess.DbConfigureManagement;
using DataAccess.DbContext;
using Domain.Models;
using Infrastructure.AppComponents.SwaggerComponents;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Добавляем автоматическое преобразование числовых значений перечислений к их названию.
    options.JsonSerializerOptions.Converters.Add(
        new JsonStringEnumConverter()
    );
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Настраиваем Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "CompanyManagement"
    });
    
    options.EnableAnnotations();

    options.UseInlineDefinitionsForEnums();     // Возможность значений по умолчанию для Enum
    
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    
    options.SchemaFilter<SwaggerIgnoreFilter>();
    options.SchemaFilter<SwaggerGenGuidFilter>();
});

// Получаем Кофигуратор БД
var dbConfigurator = DbConfigurator.CreateDbConfiguratorWithAppData(true);
// builder.Services.AddSingleton(dbConfigurator);   // внедряем конфигуратор

// Внедряем контекст БД
builder.Services.AddDbContext<AppDbContext>(options  =>
    dbConfigurator.UseDatabaseProvider(options)
);

// Внедряем зависимости на Модели
builder.Services.AddTransient<CompanyModel>();
builder.Services.AddTransient<ContactModel>();
builder.Services.AddTransient<CommunicationModel>();

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

// Инициализируем приложение
using var serviceScope = app.Services.CreateScope();
await Initializer.Init(serviceScope.ServiceProvider);

app.Run();