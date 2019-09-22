using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.Enums.Enum
{
    [ClassProperty(Name = "Api传参类型")]
    public enum ApiMethodType
    {
        HttpPost = 1,
        HttpGet = 2,
        HttpDelete = 3,
        HttpPut = 4
    }
}