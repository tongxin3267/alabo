//using System;
//using System.Collections.Generic;
//using Alabo.Domains.Repositories.EFCore;
//using Alabo.Domains.Repositories.Model;
//using System.Linq;
//using Alabo.Domains.Entities;
//using Microsoft.AspNetCore.Mvc;
//using Alabo.App.Core.Api.Filter;
//using Alabo.App.Core.Common;
//using MongoDB.Bson;
//using Alabo.App.Core.Api.Controller;
//using Alabo.App.Core.Tasks;
//using Alabo.App.Core.User;
//using Alabo.App.Share.Share.Domain.Dtos;
//using Alabo.RestfulApi;
//using ZKCloud.Open.ApiBase.Configuration;
//using Alabo.Domains.Services;
//using Alabo.Web.Mvc.Attributes;
//using Alabo.Web.Mvc.Controllers;
//using Alabo.App.Share.Share.Domain.Entities;
//using Alabo.App.Share.Share.Domain.Services;
//using Alabo.Extensions;
//using ZKCloud.Open.ApiBase.Models;
//using Alabo.App.Share.Tasks.Base;
//using Alabo.App.Core.Common.Domain.Services;

//namespace Alabo.App.Share.Share.Controllers {
//    [ApiExceptionFilter]
//    [Route("Api/RewardRule/[action]")]
//    public class ApiRewardRuleController : ApiBaseController<RewardRule, ObjectId> {
//        /// <summary>
//        /// The task manager
//        /// </summary>
//        private TaskManager _taskManager;

//        public ApiRewardRuleController(TaskManager taskManager) : base(
//            ) {
//            BaseService = Resolve<IRewardRuleService>();
//            _taskManager = taskManager;
//        }

//        /// <summary>
//        /// 分润维度
//        /// </summary>
//        /// <param name="key">The key.</param>
//        /// <returns>IActionResult.</returns>
//        public ApiResult<RewardModulesOutput> Modules(string key) {
//            var result = Resolve<IRewardRuleService>().GetModules(key, _taskManager);
//            return ApiResult.Success(result);
//        }

//        /// <summary>
//        /// 快速编辑表单
//        /// </summary>
//        /// <param name="moduleId"></param>
//        /// <returns></returns>
//        public ApiResult<RewardEditSimpleView> GetEditSimpleView([FromQuery] Guid moduleId) {
//            var result = Resolve<IRewardRuleService>().GetEditSimpleView(moduleId);
//            return ToResult<RewardEditSimpleView>(result);
//        }

//        /// <summary>
//        /// 获取详细编辑表单
//        /// </summary>
//        /// <param name="moduleId"></param>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public ApiResult<RewardRuleOutput> GetEditView([FromQuery] Guid moduleId, string id) {
//            var objectId = id.ToObjectId();
//            var result = Resolve<IRewardRuleService>().GetEditView(moduleId, objectId);
//            return ToResult<RewardRuleOutput>(result);
//        }

//        /// <summary>
//        /// Gets the user grade list by user type identifier.
//        /// </summary>
//        /// <param name="userTypeId">The name.</param>
//        /// <returns>IActionResult.</returns>
//        [HttpGet]
//        public ApiResult<IList<KeyValue>> GetUserGradeListByUserTypeId(Guid userTypeId) {
//            IList<KeyValue> list = new List<KeyValue>();
//            var userGradeList = Resolve<IGradeService>().GetGradeListByGuid(userTypeId).OrderBy(e => e.Contribute).ToList();

//            foreach (var item in userGradeList) {
//                KeyValue keyValue = new KeyValue {
//                    Key = item.Id,
//                    Name = item.Name,
//                    Value = item.Name
//                };
//                list.Add(keyValue);
//            }
//            return ApiResult.Success(list);
//        }
//    }
//}