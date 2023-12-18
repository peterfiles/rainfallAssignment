using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RainFallAssignment.BusinessLogic.HttpBaseService;
using RainFallAssignment.BusinessLogic.Interface;
using RainFallAssignment.Entities;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace RainFallAssignment.API.Controllers
{
  [ApiController]
  [Route("/rainfall")]
  public class RainFallController : ControllerBase , IRainFallAssignment
  {
    public readonly HttpClientService _httpClientService;

    public RainFallController(HttpClientService httpClientService)
    {
      _httpClientService = httpClientService;
    }


    /// <summary>
    /// Get rainfall readings by station Id
    /// </summary>
    [HttpGet("/id/{stationId}/readings", Name = "get-rainfall")]
    [Description("Retrieve the latest readings for the specified stationId")]
    [Tags("Rainfall")]
    public Task<RainFallStationReadingsResult> GetRainFallStationReading(string stationId = "")
    {
      throw new NotImplementedException();
    }


    /// <summary>
    /// Get lists of StationId's
    /// </summary>
    [HttpGet("/id/stations", Name = "get-list-stationId")]
    [Description("Retrieve a list of station data")]
    [Tags("Rainfall Station Data")]
    public async Task<RainFall> GetAllRainFallStationId()
    {
      string urlPath = "/id/stations";
      var response = await _httpClientService.GetAllStationIdAsync(urlPath);
      Console.Write(JsonConvert.SerializeObject(response));

      return null;
    }
  }
}
