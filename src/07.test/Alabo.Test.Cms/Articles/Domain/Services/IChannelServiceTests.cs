using System;
using Alabo.Extensions;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Industry.Cms.Articles.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Cms.Articles.Domain.Services
{
    public class IChannelServiceTests : CoreTest
    {
        [Fact]
        [TestMethod("Check_String")]
        public void Check_String_test()
        {
            //var script = "";
            //var result = Service<IChannelService>().Check(script);
            //Assert.True(result);
        }

        [Fact]
        [TestMethod("DataFields_ObjectId")]
        public void DataFields_ObjectId_test()
        {
            //ObjectId channelId = ObjectId.Empty;
            //var result = Service<IChannelService>().DataFields( channelId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("DataFields_String")]
        public void DataFields_String_test()
        {
            //var channelId = "";
            //var result = Service<IChannelService>().DataFields(channelId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetChannelClassType_Channel")]
        public void GetChannelClassType_Channel_test()
        {
            var channelList = Resolve<IChannelService>().GetList();
            foreach (var item in channelList)
            {
                var result = Resolve<IChannelService>().GetChannelClassType(item);
                Assert.Contains("Class", result.FullName);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void GetChannelClassType_Channel_test_Type()
        {
            foreach (ChannelType item in Enum.GetValues(typeof(ChannelType)))
            {
                if (item == ChannelType.Customer) continue;

                var fullName = $"Alabo.App.Cms.Articles.Domain.CallBacks.Channel{item.ToString()}ClassRelation";
                var type = fullName.GetTypeByFullName();
                Assert.NotNull(type);
            }
        }

        [Fact]
        [TestMethod("GetChannelId_ChannelType")]
        public void GetChannelId_ChannelType_test()
        {
            var channelType = (ChannelType) 0;
            var result = Resolve<IChannelService>().GetChannelId(channelType);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetChannelTagType_Channel")]
        public void GetChannelTagType_Channel_test()
        {
            var channelList = Resolve<IChannelService>().GetList();
            foreach (var item in channelList)
            {
                var result = Resolve<IChannelService>().GetChannelTagType(item);
                Assert.NotNull(result);
                Assert.Contains("Tag", result.FullName);
            }
        }

        [Fact]
        public void GetChannelTagType_Channel_test_Type()
        {
            foreach (ChannelType item in Enum.GetValues(typeof(ChannelType)))
            {
                if (item == ChannelType.Customer) continue;

                var fullName = $"Alabo.App.Cms.Articles.Domain.CallBacks.Channel{item.ToString()}TagRelation";
                var type = fullName.GetTypeByFullName();
                Assert.NotNull(type);
            }
        }

        [Fact]
        [TestMethod("GetSideBarTypeById_ObjectId")]
        public void GetSideBarTypeById_ObjectId_test()
        {
            //var channeId = ObjectId.Empty;
            //var result = Service<IChannelService>().GetSideBarTypeById(channeId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSideBarTypeById_String")]
        public void GetSideBarTypeById_String_test()
        {
            var channelList = Resolve<IChannelService>().GetList();
            foreach (var item in channelList)
            {
                var result = Resolve<IChannelService>().GetSideBarTypeById(item.Id);

                Assert.True(result > 0);
                var fatherId = 9000 + Convert.ToInt32(item.ChannelType);
                //  Assert.Equal(result, fatherId);
            }
        }

        [Fact]
        [TestMethod("GetSideByTypeByChannel_Channel")]
        public void GetSideByTypeByChannel_Channel_test()
        {
            var channelList = Resolve<IChannelService>().GetList();
            foreach (var item in channelList)
            {
                var result = Resolve<IChannelService>().GetSideByTypeByChannel(item);
                Assert.True(Convert.ToInt64(result) > 0);
            }
        }

        [Fact]
        [TestMethod("InitialData")]
        public void InitialData_test()
        {
            Resolve<IChannelService>().InitialData();
        }

        /*end*/
    }
}