//
//namespace Alabo.App.Open.Tasks.Configs.Range {
//    public class FixedAmountRangeConfig : ShareBaseConfig {
//    }
//
//    [TaskModule(Id, ModelName, SortOrder = 999999, ConfigurationType = typeof(FixedAmountRangeConfig), IsSupportMultipleConfiguration = true,
//         FenRunResultType = Core.Tasks.Domain.Enums.FenRunResultType.Price,
//         Intro = "不同的等级之间提成比例会有所不同，比如总监A的提成比例为30%，经理B的提成比例为20%,业务员的提成比例为10%。那么经理可以拿到业务员的级差为10%，总监可以拿到经理的级差为10%，业务员的级差为20%",
//         RelationshipType = RelationshipType.UserRecommendedRelationship)]
//    public class FixedAmountRangeModule : AssetAllocationShareModuleBase<FixedAmountRangeConfig> {
//        public const string Id = "23B9A7D3-6B42-4915-816F-D1208CC7CECB";
//
//        public const string ModelName = "团队级差收益";
//
//        private string _orderIdKey = "OrderId";
//
//        private string _orderSerialKey = "OrderSerial";
//
//        public FixedAmountRangeModule(TaskContext context, TeamPerformanceConfig config)
//            : base(context, config) {
//        }
//
//        public override ExecuteResult<ITaskResult[]> Execute(TaskParameter parameter) {
//            var baseResult = base.Execute(parameter);
//            if (baseResult.Status != ResultStatus.Success)
//                return baseResult;
//
//            IList<ITaskResult> resultList = new List<ITaskResult>();
//
//            开始计算直推奖
//           当前下单用户
//            var user = Resolve<IUserService>().GetSingle(ShareOrder.UserId);
//            base.GetShareUser(user.ParentId, out User shareUser);//从基类获取分润用户
//            if (shareUser == null) {
//                return ExecuteResult<ITaskResult[]>.Cancel("推荐用户不存在");
//            }
//            var ratio = Convert.ToDecimal(Ratios[0]);
//            decimal shareAmount = BaseFenRunAmount * ratio;//分润金额
//            CreateResultList(shareAmount, base.ShareOrderUser, shareUser, parameter, Configuration, resultList);//构建分润参数
//
//            开始计算管理分红
//           var userMap = Resolve<IUserMapService>().GetParentMapFromCache(shareUser.Id);
//            var map = userMap.ParentMap.DeserializeJson<List<ParentMap>>();
//            if (map == null)
//                return ExecuteResult<ITaskResult[]>.Cancel("未找到触发会员的Parent Map.");
//            for (int i = 0; i < map.Count; i++) {
//                如果大于团队层数
//                if (i + 1 > Configuration.TeamLevel) {
//                    break;
//                }
//                var item = map[i];
//                GetShareUser(item.UserId, out shareUser);//从基类获取分润用户
//                if (shareUser == null)
//                    continue;
//                每上一级50 %
//               var itemRatio = Math.Pow(Convert.ToDouble(Configuration.ManagerRatio), Convert.ToDouble(i + 1)).ToDecimal() * ratio;
//                if (itemRatio <= 0)
//                    continue;
//                shareAmount = BaseFenRunAmount * itemRatio;//分润金额
//
//                CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration, resultList);//构建分润参数
//            }
//            return ExecuteResult<ITaskResult[]>.Success(resultList.ToArray());
//
//            var baseResult = base.Execute(parameter);
//            if (baseResult.Status != ResultStatus.Success)
//                return baseResult;
//            long orderId = 0;
//            parameter.TryGetValue(_orderIdKey, out orderId);
//            string orderSerial = "0000001";
//            parameter.TryGetValue(_orderSerialKey, out orderSerial);
//            var user = Alabo.Helpers.Ioc.Resolve<IUserService>().GetSingle(UserId);
//            if (user == null)
//                return ExecuteResult<ITaskResult[]>.Fail($"未找到ID为{UserId}的会员.");
//            var map = user.Map.ParentMap.Deserialize(new { ParentLevel = 0L, UserId = 0L });
//            if (map == null)
//                return ExecuteResult<ITaskResult[]>.Cancel("触发会员的Parent Map未找到.");
//            long shareUserId = 0;
//            IList<ITaskResult> resultList = new List<ITaskResult>();
//            // var DistriRatio = Configuration.DistriRatio.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(e=>Convert.ToDecimal(e)).ToArray();
//            var tpMaxProportion = Configuration.TeamPerformanceRuleItems.Select(r => r.MaxProportion).ToArray();
//            if (Configuration.TeamPerformanceRuleItems == null || Configuration.TeamPerformanceRuleItems.Count == 0) {
//                return ExecuteResult<ITaskResult[]>.Cancel("分润比例和等级未设置或设置错误.");
//            }
//
//            decimal DistruAmount = 0;
//
//            var moneyTypes = Alabo.Helpers.Ioc.Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>(e => e.Status == Alabo.Domains.Enums.Status.Normal);
//
//            //计算当前会员级别的最高极差比例
//            var userGrade = Alabo.Helpers.Ioc.Resolve<IUserTypeService>().GetSingle(r => r.UserId == user.Id);
//            if (userGrade == null) {
//                return ExecuteResult<ITaskResult[]>.Cancel("当前用户等级不正常.");
//            }
//            var userRule = Configuration.TeamPerformanceRuleItems.FirstOrDefault(r => r.UserGrade == userGrade.GradeId);
//
//            //根据代数限制逐级向上查出符合等级条件的会员
//            IList<FenRunResultParameter> ParameterList = new List<FenRunResultParameter>();
//
//            //最大极差
//            var maxRatio = userRule.MaxProportion;
//
//            for (int i = 0; i < Configuration.Algebra; i++) {
//                if (map.Count < i + 1) {
//                    break;
//                }
//                var item = map[i];
//                shareUserId = item.UserId;
//
//                if (shareUserId <= 0)
//                    break;
//                var shareUser = Alabo.Helpers.Ioc.Resolve<IUserService>().GetSingle(shareUserId);
//                var shareGrade = Alabo.Helpers.Ioc.Resolve<IUserTypeService>().GetSingle(e => e.UserId == shareUserId);//上级用户的等级
//                if (shareGrade == null)
//                    return ExecuteResult<ITaskResult[]>.Fail($"未找到ID为{shareUserId}的会员.");
//
//                var shareRule = Configuration.TeamPerformanceRuleItems.FirstOrDefault(r => r.UserGrade == shareGrade.GradeId);
//                if (shareRule == null) {
//                    return ExecuteResult<ITaskResult[]>.Fail($"未找到ID为{shareUserId}的会员等级未设置分润.");
//                }
//
//                //
//                var ratio = shareRule.MaxProportion - maxRatio;
//                if (ratio <= 0) {
//                    continue;
//                }
//                maxRatio = shareRule.MaxProportion;
//
//                DistruAmount = ratio * Amount;
//
//                if (DistruAmount == 0)
//                    continue;
//
//                foreach (var contem in Configuration.RuleItems) {
//                    var resultAmount = DistruAmount * contem.Ratio;
//                    var moneyType = moneyTypes.FirstOrDefault(r => r.Id == contem.MoneyTypeId);
//                    if (moneyType == null) {
//                        Logger.LogWarning($"未找到MoneyTypeId为{contem.MoneyTypeId}的货币类型.");
//                        continue;
//                    }
//                    var Parameter = new FenRunResultParameter {
//                        ModuleId = new Guid(Id),
//                        ModuleName = ModelName,
//                        Amount = resultAmount,
//                        BillStatus = BillStatus.Success,
//                        MoneyTypeId = moneyType.Id,
//                        ReceiveUserId = shareUser.Id,
//                        ReceiveUserName = shareUser.GetUserName(),
//                        UserRemark = string.Empty,
//                        ShareLevel = item.ParentLevel,
//                        ShareStatus = Share.Domain.Enums.FenRunStates.Successful,
//                        Summary = BillLogger(user, shareUser, moneyTypes.FirstOrDefault(r => r.Currency == TriggerCurrency).Name, Amount, resultAmount, orderSerial),
//                        TriggerGradeId = Configuration.GradeId,
//                        TriggerUserTypeId = Configuration.UserTypeId,
//                        TriggerUserId = user.Id,
//                        OrderUserName = user.GetUserName(),
//                        TriggerType = Configuration.TriggerType,
//                        ExtraDate = SetExtraData(),
//                        ModuleConfigId = Configuration.Id,
//
//                        Order = new InvoiceOrder() {
//                            Id = orderId,
//                            Serial = orderSerial,
//                            Amount = Amount
//                        }
//                    };
//                    ParameterList.Add(Parameter);
//                }
//                //上级分红
//                //AddSuperiorDividendToQueue(shareUserId, DistruAmount, Configuration.TriggerType, Configuration.RuleItems, resultList);
//            }
//            //分期
//            AddStagesListToQueue(Configuration.IsAddContributionChangeToQueue, ParameterList, resultList);
//            return ExecuteResult<ITaskResult[]>.Success(resultList.ToArray());
//            return ExecuteResult<ITaskResult[]>.Success();
//        }
//    }

