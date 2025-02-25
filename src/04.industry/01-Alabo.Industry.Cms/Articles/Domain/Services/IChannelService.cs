﻿using System;
using System.Collections.Generic;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Framework.Core.Admins.Configs;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Domain.Services {

    public interface IChannelService : IService<Channel, ObjectId> {

        List<DataField> DataFields(string channelId);

        bool Check(string script);

        void InitialData();

        /// <summary>
        ///     获取频道分类类型
        /// </summary>
        /// <param name="channel"></param>
        Type GetChannelClassType(Channel channel);

        /// <summary>
        ///     获取频道标签类型
        /// </summary>
        /// <param name="channel"></param>
        Type GetChannelTagType(Channel channel);

        /// <summary>
        ///     根据频道获取Id
        /// </summary>
        /// <param name="channelType"></param>
        ObjectId GetChannelId(ChannelType channelType);
    }
}