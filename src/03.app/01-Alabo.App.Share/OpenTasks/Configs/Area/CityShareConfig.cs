using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Open.Tasks.Base;
using Alabo.App.Open.Tasks.Modules;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Web.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ZKCloud.Open.ApiBase.Models;
using ICityService = Alabo.App.Agent.Citys.Domain.Services.ICityService;

namespace Alabo.App.Open.Tasks.Configs.Area {

    [ShareModules(Name = "市代理分润",
     FenRunResultType = Core.Tasks.Domain.Enums.FenRunResultType.Price,
      Intro = "市代理分润，根据全国661个城市来划分，一个城市所有的订单都会有分润。可配置市代理分润，推荐市代理分润，市代理见点分润等，配置方式灵活多变",
      RelationshipType = Core.Tasks.Domain.Enums.RelationshipType.UserTypeRelationship,
      ConfigHelp = "市代理在后台添加，市代理推荐人也在后台添加。分润比例设置成0.2,0.01 表示市代理分润比例为20%，推荐市代理的分润为1%",
     Id = "8e5968e1-77d9-49f3-b849-85064d305821", BackGround = "bg-green-seagreen", Icon = "icon-social-dribbble", IsIncludeShareModuleConfig = true
      )]
    public class CityShareConfig : ShareBaseConfig {

        /// <summary>
        /// 地址锁定方式
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, DataSource = "Alabo.Framework.Core.Enums.Enum.AddressLockType", ListShow = true, EditShow = true)]
        [Display(Name = "地址锁定方式", Description = "选择地址锁定方式。订单收货地址：在用户下单的时候用户自行填写。会员资料地址：在会员中心修改")]
        public AddressLockType AddressLockType { get; set; }
    }

    [TaskModule(Id, ModelName, SortOrder = 909998, FenRunResultType = Core.Tasks.Domain.Enums.FenRunResultType.Price,
        Intro = "市代理分润，根据全国661个城市来划分，一个城市所有的订单都会有分润。可配置市代理分润，推荐市代理分润，市代理见点分润等，配置方式灵活多变"
       , ConfigurationType = typeof(CityShareConfig), RelationshipType = RelationshipType.UserTypeRelationship, IsSupportMultipleConfiguration = true)]
    public class CityModule : AssetAllocationShareModuleBase<CityShareConfig> {
        public const string Id = "8e5968e1-77d9-49f3-b849-85064d305821";

        public const string ModelName = "市代理分润";

        public CityModule(TaskContext context, CityShareConfig configuration) : base(context, configuration) {
        }

        /// <summary>
        /// 开始执行分润
        /// 对module配置与参数进行基础验证，子类重写后需要显式调用并判定返回值，如返回值不为Success，则不再执行子类后续逻辑
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>ExecuteResult&lt;ITaskResult[]&gt;.</returns>
        public override ExecuteResult<ITaskResult[]> Execute(TaskParameter parameter) {
            var baseResult = base.Execute(parameter);
            if (baseResult.Status != ResultStatus.Success) {
                return ExecuteResult<ITaskResult[]>.Cancel(baseResult.Message);
            }
            //TODO 2019年9月24日 城市代理商分润
            //// 如果触发方式是订单类型，用户触发和其他触发后续支出
            //long? cityRegionId = 0; // 城市区域代理Id

            //// 20190603: TriggerType.Order || TriggerType.Other
            //if (base.Configuration.TriggerType == TriggerType.Order
            //    || base.Configuration.TriggerType == TriggerType.Other) {
            //    var order = Resolve<IOrderService>().GetSingle(ShareOrder.EntityId);
            //    if (order == null) {
            //        return ExecuteResult<ITaskResult[]>.Cancel("未找到订单");
            //    }
            //    var region = new Region(); // 区域
            //    // 按收货地址
            //    if (base.Configuration.AddressLockType == AddressLockType.OrderAddress) {
            //        var userAddress = Resolve<IUserAddressService>().GetSingle(r => r.Id == order.AddressId.ToObjectId());
            //        var regStr = userAddress.RegionId.ToString();
            //        if (regStr.Length >= 4) {
            //            cityRegionId = regStr.Substring(0, 4).ToInt64();
            //        }
            //    }

            //    // 按备案地址
            //    if (base.Configuration.AddressLockType == AddressLockType.UserInfoAddress) {
            //        var orderUser = Resolve<IUserService>().GetUserDetail(order.UserId);
            //        cityRegionId = Resolve<IRegionService>().GetCityId((orderUser.Detail?.RegionId).ConvertToLong());
            //    }

            //    // 按发货人地址分润
            //    if (base.Configuration.AddressLockType == AddressLockType.DeliveryUserAddress) {
            //        var orderUserDetail = Resolve<IUserDetailService>().GetSingle(r => r.UserId == order.DeliverUserId);
            //        if (orderUserDetail != null) {
            //            cityRegionId = Resolve<IRegionService>().GetCityId(orderUserDetail.RegionId)
            //                .ConvertToLong();
            //        }
            //    }
            //}

            //// 获取城市代理
            //IList<ITaskResult> resultList = new List<ITaskResult>();
            ////var cityUserType = Resolve<IUserTypeService>().GetSingle(r => r.UserTypeId == UserTypeEnum.City.GetFieldId() && r.EntityId == cityRegionId);
            //var cityUserType = Resolve<ICityService>().GetSingle(r => r.RegionId == cityRegionId);
            //if (cityUserType == null) {
            //    return ExecuteResult<ITaskResult[]>.Cancel("该区域的城市代理未找到");
            //}
            //// 推荐关系 一二代
            //var maps = new List<ParentMap> {
            //    new ParentMap {
            //        ParentLevel = 1,
            //        UserId = cityUserType.UserId
            //    }
            //};
            //if (cityUserType.ParentUserId != 0) {
            //    maps.Add(new ParentMap {
            //        ParentLevel = 2,
            //        UserId = cityUserType.ParentUserId
            //    });
            //}
            //// 城市代理分润
            //for (var i = 0; i < Ratios.Count; i++) {
            //    if (maps.Count < i + 1) {
            //        break;
            //    }
            //    var item = maps[i];
            //    base.GetShareUser(item.UserId, out var shareUser);//从基类获取分润用户
            //    if (shareUser == null) {
            //        continue;
            //    }

            //    var ratio = Convert.ToDecimal(Ratios[i]);
            //    var shareAmount = BaseFenRunAmount * ratio;//分润金额
            //    CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration, resultList);//构建分润参数
            //}

            // return ExecuteResult<ITaskResult[]>.Success(resultList.ToArray());
            return null;
        }
    }
}