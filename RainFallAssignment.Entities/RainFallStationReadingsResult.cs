using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainFallAssignment.Entities
{
  public class RainFallStationReadingsResult
  {
    public Uri stringId { get; set; }
    public DateTime dateTime { get; set; }
    public Uri measure { get; set; }
    public decimal value { get; set; }  
  }
}
