using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using rainfallAssignment.DocumentFilters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Setting up sorted contracts
builder.Services.AddSwaggerGen(setup => { setup.SwaggerDoc("v1", new OpenApiInfo()
{
  Description = "An API which provides rainfall reading data",
  Title = "Rainfall API",
  Version = "v1",
  Contact = new OpenApiContact()
  {
    Name = "Sorted",
    Url = new Uri("http://www.sorted.com")
  }
}); 
  setup.DocumentFilter<TagDocumentFilter>();
});

builder.Services.AddSwaggerGen(servers => servers.AddServer(new OpenApiServer()
{
  Url = "http://localhost:3000",
  Description = "Rainfall Api"
}));

//builder.Services.AddSwaggerGen(tags => tags());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
