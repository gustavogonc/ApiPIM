using ApiPIM.Context;
using ApiPIM.Repository;
using ApiPIM.Repository;
using ApiPIM.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<SenhaServices>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IAppRepository, AppRepository>();
builder.Services.AddScoped<ICargosRepository, CargosRepository>();
builder.Services.AddScoped<IDepartamentosRepository, DepartamentosRepository>();
builder.Services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ICalculaDescontos, CalculaDescontos>();
builder.Services.AddScoped<IProventoRepository, ProventoRepository>();
builder.Services.AddScoped<ObterPagamentoRepository>();
builder.Services.AddScoped<FuncionarioDeducoesRepository>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}
    ).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateActor = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["TokenConfiguration:Issuer"],
            ValidAudience = builder.Configuration["TokenConfiguration:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
        };
    });





builder.Services.AddCors();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.UseCors(opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.MapControllers();

app.Run();
