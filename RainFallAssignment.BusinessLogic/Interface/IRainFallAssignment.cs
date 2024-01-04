using Newtonsoft.Json;
using RainFallAssignment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RainFallAssignment.BusinessLogic.Interface
{
  public interface IRainFallAssignment
  {
    Task<ObjectResponseResult<dynamic>> GetRainfallAsync(string stationId, double? count, CancellationToken cancellationToken);
    void UpdateJsonSerializerSettings(JsonSerializerSettings settings);
    void PrepareRequest(HttpClient client, HttpRequestMessage request, string url);
    void PrepareRequest(HttpClient client, HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
    void ProcessResponse(HttpClient client, HttpResponseMessage response);
  }
}
