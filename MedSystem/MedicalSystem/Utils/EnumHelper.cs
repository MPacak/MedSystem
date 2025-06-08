using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicalSystem.Utils
{
    public static class EnumHelper
    {
        public static List<SelectListItem> GetEnumSelectList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new SelectListItem
                {
                    Value = e.ToString(),
                    Text = e.GetDescription()
                })
                .ToList();
        }

        public static string GetDescription<T>(this T enumValue) where T : Enum
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            var attribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                     .FirstOrDefault() as DescriptionAttribute;
            return attribute?.Description ?? enumValue.ToString();
        }
    }
}
