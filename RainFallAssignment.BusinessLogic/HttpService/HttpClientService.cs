using RainFallAssignment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RainFallAssignment.BusinessLogic.HttpBaseService
{
  public abstract class HttpClientService
  {
    protected readonly HttpClient _client;
    public readonly string _baseUrl;
    protected HttpClientService(HttpClient client)
    {
      _client = client;
      _baseUrl = "http://environment.data.gov.uk/flood-monitoring/";
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <param name="httpClient"></param>
    /// <returns></returns>
    public async Task<ApiResponse> GetAllStationIdAsync(string urlPath)
    {
      HttpResponseMessage response = await _client.GetAsync(urlPath);

      var jsonResponse = await response.Content.ReadAsStringAsync();
      Console.WriteLine($"{jsonResponse}\n");

      return new ApiResponse
      {
        StatusCode = response.StatusCode,
        Content = jsonResponse
      };
    }
  }
}
