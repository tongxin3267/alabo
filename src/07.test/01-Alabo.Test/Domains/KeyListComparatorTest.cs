﻿using System;
using System.Collections.Generic;
using Xunit;
using ZKCloud.Domains.EntityHistory;
using ZKCloud.Extensions;
using ZKCloud.Test.XUnitHelpers;

namespace ZKCloud.Test.Domains
{
    /// <summary>
    ///     键列表比较器测试
    /// </summary>
    public class KeyListComparatorTest
    {
        /// <summary>
        ///     测试初始化
        /// </summary>
        public KeyListComparatorTest()
        {
            _comparator = new KeyListComparator<Guid>();
        }

        /// <summary>
        ///     键列表比较器
        /// </summary>
        private readonly KeyListComparator<Guid> _comparator;

        /// <summary>
        ///     测试列表比较
        /// </summary>
        [Fact]
        public void TestCompare()
        {
            var id = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();
            var id4 = Guid.NewGuid();
            var newList = new List<Guid>
            {
                id, id2, id3
            };
            var oldList = new List<Guid>
            {
                id2, id3, id4
            };
            var result = newList.Compare(oldList);
            Assert.Single(result.CreateList);
            Assert.Equal(id, result.CreateList[0]);

            Assert.Single(result.DeleteList);
            Assert.Equal(id4, result.DeleteList[0]);

            Assert.Equal(2, result.UpdateList.Count);
            Assert.Equal(id2, result.UpdateList[0]);
            Assert.Equal(id3, result.UpdateList[1]);
        }

        /// <summary>
        ///     测试列表比较 - 获取创建列表
        /// </summary>
        [Fact]
        public void TestCompare_CreateList()
        {
            var id = Guid.NewGuid();
            var newList = new List<Guid>
            {
                id
            };
            var result = _comparator.Compare(newList, new List<Guid>());
            Assert.Single(result.CreateList);
            Assert.Equal(id, result.CreateList[0]);
        }

        /// <summary>
        ///     测试列表比较 - 获取删除列表
        /// </summary>
        [Fact]
        public void TestCompare_DeleteList()
        {
            var id = Guid.NewGuid();
            var oldList = new List<Guid>
            {
                id
            };
            var result = _comparator.Compare(new List<Guid>(), oldList);
            Assert.Empty(result.CreateList);
            Assert.Single(result.DeleteList);
            Assert.Equal(id, result.DeleteList[0]);
        }

        /// <summary>
        ///     测试列表比较 - 获取更新列表
        /// </summary>
        [Fact]
        public void TestCompare_UpdateList()
        {
            var id = Guid.NewGuid();
            var newList = new List<Guid>
            {
                id
            };
            var oldList = new List<Guid>
            {
                id
            };
            var result = _comparator.Compare(newList, oldList);
            Assert.Empty(result.CreateList);
            Assert.Empty(result.DeleteList);
            Assert.Single(result.UpdateList);
            Assert.Equal(id, result.UpdateList[0]);
        }

        /// <summary>
        ///     测试列表比较 - 验证集合为空
        /// </summary>
        [Fact]
        public void TestCompare_Validate()
        {
            var list = new List<Guid>();
            AssertHelper.Throws<ArgumentNullException>(() => { _comparator.Compare(null, list); });
            AssertHelper.Throws<ArgumentNullException>(() => { _comparator.Compare(list, null); });
            AssertHelper.Throws<ArgumentNullException>(() => { list.Compare(null); });
        }
    }
}