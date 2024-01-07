using Microsoft.AspNetCore.Mvc;
using RainFallAssignment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainFallAssignment.BusinessLogic.Interface
{
  public interface IRainFallAssignment
  {
    Task<RainFallResponseData> GetAllRainFallStationId();
    Task<RainFallResponseData> GetRainFallStationReading(string stationId);

  }
}
