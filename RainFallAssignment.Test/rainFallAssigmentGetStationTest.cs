using Microsoft.VisualStudio.TestTools.UnitTesting;
using RainFallAssignment.BusinessLogic.Interface;
using System;

namespace RainFallAssignment.Test
{
  [TestClass]
  public class rainFallAssigmentGetStationTest
  {
    private readonly IRainFallAssignment _rainFallAssignment;

    public rainFallAssigmentGetStationTest(IRainFallAssignment rainFallAssignment)
    {
      _rainFallAssignment = rainFallAssignment;
    }
    [TestMethod]
    public void ValidStaionId()
    {

      var stationId = "E7050";
      var response = _rainFallAssignment.GetRainFallStationReading(stationId);
      //response.
    }
  }
}
