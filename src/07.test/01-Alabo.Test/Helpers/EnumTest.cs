﻿using System;
using Xunit;
using ZKCloud.Helpers;
using ZKCloud.Test.Samples;
using ZKCloud.Test.XUnitHelpers;

namespace ZKCloud.Test.Helpers
{
    /// <summary>
    ///     测试枚举操作
    /// </summary>
    public class EnumTest
    {
        /// <summary>
        ///     测试获取枚举实例
        /// </summary>
        [Theory]
        [InlineData("C", EnumSample.C)]
        [InlineData("3", EnumSample.C)]
        public void TestParse(string memeber, EnumSample sample)
        {
            Assert.Equal(sample, Enums.Parse<EnumSample>(memeber));
        }

        /// <summary>
        ///     测试获取枚举实例 - 参数为空,抛出异常
        /// </summary>
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void TestParse_MemberIsEmpty(string member)
        {
            AssertHelper.Throws<ArgumentNullException>(() => { Enums.Parse<EnumSample>(member); }, "member");
        }

        /// <summary>
        ///     测试获取枚举实例 - 可空枚举
        /// </summary>
        [Theory]
        [InlineData(null, null)]
        [InlineData("", null)]
        [InlineData(" ", null)]
        [InlineData("C", EnumSample.C)]
        [InlineData("3", EnumSample.C)]
        public void TestParse_Nullable(string memeber, EnumSample? sample)
        {
            Assert.Equal(sample, Enums.Parse<EnumSample?>(memeber));
        }

        /// <summary>
        ///     测试获取枚举成员名
        /// </summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("C", "C")]
        [InlineData(3, "C")]
        [InlineData(EnumSample.C, "C")]
        public void TestGetName(object member, string name)
        {
            Assert.Equal(name, Enums.GetName<EnumSample>(member));
        }

        /// <summary>
        ///     测试获取枚举成员名 - 可空枚举
        /// </summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("C", "C")]
        [InlineData(3, "C")]
        [InlineData(EnumSample.C, "C")]
        public void TestGetName_Nullable(object member, string name)
        {
            Assert.Equal(name, Enums.GetName<EnumSample?>(member));
        }

        /// <summary>
        ///     测试获取枚举成员值
        /// </summary>
        [Theory]
        [InlineData("C", 3)]
        [InlineData(3, 3)]
        [InlineData(EnumSample.C, 3)]
        public void TestGetValue(object member, int value)
        {
            Assert.Equal(value, Enums.GetValue<EnumSample>(member));
        }

        /// <summary>
        ///     测试获取枚举成员值 - 可空枚举
        /// </summary>
        [Theory]
        [InlineData("C", 3)]
        [InlineData(3, 3)]
        [InlineData(EnumSample.C, 3)]
        public void TestGetValue_Nullable(object member, int value)
        {
            Assert.Equal(value, Enums.GetValue<EnumSample?>(member));
        }

        /// <summary>
        ///     测试获取枚举描述
        /// </summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("A", "A")]
        [InlineData("B", "B2")]
        [InlineData(2, "B2")]
        [InlineData(EnumSample.B, "B2")]
        public void TestGetDescription(object member, string description)
        {
            Assert.Equal(description, Enums.GetDescription<EnumSample>(member));
        }

        /// <summary>
        ///     测试获取枚举描述 - 可空枚举
        /// </summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("A", "A")]
        [InlineData("B", "B2")]
        [InlineData(2, "B2")]
        [InlineData(EnumSample.B, "B2")]
        public void TestGetDescription_Nullable(object member, string description)
        {
            Assert.Equal(description, Enums.GetDescription<EnumSample?>(member));
        }

        /// <summary>
        ///     测试获取项集合
        /// </summary>
        [Fact]
        public void TestGetItems()
        {
            var items = Enums.GetItems<EnumSample>();
            Assert.Equal(5, items.Count);
            Assert.Equal("A", items[0].Text);
            Assert.Equal(1, items[0].Value);
            Assert.Equal("D4", items[3].Text);
            Assert.Equal(4, items[3].Value);
            Assert.Equal("E5", items[4].Text);
            Assert.Equal(5, items[4].Value);
        }

        /// <summary>
        ///     测试获取项集合 - 可空枚举
        /// </summary>
        [Fact]
        public void TestGetItems_Nullable()
        {
            var items = Enums.GetItems<EnumSample?>();
            Assert.Equal(5, items.Count);
            Assert.Equal("A", items[0].Text);
            Assert.Equal(1, items[0].Value);
            Assert.Equal("D4", items[3].Text);
            Assert.Equal(4, items[3].Value);
            Assert.Equal("E5", items[4].Text);
            Assert.Equal(5, items[4].Value);
        }

        /// <summary>
        ///     测试获取项集合 - 可空枚举
        /// </summary>
        [Fact]
        public void TestGetItems_Nullable_Type()
        {
            var items = Enums.GetItems(typeof(EnumSample?));
            Assert.Equal(5, items.Count);
            Assert.Equal("A", items[0].Text);
            Assert.Equal(1, items[0].Value);
            Assert.Equal("D4", items[3].Text);
            Assert.Equal(4, items[3].Value);
            Assert.Equal("E5", items[4].Text);
            Assert.Equal(5, items[4].Value);
        }

        /// <summary>
        ///     测试获取项集合
        /// </summary>
        [Fact]
        public void TestGetItems_Type()
        {
            var items = Enums.GetItems(typeof(EnumSample));
            Assert.Equal(5, items.Count);
            Assert.Equal("A", items[0].Text);
            Assert.Equal(1, items[0].Value);
            Assert.Equal("D4", items[3].Text);
            Assert.Equal(4, items[3].Value);
            Assert.Equal("E5", items[4].Text);
            Assert.Equal(5, items[4].Value);
        }

        /// <summary>
        ///     测试获取项集合 - 验证枚举类型
        /// </summary>
        [Fact]
        public void TestGetItems_Validate()
        {
            AssertHelper.Throws<InvalidOperationException>(() => { Enums.GetItems<Sample>(); },
                "类型 Util.Tests.Samples.Sample 不是枚举");
        }

        /// <summary>
        ///     测试获取枚举成员名 - 验证传入的枚举参数并非枚举类型
        /// </summary>
        [Fact]
        public void TestGetName_Validate()
        {
            Assert.Equal(string.Empty, Enums.GetName(typeof(Sample), 3));
        }

        /// <summary>
        ///     测试获取枚举成员值 - 验证
        /// </summary>
        [Fact]
        public void TestGetValue_Validate()
        {
            AssertHelper.Throws<ArgumentNullException>(() => Enums.GetValue<EnumSample>(null), "member");
            AssertHelper.Throws<ArgumentNullException>(() => Enums.GetValue<EnumSample>(string.Empty), "member");
            AssertHelper.Throws<ArgumentNullException>(() => Enums.GetValue<Sample>(string.Empty), "member");
        }
    }
}