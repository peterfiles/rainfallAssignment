using Microsoft.AspNetCore.Mvc;
using RainFallAssignment.API.Helper;
using RainFallAssignment.BusinessLogic.Interface;
using RainFallAssignment.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RainFallAssignment.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class RainFallController : ControllerBase
  {
    private readonly IRainFallAssignment _rainFallAssignment;
    private readonly HttpHelperClass _httpHelperClass;
    public RainFallController(IRainFallAssignment rainFallAssignment)
    {
      _rainFallAssignment = rainFallAssignment;
      _httpHelperClass = new HttpHelperClass();
    }

    /// <summary>
    /// Get rainfall readings by station Id
    /// </summary>
    [HttpGet("/id/{stationId}/readings", Name = "get-rainfall")]
    [Tags("Rainfall")]
    [SwaggerResponse((int)HttpStatusCode.OK, "A list of rainfall readings successfully retrieved", Type = typeof(RainFallStationReadingsResult))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid request", Type = typeof(IEnumerable<dynamic>))]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "No readings found for the specified stationId", Type = typeof(IEnumerable<dynamic>))]
    [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error", Type = typeof(IEnumerable<dynamic>))]
    public IActionResult GetRainFallStationReading(string stationId = "")
    {
      var returnContent = _rainFallAssignment.GetRainFallStationReading(stationId);
      return _httpHelperClass.ReturnResponseResult(returnContent);
    
    }


    /// <summary>
    /// Get lists of StationId's
    /// </summary>
    [HttpGet("/id/stations", Name = "get-list-stationId")]
    [Tags("Rainfall Station Data")]
    public List<RainFall> GetAllStationId()
    {
      var returnContent = _rainFallAssignment.GetAllRainFallStationId();

      if (returnContent.Result.httpResponseMessageContent.StatusCode == HttpStatusCode.NotFound)
      {
        throw new BadHttpRequestException("No readings found for the specified stationId", (int)HttpStatusCode.NotFound);
      }

      return returnContent.Result.rainFallResultList;
    }
  }
}
