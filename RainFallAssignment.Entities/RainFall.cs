using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainFallAssignment.Entities
{
  public class RainFall
  {
    public Uri stringId { get; set; }
    public string easting { get; set; }
    public string gridReference { get; set; }
    public string label { get; set; }
    public float distanct_lat { get; set; }
    public float distance_long { get; set; } 
    public List<RainFallMeasures> measuresEntities { get; set; }
    public int northing { get; set; }
    public string notation { get; set; }
    public string stationReference { get; set; }
  }

}
