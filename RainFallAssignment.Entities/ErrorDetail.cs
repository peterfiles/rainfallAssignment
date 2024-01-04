using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainFallAssignment.Entities
{
  public  class ErrorDetail
  {
    [JsonProperty("propertyName", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public string PropertyName { get; set; }

    [JsonProperty("message", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
    public string Message { get; set; }

    private IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [JsonExtensionData]
    public IDictionary<string, object> AdditionalProperties
    {
      get { return _additionalProperties; }
      set { _additionalProperties = value; }
    }


  }
}
