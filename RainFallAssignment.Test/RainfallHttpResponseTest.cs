using Microsoft.VisualStudio.TestTools.UnitTesting;
using RainFallAssignment.BusinessLogic.Interface;
using System;
using System.Net;

namespace RainFallAssignment.Test
{
  [TestClass]
  public class RainfallHttpResponseTest
  {
    private readonly IRainFallAssignment _rainFallAssignment;
    //private readonly HttpHelperClass _httpHelperClass;

    public RainfallHttpResponseTest(IRainFallAssignment rainFallAssignment){
        _rainFallAssignment = rainFallAssignment; ;
    }
    /// <summary>
    /// Get Response Http Response Status 200
    /// </summary>
    [TestMethod]
    public void GetValidResponse()
    {
      var stationId = ""; 
      var returnContent = _rainFallAssignment.GetRainFallStationReading(stationId);
      returnContent.Result.httpResponseMessageContent.StatusCode = HttpStatusCode.OK;
      //var response = 
      //Assert.AreEqual(HttpStatusCode.OK, )
    }
  }
}
