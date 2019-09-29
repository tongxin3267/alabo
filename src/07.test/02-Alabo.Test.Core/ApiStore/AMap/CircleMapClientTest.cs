using Alabo.Data.People.Circles.Client;
using Alabo.Extensions;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Core.ApiStore.AMap
{
    public class CircleMapClientTest : CoreTest
    {
        [Fact]
        public void District_Test()
        {
            var client = new CircleMapClient();
            var result = client.GetCircleMap();
            Assert.NotNull(result);
        }

        [Fact]
        public void GetCircleList_Init_test()
        {
            Resolve<ICircleService>().Init();
        }

        [Fact]
        public void GetCircleList_test()
        {
            var client = new CircleMapClient();
            var circles = client.GetCircleList();
            Assert.NotNull(circles);
        }

        [Fact]
        public void MapToCircle_Test()
        {
            var client = new CircleMapClient();
            var circles = client.MapToCircle();
            var json = circles.ToJson();
            Assert.NotNull(circles);
        }
    }
}