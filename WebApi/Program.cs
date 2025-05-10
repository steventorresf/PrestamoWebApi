using Application.Clientes.GuardarCliente;
using Application.Clientes.ObtenerClientes;
using Application.Prestamos.CrearPrestamo;
using Application.Prestamos.ObtenerPrestamoDetalle;
using Application.Prestamos.ObtenerPrestamosCongelados;
using Application.Prestamos.ObtenerPrestamosPendientes;
using Application.Prestamos.ObtenerPrestamosPorClienteId;
using Application.TablaDetalles.ObtenerTablaDetallesPorCodigos;
using Application.Usuarios.ObtenerUsuarioPorLogin;
using Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Files;
using System.Reflection;
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

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddTransient<IRequestHandler<ObtenerClientesRequest, List<ObtenerClientesResponse>>, ObtenerClientesHandler>();
builder.Services.AddTransient<IRequestHandler<GuardarClienteRequest, GuardarClienteResponse>, GuardarClienteHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerUsuarioPorLoginRequest, ObtenerUsuarioPorLoginResponse>, ObtenerUsuarioPorLoginHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerPrestamosPorClienteIdRequest, List<ObtenerPrestamosPorClienteIdResponse>>, ObtenerPrestamosPorClienteIdHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerTablaDetallesPorCodigosRequest, List<ObtenerTablaDetallesPorCodigosResponse>>, ObtenerTablaDetallesPorCodigosHandler>();
builder.Services.AddTransient<IRequestHandler<CrearPrestamoRequest, bool>, CrearPrestamoHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerPrestamoDetalleRequest, List<ObtenerPrestamoDetalleResponse>>, ObtenerPrestamoDetalleHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerPrestamosPendientesRequest, List<ObtenerPrestamosPendientesResponse>>, ObtenerPrestamosPendientesHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerPrestamosCongeladosRequest, List<ObtenerPrestamosCongeladosResponse>>, ObtenerPrestamosCongeladosHandler>();

builder.Services.AddScoped<ILogErrorFile, LogErrorFile>();

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
