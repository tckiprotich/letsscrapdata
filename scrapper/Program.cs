global using HtmlAgilityPack;
global using scrapper.Models;
global using scrapper.Data;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Sqlite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// use sqlite
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app.db"));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/", () => {
    return "Lets do this!";
});

app.Run();
