using System.Text;
using GestionePratiche.Repository;
using GestionePratiche.Services.PraticheService;
using GestionePratiche.Services.PraticheService.SuperHeroAPI.Services.PraticheService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using SistemaGestionePratiche.Repository.PraticheRepository;

var builder = WebApplication.CreateBuilder(args);

var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

// Jwt configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });

//ELK Configuration
builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration
    .WriteTo.Console()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200")));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPraticheService, PraticheService>();
builder.Services.AddScoped<IPraticheRepository, PraticheRepository>();
builder.Services.AddDbContext<DataContext>();
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders(); // Rimuovi eventuali provider di logging predefiniti
    loggingBuilder.AddSerilog();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
