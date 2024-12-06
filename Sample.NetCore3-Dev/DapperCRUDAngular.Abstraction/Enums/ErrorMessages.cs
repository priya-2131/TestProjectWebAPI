using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace DapperCRUDAngular.Abstraction.Enums
{
    public static class ErrorMessages
    {
        public static string GetDescription(this Enum enumValue)
        {
            return enumValue.GetType()
                       .GetMember(enumValue.ToString())
                       .First()
                       .GetCustomAttribute<DescriptionAttribute>()?
                       .Description ?? string.Empty;
        }
        public enum enumErrorMessages
        {
            [Description("{0} Is Required")]
            Required_Field_Validation = 1,
        }
    }


}
