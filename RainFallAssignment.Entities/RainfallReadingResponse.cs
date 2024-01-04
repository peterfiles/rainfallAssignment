using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainFallAssignment.Entities
{
  public class RainfallReadingResponse
  {
    [JsonProperty("readings", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public ICollection<RainfallReading> Readings { get; set; }

    private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

    [JsonExtensionData]
    public IDictionary<string, object> AdditionalProperties
    {
      get { return _additionalProperties; }
      set { _additionalProperties = value; }
    }


  }
}
