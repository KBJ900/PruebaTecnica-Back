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

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowLocalhost3000",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3000")  
                .AllowAnyHeader()                      
                .AllowAnyMethod();                     
        });
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;

    });
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
app.UseRouting();
app.UseCors("AllowLocalhost3000");


app.UseAuthorization();

app.MapControllers();

app.Run();
