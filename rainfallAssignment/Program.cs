using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using rainfallAssignment.DocumentFilters;
using RainFallAssignment.API.Controllers;
using RainFallAssignment.BusinessLogic.BaseService;
using RainFallAssignment.BusinessLogic.Helpers;
using RainFallAssignment.BusinessLogic.HttpBaseService;
using RainFallAssignment.BusinessLogic.Interface;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.
builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                      policy.WithOrigins("http://localhost:3000", "http://localhost:7061",
                                            "http://environment.data.gov.uk/flood-monitoring");
                    });
});
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
  setup.EnableAnnotations();

  // using System.Reflection;
  var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
  setup.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});
builder.Services.AddScoped<HttpClientBaseService>();
builder.Services.AddScoped<Helper>();
builder.Services.AddScoped<IRainFallAssignment, RainFallService>();

//builder.Services.AddSwaggerGen(servers => servers.AddServer(new OpenApiServer()
//{
//  Url = "http://localhost:7061",
//  Description = "Rainfall Api"
//}));
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("RainfallAPI", rainFallClient =>
{
  rainFallClient.BaseAddress = new Uri("http://environment.data.gov.uk/flood-monitoring");
});
//builder.Services.AddSwaggerGen(tags => tags());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();
app.Run();
