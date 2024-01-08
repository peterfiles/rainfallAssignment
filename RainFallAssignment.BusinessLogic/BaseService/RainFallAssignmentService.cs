using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RainFallAssignment.BusinessLogic.HttpBaseService;
using RainFallAssignment.BusinessLogic.Interface;
using RainFallAssignment.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RainFallAssignment.BusinessLogic.BaseService
{
  public class RainFallAssignmentService : IRainFallAssignment
  {
    private readonly HttpClientBaseService _httpClientService;
    public RainFallAssignmentService(HttpClientBaseService httpClientService) { 
      _httpClientService = httpClientService;
    }

    public async Task<RainFallResponseData> GetAllRainFallStationId()
    {
      var rainFallQueryResponse = await _httpClientService.Get("RainfallAPI", "/id/stations?parameter=rainfall&_limit=50");
      //return JsonConvert.DeserializeObject<Task<RainFall>>(JsonConvert.SerializeObject(.));
      //Replacing Keys to match entities
      //@id = id
      //long = distance_long

      var responseObject = rainFallQueryResponse.Content.ReadAsStringAsync();
      var newString = responseObject.Result.Replace("long", "distance_long");
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
      return new RainFallResponseData
      {
        rainFallResultList = parseValues,
        httpResponseMessageContent = rainFallQueryResponse
      };
    }

    public async Task<RainFallResponseData> GetRainFallStationReading(string stationId)
    {
      var rainFallQueryResponse = await _httpClientService.Get("RainfallAPI", string.Format("/id/stations/{0}/readings?_sorted&_limit=100", stationId));
      //return JsonConvert.DeserializeObject<Task<RainFall>>(JsonConvert.SerializeObject(.));
      //Replacing Keys to match entities
      //@id = id
      //long = distance_long

      var responseObject = rainFallQueryResponse.Content.ReadAsStringAsync();
      var newString = responseObject.Result.Replace("@id", "stringId");
      dynamic responseToJson = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(newString));

      List<RainFallStationReadingsResult> parseValues = new List<RainFallStationReadingsResult>();
      foreach (dynamic item in JsonConvert.DeserializeObject<dynamic>(responseToJson))
      {
        if (item.Name == "items")
        {
          parseValues = JsonConvert.DeserializeObject<List<RainFallStationReadingsResult>>(JsonConvert.SerializeObject(item.Value));
          rainFallQueryResponse.StatusCode = parseValues.Count == 0 ? HttpStatusCode.NotFound : HttpStatusCode.OK; 
        }
      }

      return new RainFallResponseData { rainFallStationReadingResultList = parseValues, httpResponseMessageContent = rainFallQueryResponse};
    }

    public async Task<RainFallResponseData> GetRainFallStationReading(HttpClient client,string stationId)
    {
      var rainFallQueryResponse = await _httpClientService.GetWithHttpClient(client,"RainfallAPI", string.Format("/id/stations/{0}/readings?_sorted&_limit=100", stationId));
      //return JsonConvert.DeserializeObject<Task<RainFall>>(JsonConvert.SerializeObject(.));
      //Replacing Keys to match entities
      //@id = id
      //long = distance_long

      var responseObject = rainFallQueryResponse.Content.ReadAsStringAsync();
      var newString = responseObject.Result.Replace("@id", "stringId");
      dynamic responseToJson = JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(newString));

      List<RainFallStationReadingsResult> parseValues = new List<RainFallStationReadingsResult>();
      foreach (dynamic item in JsonConvert.DeserializeObject<dynamic>(responseToJson))
      {
        if (item.Name == "items")
        {
          parseValues = JsonConvert.DeserializeObject<List<RainFallStationReadingsResult>>(JsonConvert.SerializeObject(item.Value));
          rainFallQueryResponse.StatusCode = parseValues.Count == 0 ? HttpStatusCode.NotFound : HttpStatusCode.OK;
        }
      }

      return new RainFallResponseData { rainFallStationReadingResultList = parseValues, httpResponseMessageContent = rainFallQueryResponse };
    }
  }
}
