using RainFallAssignment.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainFallAssignment.BusinessLogic.BaseService
{
  public partial class ApiExceptionBase<TResult> : ApiException
  {
    public TResult Result { get; private set; }

    public ApiExceptionBase(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException)
        : base(message, statusCode, response, headers, innerException)
    {
      Result = result;
    }
  }
}
