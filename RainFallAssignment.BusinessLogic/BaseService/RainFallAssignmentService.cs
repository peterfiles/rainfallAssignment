using Newtonsoft.Json;
using RainFallAssignment.BusinessLogic.HttpBaseService;
using RainFallAssignment.BusinessLogic.Interface;
using RainFallAssignment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainFallAssignment.BusinessLogic.BaseService
{
  public class RainFallAssignmentService : IRainFallAssignment
  {
    private readonly HttpClientBaseService _httpClientService;
    public RainFallAssignmentService(HttpClientBaseService httpClientService) { 
      _httpClientService = httpClientService;
    }
    public List<RainFall> GetAllRainFallStationId()
    {
      var rainFallQueryResponse = _httpClientService.Get("RainfallAPI", "/id/stations?parameter=rainfall&_limit=50");
      //return JsonConvert.DeserializeObject<Task<RainFall>>(JsonConvert.SerializeObject(.));
      //Replacing Keys to match entities
      //@id = id
      //long = distance_long
      var newString = rainFallQueryResponse.Result.Replace("long", "distance_long");
      newString.Replace("@id", "id");
      dynamic responseToJson = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(newString));

      List<RainFall> parseValues = new List<RainFall>();
      foreach (dynamic item in JsonConvert.DeserializeObject<dynamic>(responseToJson))
      {
        if (item.Name == "items")
        {
          parseValues  = JsonConvert.DeserializeObject<List<RainFall>>(JsonConvert.SerializeObject(item.Value));
        }
      }
      return parseValues;
    }

    public Task<RainFallStationReadingsResult> GetRainFallStationReading(string stationId)
    {
      throw new NotImplementedException();
    }
  }
}
