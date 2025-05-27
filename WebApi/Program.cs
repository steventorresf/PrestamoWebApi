using Application.Clientes.GuardarCliente;
using Application.Clientes.ModificarEstadoCliente;
using Application.Clientes.ObtenerClientes;
using Application.DiasNoHabiles.GuardarDiasNoHabiles;
using Application.DiasNoHabiles.ObtenerDiasNoHabilesPorUsuario;
using Application.Movimientos.EliminarMovimiento;
using Application.Movimientos.GuardarMovimiento;
using Application.Movimientos.ObtenerGanancias;
using Application.Movimientos.ObtenerMovimientosPorPrestamo;
using Application.Prestamos.CrearPrestamo;
using Application.Prestamos.FinalizarPrestamo;
using Application.Prestamos.ModificarEstadoPrestamo;
using Application.Prestamos.ObtenerBalanceGeneral;
using Application.Prestamos.ObtenerCalculoCuotas;
using Application.Prestamos.ObtenerPrestamoDetalle;
using Application.Prestamos.ObtenerPrestamosAnulados;
using Application.Prestamos.ObtenerPrestamosCongelados;
using Application.Prestamos.ObtenerPrestamosPendientes;
using Application.Prestamos.ObtenerPrestamosPorClienteId;
using Application.PrestamosDetalles.AnularCobro;
using Application.PrestamosDetalles.EliminarPrestamoDetalle;
using Application.PrestamosDetalles.GuardarAbono;
using Application.PrestamosDetalles.GuardarCobro;
using Application.PrestamosDetalles.GuardarPrestamoDetalle;
using Application.PrestamosDetalles.ObtenerGananciasEsperadas;
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
using Persistence.Interfaces;
using Persistence.Interfaces.Implementations;
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
builder.Services.AddTransient<IRequestHandler<ModificarEstadoClienteRequest, ModificarEstadoClienteResponse>, ModificarEstadoClienteHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerUsuarioPorLoginRequest, ObtenerUsuarioPorLoginResponse>, ObtenerUsuarioPorLoginHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerPrestamosPorClienteIdRequest, List<ObtenerPrestamosPorClienteIdResponse>>, ObtenerPrestamosPorClienteIdHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerTablaDetallesPorCodigosRequest, List<ObtenerTablaDetallesPorCodigosResponse>>, ObtenerTablaDetallesPorCodigosHandler>();
builder.Services.AddTransient<IRequestHandler<CrearPrestamoRequest, bool>, CrearPrestamoHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerPrestamoDetalleRequest, List<ObtenerPrestamoDetalleResponse>>, ObtenerPrestamoDetalleHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerPrestamosPendientesRequest, List<ObtenerPrestamosPendientesResponse>>, ObtenerPrestamosPendientesHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerPrestamosCongeladosRequest, List<ObtenerPrestamosCongeladosResponse>>, ObtenerPrestamosCongeladosHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerPrestamosAnuladosRequest, List<ObtenerPrestamosAnuladosResponse>>, ObtenerPrestamosAnuladosHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerCalculoCuotasRequest, ObtenerCalculoCuotasResponse>, ObtenerCalculoCuotasHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerBalanceGeneralRequest, ObtenerBalanceGeneralResponse>, ObtenerBalanceGeneralHandler>();
builder.Services.AddTransient<IRequestHandler<ModificarEstadoPrestamoRequest, bool>, ModificarEstadoPrestamoHandler>();
builder.Services.AddTransient<IRequestHandler<FinalizarPrestamoRequest, bool>, FinalizarPrestamoHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerMovimientosPorPrestamoRequest, List<ObtenerMovimientosPorPrestamoResponse>>, ObtenerMovimientosPorPrestamoHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerGananciasRequest, List<ObtenerGananciasResponse>>, ObtenerGananciasHandler>();
builder.Services.AddTransient<IRequestHandler<GuardarMovimientoRequest, bool>, GuardarMovimientoHandler>();
builder.Services.AddTransient<IRequestHandler<EliminarMovimientoRequest, bool>, EliminarMovimientoHandler>();
builder.Services.AddTransient<IRequestHandler<GuardarDiasNoHabilesRequest, bool>, GuardarDiasNoHabilesHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerDiasNoHabilesPorUsuarioRequest, List<ObtenerDiasNoHabilesPorUsuarioResponse>>, ObtenerDiasNoHabilesPorUsuarioHandler>();
builder.Services.AddTransient<IRequestHandler<GuardarPrestamoDetalleRequest, bool>, GuardarPrestamoDetalleHandler>();
builder.Services.AddTransient<IRequestHandler<GuardarCobroRequest, bool>, GuardarCobroHandler>();
builder.Services.AddTransient<IRequestHandler<GuardarAbonoRequest, bool>, GuardarAbonoHandler>();
builder.Services.AddTransient<IRequestHandler<AnularCobroRequest, bool>, AnularCobroHandler>();
builder.Services.AddTransient<IRequestHandler<EliminarPrestamoDetalleRequest, bool>, EliminarPrestamoDetalleHandler>();
builder.Services.AddTransient<IRequestHandler<ObtenerGananciasEsperadasRequest, List<ObtenerGananciasEsperadasResponse>>, ObtenerGananciasEsperadasHandler>();

builder.Services.AddScoped<ITablaDetalleRepository, TablaDetalleRepository>();
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
