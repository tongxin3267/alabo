using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.Framework.Core.Admins.Configs;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;

namespace Alabo.App.Cms.Articles.Domain.Services {

    public interface IChannelService : IService<Channel, ObjectId> {

        List<DataField> DataFields(string channelId);

        /// <summary>
        /// Gets the side by type by channel.
        /// </summary>
        /// <param name="channel">The channel.</param>

        SideBarType GetSideByTypeByChannel(Channel channel);

        bool Check(string script);

        void InitialData();

        /// <summary>
        ///     根据频道Id获取类型
        /// </summary>
        /// <param name="channeId"></param>
        SideBarType GetSideBarTypeById(ObjectId channeId);

        /// <summary>
        /// 获取频道分类类型
        /// </summary>
        /// <param name="channel"></param>
        Type GetChannelClassType(Channel channel);

        /// <summary>
        /// 获取频道标签类型
        /// </summary>
        /// <param name="channel"></param>

        Type GetChannelTagType(Channel channel);

        /// <summary>
        /// 根据频道获取Id
        /// </summary>
        /// <param name="channelType"></param>

        ObjectId GetChannelId(ChannelType channelType);
    }
}