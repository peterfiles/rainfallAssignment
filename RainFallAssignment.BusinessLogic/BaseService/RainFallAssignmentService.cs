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
    public Task<string> GetAllRainFallStationId()
    {
      return _httpClientService.Get("RainfallAPI", "/id/stations?parameter=rainfall&_limit=50");
    }

    public Task<RainFallStationReadingsResult> GetRainFallStationReading(string stationId)
    {
      throw new NotImplementedException();
    }
  }
}
