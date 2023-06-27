using Application;
using Application.Implementations;
using Domain.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Implementations;
using Persistence.Repositories;
using Persistence.Repositories.Implementations;
using System.Text;
using WebApi.Filters;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

string _corsConfiguration = "_corsConfiguration";


// Add services to the container.
builder.Services.AddCors();
builder.Services.AddDbContext<BaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:PrestamistaDbContext"]);
});

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IPrestamoService, PrestamoService>();
builder.Services.AddScoped<ITablaDetalleService, TablaDetalleService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IFileTxtService, FileTxtService>();

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPrestamoRepository, PrestamoRepository>();
builder.Services.AddScoped<ITablasDetalleRepository, TablasDetalleRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<UserValidationFilter>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PrestamoFacilWebApi",
        Version = "v1",
        Description = "API para Prestamo Fácil Web",
        Contact = new OpenApiContact
        {
            Name = "Steven Torres Fernández",
            Email = "steventorresf@gmail.com"
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Escriba 'Bearer' [espacio] y luego ingrese el token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
});

builder.Services.Configure<AppSettings>(builder.Configuration)
.AddCors(options =>
    options.AddPolicy(_corsConfiguration,
    builder =>
    {
        builder.WithOrigins("*", "http://localhost:7238", "https://localhost:7238");
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    }
));

builder.Services
    .AddHttpContextAccessor()
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"]))
        };
    });


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    ); // allow credentials

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
