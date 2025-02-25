﻿using Alabo.Cloud.People.GradeInfos.Domain.Servcies;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.People.Users.ViewModels.Admin;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.Grades.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Cloud.People.GradeInfos.Domain.Entities;

namespace Alabo.Cloud.Reports.UserRecommend.Services {

    public class UserRecommendService : ServiceBase, IUserRecommendService {

        public UserRecommendService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary>
        ///     获取直推会员、间推、团队的等级分布
        /// </summary>
        /// <param name="query"></param>
        public PagedList<UserGradeInfoView> GetUserGradeInfoPageList(object query) {
            var pageList = Resolve<IGradeInfoService>().GetPagedList(query);
            var dictionary = query.DeserializeJson<Dictionary<string, string>>();
            dictionary.TryGetValue("type", out var type);
            var gradeInfList = new List<UserGradeInfoView>();
            var userIds = pageList.Select(r => r.UserId).Distinct().ToList();
            var users = Resolve<IUserService>().GetList(userIds);
            foreach (var gradeInfo in pageList) {
                var user = users.FirstOrDefault(r => r.Id == gradeInfo.UserId);
                if (user == null) {
                    continue;
                }

                var view = new UserGradeInfoView();
                //  AutoMapping.SetValue(gradeInfo, view);
                view.Id = gradeInfo.UserId; //Id=用户Id
                view.GradeName = Resolve<IGradeService>().GetGrade(user.GradeId)?.Name;
                view.UserName = gradeInfo.UserName;

                if (type == "recomend") {
                    var result = GetDictionary(gradeInfo.RecomendGradeInfo);
                    view.GradeInfo = result.Item1;
                    view.GradeInfoString = result.Item2;
                    view.TotalCountString = $"直推总数{gradeInfo.RecomendCount}";
                }

                if (type == "second") {
                    var result = GetDictionary(gradeInfo.SecondGradeInfo);
                    view.GradeInfo = result.Item1;
                    view.GradeInfoString = result.Item2;
                    view.TotalCountString = $"间推总数{gradeInfo.SecondCount}";
                }

                if (type == "team") {
                    var result = GetDictionary(gradeInfo.TeamGradeInfo);
                    view.GradeInfo = result.Item1;
                    view.GradeInfoString = result.Item2;
                    view.TotalCountString = $"团队总数{gradeInfo.TeamCount}";
                }

                view.DisplayName = $"直推会员{gradeInfo.RecomendCount}个";
                view.ModifiedTime = gradeInfo.ModifiedTime;
                gradeInfList.Add(view);
            }

            return PagedList<UserGradeInfoView>.Create(gradeInfList, pageList.RecordCount, pageList.PageSize,
                pageList.PageIndex);
        }

        private Tuple<Dictionary<Guid, long>, string> GetDictionary(IEnumerable<GradeInfoItem> gradeInfo) {
            var dictionary = new Dictionary<Guid, long>();
            var str = string.Empty;
            if (gradeInfo != null) {
                gradeInfo.Foreach(r => {
                    var grade = Resolve<IGradeService>().GetGrade(r.GradeId);
                    dictionary.Add(r.GradeId, r.Count);
                    str += $"{grade.Name}({r.Count})  ";
                });
            }

            str = $"<code>{str}</code>";
            return Tuple.Create(dictionary, str);
        }
    }
}