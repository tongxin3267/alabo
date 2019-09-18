using System.IO;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;
using ZKCloud.Core.Files;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Files
{
    public class DirectoryHelperTests : CoreTest
    {
        [Fact]
        public void CreateIfNotExists_StateUnderTest_ExpectedBehavior()
        {
            var path = @"D:\Test ZkCloud\Common Files\Services\011\0254\012\0454";
            DirectoryHelper.CreateIfNotExists(path);
            Assert.True(Directory.Exists(path));
            Directory.Delete(path);
            Assert.False(Directory.Exists(path));
        }

        [Fact]
        public void GetAllDirectory_StateUnderTest_ExpectedBehavior()
        {
        }

        [Fact]
        public void GetAllDirectoryTest()
        {
            var path = FileHelper.WwwRootPath;

            var paths = DirectoryHelper.GetAllDirectory(path);
            Assert.NotNull(paths);
        }

        [Fact]
        public void GetPathArray_StateUnderTest_ExpectedBehavior()
        {
            var path = @"C:\Program Files\IIS\Microsoft Web Deploy V3\de";
            var list = DirectoryHelper.PathConvertToList(path);
            Assert.NotNull(list);
            Assert.True(list.Any());
            Assert.Equal("Program Files", list[1]);
            Assert.Equal(5, list.Count);
        } /*end*/
    }
}