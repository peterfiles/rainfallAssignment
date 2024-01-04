using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainFallAssignment.Entities
{
  public class RainfallReading
  {
    [JsonProperty("dateMeasured", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public System.DateTimeOffset DateMeasured { get; set; }

    [JsonProperty("amountMeasured", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public decimal AmountMeasured { get; set; }

    private IDictionary<string, object> _additionalProperties = new Dictionary<string, object>();

    [JsonExtensionData]
    public IDictionary<string, object> AdditionalProperties
    {
      get { return _additionalProperties; }
      set { _additionalProperties = value; }
    }


  }
}
