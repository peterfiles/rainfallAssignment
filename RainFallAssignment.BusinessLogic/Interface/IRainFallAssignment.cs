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
    List<RainFall> GetAllRainFallStationId();
    Task<RainFallStationReadingsResult> GetRainFallStationReading(string stationId);
  }
}
