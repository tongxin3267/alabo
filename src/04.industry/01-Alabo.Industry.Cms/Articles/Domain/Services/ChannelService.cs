using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.Admins.Configs;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Domain.Services
{
    public class ChannelService : ServiceBase<Channel, ObjectId>, IChannelService
    {
        public ChannelService(IUnitOfWork unitOfWork, IRepository<Channel, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }

        /// <summary>
        ///     获取频道分类类型
        /// </summary>
        /// <param name="channel"></param>
        public Type GetChannelClassType(Channel channel)
        {
            var fullName =
                $"Alabo.App.Cms.Articles.Domain.CallBacks.Channel{channel.ChannelType.ToString()}ClassRelation";
            // string fullName = $"Alabo.App.Cms.Articles.Domain.CallBacks.ChannelArticleCalssRelation";
            var type = fullName.GetTypeByFullName();
            return type;
        }

        /// <summary>
        ///     获取频道标签类型
        /// </summary>
        /// <param name="channel"></param>
        public Type GetChannelTagType(Channel channel)
        {
            var fullName =
                $"Alabo.App.Cms.Articles.Domain.CallBacks.Channel{channel.ChannelType.ToString()}TagRelation";
            var type = fullName.GetTypeByFullName();
            return type;
        }

        public bool Check(string script)
        {
            // return Repository.IsHavingData(script);
            return false;
        }

        public List<DataField> DataFields(string channelId)
        {
            var channel = GetSingle(r => r.Id == channelId.ToObjectId());
            if (channel != null)
            {
                var list = channel.FieldJson.DeserializeJson<List<DataField>>();
                return list;
            }

            return null;
        }

        public void InitialData()
        {
            var list = GetList();
            if (!list.Any())
            {
                var channelTypeEnum = typeof(ChannelType);
                foreach (var channelTypeName in Enum.GetNames(channelTypeEnum))
                {
                    var channelType = (ChannelType)Enum.Parse(channelTypeEnum, channelTypeName);
                    if (channelType >= 0)
                    {
                        var channel = new Channel
                        {
                            ChannelType = channelType,
                            Name = channelType.GetDisplayName(),
                            Status = Status.Normal,
                            Id = channelType.GetFieldAttribute().GuidId.ToObjectId(),
                            Icon = channelType.GetFieldAttribute().Icon
                        };
                        AddOrUpdate(channel, false);
                    }
                }
            }
        }

        public SideBarType GetSideByTypeByChannel(Channel channel)
        {
            switch (channel.ChannelType)
            {
                case ChannelType.Article:
                    return SideBarType.ArticleSideBarSideBar;

                case ChannelType.Comment:
                    return SideBarType.CommentSideBar;

                case ChannelType.Video:
                    return SideBarType.VideoSideBar;

                case ChannelType.Images:
                    return SideBarType.ImagesSideBar;

                case ChannelType.Down:
                    return SideBarType.DownSideBar;

                case ChannelType.Question:
                    return SideBarType.QuestionSideBar;

                case ChannelType.Help:
                    return SideBarType.HelpSideBar;

                case ChannelType.Job:
                    return SideBarType.JobSideBar;

                case ChannelType.TopLine:
                    return SideBarType.NewsSideBar;

                case ChannelType.Message:
                    return SideBarType.MessageSideBar;

                case ChannelType.UserNotice:
                    return SideBarType.UserNoticeSideBar;
            }

            return SideBarType.ArticleSideBarSideBar;
        }

        public ObjectId GetChannelId(ChannelType channelType)
        {
            switch (channelType)
            {
                case ChannelType.Article:
                    return ChannelType.Article.GetFieldAttribute().GuidId.ToSafeObjectId();

                case ChannelType.Comment:
                    return ChannelType.Comment.GetFieldAttribute().GuidId.ToSafeObjectId();

                case ChannelType.Video:
                    return ChannelType.Video.GetFieldAttribute().GuidId.ToSafeObjectId();

                case ChannelType.Images:
                    return ChannelType.Images.GetFieldAttribute().GuidId.ToSafeObjectId();

                case ChannelType.Down:
                    return ChannelType.Down.GetFieldAttribute().GuidId.ToSafeObjectId();

                case ChannelType.Question:
                    return ChannelType.Question.GetFieldAttribute().GuidId.ToSafeObjectId();

                case ChannelType.Help:
                    return ChannelType.Help.GetFieldAttribute().GuidId.ToSafeObjectId();

                case ChannelType.Job:
                    return ChannelType.Job.GetFieldAttribute().GuidId.ToSafeObjectId();

                case ChannelType.TopLine:
                    return ChannelType.TopLine.GetFieldAttribute().GuidId.ToSafeObjectId();

                case ChannelType.Message:
                    return ChannelType.Message.GetFieldAttribute().GuidId.ToSafeObjectId();

                case ChannelType.UserNotice:
                    return ChannelType.UserNotice.GetFieldAttribute().GuidId.ToSafeObjectId();
            }

            return ChannelType.Article.GetFieldAttribute().GuidId.ToSafeObjectId();
        }
    }
}