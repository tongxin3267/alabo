using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Xunit;
using Alabo.Core.Enums.Enum;
using Alabo.Core.Reflections.Services;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Test.Base.Core.Model;
using Convert = System.Convert;

namespace Alabo.Test.Generation {

    /// <summary>
    /// 后台所有的枚举数据
    /// </summary>
    public class EnumJson : CoreTest {

        [Fact]
        public void CreateJson() {
            var list = Resolve<ITypeService>().GetEnumList();
            var json = list.ToJsoCamelCase();
        }
    }
}