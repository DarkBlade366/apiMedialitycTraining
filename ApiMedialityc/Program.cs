using System.Reflection.Metadata;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Users.Handlers;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Conexion con DB
var connection = builder.Configuration.GetConnectionString("DbMedialityc");
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseNpgsql(connection)
); 

//Configuracion del Swagger
builder.Services.AddSwaggerDocument(config =>
    config.PostProcess = doc =>
    {
        doc.Info.Title = "Api Medialityc Training";
        doc.Info.Description = "API for Medialitic company for user, reservation and resource management";
    }
);

builder.Services.AddFastEndpoints();
builder.Services.AddEndpointsApiExplorer();

//Handlers
builder.Services.AddScoped<CreateUserHandler>();

var app = builder.Build();


app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi();

app.Run();