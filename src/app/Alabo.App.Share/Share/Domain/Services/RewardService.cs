using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Reports.Domain.Services;
using Alabo.App.Core.Reports.Model;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Core.User;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Share.Share.Domain.Dto;
using Alabo.App.Share.Share.Domain.Enums;
using Alabo.App.Share.Share.Domain.Repositories;
using Alabo.App.Share.Share.ViewModels;
using Alabo.App.Share.Tasks;
using Alabo.App.Share.Tasks.Base;
using Alabo.App.Shop.Product.Domain.CallBacks;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;
using ZKCloud.Open.Share.Models;
using Alabo.Reflections;
using Alabo.Runtime;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.Share.Domain.Services {

    using Reward = Entities.Reward;
    using User = Core.User.Domain.Entities.User;

    /// <summary>
    /// Class RewardService.
    /// </summary>
    public class RewardService : ServiceBase<Reward, long>, IRewardService {

        /// <summary>
        /// The reward repository
        /// </summary>
        private IRewardRepository _rewardRepository;

        /// <summary>
        /// The 会员 repository
        /// </summary>
        private IUserRepository _userRepository;

        /// <summary>
        /// 获取s the 视图 reward 分页 list.
        /// </summary>
        /// <param name="userInput">The 会员 input.</param>
        /// <param name="context">上下文</param>
        public PagedList<ViewAdminReward> GetViewRewardPageList(RewardInput userInput, HttpContext context) {
            if (!userInput.Serial.IsNullOrEmpty() && userInput.Serial.Length > 8) {
                userInput.Serial = userInput.Serial.Substring(1, userInput.Serial.Length - 1).TrimStart('0');
            }
            var rewardList = _rewardRepository.GetRewardList(userInput, out var count);
            var shareModuleList = Resolve<ITaskModuleConfigService>().GetList(context);
            var shareUserIds = rewardList.Select(r => r.UserId).Distinct().ToList();
            var orderUserIds = rewardList.Select(r => r.OrderUserId).Distinct().ToList();
            shareUserIds = shareUserIds.Concat(orderUserIds).ToList();
            var users = _userRepository.GetList();
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            IList<ViewAdminReward> result = new List<ViewAdminReward>();
            foreach (var item in rewardList) {
                var viewAdminReward = new ViewAdminReward {
                    Reward = item,
                    //OrderUser = users.FirstOrDefault(r => r.Id == item.OrderUserId),
                    //ShareUser = users.FirstOrDefault(r => r.Id == item.UserId),
                    //MoneyType = moneyTypes.FirstOrDefault(r => r.Id == item.MoneyTypeId),
                    //ShareModule = shareModuleList?.FirstOrDefault(r => r.Id == item.ModuleConfigId),
                    //TaskModuleAttribute = Resolve<ITaskQueueService>().GetTaskModuleAttribute(item.ModuleId),

                    Status = item.Status,
                    RewardId = item.Id,
                    ModuleId = item.ModuleId,
                    OrderUserId = item.OrderUserId,
                    ShareUserId = item.UserId,
                    OrderUserName = Resolve<IUserService>().GetUserStyle(users.FirstOrDefault(r => r.Id == item.OrderUserId)),
                    ShareUserName = Resolve<IUserService>().GetUserStyle(users.FirstOrDefault(r => r.Id == item.UserId)),
                    MoneyTypeName = moneyTypes.FirstOrDefault(r => r.Id == item.MoneyTypeId).Name,
                    ShareModuleName = shareModuleList?.FirstOrDefault(r => r.Id == item.ModuleConfigId).Name,
                    TaskModuleAttributeName = Resolve<ITaskQueueService>().GetTaskModuleAttribute(item.ModuleId).Name,
                    RewardAmount = item.Amount,
                    AfterAmount = item.AfterAmount,
                    Intro = item.Intro,
                    CreateTimeStr = item.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                };
                if (users.FirstOrDefault(r => r.Id == item.OrderUserId) == null) {
                    viewAdminReward.OrderUser = new User();
                }
                if (users.FirstOrDefault(r => r.Id == item.UserId) == null) {
                    viewAdminReward.OrderUser = new User();
                }
                result.Add(viewAdminReward);
            }
            return PagedList<ViewAdminReward>.Create(result, count, userInput.PageSize, userInput.PageIndex);
        }

        /// <summary>
        /// </summary>
        /// <param name="query"></param>

        public PagedList<ViewHomeReward> GetUserPage(object query) {
            var user = query.ToUserObject();
            var rewardList = Resolve<IRewardService>().GetPagedList(query, u => u.UserId == user.Id);
            var users = Resolve<IUserService>().GetList();
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var result = new List<ViewHomeReward>();
            rewardList.ForEach(u => {
                var view = AutoMapping.SetValue<ViewHomeReward>(u);
                view.OrderUserName = Resolve<IUserService>().GetHomeUserStyle(users.FirstOrDefault(z => z.Id == u.UserId));
                view.MoneyTypeName = moneyTypes.FirstOrDefault(z => z.Id == u.MoneyTypeId)?.Name;
                result.Add(view);
            });
            return PagedList<ViewHomeReward>.Create(result, rewardList.RecordCount, rewardList.PageSize, rewardList.PageIndex);
        }

        /// <summary>
        /// 获取分润详情
        /// </summary>
        /// <param name="Id">Id标识</param>
        public ViewAdminReward GetRewardView(long Id) {
            var reward = GetSingle(r => r.Id == Id);
            if (reward == null) {
                return null;
            }
            var viewAdmin = new ViewAdminReward {
                Reward = reward
            };
            var shareModule = new ShareModule {
                Name = "招商奖"
            };
            viewAdmin.ShareModule = shareModule;
            viewAdmin.OrderUser = Resolve<IUserService>().GetSingle(reward.OrderUserId);
            viewAdmin.ShareUser = Resolve<IUserService>().GetSingle(reward.UserId);
            viewAdmin.MoneyTypeName = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>()
                .FirstOrDefault(u => u.Id == viewAdmin.Reward.MoneyTypeId).Name;

            return viewAdmin;
        }

        /// <summary>
        /// 批量添加或更新分红记录
        /// </summary>
        /// <param name="soucre">The soucre.</param>
        public void AddOrUpdate(IEnumerable<Reward> soucre) {
            if (soucre == null) {
                throw new ArgumentNullException(nameof(soucre));
            }

            foreach (var item in soucre) {
                AddOrUpdate(item);
            }
        }

        /// <summary>
        /// 添加或更新分红记录
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void AddOrUpdate(Reward entity) {
            if (entity.Id > 0) {
                Repository<IRewardRepository>().UpdateSingle(entity);
            } else {
                Repository<IRewardRepository>().AddSingle(entity);
            }
        }

        /// <summary>
        /// 获取一条分红记录
        /// </summary>
        /// <param name="id">c分红记录ID</param>
        public Reward GetSingle(long? id) {
            return Repository<IRewardRepository>().GetSingle(e => e.Id == id);
        }

        /// <summary>
        /// Tries the 获取 模块 特性.
        /// </summary>
        /// <param name="id">Id标识</param>
        public ShareModulesAttribute TryGetModuleAttribute(string id) {
            var sobj = new ShareModulesAttribute();
            var assemblies = RuntimeContext.Current.GetPlatformRuntimeAssemblies();
            var moduleTypess = assemblies.SelectMany(e => e.GetTypes()).Where(e => e.GetInterfaces().Contains(typeof(IModuleConfig))).ToArray();

            foreach (var item in moduleTypess) {
                var attributes = item.GetTypeInfo().GetAttributes<ShareModulesAttribute>();
                if (attributes == null || attributes.Count() <= 0) {
                    continue;
                }

                foreach (var items in attributes) {
                    if (items.Id.ToUpper() == id.ToUpper()) {
                        sobj = items;
                    }
                }
            }

            return sobj;
        }

        /// <summary>
        /// 获取后台商品列表，moduleID为空时，获取所有的列表
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="moduleId">The 模块 identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="isEnable">The is enable.</param>
        public IList<ViewShareModuleList> GetViewShareModuleList(HttpContext context, Guid moduleId, string name, int? isEnable) {
            var moduleList = Resolve<ITaskModuleConfigService>().GetList(context);
            if (moduleId != Guid.Empty) {
                moduleList = moduleList.Where(r => r.ModuleId == moduleId).ToList();
            }
            if (!name.IsNullOrEmpty()) {
                moduleList = moduleList.Where(r => r.Name.Contains(name)).ToList();
            }
            if (isEnable == 1) {
                moduleList = moduleList.Where(r => r.IsEnable).ToList();
            }
            if (isEnable == 2) {
                moduleList = moduleList.Where(r => r.IsEnable == false).ToList();
            }
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var userTypes = Resolve<IAutoConfigService>().GetList<UserTypeConfig>();
            var productLines = Resolve<IProductLineService>().GetList();
            var shopMallConfigs = Resolve<IAutoConfigService>().GetList<PriceStyleConfig>();

            var result = new List<ViewShareModuleList>();
            foreach (var item in moduleList) {
                var taskModuleAttribute = Resolve<ITaskQueueService>().GetTaskModuleAttribute(item.ModuleId);
                var shareBaseConfig = JsonConvert.DeserializeObject<ShareBaseConfigList>(item.ConfigValue);

                var viewShare = AutoMapping.SetValue<ViewShareModuleList>(item);
                AutoMapping.SetValue(shareBaseConfig, viewShare);
                viewShare.SmsNotification = shareBaseConfig.TemplateRule.SmsNotification;
                viewShare.ConfigName = taskModuleAttribute?.Name;
                viewShare.Id = item.Id;

                //资产分配
                foreach (var rule in shareBaseConfig.RuleItems) {
                    var moneyType = moneyTypes.FirstOrDefault(r => r.Id == rule.MoneyTypeId);
                    viewShare.RuleItemsIntro += $"{moneyType?.Name}({rule.Ratio})";
                }
                viewShare.RuleItemsIntro = $"<code>{viewShare.RuleItemsIntro}</code>";
                //比例
                var radios = shareBaseConfig.DistriRatio?.Split(",");
                viewShare.DistriRatio = string.Empty;
                radios?.Foreach(r => {
                    viewShare.DistriRatio += $"<span class='m-badge m-badge--focus '>{r}</span>";
                });

                //用户类型限制
                if (!shareBaseConfig.ShareUser.IsLimitShareUserType) {
                    viewShare.ShareUserIntro = "不限类型 不限等级";
                } else {
                    var userType = userTypes.FirstOrDefault(r => r.Id == shareBaseConfig.ShareUser.ShareUserTypeId);

                    //等级限制
                    if (!shareBaseConfig.ShareUser.IsLimitShareUserGrade) {
                        viewShare.ShareUserIntro = $"限{userType?.Name} 不限等级";
                    } else {
                        var grade = Resolve<IGradeService>().GetGradeByUserTypeIdAndGradeId(shareBaseConfig.ShareUser.ShareUserTypeId, shareBaseConfig.ShareUser.ShareUserGradeId);
                        viewShare.ShareUserIntro = $"限{userType?.Name} {grade?.Name}";
                    }
                }

                if (!shareBaseConfig.OrderUser.IsLimitOrderUserType) {
                    viewShare.OrderUserIntro = "不限类型 不限等级";
                } else {
                    var userType = userTypes.FirstOrDefault(r => r.Id == shareBaseConfig.OrderUser.OrderUserTypeId);

                    //等级限制
                    if (!shareBaseConfig.OrderUser.IsLimitOrderUserGrade) {
                        viewShare.OrderUserIntro = $"限{userType?.Name} 不限等级";
                    } else {
                        var grade = Resolve<IGradeService>().GetGradeByUserTypeIdAndGradeId(shareBaseConfig.OrderUser.OrderUserTypeId, shareBaseConfig.OrderUser.OrderUserGradeId);
                        viewShare.OrderUserIntro = $"限{userType?.Name} {grade?.Name}";
                    }
                }
                //如果是商城订单
                if (shareBaseConfig.TriggerType == Core.Tasks.Domain.Enums.TriggerType.Order) {
                    viewShare.ProductRangIntro += shareBaseConfig.ProductRule.AmountType.GetDisplayName() + " ";
                    viewShare.ProductRangIntro += shareBaseConfig.ProductRule.ProductModel.GetDisplayName() + "<br/>";
                    if (shareBaseConfig.ProductRule.ProductModel == ProductModelType.ProductLine) {
                        var limitProductIdArray = shareBaseConfig.ProductRule.ProductLines
                         .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                         .Where(e => e.IsNumber())
                         .Select(e => Convert.ToInt64(e))
                         .ToArray();
                        var lines = productLines.Where(r => limitProductIdArray.Contains(r.Id));
                        lines.Foreach(r => {
                            viewShare.ProductRangIntro += r.Name + " ";
                        });
                    }
                    if (shareBaseConfig.ProductRule.ProductModel == ProductModelType.ShoppingMall) {
                        var priceStyle = shopMallConfigs.FirstOrDefault(r => r.Id == shareBaseConfig.ProductRule.PriceStyleId);
                        viewShare.ProductRangIntro += priceStyle?.Name;
                    }
                }

                result.Add(viewShare);
            }
            result = result.OrderByDescending(r => r.Id).OrderBy(r => r.IsLock).ToList();
            return result;
        }

        /// <summary>
        /// 获取视图模块
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="moduleId">The 模块 identifier.</param>
        /// <param name="Id">Id标识</param>
        /// <param name="copy">The copy.</param>
        public ViewModuleConfig GetViewModuleConfig(HttpContext context, Guid moduleId, long Id, int copy) {
            var view = new ViewModuleConfig() {
                ModuleId = moduleId,
            };
            var shareModule = Resolve<ITaskModuleConfigService>().GetSingle(context, Id);

            if (shareModule != null) {
                view.ShareModule = shareModule;
                if (!string.IsNullOrEmpty(shareModule.ConfigValue)) {
                    var shareBaseConfig = JsonConvert.DeserializeObject<ShareBaseConfigList>(shareModule.ConfigValue);
                    if (shareBaseConfig != null && shareBaseConfig.RuleItems.Count > 0) {
                        view.ConfigurationValue = shareModule.ConfigValue;
                        // 动态将ShareModel的值复制到视图中
                        view = AutoMapping.SetValue(shareModule, view);
                        view = AutoMapping.SetValue(shareBaseConfig, view);
                        view = AutoMapping.SetValue(shareBaseConfig.OrderUser, view);
                        view = AutoMapping.SetValue(shareBaseConfig.ShareUser, view);
                        view = AutoMapping.SetValue(shareBaseConfig.ProductRule, view);
                        view = AutoMapping.SetValue(shareBaseConfig.BaseRule, view);
                    }
                }
                view.Id = shareModule.Id;
                // 如果是复制模式
                if (copy == 1) {
                    view.Id = 0;
                    view.ShareModule.Id = 0;
                    view.IsLock = false;
                    view.IsCopy = true;
                }
            } else {
                view.ShareModule = new ShareModule();
                IList<AssetsRule> ruleItems = new List<AssetsRule>();
                var rule = new AssetsRule {
                    MoneyTypeId = Currency.Cny.GetCustomAttr<FieldAttribute>().GuidId.ToGuid(),
                    Ratio = 0.8m
                };
                ruleItems.Add(rule);
                var moneyType = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>().FirstOrDefault(r => r.Id != Currency.Cny.GetCustomAttr<FieldAttribute>().GuidId.ToGuid() && r.Status == Status.Normal);
                rule = new AssetsRule {
                    MoneyTypeId = moneyType.Id,
                    Ratio = 0.2m
                };
                ruleItems.Add(rule);
                view.RuleItems = ruleItems;
            }

            return view;
        }

        public PagedList<ViewHomeReward> GetUserPage(object query, Reward entity) {
            throw new NotImplementedException();
        }

        public ShareBaseConfig GetShareBaseConfig(long moduleConfigId) {
            var shareModuleReports = Resolve<IReportService>().GetValue<ShareModuleReports>();
            var findModuleList = shareModuleReports.ShareModuleList.ToObject<List<ShareModule>>();
            var find = findModuleList.FirstOrDefault(r => r.Id == moduleConfigId);
            if (find != null) {
                var shareBaseConfig = JsonConvert.DeserializeObject<ShareBaseConfigList>(find.ConfigValue);
                return shareBaseConfig;
            }
            return null;
        }

        public PagedList<ViewAdminReward> GetRewardList(object query) {
            //if (!userInput.Serial.IsNullOrEmpty() && userInput.Serial.Length > 8) {
            //    userInput.Serial = userInput.Serial.Substring(1, userInput.Serial.Length - 1).TrimStart('0');
            //}

            var rewardList = GetPagedList(query);
            var shareUserIds = rewardList.Select(r => r.UserId).Distinct().ToList();
            var orderUserIds = rewardList.Select(r => r.OrderUserId).Distinct().ToList();
            shareUserIds = shareUserIds.Concat(orderUserIds).ToList();
            var users = _userRepository.GetList();
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            IList<ViewAdminReward> result = new List<ViewAdminReward>();
            foreach (var item in rewardList) {
                var viewAdminReward = new ViewAdminReward {
                    Reward = item,
                    //OrderUser = users.FirstOrDefault(r => r.Id == item.OrderUserId),
                    //ShareUser = users.FirstOrDefault(r => r.Id == item.UserId),
                    //MoneyType = moneyTypes.FirstOrDefault(r => r.Id == item.MoneyTypeId),
                    //ShareModule = shareModuleList?.FirstOrDefault(r => r.Id == item.ModuleConfigId),
                    //TaskModuleAttribute = Resolve<ITaskQueueService>().GetTaskModuleAttribute(item.ModuleId),

                    Status = item.Status,
                    RewardId = item.Id,
                    ModuleId = item.ModuleId,
                    OrderUserId = item.OrderUserId,
                    ShareUserId = item.UserId,
                    OrderUserName = Resolve<IUserService>().GetUserStyle(users.FirstOrDefault(r => r.Id == item.OrderUserId)),
                    ShareUserName = Resolve<IUserService>().GetUserStyle(users.FirstOrDefault(r => r.Id == item.UserId)),
                    MoneyTypeName = moneyTypes.FirstOrDefault(r => r.Id == item.MoneyTypeId).Name,
                    TaskModuleAttributeName = Resolve<ITaskQueueService>().GetTaskModuleAttribute(item.ModuleId).Name,
                    RewardAmount = item.Amount,
                    AfterAmount = item.AfterAmount,
                    Intro = item.Intro,
                    CreateTimeStr = item.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                };
                if (users.FirstOrDefault(r => r.Id == item.OrderUserId) == null) {
                    viewAdminReward.OrderUser = new User();
                }
                if (users.FirstOrDefault(r => r.Id == item.UserId) == null) {
                    viewAdminReward.OrderUser = new User();
                }
                result.Add(viewAdminReward);
            }
            return PagedList<ViewAdminReward>.Create(result, 15, 15, 15);
        }

        public RewardService(IUnitOfWork unitOfWork, IRepository<Reward, long> repository) : base(unitOfWork, repository) {
            _rewardRepository = Repository<IRewardRepository>();
            _userRepository = Repository<IUserRepository>();
        }
    }
}