using Xunit;
using ZKCloud.Reflections;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Reflections
{
    public class ReflectionServicesTest : CoreTest
    {
        /// <summary>
        ///     获取所有的服务
        /// </summary>
        [Fact]
        public void GetAllServices()
        {
            var serviceTypes = Reflection.GetAllServices();
            Assert.NotNull(serviceTypes);
        }
    }
}