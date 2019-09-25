using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Market.UserRightss.Domain.CallBack;
using Alabo.App.Market.UserRightss.Domain.Dtos;
using Alabo.App.Market.UserRightss.Domain.Entities;
using Alabo.App.Market.UserRightss.Domain.Enums;
using Alabo.App.Market.UserRightss.Domain.Repositories;
using Alabo.Core.Regex;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Linq.Dynamic;
using Alabo.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.App.Share.Kpi.Domain.Entities;
using Alabo.App.Share.Kpi.Domain.Services;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.Domain.Entities;
using Alabo.App.Shop.Order.Domain.Entities.Extensions;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.Randoms;
using ZKCloud.App.Core.UserType.Domain.CallBacks;

namespace Alabo.App.Market.UserRightss.Domain.Services {

    public class UserRightsService : ServiceBase<UserRights, long>, IUserRightsService {

        public UserRightsService(IUnitOfWork unitOfWork, IRepository<UserRights, long> repository) : base(
            unitOfWork, repository) {
        }

        #region 商家服务订购

        /// <summary>
        ///     商家服务订购
        /// </summary>
        /// <param name="orderBuyInput"></param>
        /// <returns></returns>
        public async Task<Tuple<ServiceResult, OrderBuyOutput>> Buy(UserRightsOrderInput orderBuyInput) {
            #region 安全验证

            var result = ServiceResult.Success;
            var orderBuyOutput = new OrderBuyOutput();
            var user = Resolve<IUserService>().GetNomarlUser(orderBuyInput.UserId);
            if (user == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("用户不存在，或状态不正常"), orderBuyOutput);
            }

            orderBuyInput.BuyUser = user; // 购买用户等于当前用户

            user.Detail = null; // 减少体积保存
            user.Map = null;
            if (orderBuyInput.GradeId.IsGuidNullOrEmpty()) {
                return Tuple.Create(ServiceResult.FailedWithMessage("等级Id不能为空"), orderBuyOutput);
            }

            orderBuyInput.User = user;
            var userGrades = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var buyGrade = userGrades.FirstOrDefault(r => r.Id == orderBuyInput.GradeId);
            if (buyGrade == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("购买的等级不存在"), orderBuyOutput);
            }

            var userRightConfig = Resolve<IAutoConfigService>().GetList<UserRightsConfig>()
                .FirstOrDefault(c => c.GradeId == buyGrade.Id);
            if (userRightConfig != null && !userRightConfig.IsOpen) {
                return Tuple.Create(ServiceResult.FailedWithMessage("该等级暂未开放"), orderBuyOutput);
            }

