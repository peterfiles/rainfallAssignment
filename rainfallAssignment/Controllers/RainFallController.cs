using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RainFallAssignment.BusinessLogic.BaseService;
using RainFallAssignment.BusinessLogic.HttpBaseService;
using RainFallAssignment.BusinessLogic.Interface;
using RainFallAssignment.Entities;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace RainFallAssignment.API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class RainFallController : ControllerBase 
  {
    private readonly IRainFallAssignment _rainFallAssignment;
    public RainFallController(IRainFallAssignment rainFallAssignment)
    {
      _rainFallAssignment = rainFallAssignment;
    }


    /// <summary>
    /// Get rainfall readings by station Id
    /// </summary>
    [HttpGet("/id/{stationId}/readings", Name = "get-rainfall")]
    [Tags("Rainfall")]
    public Task<RainFallStationReadingsResult> GetRainFallStationReading(string stationId = "")
    {
      throw new NotImplementedException();
    }


    /// <summary>
    /// Get lists of StationId's
    /// </summary>
    [HttpGet("/id/stations", Name = "get-list-stationId")]
    [Tags("Rainfall Station Data")]
    public Task<string> GetAllStationId()
    {
      return _rainFallAssignment.GetAllRainFallStationId();
    }
  }
}
