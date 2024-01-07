using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RainFallAssignment.Entities
{
  public class RainFallResponseData
  {
    public List<RainFallStationReadingsResult> rainFallStationReadingResultList { get; set; }
    public List<RainFall> rainFallResultList { get; set; }
    public HttpResponseMessage httpResponseMessageContent { get; set; }
  }
}