            orderBuyInput.BuyGrade = buyGrade;
            orderBuyInput.CurrentGrade = userGrades.FirstOrDefault(r => r.Id == user.GradeId);
            if (orderBuyInput.CurrentGrade == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("您的等级不存在"), orderBuyOutput);
            }

            // 准营销中心，和营销中心的开通只能是管理员 动态配置,不用写死
            //if (orderBuyInput.GradeId == Guid.Parse("f2b8d961-3fec-462d-91e8-d381488ea972") || orderBuyInput.GradeId == Guid.Parse("cc873faa-749b-449b-b85a-c7d26f626feb"))
            //{
            if (orderBuyInput.OpenType == UserRightOpenType.AdminOpenHightGrade) {
                if (!Resolve<IUserService>().IsAdmin(user.Id)) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("您不是管理员无权开通"), orderBuyOutput);
                }
                if (orderBuyInput.Parent.IsNullOrEmpty()) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("推荐人不能为空"), orderBuyOutput);
                }
                orderBuyInput.ParnetUser = Resolve<IUserService>().GetSingleByUserNameOrMobile(orderBuyInput.Parent);
                if (orderBuyInput.ParnetUser == null) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("推荐人不能为空，不能开通营销中心"), orderBuyOutput);
                }
            }
            //}

            if (orderBuyInput.OpenType == UserRightOpenType.OpenToOtherByPay || orderBuyInput.OpenType == UserRightOpenType.AdminOpenHightGrade ||
                orderBuyInput.OpenType == UserRightOpenType.OpenToOtherByRight) {
                if (orderBuyInput.Mobile.IsNullOrEmpty()) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("请输入手机号码"), orderBuyOutput);
                }
                if (orderBuyInput.Name.IsNullOrEmpty()) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("请输入公司名称或姓名"), orderBuyOutput);
                }

                // 查找是否为注册用户
                var find = Resolve<IUserService>().GetSingleByUserNameOrMobile(orderBuyInput.Mobile);
                if (find != null) {
                    var findUserGrade = userGrades.FirstOrDefault(r => r.Id == find.GradeId);
                    if (findUserGrade == null) {
                        return Tuple.Create(ServiceResult.FailedWithMessage("激活的用户等级不存在"), orderBuyOutput);
                    }

                    if (findUserGrade.Price > 0.01m) {
                        return Tuple.Create(ServiceResult.FailedWithMessage("该用户已激活"), orderBuyOutput);
                    }

                    //var records = Resolve<IRecordService>().GetListNoTracking(s => s.Type == typeof(UserDetail).FullName);
                    //var findRecord = records.FirstOrDefault(s => s.UserId == user.Id);
                    //var findUser = findRecord?.Value?.ToObject<RegInput>();
                    //if (findUser != null)
                    //    orderBuyInput.RegInfo = findUser;
                    //else
                    //{
                    orderBuyInput.RegInfo = new RegInput() {
                        Mobile = find.Mobile,
                        UserName = find.Mobile,
                        Name = find.Name,
                        Password = "该用户已登录",
                        PayPassword = "该用户已登录",
                        ParentId = find.ParentId
                    };
                    // }
                } else {
                    if (!RegexHelper.CheckMobile(orderBuyInput.Mobile)) {
                        return Tuple.Create(ServiceResult.FailedWithMessage("手机号码格式不正确"), orderBuyOutput);
                    }
                    // 注册新用户
                    var password = RandomHelper.PassWord();
                    var payPassword = RandomHelper.PayPassWord();
                    var regInput = new RegInput {
                        Mobile = orderBuyInput.Mobile,
                        UserName = orderBuyInput.Mobile,
                        Name = orderBuyInput.Name,
                        Password = password,
                        PayPassword = payPassword,
                        ParentId = orderBuyInput.UserId,
                    };
                    orderBuyInput.RegInfo = regInput;
                    // 如果是管理员，则推荐人Id改为输入的推荐人Id
                    if (orderBuyInput.OpenType == UserRightOpenType.AdminOpenHightGrade) {
                        regInput.ParentId = orderBuyInput.ParnetUser.Id;
                    }
                    //
                    var userRegResult = Resolve<IUserBaseService>().Reg(regInput);
                    if (!userRegResult.Item1.Succeeded) {
                        return Tuple.Create(ServiceResult.FailedWithMessage("新会员注册失败" + userRegResult),
                            orderBuyOutput);
                    }
                    // await Resolve<IRecordService>().AddAsync(new Record() { Type = typeof(UserDetail).FullName, Value = regInput.ToJsons() });

                    find = Resolve<IUserService>().GetSingle(orderBuyInput.Mobile);

                    var message = $"恭喜您，您的账号已注册成功。您的登录账号为：{regInput.Mobile},初始登录密码：{password} 初始支付密码：{payPassword} 请妥善保管.";
                    Resolve<IOpenService>().SendRaw(find.Mobile, message);

                    message = $"您推荐的商家已注册成功.登录账号：{regInput.Mobile},初始登录密码：{password} 初始支付密码：{payPassword} 请尽快让商家悉知,第一时间指导商家使用系统.";

                    var userDetail = Resolve<IUserDetailService>().GetSingle(u => u.UserId == user.Id);
                    userDetail.RegionId = orderBuyInput.RegionId.ToInt64();
                    Resolve<IUserDetailService>().Update(userDetail);
                    Resolve<IOpenService>().SendRaw(user.Mobile, message);
                }

                orderBuyInput.BuyUser = find;
            }

            #endregion 安全验证

            orderBuyInput.UserRightList = GetList(r => r.UserId == user.Id);

            // 如果是帮别人开通
            if (orderBuyInput.OpenType == UserRightOpenType.OpenToOtherByRight) {
                return await OpenToOther(orderBuyInput);
            }

            // 其他三种情况都需要支付 +或者管理员帮他们开通
            return await OpenSelfOrUpgrade(orderBuyInput);
        }

        #endregion 商家服务订购

        #region 获取实际价格

        /// <summary>
        ///     获取需要支付的价格
        /// </summary>
        public Tuple<ServiceResult, decimal> GetPayPrice(UserRightsOrderInput orderInput) {
            var result = ServiceResult.Success;
            if (orderInput.OpenType == UserRightOpenType.OpenToOtherByRight) {
                return Tuple.Create(ServiceResult.FailedWithMessage("帮他购买时无价格计算"), 0m);
            }

            if (orderInput.OpenType == UserRightOpenType.OpenToOtherByPay) {
                if (orderInput.User.Id == orderInput.BuyUser.Id) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("不能帮自己购买"), 0m);
                }
                var price = orderInput.BuyGrade.Price;
                return Tuple.Create(result, price);
            } else {
                if (orderInput.BuyGrade.Price <= orderInput.CurrentGrade.Price && orderInput.OpenType != UserRightOpenType.AdminOpenHightGrade) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("购买的等级不能低于当前等级"), 0m);
                }

                var findRight = orderInput.UserRightList.FirstOrDefault(r => r.GradeId == orderInput.BuyGrade.Id);
                if (findRight != null && orderInput.OpenType != UserRightOpenType.AdminOpenHightGrade) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("用户当前等级权益已存在，不能重复购买"), 0m);
                }

                var userRightsConfigs = Resolve<IAutoConfigService>().GetList<UserRightsConfig>();
                var userGrades = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
                // 如果包含，则为升级
                if (userRightsConfigs.Select(r => r.GradeId).Contains(orderInput.User.GradeId)) {
                    if (orderInput.OpenType != UserRightOpenType.Upgrade && orderInput.OpenType != UserRightOpenType.AdminOpenHightGrade) {
                        return Tuple.Create(ServiceResult.FailedWithMessage("开通方式有错,应该为自身升级"), 0m);
                    }
                } else {
                    if (orderInput.OpenType != UserRightOpenType.OpenSelf) {
                        return Tuple.Create(ServiceResult.FailedWithMessage("开通方式有错,应该为自身购买"), 0m);
                    }
                }

                // 计算价格
                var price = orderInput.BuyGrade.Price;
                if (orderInput.OpenType == UserRightOpenType.Upgrade) {
                    price = orderInput.BuyGrade.Price - orderInput.CurrentGrade.Price;
                }

                if (price <= 0) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("购买等级价格设置为0"), 0m);
                }

                if (price != orderInput.Price) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("前后台计算价格不一样"), 0m);
                }

                return Tuple.Create(result, price);
            }
        }

        #endregion 获取实际价格

        #region 帮别人开通

        /// <summary>
        ///     帮别人开通
        /// </summary>
        /// <param name="orderBuyInput"></param>
        /// <returns></returns>
        public async Task<Tuple<ServiceResult, OrderBuyOutput>> OpenToOther(UserRightsOrderInput orderBuyInput) {
            var result = ServiceResult.Success;
            var orderBuyOutput = new OrderBuyOutput();

            // 检查用户的名额
            var userGrades = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var userRight = GetSingle(r => r.UserId == orderBuyInput.User.Id && r.GradeId == orderBuyInput.GradeId);
            if (userRight == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage($"您无{orderBuyInput.BuyGrade?.Name}名额端口"),
                    orderBuyOutput);
            }

            if (userRight.TotalCount - userRight.TotalUseCount < 1) {
                return Tuple.Create(ServiceResult.FailedWithMessage($"您{orderBuyInput.BuyGrade?.Name}名额端口，已用完"),
                    orderBuyOutput);
            }

            if (orderBuyInput.BuyUser == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("新会员未注册成功"), orderBuyOutput);
            }

            var context = Repository<IUserRightsRepository>().RepositoryContext;
            try {
                context.BeginTransaction();
                var kpi = new Kpi {
                    ModuleId = orderBuyInput.GradeId,
                    UserId = orderBuyInput.UserId,
                    Type = TimeType.NoLimit,
                    Value = 1
                };
                var lastKpiSingle = Resolve<IKpiService>()
                    .GetSingle(r => r.ModuleId == orderBuyInput.GradeId && r.Type == TimeType.NoLimit);
                if (lastKpiSingle != null) {
                    kpi.TotalValue = lastKpiSingle.TotalValue + kpi.Value;
                }

                // 新增Kpi记录，使用Kpi 记录表保存记录数据
                Resolve<IKpiService>().Add(kpi);

                // 使用数+1
                userRight.TotalUseCount += 1;
                Update(userRight);

                // 修改等级
                var updateUser = Resolve<IUserService>().GetSingle(r => r.Id == orderBuyInput.BuyUser.Id);
                updateUser.GradeId = orderBuyInput.GradeId;
                Resolve<IUserService>().Update(updateUser);

                // 增加相关权益
                // 添加用户权益
                var userRightConfigList = Resolve<IAutoConfigService>().GetList<UserRightsConfig>();
                var userRightConfig = userRightConfigList.FirstOrDefault(r => r.GradeId == orderBuyInput.GradeId);
                if (userRightConfig != null) {
                    var addList = new List<UserRights>();
                    foreach (var rightItem in userRightConfig.UserRightItems) {
                        UserRights addItem = new UserRights {
                            GradeId = rightItem.GradeId,
                            TotalCount = rightItem.Count,
                            UserId = orderBuyInput.BuyUser.Id
                        };
                        addList.Add(addItem);
                    }

                    if (addList.Count > 0) {
                        AddMany(addList);
                    }
                }

                //开通成功修改UserDetail表地址
                var buyUserDetail = Resolve<IUserDetailService>().GetSingle(u => u.UserId == orderBuyInput.BuyUser.Id);
                if (buyUserDetail.RegionId <= 0) {
                    buyUserDetail.RegionId = orderBuyInput.RegionId.ToInt64();
                    Resolve<IUserDetailService>().Update(buyUserDetail);
                }

                context.SaveChanges();
                context.CommitTransaction();
                //orderBuyOutput.RegUser = orderBuyInput.RegInfo;
                //orderBuyOutput.BuyGrade = orderBuyInput.BuyGrade;
                //var buyUser = Resolve<IUserService>().GetSingle(u => u.Mobile == orderBuyInput.BuyUser.Mobile);

                orderBuyOutput.RegUser = orderBuyInput.RegInfo;
                orderBuyOutput.BuyGrade = orderBuyInput.BuyGrade;// Resolve<IAutoConfigService>().GetList<UserGradeConfig>().FirstOrDefault(s => s.Id == orderBuyInput.BuyUser.GradeId);
            } catch (Exception ex) {
                context.RollbackTransaction();
                result = ServiceResult.FailedWithMessage(ex.Message);
            } finally {
                context.DisposeTransaction();
            }

            SendMessage(orderBuyInput.BuyUser, orderBuyInput.GradeId);
            return Tuple.Create(result, orderBuyOutput);
        }

        #endregion 帮别人开通

        #region 自己开通或升级,支付钱帮朋友开通，管理员开通营销中心

        public async Task<Tuple<ServiceResult, OrderBuyOutput>> OpenSelfOrUpgrade(UserRightsOrderInput orderInput) {
            var result = ServiceResult.Success;
            var orderBuyOutput = new OrderBuyOutput();
            //if (orderInput.GradeId == Guid.Parse("72be65e6-3000-414d-972e-1a3d4a366001"))
            //{
            //    result = ServiceResult.FailedWithMessage("标准商家不能自己支付开通");
            //    new Tuple<ServiceResult, OrderBuyOutput>(result, orderBuyOutput);
            //}

            var payPrice = GetPayPrice(orderInput);
            if (!payPrice.Item1.Succeeded) {
                return Tuple.Create(payPrice.Item1, orderBuyOutput);
            }

            var context = Repository<IUserRightsRepository>().RepositoryContext;

            try {
                context.BeginTransaction();

                var order = new Order {
                    UserId = orderInput.BuyUser.Id,
                    StoreId = 0,
                    OrderStatus = OrderStatus.WaitingBuyerPay,
                    OrderType = OrderType.VirtualOrder,
                    TotalAmount = payPrice.Item2,
                    TotalCount = 1,
                    PayId = 0,
                    PaymentAmount = payPrice.Item2
                };
                // 订单扩展数据
                var userRightOrder = AutoMapping.SetValue<UserRightsOrderInput>(orderInput);

                order.OrderExtension = new OrderExtension {
                    // 价格信息
                    OrderAmount = new OrderAmount {
                        TotalProductAmount = payPrice.Item2, // 商品总价
                        ExpressAmount = 0m, // 邮费
                        FeeAmount = 0m // 服务费
                    },
                    AttachContent = userRightOrder.ToJsons(),
                    User = orderInput.BuyUser, // 下单用户
                    Store = null
                };

                order.AccountPay = order.AccountPayPair.ToJsons();
                order.Extension = order.OrderExtension.ToJsons();

                // 如果发货人不为空

                Resolve<IOrderService>().Add(order);
                var orderList = new List<Order>
                {
                    order
                };
                var singlePayInput = new SinglePayInput {
                    Orders = orderList,
                    User = orderInput.User,
                    OrderUser = orderInput.BuyUser,
                    ExcecuteSqlList = new BaseServiceMethod {
                        Method = "ExcecuteSqlList",
                        ServiceName = typeof(IUserRightsService).Name,
                        Parameter = order.Id
                    },
                    AfterSuccess = new BaseServiceMethod {
                        Method = "AfterPaySuccess",
                        ServiceName = typeof(IUserRightsService).Name,
                        Parameter = order.Id
                    },
                    TriggerType = TriggerType.Other,
                    BuyerCount = 1,
                    RedirectUrl = "/pages/index?path=successful_opening",
                };
                var payResult = Resolve<IOrderAdminService>().AddSinglePay(singlePayInput);
                if (!payResult.Item1.Succeeded) {
                    // 支付记录添加失败，回滚
                    context.RollbackTransaction();
                    return Tuple.Create(payResult.Item1, new OrderBuyOutput());
                }

                //更新人民币支付记录Id
                var orderIds = orderList.Select(e => e.Id).ToList();
                Resolve<IOrderService>().Update(r => { r.PayId = payResult.Item2.Id; }, e => orderIds.Contains(e.Id));

                // 输出赋值
                orderBuyOutput.PayAmount = payResult.Item2.Amount;
                orderBuyOutput.PayId = payResult.Item2.Id;
                orderBuyOutput.OrderIds = orderList.Select(r => r.Id).ToList();

                //开通成功修改UserDetail表地址
                var buyUserDetail = Resolve<IUserDetailService>().GetSingle(u => u.UserId == orderInput.BuyUser.Id);
                if (buyUserDetail.RegionId <= 0) {
                    buyUserDetail.RegionId = orderInput.RegionId.ToInt64();
                    Resolve<IUserDetailService>().Update(buyUserDetail);
                }

                context.SaveChanges();
                context.CommitTransaction();
                //var buyUser = Resolve<IUserService>().GetSingle(u => u.Mobile == orderInput.BuyUser.Mobile);

                orderBuyOutput.RegUser = orderInput.RegInfo;
                orderBuyOutput.BuyGrade = orderInput.BuyGrade;// Resolve<IAutoConfigService>().GetList<UserGradeConfig>().FirstOrDefault(s => s.Id == orderInput.BuyUser.GradeId);
            } catch (Exception ex) {
                context.RollbackTransaction();
                result = ServiceResult.FailedWithMessage(ex.Message);
            } finally {
                context.DisposeTransaction();
            }
            // 删除缓存
            Resolve<IUserService>().DeleteUserCache(orderInput.User.Id, orderInput.User.UserName);
            Resolve<IUserService>().DeleteUserCache(orderInput.BuyUser.Id, orderInput.BuyUser.UserName);
            return new Tuple<ServiceResult, OrderBuyOutput>(result, orderBuyOutput);
        }

        #endregion 自己开通或升级,支付钱帮朋友开通，管理员开通营销中心

        #region 获取显示视图

        /// <summary>
        ///     获取显示视图
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<UserRightsOutput> GetView(long userId) {
            var isAdmin = Resolve<IUserService>().IsAdmin(userId);
            var userRightsOutputList = GetViewByCache(isAdmin);
            if (userId > 0) {
                var user = Resolve<IUserService>().GetSingle(r => r.Id == userId); //  非缓存中读取

                if (user != null && user.Status == Status.Normal) {
                    // 删除缓存
                    Resolve<IUserService>().DeleteUserCache(user.Id, user.UserName);
                    var userRightsConfigs = Resolve<IAutoConfigService>().GetList<UserRightsConfig>();
                    var userGrades = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
                    var userGrade = userGrades.FirstOrDefault(r => r.Id == user.GradeId);
                    userRightsOutputList.Foreach(r => {
                        r.CurrnetGradeName = userGrade?.Name;
                        r.UserName = user.GetUserName();
                    });

                    // 如果包含
                    if (userRightsConfigs.Select(r => r.GradeId).Contains(user.GradeId)) {
                        var userRightsList = Resolve<IUserRightsService>().GetList(r => r.UserId == user.Id);

                        if (userGrade != null) {
                            foreach (var userRightOutputItem in userRightsOutputList) {
                                var itemGrade = userGrades.FirstOrDefault(r => r.Id == userRightOutputItem.GradeId);
                                if (itemGrade == null) {
                                    continue;
                                }
                                var gradeConfig = userRightsConfigs.Single(s => s.GradeId == itemGrade.Id);
                                //是否显示该等级,前面已经判断了 这里无法控制是否显示
                                //if (gradeConfig != null && !gradeConfig.IsShowGradePage)
                                //{
                                //    continue;
                                //}
                                userRightOutputItem.SalePrice = itemGrade.Price;
                                //是否开放按钮
                                if (userRightOutputItem.IsOpen) {
                                    //升级会员
                                    if (itemGrade.Price > userGrade.Price) {
                                        //判断是否已经登记地址
                                        var userDetail = Resolve<IUserDetailService>().GetSingle(u => u.UserId == userId);
                                        if (userDetail.RegionId <= 0) {
                                            userRightOutputItem.IsRegion = false;
                                        }

                                        if (gradeConfig.CanUpgradeBySelf) {
                                            userRightOutputItem.OpenType = UserRightOpenType.Upgrade;
                                            userRightOutputItem.Price = itemGrade.Price - userGrade.Price;
                                            userRightOutputItem.ButtonText = $"补{itemGrade.Price - userGrade.Price}升级{userRightOutputItem.Name}";
                                        }
                                    } else {
                                        //帮朋友开通
                                        var userRightItem = userRightsList.FirstOrDefault(r => r.GradeId == userRightOutputItem.GradeId);
                                        long remainCount = 0; // 剩余数量
                                        if (userRightItem != null) {
                                            userRightOutputItem.TotalCount = userRightItem.TotalCount;
                                            remainCount = userRightItem.TotalCount - userRightItem.TotalUseCount;
                                        }
                                        //判断是否已经登记地址
                                        userRightOutputItem.IsRegion = false;

                                        userRightOutputItem.RemainCount = remainCount;
                                        userRightOutputItem.TotalUseCount = userRightItem == null ? 0 : userRightItem.TotalUseCount;

                                        //// 20190403 标准商家直接用端口开通
                                        //if (remainCount > 0 && userRightOutputItem.GradeId == Guid.Parse("72be65e6-3000-414d-972e-1a3d4a366001"))
                                        //{
                                        //    userRightOutputItem.OpenType = UserRightOpenType.OpenToOtherByRight;
                                        //    userRightOutputItem.Price = 0m;
                                        //    userRightOutputItem.ButtonText = $"确认开通{userRightOutputItem.Name}";
                                        //}
                                        //else
                                        //{
                                        //    userRightOutputItem.OpenType = UserRightOpenType.OpenToOtherByPay;
                                        //    userRightOutputItem.Price = itemGrade.Price;
                                        //    userRightOutputItem.ButtonText =
                                        //        $"支付{itemGrade.Price}元帮朋友开通{userRightOutputItem.Name}";
                                        //}

                                        //有端口的时候
                                        if (remainCount > 0) {
                                            if (!gradeConfig.IsHavePortNeedToPay) {
                                                //使用端口名额帮朋友开通
                                                userRightOutputItem.OpenType = UserRightOpenType.OpenToOtherByRight;
                                                userRightOutputItem.ButtonText = $"帮朋友开通{itemGrade?.Name}";
                                            } else {
                                                //支付金额帮朋友开通
                                                userRightOutputItem.OpenType = UserRightOpenType.OpenToOtherByPay;
                                                userRightOutputItem.Price = itemGrade.Price;
                                                userRightOutputItem.ButtonText = $"支付{itemGrade.Price}元帮朋友开通{userRightOutputItem.Name}";
                                            }
                                        } else {
                                            //无端口
                                            if (gradeConfig.HaveNotPortCanOpen) {
                                                //支付金额帮胖友开通
                                                userRightOutputItem.OpenType = UserRightOpenType.OpenToOtherByPay;
                                                userRightOutputItem.Price = itemGrade.Price;
                                                userRightOutputItem.ButtonText = $"支付{itemGrade.Price}元帮朋友开通{userRightOutputItem.Name}";
                                            } else {
                                                userRightOutputItem.ButtonText = null;
                                            }
                                        }
                                        if (gradeConfig.IsCanOnlyAdminOpen) {
                                            userRightOutputItem.OpenType = UserRightOpenType.AdminOpenHightGrade;
                                            userRightOutputItem.ButtonText = $"管理员权限开通";
                                        }

                                        ////// 按要求全部要钱(2019.04.02)
                                        //userRightOutputItem.OpenType = UserRightOpenType.OpenToOtherByPay;
                                        //userRightOutputItem.Price = itemGrade.Price;
                                        //userRightOutputItem.ButtonText =
                                        //    $"支付{itemGrade.Price}元帮朋友开通{userRightOutputItem.Name}";
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return userRightsOutputList;
        }

        #endregion 获取显示视图

        #region

        /// <summary>
        ///     获取显示视图
        /// </summary>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        public IList<UserRightsOutput> GetView(bool isAdmin) {
            var userRightsOutputList = GetViewByCache(isAdmin);

            var userRightsConfigs = Resolve<IAutoConfigService>().GetList<UserRightsConfig>();
            var userGrades = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();

            foreach (var userRightOutputItem in userRightsOutputList) {
                var itemGrade = userGrades.FirstOrDefault(r => r.Id == userRightOutputItem.GradeId);
                if (itemGrade == null) {
                    continue;
                }
                var gradeConfig = userRightsConfigs.Single(s => s.GradeId == itemGrade.Id);

                //userRightOutputItem.SalePrice = itemGrade.Price;

                userRightOutputItem.ButtonText = null;
            }

            return userRightsOutputList;
        }

        #endregion

        #region 获取没有userId的数据

        /// <summary>
        ///     获取没有userId的数据
        /// </summary>
        /// <returns></returns>
        private IList<UserRightsOutput> GetViewByCache(bool isAdmin) {
            var result = new List<UserRightsOutput>();
            var userRightsConfigs = Resolve<IAutoConfigService>().GetList<UserRightsConfig>().OrderBy(r => r.SortOrder);
            var userGrades = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var privilegeDict = Resolve<IAutoConfigService>().GetList<GradePrivilegesConfig>().ToDictionary(g => g.Id);

            //var isAdmin = Resolve<IUserService>().IsAdmin(userId);

            foreach (var item in userRightsConfigs) {
                var itemGrade = userGrades.FirstOrDefault(r => r.Id == item.GradeId);
                //如果不是管理员就判断是否显示该等级
                if (!isAdmin) {
                    //是否显示该等级
                    if (!item.IsShowGradePage) {
                        continue;
                    }
                }

                if (itemGrade != null) {
                    var userRightsOutput = AutoMapping.SetValue<UserRightsOutput>(itemGrade);
                    userRightsOutput.BackGroundImage = Resolve<IApiService>().ApiImageUrl(item.BackGroundImage);
                    userRightsOutput.ThemeColor = item.ThemeColor;
                    userRightsOutput.Icon = Resolve<IApiService>().ApiImageUrl(itemGrade.Icon);
                    userRightsOutput.Intro = item.Intro;
                    userRightsOutput.GradeId = item.GradeId;
                    userRightsOutput.IsOpen = item.IsOpen;
                    userRightsOutput.Price = itemGrade.Price;
                    //是否开放按钮
                    if (userRightsOutput.IsOpen) {
                        if (item.CanUpgradeBySelf) {
                            // 无登录用户
                            userRightsOutput.OpenType = UserRightOpenType.OpenSelf;
                            //if (userRightsOutput.OpenType == UserRightOpenType.OpenToOtherByPay)
                            //    userRightsOutput.ButtonText = $"支付{itemGrade?.Price}元开通{itemGrade?.Name}";
                            //else if (userRightsOutput.OpenType == UserRightOpenType.OpenToOtherByRight)
                            //    userRightsOutput.ButtonText = $"帮朋友开通{itemGrade?.Name}";
                            //else if (userRightsOutput.OpenType == UserRightOpenType.Upgrade)
                            userRightsOutput.ButtonText = $"支付{itemGrade?.Price}元升级为{itemGrade?.Name}";
                        }
                    }

                    //else
                    //{
                    //    userRightsOutput.OpenType = UserRightOpenType.None;
                    //    userRightsOutput.ButtonText = $"即将开通，敬请期待";
                    //}

                    // 获取权益
                    var privilegeIds = itemGrade?.GradePrivileges.ToGuidList()
                        ?.OrderBy(id => privilegeDict[id].SortOrder);
                    if (privilegeIds != null) {
                        foreach (var itemId in privilegeIds) {
                            var gradePrivilegeItem = privilegeDict[itemId];
                            var privilegesItem = AutoMapping.SetValue<PrivilegesItem>(gradePrivilegeItem);
                            privilegesItem.Icon = Resolve<IApiService>().ApiImageUrl(privilegesItem.Icon);
                            userRightsOutput.Privileges.Add(privilegesItem);
                        }
                    }

                    result.Add(userRightsOutput);
                }
            }

            return result;
        }

        #endregion 获取没有userId的数据

        public List<string> ExcecuteSqlList(List<object> entityIdList) {
            // 会员权益，订单Id只有一个

            var sqlList = new List<string>();
            var orderId = entityIdList.FirstOrDefault();
            if (orderId.ConvertToLong(0) <= 0) {
                return sqlList;
            }

            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderId.ConvertToLong(0));
            if (order == null) {
                return sqlList;
            }

            if (order.OrderExtension == null) {
                return sqlList;
            }

            var userRightsOrder = order.OrderExtension.AttachContent.ToObject<UserRightsOrderInput>();
            if (userRightsOrder == null) {
                return sqlList;
            }

            var buyUser = Resolve<IUserService>().GetSingle(userRightsOrder.BuyUser?.Id);
            if (buyUser == null) {
                return sqlList;
            }

            var userGrades = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var findGrade = userGrades.FirstOrDefault(r => r.Id == userRightsOrder.GradeId);
            if (findGrade == null) {
                return sqlList;
            }

            var userRightsList = Resolve<IUserRightsService>().GetList(r => r.UserId == order.UserId);
            // 添加用户权益
            var sql = string.Empty;
            var userRightConfigList = Resolve<IAutoConfigService>().GetList<UserRightsConfig>();
            // 升级以后的用户权益
            var upgradeUserRightConfig = userRightConfigList.FirstOrDefault(r => r.GradeId == userRightsOrder.GradeId);
            // 当前用户的权益
            var currentUserRightConfig =
                userRightConfigList.FirstOrDefault(r => r.GradeId == order.OrderExtension.User.GradeId);
            if (upgradeUserRightConfig != null) {
                foreach (var rightItem in upgradeUserRightConfig.UserRightItems) {
                    var userRightItem = userRightsList.FirstOrDefault(r => r.GradeId == rightItem.GradeId);
                    if (userRightItem != null) {
                        // 需增加的端口数量
                        var count = rightItem.Count;
                        // 当前会员的等级的权益
                        var currentRightItem =
                            currentUserRightConfig?.UserRightItems?.FirstOrDefault(r =>
                                r.GradeId == userRightItem.GradeId);
                        if (currentRightItem != null) {
                            // 计算需要增加的端口数
                            count = rightItem.Count - currentRightItem.Count;
                            if (count < 0) {
                                count = rightItem.Count;
                            }
                        }

                        sql =
                            $"update Market_UserRights set TotalCount=TotalCount+{count} where UserId={buyUser.Id} and GradeId='{rightItem.GradeId}'";
                        sqlList.Add(sql);
                    } else {
                        sql =
                            $"INSERT INTO [dbo].[Market_UserRights] ([GradeId] ,[UserId] ,[TotalUseCount],[TotalCount] ,[CreateTime]) VALUES('{rightItem.GradeId}',{buyUser.Id},0,{rightItem.Count},'{DateTime.Now}')";
                        sqlList.Add(sql);
                    }
                }
            }

            // 修改等级
            sql = $"Update User_User Set GradeId='{findGrade.Id}' where Id={buyUser.Id}";
            sqlList.Add(sql);
            return sqlList;
        }

        public void AfterPaySuccess(List<object> entityIdList) {
            var orderId = entityIdList.FirstOrDefault();
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderId.ConvertToLong(0));

            UserRightsOrderInput userRightsOrder;
            if (order != null) {
                userRightsOrder = order.OrderExtension.AttachContent.ToObject<UserRightsOrderInput>();
                if (userRightsOrder != null) {
                    var buyUser = Resolve<IUserService>().GetSingle(userRightsOrder.BuyUser?.Id);
                    if (buyUser != null) {
                        var upgradeRecord = new UpgradeRecord {
                            AfterGradeId = userRightsOrder.GradeId,
                            UserId = buyUser.Id,
                            Type = UpgradeType.Buy
                        };
                        if (order.OrderExtension.User != null) {
                            upgradeRecord.BeforeGradeId = order.OrderExtension.User.GradeId;
                        }
                        var beforeGrade = Resolve<IGradeService>().GetGrade(buyUser.GradeId); // 当前等级
                        var afterGrade = Resolve<IGradeService>().GetGrade(upgradeRecord.AfterGradeId);// 升级后等级
                        if (afterGrade.Id == afterGrade.Id) {
                            //throw new ValidException("非法调用");
                            Resolve<IUserRightsService>().Log($"购买者:{userRightsOrder.BuyUser?.Id},order:{order.Id}=>{afterGrade.Id + "==" + afterGrade.Id}非法调用!");
                            return;
                        }
                        if (afterGrade.Contribute <= beforeGrade.Contribute) {
                            Resolve<IUserRightsService>().Log($"购买者:{userRightsOrder.BuyUser?.Id},order:{order.Id}=>{afterGrade.Contribute + "<=" + beforeGrade.Contribute}非法调用!");
                            //throw new ValidException("非法调用");
                            return;
                        }
                        // 按要求所有开通, 既要花钱又扣端口 2019.04.02
                        if (true) {
                            // 检查端口, 检查用户的名额
                            var userGrades = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
                            var userRight = GetSingle(r => r.UserId == userRightsOrder.UserId && r.GradeId == userRightsOrder.GradeId);

                            if (userRight != null && userRight.TotalCount - userRight.TotalUseCount > 0 && userRightsOrder.BuyUser != null) {
                                var kpi = new Kpi {
                                    ModuleId = userRightsOrder.GradeId,
                                    UserId = userRightsOrder.UserId,
                                    Type = TimeType.NoLimit,
                                    Value = 1
                                };
                                var lastKpiSingle = Resolve<IKpiService>()
                                    .GetSingle(r => r.ModuleId == userRightsOrder.GradeId && r.Type == TimeType.NoLimit);
                                if (lastKpiSingle != null) {
                                    kpi.TotalValue = lastKpiSingle.TotalValue + kpi.Value;
                                }

                                // 新增Kpi记录，使用Kpi 记录表保存记录数据
                                Resolve<IKpiService>().Add(kpi);

                                // 使用数+1
                                userRight.TotalUseCount += 1;
                                Update(userRight);
                            }
                        }

                        Resolve<IUpgradeRecordService>().Add(upgradeRecord);
                        // 删除缓存
                        Resolve<IUserService>().DeleteUserCache(buyUser.Id, buyUser.UserName);
                        // 发送短信
                        SendMessage(buyUser, userRightsOrder.GradeId);
                    }
                }
            }
        }

        /// <summary>
        ///  发送短信
        /// </summary>
        /// <param name="buyUser"></param>
        /// <param name="gradeId"></param>
        private void SendMessage(User buyUser, Guid gradeId) {
            if (buyUser != null) {
                var buyGrade = Resolve<IGradeService>().GetGrade(gradeId);
                var message = $"尊敬的用户您好,您已成为{buyGrade.Name},请登录系统查看相关的权益和熟悉相关操作";
                Resolve<IOpenService>().SendRaw(buyUser.Mobile, message);

                message = GetUserRightIntro(buyUser.Id);
                if (!message.IsNullOrEmpty()) {
                    Resolve<IOpenService>().SendRaw(buyUser.Mobile, message);
                }

                // 发送推荐人
                var parentUser = Resolve<IUserService>().GetSingle(buyUser.ParentId);
                if (parentUser != null) {
                    buyGrade = Resolve<IGradeService>().GetGrade(gradeId);
                    message = $"尊敬的用户您好,您推荐的商户{buyUser.Mobile}已成为{buyGrade.Name},请悉知并在指导商家熟悉{buyGrade.Name}相关的权益";
                    Resolve<IOpenService>().SendRaw(parentUser.Mobile, message);

                    message = GetUserRightIntro(parentUser.Id);
                    if (!message.IsNullOrEmpty()) {
                        Resolve<IOpenService>().SendRaw(parentUser.Mobile, message);
                    }
                }
            }
        }

        /// <summary>
        /// 获取用户权益描述信息
        /// </summary>
        /// <param name="userId"></param>
        private string GetUserRightIntro(long userId) {
            var userRights = GetList(r => r.UserId == userId);
            var message = string.Empty;
            if (userRights.Count > 0) {
                message = $"您当前包含如下权益端口：";
                var grades = Resolve<IGradeService>().GetUserGradeList();
                foreach (var item in userRights) {
                    var grade = grades.FirstOrDefault(r => r.Id == item.GradeId);
                    message +=
                        $"{grade.Name}共:{item.TotalCount},已使用:{item.TotalUseCount},剩余:{item.TotalCount - item.TotalUseCount} ";
                }
            }
            return message;
        }

        #region 后台添加或编辑

        public UserRights GetEditView(object id) {
            var key = id.ConvertToLong();
            var find = GetSingle(r => r.Id == key);
            if (find == null) {
                return new UserRights();
            } else {
                find.UserName = Resolve<IUserService>().GetSingle(find.UserId)?.UserName;
            }

            return find;
        }

        public ServiceResult AddOrUpdate(UserRights view) {
            // 验证用户
            var result = Resolve<IValidService>().VerifyUser(view.UserName);
            if (!result.Succeeded) {
                return result;
            }
            // 验证等级
            result = Resolve<IValidService>().VerifyUserGrade(view.GradeId);
            if (!result.Succeeded) {
                return result;
            }

            var user = Resolve<IUserService>().GetSingle(view.UserName);
            var find = GetSingle(view.Id);
            if (find != null) {
                if (find.GradeId != view.GradeId) {
                    return ServiceResult.FailedWithMessage("该用户该等级权益已存在，请选择后编辑");
                }
                if (find.UserId != user.Id) {
                    return ServiceResult.FailedWithMessage("您不能编辑其他用户权益，请输入正确的用户名");
                }

                find.TotalUseCount = view.TotalUseCount.ConvertToLong();
                find.TotalCount = view.TotalCount.ConvertToLong();
                if (!Update(find)) {
                    return ServiceResult.FailedWithMessage("编辑权益失败");
                }
            } else {
                var model = new UserRights {
                    TotalUseCount = view.TotalUseCount.ConvertToLong(),
                    TotalCount = view.TotalCount.ConvertToLong(),
                    UserId = user.Id,
                    GradeId = view.GradeId
                };
                if (!Add(model)) {
                    return ServiceResult.FailedWithMessage("添加失败");
                }
            }

            return ServiceResult.Success;
        }

        #endregion 后台添加或编辑
    }
}