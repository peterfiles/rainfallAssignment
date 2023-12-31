﻿using Newtonsoft.Json;
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
    public Task<HttpResponseMessage> Get(string clientAPI, string urlPath)
    {
      var httpClient = _httpClientFactory.CreateClient(clientAPI);
      if(httpClient == null)
      {
        throw new NullReferenceException("HttpClient shouldn't be null");
      }
      var httpResponse =  httpClient.GetAsync(httpClient.BaseAddress + urlPath);
     
      return httpResponse;
    }

    public Task<HttpResponseMessage> GetWithHttpClient(HttpClient client, string clientAPI, string urlPath)
    {
      if (client == null)
      {
        throw new NullReferenceException("HttpClient shouldn't be null");
      }
      var httpResponse = client.GetAsync(client.BaseAddress + urlPath);
      return httpResponse;
    }
  }
}
