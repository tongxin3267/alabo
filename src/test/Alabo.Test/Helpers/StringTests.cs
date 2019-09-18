using System;
using System.Collections.Generic;
using Xunit;
using String = ZKCloud.Core.Helpers.String;

namespace ZKCloud.Test.Helpers
{
    public class StringTests
    {
        /// <summary>
        ///     ���Ի�ȡƴ������
        /// </summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData("�й�", "zg")]
        [InlineData("a1����b2", "a1bcb2")]
        [InlineData("����", "tt")]
        [InlineData("��", "y")]
        public void TestPinYin(string input, string result)
        {
            Assert.Equal(result, String.PinYin(input));
        }

        /// <summary>
        ///     ����ĸСд
        /// </summary>
        [Theory]
        [InlineData(null, "")]
        [InlineData("", "")]
        [InlineData(" ", "")]
        [InlineData("a", "a")]
        [InlineData("A", "a")]
        [InlineData("Ab", "ab")]
        [InlineData("AB", "aB")]
        [InlineData("Abc", "abc")]
        public void TestFirstLowerCase(string value, string result)
        {
            Assert.Equal(result, String.FirstLowerCase(value));
        }

        /// <summary>
        ///     ���Խ���������Ϊ���ָ������ַ���
        /// </summary>
        [Fact]
        public void TestJoin()
        {
            Assert.Equal("1,2,3", String.Join(new List<int> {1, 2, 3}));
            Assert.Equal("'1','2','3'", String.Join(new List<int> {1, 2, 3}, "'"));
            Assert.Equal("123", String.Join(new List<int> {1, 2, 3}, "", ""));
            Assert.Equal("\"1\",\"2\",\"3\"", String.Join(new List<int> {1, 2, 3}, "\""));
            Assert.Equal("1 2 3", String.Join(new List<int> {1, 2, 3}, "", " "));
            Assert.Equal("1;2;3", String.Join(new List<int> {1, 2, 3}, "", ";"));
            Assert.Equal("1,2,3", String.Join(new List<string> {"1", "2", "3"}));
            Assert.Equal("'1','2','3'", String.Join(new List<string> {"1", "2", "3"}, "'"));

            var list = new List<Guid>
            {
                new Guid("83B0233C-A24F-49FD-8083-1337209EBC9A"),
                new Guid("EAB523C6-2FE7-47BE-89D5-C6D440C3033A")
            };
            Assert.Equal("83B0233C-A24F-49FD-8083-1337209EBC9A,EAB523C6-2FE7-47BE-89D5-C6D440C3033A".ToLower(),
                String.Join(list));
            Assert.Equal("'83B0233C-A24F-49FD-8083-1337209EBC9A','EAB523C6-2FE7-47BE-89D5-C6D440C3033A'".ToLower(),
                String.Join(list, "'"));
        } /*end*/
    }
}