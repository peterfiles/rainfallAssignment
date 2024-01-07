using Microsoft.AspNetCore.Mvc;
using RainFallAssignment.API.Controllers;
using RainFallAssignment.BusinessLogic.Interface;
using RainFallAssignment.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RainFallAssignment.API.Helper
{
  public class HttpHelperClass : ControllerBase
  {
    public IActionResult ReturnResponseResult(Task<RainFallResponseData> returnData)
    {
      switch (returnData.Result.httpResponseMessageContent.StatusCode)
      {
        case (HttpStatusCode.OK):
          StatusCode(200, "A list of rainfall readings successfully retrieved");
          return (IActionResult)Ok(returnData.Result.rainFallStationReadingResultList);
        case (HttpStatusCode.BadRequest):
          return BadRequest("Invalid request");
        case (HttpStatusCode.NotFound):
          return NotFound("No readings found for the specified stationId");
        case (HttpStatusCode.InternalServerError):
          return StatusCode(500, "Internal server error");
        default:
          return NoContent();
      }
    }
  }
}
