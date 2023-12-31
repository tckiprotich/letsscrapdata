global using CsvHelper;
global using HtmlAgilityPack;
global using ExploreParks.Data;
global using ExploreParks.Models;
global using ExploreParks.Services;
global using Microsoft.OpenApi.Models;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Design;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddScoped<IExploreParksInterface, ExploreParksInterface>();

// Sqlite database
builder.Services.AddDbContext<ExploreParksDbContext>(options => options.UseSqlite("Data Source=ExploreParks.db"));

// swagger documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExploreParks", Version = "v1" });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
