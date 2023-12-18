using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace rainfallAssignment.DocumentFilters
{
  public class TagDocumentFilter : IDocumentFilter
  {
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
      swaggerDoc.Tags = new List<OpenApiTag>
      {
        new OpenApiTag { Name = "Rainfall", Description = "Operations relating to rainfall"}
      };
    }
  }
}
