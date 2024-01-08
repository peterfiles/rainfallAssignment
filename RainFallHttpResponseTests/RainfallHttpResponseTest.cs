using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Moq;
using NUnit.Framework;
using RainFallAssignment.API.Controllers;
using RainFallAssignment.API.Helper;
using RainFallAssignment.BusinessLogic.BaseService;
using RainFallAssignment.BusinessLogic.HttpBaseService;
using RainFallAssignment.BusinessLogic.Interface;
using RainFallAssignment.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace RainFallHttpResponseTests
{
  [TestFixture]
  public class RainfallHttpResponseTest
  {
    private HttpHelperClass _httpHelperClass;
    private HttpClientBaseService _httpClientBaseService;
    private RainFallAssignmentService _railFallAssignmentService;
    private HttpClient _httpClient;
    private string _urlPath = "/id/stations?parameter=rainfall&_limit=50";

    Mock<IHttpClientFactory> _httpClientFactory;

    [SetUp]
    public void SetUp()
    {
      _httpClient = new HttpClient();
      var clientName = "RainfallAPI";
      _httpClientFactory = new Mock<IHttpClientFactory>();
      _httpClientFactory.Setup(_ => _.CreateClient(clientName)).Returns(_httpClient);
      _httpClient.BaseAddress = new Uri("http://environment.data.gov.uk/flood-monitoring");
      _httpClientBaseService = new HttpClientBaseService(_httpClientFactory.Object);
      _railFallAssignmentService = new RainFallAssignmentService(_httpClientBaseService);
      _httpHelperClass = new HttpHelperClass();
    }

    /// <summary>
    /// Unit testing for response codes
    /// stationID is Existing
    /// Status code: 200
    /// </summary>
    [Test]
    public void GetValidResponse()
    {
      var stationId = "4163";
      _httpClient.GetAsync(_httpClient.BaseAddress + _urlPath);
      var returnContent = _railFallAssignmentService.GetRainFallStationReading(_httpClient,stationId);
      var response = _httpHelperClass.ReturnResponseResult(returnContent);
      Assert.That(((IStatusCodeActionResult)response).StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
    }

    /// <summary>
    /// stationID is not existing
    /// Status code: 404
    /// </summary>
    [Test]
    public void GetResponse404()
    {
      var stationId = "3002";
      _httpClient.GetAsync(_httpClient.BaseAddress + _urlPath);
      var returnContent = _railFallAssignmentService.GetRainFallStationReading(_httpClient, stationId);
      var response = _httpHelperClass.ReturnResponseResult(returnContent);
      Assert.That(((IStatusCodeActionResult)response).StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
    }

    /// <summary>
    /// According to: https://en.ryte.com/wiki/Status_Code_400
    /// * 400 bad request: All errors with the status code 4xx indicate an invalid request from a client to a server.
    /// Status code: 400
    /// </summary>
    [Test]
    public void GetResponse400()
    {
      var stationId = "3002";
      _httpClient.GetAsync(_httpClient.BaseAddress + "test" + _urlPath + "&testingforBadRequest");
      var returnContent = _railFallAssignmentService.GetRainFallStationReading(_httpClient, stationId);
      var response = _httpHelperClass.ReturnResponseResult(returnContent);
      Assert.That(((IStatusCodeActionResult)response).StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
    }

    /// <summary>
    /// Status code: 500
    /// </summary>
    [Test]
    public void GetResponse500()
    {
      _httpClient.GetAsync(_httpClient.BaseAddress +  _urlPath );
      _httpClient = new HttpClient(); 
      Assert.Throws<NullReferenceException>(() => _httpClientBaseService.GetWithHttpClient(null, null,null));
    }

  }
}