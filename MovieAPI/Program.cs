using MovieAPI.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Swashbuckle.AspNetCore.Filters;
using MovieAPI.DTOs;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MovieAPI", Version = "v1" });
    c.EnableAnnotations();
    c.ExampleFilters();

});
builder.Services.AddSwaggerExamplesFromAssemblyOf<MovieCreateExample>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<MovieUpdateExample>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
