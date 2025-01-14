﻿using System.Collections.Generic;
using Xunit;
using ZKCloud.Helpers;
using ZKCloud.Maps;
using ZKCloud.Test.Samples;

namespace ZKCloud.Test.Maps {
    /// <summary>
    /// 对象映射测试
    /// </summary>
    public class MapTest {
        /// <summary>
        /// 测试映射
        /// </summary>
        [Fact]
        public void TestMapTo() {
            Sample sample = new Sample();
            Sample2 sample2 = new Sample2 { StringValue = "a" };
            sample2.MapTo( sample );
            Assert.Equal( "a", sample.StringValue );
        }

        /// <summary>
        /// 测试映射
        /// </summary>
        [Fact]
        public void TestMapTo_2() {
            Sample sample = new Sample { StringValue = "a" };
            Sample2 sample2 = sample.MapTo<Sample2>();
            Assert.Equal( "a", sample2.StringValue );
        }

        /// <summary>
        /// 测试映射集合
        /// </summary>
        [Fact]
        public void TestMapTo_List() {
            List<Sample> sampleList = new List<Sample> { new Sample { StringValue = "a" }, new Sample { StringValue = "b" } };
            List<Sample2> sample2List = new List<Sample2>();
            sampleList.MapTo( sample2List );
            Assert.Equal( 2, sample2List.Count );
            Assert.Equal( "a", sample2List[0].StringValue );
        }

        /// <summary>
        /// 测试映射集合
        /// </summary>
        [Fact]
        public void TestMapTo_List_2() {
            List<Sample> sampleList = new List<Sample> { new Sample { StringValue = "a" }, new Sample { StringValue = "b" } };
            List<Sample2> sample2List = sampleList.MapTo<List<Sample2>>();
            Assert.Equal( 2, sample2List.Count );
            Assert.Equal( "a", sample2List[0].StringValue );
        }

        /// <summary>
        /// 测试映射集合
        /// </summary>
        [Fact]
        public void TestMapToList() {
            List<Sample> sampleList = new List<Sample> { new Sample { StringValue = "a" }, new Sample { StringValue = "b" } };
            List<Sample2> sample2List = sampleList.MapToList<Sample2>();
            Assert.Equal( 2, sample2List.Count );
            Assert.Equal( "a", sample2List[0].StringValue );
        }

        /// <summary>
        /// 映射集合 - 测试空集合
        /// </summary>
        [Fact]
        public void TestMapToList_Empty() {
            List<Sample> sampleList = new List<Sample>();
            List<Sample2> sample2List = sampleList.MapToList<Sample2>();
            Assert.Empty( sample2List );
        }

        /// <summary>
        /// 映射集合 - 测试数组
        /// </summary>
        [Fact]
        public void TestMapToList_Array() {
            Sample[] sampleList = new Sample[] { new Sample { StringValue = "a" }, new Sample { StringValue = "b" } };
            List<Sample2> sample2List = sampleList.MapToList<Sample2>();
            Assert.Equal( 2, sample2List.Count );
            Assert.Equal( "a", sample2List[0].StringValue );
        }

        /// <summary>
        /// 并发测试
        /// </summary>
        [Fact]
        public void TestMapTo_MultipleThread() {
            Thread.WaitAll( () => {
                var sample = new Sample { StringValue = "a" };
                var sample2 = sample.MapTo<Sample2>();
                Assert.Equal( "a", sample2.StringValue );
            },() => {
                var sample = new DtoSample { Name = "a" };
                var sample2 = sample.MapTo<EntitySample>();
                Assert.Equal( "a", sample2.Name );
            }, () => {
                var sample = new Sample2 { StringValue = "a" };
                var sample2 = sample.MapTo<Sample>();
                Assert.Equal( "a", sample2.StringValue );
            }, () => {
                var sample = new EntitySample{ Name = "a" };
                var sample2 = sample.MapTo<DtoSample>();
                Assert.Equal( "a", sample2.Name );
            } );
        }

        /// <summary>
        /// 测试忽略特性
        /// </summary>
        [Fact]
        public void TestMapTo_Ignore() {
            DtoSample sample2 = new DtoSample { Name = "a",IgnoreValue = "b"};
            EntitySample sample = sample2.MapTo<EntitySample>();
            Assert.Equal( "a", sample.Name );
            Assert.Null( sample.IgnoreValue );
            DtoSample sample3 = sample.MapTo<DtoSample>();
            Assert.Equal( "a", sample3.Name );
            Assert.Null( sample3.IgnoreValue );
        }

        /// <summary>
        /// 测试Castle代理类
        /// </summary>
        [Fact]
        public void TestMapTo_CastleProxy() {
            Castle.DynamicProxy.ProxyGenerator proxyGenerator = new Castle.DynamicProxy.ProxyGenerator();
            var proxy = proxyGenerator.CreateClassProxy<DtoSample>();
            proxy.Name = "a";
            EntitySample sample = proxy.MapTo<EntitySample>();
            Assert.Equal( "a", sample.Name );

            var proxy2 = proxyGenerator.CreateClassProxy<DtoSample>();
            proxy2.Name = "b";
            sample = proxy2.MapTo<EntitySample>();
            Assert.Equal( "b", sample.Name );
        }
    }
}
