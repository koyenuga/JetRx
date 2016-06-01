using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JetRx.Entities;

namespace JetRx.Common.Extensions
{
    public static class HelperExtension
    {
      
        public static void SetDefaultValue<T>(this T obj, object defaultValue)
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                var propval = prop.GetValue(obj, null);
                if (prop.PropertyType == typeof(string))
                {
                    if(string.IsNullOrWhiteSpace(propval as string))
                    {
                        if (prop.CanWrite)
                            prop.SetValue(obj, defaultValue, null);
                    }
                }
                else
                {
                    if (propval == null)
                    {
                        if (prop.CanWrite)
                            prop.SetValue(obj, defaultValue, null);
                    }
                }
            }
        }
    }
}
