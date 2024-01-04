using Newtonsoft.Json;
using RainFallAssignment.BusinessLogic.HttpBaseService;
using RainFallAssignment.BusinessLogic.Interface;
using RainFallAssignment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RainFallAssignment.BusinessLogic.BaseService
{
  public class RainFallService : IRainFallAssignment
  {

    private readonly HttpClientBaseService _httpClientBaseService;
    public RainFallService(HttpClientBaseService httpClientBaseService) { 
    _httpClientBaseService  = httpClientBaseService;
    }

    public Task<ObjectResponseResult<dynamic>> GetRainfallAsync(string stationId, double? count, CancellationToken cancellationToken)
    {
      return _httpClientBaseService.GetRainfallAsync(stationId, count, CancellationToken.None);
    }

    public void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
    {
      throw new NotImplementedException();
    }

    public void PrepareRequest(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder)
    {
      throw new NotImplementedException();
    }

    public void ProcessResponse(HttpClient client, HttpResponseMessage response)
    {
      throw new NotImplementedException();
    }

    public void UpdateJsonSerializerSettings(JsonSerializerSettings settings)
    {
      throw new NotImplementedException();
    }
  }
}
