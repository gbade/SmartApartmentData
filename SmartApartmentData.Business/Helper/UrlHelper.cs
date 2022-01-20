using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SmartApartmentData.Business.Helper
{
    public static class UrlHelper
    {
        public static string ToUrlQueryString(this object request, string separator = ",")
        {
            if (request == null)
                throw new ArgumentNullException("request");

            
            var properties = request.GetType().GetProperties()
                .Where(x => x.GetIndexParameters().Length == 0)
                .Where(x => x.CanRead)
                .Where(x => x.GetValue(request, null) != null)
                .ToDictionary(x => x.Name, x => x.GetValue(request, null));

            
            var propertyNames = properties
                .Where(x => !(x.Value is string) && x.Value is IEnumerable)
                .Select(x => x.Key)
                .ToList();

            
            foreach (var key in propertyNames)
            {
                var valueType = properties[key].GetType();
                var valueElemType = valueType.IsGenericType
                                        ? valueType.GetGenericArguments()[0]
                                        : valueType.GetElementType();
                
                if (valueElemType != null
                    && (valueElemType.IsPrimitive || valueElemType == typeof(string)))
                {
                    var enumerable = properties[key] as IEnumerable;
                    properties[key] = string.Join(separator, enumerable.Cast<object>());
                }
            }

            
            return string.Join(
                "&", properties
                .Where(x => !string.IsNullOrEmpty(x.Value.ToString())
                    && !x.Value.ToString().Equals("0"))
                .Select(x => string.Concat(
                    "q=",
                    Uri.EscapeDataString(x.Key), ":",
                    Uri.EscapeDataString(EscapeSpecialChar(x.Value.ToString())))
                )
           );
        }

        private static string EscapeSpecialChar(string str)
        {
            char c = str[0];
            var isSpecialChar = (c == '-' || c == '*' || c == '+');
            if (isSpecialChar)
                str = str.Insert(0, "\\");

            return str;
        }
    }
}
