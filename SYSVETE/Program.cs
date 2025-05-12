using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using SYSVETE.Autorizacion;
using SYSVETE.Helpers;
using SYSVETE.Models;
using SYSVETE.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opciones =>
opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

//Aca configuro para que la clase AppSetting mape el secret para generar los tokens de sesion
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

//Para cargar datos en la DB
builder.Services.AddTransient<DataSeeder>();

//Fast Report
builder.Services.AddTransient(provider =>
{
    return new FReport(builder.Configuration.GetConnectionString("SYSVETE"));
});

//Agregar Servicios aca
builder.Services.AddScoped<IJWTUtils, JWTUtils>();
builder.Services.AddScoped<IUsuario, UsuarioService>();
builder.Services.AddScoped<IModulo, ModuloService>();
builder.Services.AddScoped<IRol, RolService>();
builder.Services.AddScoped<IPersona, PersonaService>();
builder.Services.AddScoped<IPermiso, PermisoService>();
builder.Services.AddScoped<ITipoInsumoService, TipoInsumoService>();
builder.Services.AddScoped<IInsumoService, InsumoService>();
builder.Services.AddScoped<IUnidadMedidaService, UnidadMedidaService>();
builder.Services.AddScoped<IPresentacionService, PresentacionService>();
builder.Services.AddScoped<IImpuestoService, ImpuestoService>();
builder.Services.AddScoped<IEspecieService, EspecieService>();
builder.Services.AddScoped<IRazaService, RazaService>();
builder.Services.AddScoped<ITratamientoService, TratamientoService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<ICompraService, CompraService>();
builder.Services.AddScoped<ICompraDetalleService, CompraDetalleService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IPatologiaService, PatologiaService>();
builder.Services.AddScoped<IVacunaService, VacunaService>();
builder.Services.AddScoped<IProcedimientoService, ProcedimientoService>();
builder.Services.AddScoped<IHistorialClinicoService, HistorialClinicoService>();
builder.Services.AddScoped<IVentaService, VentaService>();
builder.Services.AddScoped<IVentaDetalleService, VentaDetalleService>();
builder.Services.AddScoped<IDeudaProveedorService, DeudaProveedorService>();
builder.Services.AddScoped<IPagoVentaService, PagoVentaService>();
builder.Services.AddScoped<IStockServiceService, StockServiceService>();

//Se agregar el Contexto de la base de datos
builder.Services.AddDbContext<SYSVETEContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SYSVETE")));

var app = builder.Build();

//Para que carge los datos iniciales en la db ejecutar con el argumento "seeddata"
if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DataSeeder>();
        service.Seed();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

//Middleware para autenticaciones
app.UseMiddleware<AutorizarMiddleware>();

app.MapControllers();

app.Run();
