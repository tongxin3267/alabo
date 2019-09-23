﻿using Xunit;
using Alabo.App.Core.ApiStore.AMap.District;

namespace Alabo.Test.Core.ApiStore.AMap
{
    public class DistrictTest
    {
        [Fact]
        public void District_Test()
        {
            var client = new RegionMapClient();
            var result = client.GetMapDistrict();
            Assert.NotNull(result);
            Assert.Equal(1, result.Status);
        }
    }
}