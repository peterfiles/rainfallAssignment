using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RainFallAssignment.BusinessLogic.BaseService;
using RainFallAssignment.BusinessLogic.Helpers;
using RainFallAssignment.BusinessLogic.HttpBaseService;
using RainFallAssignment.BusinessLogic.Interface;
using RainFallAssignment.Entities;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime;
using System.Text.Json.Serialization;

namespace RainFallAssignment.API.Controllers
{
  /// <summary>
  /// Rainfall main controller
  /// </summary>
  [ApiController]
  [Route("[controller]")]
  [Produces("application/json")]
  public class RainFallController : ControllerBase 
  {
    private readonly IRainFallAssignment _rainFallAssignment;
    private readonly Helper _helper;

    /// <summary>
    /// Constructor
    /// </summary>
    public RainFallController(IRainFallAssignment rainFallAssignment, Helper helper)
    {
      _rainFallAssignment = rainFallAssignment;
      _helper = helper;
    }


    /// <summary>
    /// Get rainfall readings by station Id
    /// </summary>
    [HttpGet("/id/{stationId}/readings", Name = "get-rainfall")]
    [Tags("Rainfall")]
    /// <summary>Get rainfall readings by station Id</summary>
    /// <param name="stationId">The id of the reading station</param>
    /// <param name="count">The number of readings to return</param>
    /// <returns>A list of rainfall readings successfully retrieved</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public Task<ObjectResponseResult<dynamic>> GetRainfallAsync(string stationId, double? count)
    {
      return _rainFallAssignment.GetRainfallAsync(stationId, count, CancellationToken.None);
    }

    /// <summary>
    /// Get lists of StationId's
    /// </summary>
    //[HttpGet("/id/stations", Name = "get-list-stationId")]
    //[Tags("Rainfall Station Data")]
    //public dynamic GetAllStationId()
    //{
    //  return _rainFallAssignment.GetAllRainFallStationId();
    //}
  }
}
