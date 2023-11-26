global using HtmlAgilityPack;
global using scrapper.Models;
global using scrapper.Data;
global using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// in-memory database
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("Scrapper"));


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
