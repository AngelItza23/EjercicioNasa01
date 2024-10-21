//var builder = WebApplication.CreateBuilder(args);

//// Configurar Swagger
//builder.Services.AddControllers();
////builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.OperationFilter<SwaggerFileOperationFilter>(); // Este filtro manejará los archivos
//    // Información adicional del documento Swagger (opcional)
//    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//    {
//        Title = "API de Movimientos",
//        Version = "v1",
//        Description = "Documentación para la API de Movimientos Contables"
//    });
//});

//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ejercicio API v1");

//    });
//}

//app.UseRouting();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();

//===============================================
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Agregar Swagger
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    // Filtro para manejar archivos (IFormFile)
    c.OperationFilter<SwaggerFileOperationFilter>();

    // Configurar SwaggerDoc
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API de Ejercicios",
        Version = "v1"
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()        // Permitir cualquier origen
              .AllowAnyMethod()        // Permitir cualquier método (GET, POST, PUT, etc.)
              .AllowAnyHeader();       // Permitir cualquier encabezado
    });
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Ejercicios v1");
    });
}



app.UseAuthorization();
app.MapControllers();
app.Run();








//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.OperationFilter<SwaggerFileOperationFilter>(); // Este filtro manejará los archivos
//});

//var app = builder.Build();




////if (app.Environment.IsDevelopment())
////{
////    app.UseSwagger();
////    app.UseSwaggerUI(c =>
////    {
////        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
////    });
////}

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();
