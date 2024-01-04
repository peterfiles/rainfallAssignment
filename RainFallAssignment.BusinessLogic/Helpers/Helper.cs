using Newtonsoft.Json;
using RainFallAssignment.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RainFallAssignment.BusinessLogic.Helpers
{
  public class Helper
  {

    public bool ReadResponseAsString { get; set; }
    public async virtual Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(HttpResponseMessage response, IReadOnlyDictionary<string, IEnumerable<string>> headers, JsonSerializerSettings settings)
    {
      if (response == null || response.Content == null)
      {
        return new ObjectResponseResult<T>(default(T), string.Empty);
      }

      if (ReadResponseAsString)
      {
        var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        try
        {
          var typedBody = JsonConvert.DeserializeObject<T>(responseText, settings);
          return new ObjectResponseResult<T>(typedBody, responseText);
        }
        catch (JsonException exception)
        {
          var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
          throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
        }
      }
      else
      {
        try
        {
          using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
          using (var streamReader = new StreamReader(responseStream))
          using (var jsonTextReader = new JsonTextReader(streamReader))
          {
            var serializer = JsonSerializer.Create(settings);
            var typedBody = serializer.Deserialize<T>(jsonTextReader);
            return new ObjectResponseResult<T>(typedBody, string.Empty);
          }
        }
        catch (JsonException exception)
        {
          var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
          throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
        }
      }
    }
    public string ConvertToString(object value, CultureInfo cultureInfo)
    {
      if (value is Enum)
      {
        string name = Enum.GetName(value.GetType(), value);
        if (name != null)
        {
          var field = IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
          if (field != null)
          {
            var attribute = CustomAttributeExtensions.GetCustomAttribute(field, typeof(EnumMemberAttribute))
                as EnumMemberAttribute;
            if (attribute != null)
            {
              return attribute.Value != null ? attribute.Value : name;
            }
          }
        }
      }
      else if (value is bool)
      {
        return Convert.ToString(value, cultureInfo).ToLowerInvariant();
      }
      else if (value is byte[])
      {
        return Convert.ToBase64String((byte[])value);
      }
      else if (value != null && value.GetType().IsArray)
      {
        var array = Enumerable.OfType<object>((Array)value);
        return string.Join(",", Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
      }

      return Convert.ToString(value, cultureInfo);
    }
  }
}
