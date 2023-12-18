using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RainFallAssignment.Entities
{
  public class RainFallMeasures
  {
    public Uri stringId { get; set; } 
    public string parameter { get; set; }
    public string parameterName { get; set; }
    public int period { get; set; } 
    public string qualifier { get; set; } 
    public string unitName { get; set; }  
  }
}
