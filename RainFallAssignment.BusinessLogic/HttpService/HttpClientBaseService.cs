using Newtonsoft.Json;
using RainFallAssignment.BusinessLogic.BaseService;
using RainFallAssignment.BusinessLogic.Helpers;
using RainFallAssignment.BusinessLogic.Interface;
using RainFallAssignment.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RainFallAssignment.BusinessLogic.HttpBaseService
{
  public class HttpClientBaseService 
  {
    //private string _baseUrl = "http://localhost:3000";
    private string _baseUrl = "http://environment.data.gov.uk/flood-monitoring";
    private Lazy<JsonSerializerSettings> _settings;
    private HttpClient _httpClient;
    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <summary>Get rainfall readings by station Id</summary>
    /// <param name="stationId">The id of the reading station</param>
    /// <param name="count">The number of readings to return</param>
    /// <returns>A list of rainfall readings successfully retrieved</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public string BaseUrl
    {
      get { return _baseUrl; }
      set { _baseUrl = value; }
    }

    private Helper _helper;
    private RainFallService _rainfallAssignment;
    public HttpClientBaseService(Helper helper, HttpClient httpClient, RainFallService rainfallAssignment)
    {
      _helper = helper;
      _httpClient = httpClient;
      _settings = new Lazy<JsonSerializerSettings>(() =>
      {
        var settings = new JsonSerializerSettings();
        _rainfallAssignment.UpdateJsonSerializerSettings(settings);
        return settings;
      });

    }
    public async Task<ObjectResponseResult<dynamic>> GetRainfallAsync(string stationId, double? count, CancellationToken cancellationToken)
    {
      var retValue = new ObjectResponseResult<dynamic>();
      if (stationId == null)
        throw new ArgumentNullException("stationId");

      var urlBuilder_ = new StringBuilder();
      urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/rainfall/id/{stationId}/readings?");
      urlBuilder_.Replace("{stationId}", Uri.EscapeDataString(_helper.ConvertToString(stationId, CultureInfo.InvariantCulture)));
      if (count != null)
      {
        urlBuilder_.Append(Uri.EscapeDataString("count") + "=").Append(Uri.EscapeDataString(_helper.ConvertToString(count, CultureInfo.InvariantCulture))).Append("&");
      }
      urlBuilder_.Length--;

      var client_ = _httpClient;
      try
      {
        using (var request_ = new HttpRequestMessage())
        {
          request_.Method = new HttpMethod("GET");

          _rainfallAssignment.PrepareRequest(client_, request_, urlBuilder_);
          var url_ = urlBuilder_.ToString();
          request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);
          _rainfallAssignment.PrepareRequest(client_, request_, url_);

          var response_ = await client_.SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
          try
          {
            var headers_ =  Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
            if (response_.Content != null && response_.Content.Headers != null)
            {
              foreach (var item_ in response_.Content.Headers)
                headers_[item_.Key] = item_.Value;
            }

            _rainfallAssignment.ProcessResponse(client_, response_);

            var status_ = ((int)response_.StatusCode).ToString();
            if (status_ == "200")
            {
              return await _helper.ReadObjectResponseAsync<dynamic>(response_, null, _settings.Value);
            }
            else
            if (status_ == "400")
            {
              string responseText_ = (response_.Content == null) ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
              throw new ApiException("Invalid request", (int)response_.StatusCode, responseText_, headers_, null);
            }
            else
            if (status_ == "404")
            {
              string responseText_ = (response_.Content == null) ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
              throw new ApiException("No readings found for the specified stationId", (int)response_.StatusCode, responseText_, headers_, null);
            }
            else
            if (status_ == "500")
            {
              string responseText_ = (response_.Content == null) ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
              throw new ApiException("Internal server error", (int)response_.StatusCode, responseText_, headers_, null);
            }
            else
            if (status_ != "200" && status_ != "204")
            {
              var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
              throw new ApiException("The HTTP status code of the response was not expected (" + (int)response_.StatusCode + ").", (int)response_.StatusCode, responseData_, headers_, null);
            }
          }
          finally
          {
            if (response_ != null)
              response_.Dispose();
          }
        }

      }

      finally
      {
      }

      return retValue;

    }


    protected JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

  }
}
