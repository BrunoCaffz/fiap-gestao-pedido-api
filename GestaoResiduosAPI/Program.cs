using GestaoResiduosAPI.Config;
using GestaoResiduosAPI.Data;
using GestaoResiduosAPI.Middleware;
using GestaoResiduosAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Detecta se está rodando em container (Linux Docker)
var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

// =======================
// CONFIGURAÇÃO DO BANCO
// =======================
if (isDocker)
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseInMemoryDatabase("GestaoResiduosDb"));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

// ==========================
// CONFIGURAÇÃO DO JWT
// ==========================
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// ===========================
// ADD CONTROLLERS + SWAGGER
// ===========================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GestaoResiduosAPI",
        Version = "v1"
    });

    // Esquema de segurança Bearer
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Insira o token JWT assim: **Bearer {seu_token}**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    c.AddSecurityDefinition("Bearer", securityScheme);

    // Exige o Bearer em todos os endpoints (ou pelo menos define globalmente)
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securityScheme,
            Array.Empty<string>()
        }
    });
});

// ===========================
// INJEÇÃO DE DEPENDÊNCIAS
// ===========================
builder.Services.AddScoped<ColetaService>();
builder.Services.AddScoped<AlertaService>();
builder.Services.AddScoped<RotaService>();
builder.Services.AddScoped<AuthService>(); // <– ESSENCIAL PARA JWT

var app = builder.Build();

// ============================================
// APLICA MIGRATIONS OU SEED (DEPENDE DO DOCKER)
// ============================================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (isDocker)
    {
        SeedData.Seed(db);
    }
    else
    {
        db.Database.Migrate();
    }
}

// ========================
// PIPELINE DA APLICAÇÃO
// ========================
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();  // <– OBRIGATÓRIO ANTES DO AUTHORIZATION
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();

// Necessário para testes de integração
public partial class Program { }
