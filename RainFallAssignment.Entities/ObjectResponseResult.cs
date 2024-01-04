using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainFallAssignment.Entities
{
  public struct ObjectResponseResult<T>
  {
    public ObjectResponseResult(T responseObject, string responseText)
    {
      this.Object = responseObject;
      this.Text = responseText;
    }

    public T Object { get; }

    public string Text { get; }
  }
}
