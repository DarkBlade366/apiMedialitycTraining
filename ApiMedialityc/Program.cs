using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using ApiMedialityc.Data;
using ApiMedialityc.Features.Common;
using ApiMedialityc.Features.Users.DTOs;
using ApiMedialityc.Features.Users.Handlers;
using ApiMedialityc.Features.Users.Queries;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NSwag;
var builder = WebApplication.CreateBuilder(args);
// Conexion con DB
var connection = builder.Configuration.GetConnectionString("DbMedialityc");
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseNpgsql(connection)
); 
// Configuración JWT 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) 
    .AddJwtBearer(options => 
    { 
        options.TokenValidationParameters = new TokenValidationParameters 
        { 
            ValidateIssuer = true, 
            ValidateAudience = true, 
            ValidateLifetime = true, 
            ValidateIssuerSigningKey = true, 
            ValidIssuer = builder.Configuration["Jwt:Issuer"], 
            ValidAudience = builder.Configuration["Jwt:Audience"], 
            IssuerSigningKey = new SymmetricSecurityKey( 
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            
            RoleClaimType = ClaimTypes.Role,
            NameClaimType = ClaimTypes.NameIdentifier
        };
    }); 
builder.Services.AddAuthorization();
//Configuracion del Swagger
builder.Services
    .AddFastEndpoints()
    .SwaggerDocument(o =>
    {
        //o.EnableJWTBearerAuth = false;
        o.DocumentSettings = s =>
        {
            s.Title = "Api Medialityc Training";
            s.Description = "API for Medialitic company for user, reservation and resource management";
            s.Version = "Only User";
        /*
            s.AddAuth("Bearer", new()
        {
            Type = OpenApiSecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            BearerFormat = "JWT",
        });
        */  
        };
    });

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseAuthentication(); 
app.UseAuthorization();

app.UseFastEndpoints();

app.UseOpenApi();

app.UseSwaggerGen();

app.Run();