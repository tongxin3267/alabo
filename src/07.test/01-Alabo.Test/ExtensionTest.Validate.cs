﻿using System;
using System.Collections.Generic;
using Xunit;
using ZKCloud.Extensions;

namespace ZKCloud.Test
{
    /// <summary>
    ///     扩展测试 - 验证
    /// </summary>
    public partial class ExtensionTest
    {
        /// <summary>
        ///     测试是否空值 - 字符串
        /// </summary>
        [Theory]
        [InlineData(null, true)]
        [InlineData("", true)]
        [InlineData(" ", true)]
        [InlineData("a", false)]
        public void TestIsEmpty_String(string value, bool result)
        {
            Assert.Equal(value.IsEmpty(), result);
        }

        /// <summary>
        ///     检查空值，不为空则正常执行
        /// </summary>
        [Fact]
        public void TestCheckNull()
        {
            var test = new object();
            test.CheckNull("test");
        }

        /// <summary>
        ///     测试是否空值 - Guid
        /// </summary>
        [Fact]
        public void TestIsEmpty_Guid()
        {
            Assert.True(Guid.Empty.IsEmpty());
            Assert.False(Guid.NewGuid().IsEmpty());
        }

        /// <summary>
        ///     测试是否空值 - 可空Guid
        /// </summary>
        [Fact]
        public void TestIsEmpty_Guid_Nullable()
        {
            Guid? value = null;
            Assert.True(value.IsEmpty());
            value = Guid.Empty;
            Assert.True(value.IsEmpty());
            value = Guid.NewGuid();
            Assert.False(value.IsEmpty());
        }

        /// <summary>
        ///     测试是否空值 - 集合
        /// </summary>
        [Fact]
        public void TestIsEmpty_List()
        {
            List<int> list = null;
            Assert.True(list.IsEmpty());
            list = new List<int>();
            Assert.True(list.IsEmpty());
            list.Add(1);
            Assert.False(list.IsEmpty());
        }
    }
}