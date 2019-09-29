using Alabo.Extensions;
using Alabo.Framework.Core.Reflections.Services;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Generation
{
    /// <summary>
    ///     后台所有的枚举数据
    /// </summary>
    public class EnumJson : CoreTest
    {
        [Fact]
        public void CreateJson()
        {
            var list = Resolve<ITypeService>().GetEnumList();
            var json = list.ToJsoCamelCase();
        }
    }
}