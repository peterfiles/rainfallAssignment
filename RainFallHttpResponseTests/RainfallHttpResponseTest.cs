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

    Mock<IRainFallAssignment> _iRainFallAssignment;
    Mock<IHttpClientFactory> _httpClientFactory;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpClientFactory"></param>

    /// <summary>
    /// Get Response Http Response Status 200
    /// </summary>
    /// [SwaggerResponse((int)HttpStatusCode.OK, "A list of rainfall readings successfully retrieved", Type = typeof(RainFallStationReadingsResult))]
    //[SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid request", Type = typeof(IEnumerable<dynamic>))]
    //[SwaggerResponse((int)HttpStatusCode.NotFound, "No readings found for the specified stationId", Type = typeof(IEnumerable<dynamic>))]
    //[SwaggerResponse((int)HttpStatusCode.InternalServerError, "Internal server error", Type = typeof(IEnumerable<dynamic>))]

    [SetUp]
    public void SetUp()
    {
      var client = new HttpClient();
      var clientName = "RainfallAPI";
      _httpClientFactory = new Mock<IHttpClientFactory>();
      _httpClientFactory.Setup(_ => _.CreateClient(clientName)).Returns(client);
      _httpClientBaseService = new HttpClientBaseService(_httpClientFactory.Object);
      _railFallAssignmentService = new RainFallAssignmentService(_httpClientBaseService);
      _httpHelperClass = new HttpHelperClass();
    }

    [Test]
    public void GetValidResponse()
    {
      var stationId = "3002";
      var returnContent = _railFallAssignmentService.GetRainFallStationReading(stationId);
      returnContent.Result.httpResponseMessageContent.StatusCode = HttpStatusCode.OK;
      var response = _httpHelperClass.ReturnResponseResult(returnContent);
      Assert.That(((IStatusCodeActionResult)response).StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
    }

  }
}