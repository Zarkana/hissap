using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace HISSAP1.Helpers
{
  public static class ModelStateDictionaryExtensions
  {
    public static void SetModelValue(this ModelStateDictionary modelState, string key, object rawValue)
    {
      modelState.SetModelValue(key, new ValueProviderResult(rawValue, String.Empty, CultureInfo.InvariantCulture));
    }
  }
}