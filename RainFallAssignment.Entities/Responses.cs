using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainFallAssignment.Entities
{
  public partial class Responses
  {
    private IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

    [JsonExtensionData]
    public IDictionary<string, object> AdditionalProperties
    {
      get { return _additionalProperties; }
      set { _additionalProperties = value; }
    }


  }
}
