using Newtonsoft.Json;
using RainFallAssignment.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RainFallAssignment.BusinessLogic.HttpBaseService
{
  public class HttpClientBaseService
  {
    private readonly IHttpClientFactory _httpClientFactory;

    public HttpClientBaseService(IHttpClientFactory httpClientFactory)
    {
      _httpClientFactory = httpClientFactory;
    }
    /// <summary>
    /// Use generic method for get
    /// </summary>
    /// <returns></returns>
    public async Task<string> Get(string clientAPI, string urlPath)
    {
      var httpClient = _httpClientFactory.CreateClient(clientAPI);
      var httpResponse = await httpClient.GetAsync(httpClient.BaseAddress + urlPath);
      //if (httpResponse.IsSuccessStatusCode)
      //{

      //}
      return JsonConvert.SerializeObject(httpResponse);
    }
  }
}
