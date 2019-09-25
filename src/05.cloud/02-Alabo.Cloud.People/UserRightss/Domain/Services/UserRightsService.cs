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

        #region �̼ҷ��񶩹�

        /// <summary>
        ///     �̼ҷ��񶩹�
        /// </summary>
        /// <param name="orderBuyInput"></param>
        /// <returns></returns>
        public async Task<Tuple<ServiceResult, OrderBuyOutput>> Buy(UserRightsOrderInput orderBuyInput) {
            #region ��ȫ��֤

            var result = ServiceResult.Success;
            var orderBuyOutput = new OrderBuyOutput();
            var user = Resolve<IUserService>().GetNomarlUser(orderBuyInput.UserId);
            if (user == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("�û������ڣ���״̬������"), orderBuyOutput);
            }

            orderBuyInput.BuyUser = user; // �����û����ڵ�ǰ�û�

            user.Detail = null; // �����������
            user.Map = null;
            if (orderBuyInput.GradeId.IsGuidNullOrEmpty()) {
                return Tuple.Create(ServiceResult.FailedWithMessage("�ȼ�Id����Ϊ��"), orderBuyOutput);
            }

            orderBuyInput.User = user;
            var userGrades = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var buyGrade = userGrades.FirstOrDefault(r => r.Id == orderBuyInput.GradeId);
            if (buyGrade == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("����ĵȼ�������"), orderBuyOutput);
            }

            var userRightConfig = Resolve<IAutoConfigService>().GetList<UserRightsConfig>()
                .FirstOrDefault(c => c.GradeId == buyGrade.Id);
            if (userRightConfig != null && !userRightConfig.IsOpen) {
                return Tuple.Create(ServiceResult.FailedWithMessage("�õȼ���δ����"), orderBuyOutput);
            }

            orderBuyInput.BuyGrade = buyGrade;
            orderBuyInput.CurrentGrade = userGrades.FirstOrDefault(r => r.Id == user.GradeId);
            if (orderBuyInput.CurrentGrade == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("���ĵȼ�������"), orderBuyOutput);
            }

            // ׼Ӫ�����ģ���Ӫ�����ĵĿ�ֻͨ���ǹ���Ա ��̬����,����д��
            //if (orderBuyInput.GradeId == Guid.Parse("f2b8d961-3fec-462d-91e8-d381488ea972") || orderBuyInput.GradeId == Guid.Parse("cc873faa-749b-449b-b85a-c7d26f626feb"))
            //{
            if (orderBuyInput.OpenType == UserRightOpenType.AdminOpenHightGrade) {
                if (!Resolve<IUserService>().IsAdmin(user.Id)) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("�����ǹ���Ա��Ȩ��ͨ"), orderBuyOutput);
                }
                if (orderBuyInput.Parent.IsNullOrEmpty()) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("�Ƽ��˲���Ϊ��"), orderBuyOutput);
                }
                orderBuyInput.ParnetUser = Resolve<IUserService>().GetSingleByUserNameOrMobile(orderBuyInput.Parent);
                if (orderBuyInput.ParnetUser == null) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("�Ƽ��˲���Ϊ�գ����ܿ�ͨӪ������"), orderBuyOutput);
                }
            }
            //}

            if (orderBuyInput.OpenType == UserRightOpenType.OpenToOtherByPay || orderBuyInput.OpenType == UserRightOpenType.AdminOpenHightGrade ||
                orderBuyInput.OpenType == UserRightOpenType.OpenToOtherByRight) {
                if (orderBuyInput.Mobile.IsNullOrEmpty()) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("�������ֻ�����"), orderBuyOutput);
                }
                if (orderBuyInput.Name.IsNullOrEmpty()) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("�����빫˾���ƻ�����"), orderBuyOutput);
                }

                // �����Ƿ�Ϊע���û�
                var find = Resolve<IUserService>().GetSingleByUserNameOrMobile(orderBuyInput.Mobile);
                if (find != null) {
                    var findUserGrade = userGrades.FirstOrDefault(r => r.Id == find.GradeId);
                    if (findUserGrade == null) {
                        return Tuple.Create(ServiceResult.FailedWithMessage("������û��ȼ�������"), orderBuyOutput);
                    }

                    if (findUserGrade.Price > 0.01m) {
                        return Tuple.Create(ServiceResult.FailedWithMessage("���û��Ѽ���"), orderBuyOutput);
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
                        Password = "���û��ѵ�¼",
                        PayPassword = "���û��ѵ�¼",
                        ParentId = find.ParentId
                    };
                    // }
                } else {
                    if (!RegexHelper.CheckMobile(orderBuyInput.Mobile)) {
                        return Tuple.Create(ServiceResult.FailedWithMessage("�ֻ������ʽ����ȷ"), orderBuyOutput);
                    }
                    // ע�����û�
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
                    // ����ǹ���Ա�����Ƽ���Id��Ϊ������Ƽ���Id
                    if (orderBuyInput.OpenType == UserRightOpenType.AdminOpenHightGrade) {
                        regInput.ParentId = orderBuyInput.ParnetUser.Id;
                    }
                    //
                    var userRegResult = Resolve<IUserBaseService>().Reg(regInput);
                    if (!userRegResult.Item1.Succeeded) {
                        return Tuple.Create(ServiceResult.FailedWithMessage("�»�Աע��ʧ��" + userRegResult),
                            orderBuyOutput);
                    }
                    // await Resolve<IRecordService>().AddAsync(new Record() { Type = typeof(UserDetail).FullName, Value = regInput.ToJsons() });

                    find = Resolve<IUserService>().GetSingle(orderBuyInput.Mobile);

                    var message = $"��ϲ���������˺���ע��ɹ������ĵ�¼�˺�Ϊ��{regInput.Mobile},��ʼ��¼���룺{password} ��ʼ֧�����룺{payPassword} �����Ʊ���.";
                    Resolve<IOpenService>().SendRaw(find.Mobile, message);

                    message = $"���Ƽ����̼���ע��ɹ�.��¼�˺ţ�{regInput.Mobile},��ʼ��¼���룺{password} ��ʼ֧�����룺{payPassword} �뾡�����̼�Ϥ֪,��һʱ��ָ���̼�ʹ��ϵͳ.";

                    var userDetail = Resolve<IUserDetailService>().GetSingle(u => u.UserId == user.Id);
                    userDetail.RegionId = orderBuyInput.RegionId.ToInt64();
                    Resolve<IUserDetailService>().Update(userDetail);
                    Resolve<IOpenService>().SendRaw(user.Mobile, message);
                }

                orderBuyInput.BuyUser = find;
            }

            #endregion ��ȫ��֤

            orderBuyInput.UserRightList = GetList(r => r.UserId == user.Id);

            // ����ǰ���˿�ͨ
            if (orderBuyInput.OpenType == UserRightOpenType.OpenToOtherByRight) {
                return await OpenToOther(orderBuyInput);
            }

            // ���������������Ҫ֧�� +���߹���Ա�����ǿ�ͨ
            return await OpenSelfOrUpgrade(orderBuyInput);
        }

        #endregion �̼ҷ��񶩹�

        #region ��ȡʵ�ʼ۸�

        /// <summary>
        ///     ��ȡ��Ҫ֧���ļ۸�
        /// </summary>
        public Tuple<ServiceResult, decimal> GetPayPrice(UserRightsOrderInput orderInput) {
            var result = ServiceResult.Success;
            if (orderInput.OpenType == UserRightOpenType.OpenToOtherByRight) {
                return Tuple.Create(ServiceResult.FailedWithMessage("��������ʱ�޼۸����"), 0m);
            }

            if (orderInput.OpenType == UserRightOpenType.OpenToOtherByPay) {
                if (orderInput.User.Id == orderInput.BuyUser.Id) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("���ܰ��Լ�����"), 0m);
                }
                var price = orderInput.BuyGrade.Price;
                return Tuple.Create(result, price);
            } else {
                if (orderInput.BuyGrade.Price <= orderInput.CurrentGrade.Price && orderInput.OpenType != UserRightOpenType.AdminOpenHightGrade) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("����ĵȼ����ܵ��ڵ�ǰ�ȼ�"), 0m);
                }

                var findRight = orderInput.UserRightList.FirstOrDefault(r => r.GradeId == orderInput.BuyGrade.Id);
                if (findRight != null && orderInput.OpenType != UserRightOpenType.AdminOpenHightGrade) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("�û���ǰ�ȼ�Ȩ���Ѵ��ڣ������ظ�����"), 0m);
                }

                var userRightsConfigs = Resolve<IAutoConfigService>().GetList<UserRightsConfig>();
                var userGrades = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
                // �����������Ϊ����
                if (userRightsConfigs.Select(r => r.GradeId).Contains(orderInput.User.GradeId)) {
                    if (orderInput.OpenType != UserRightOpenType.Upgrade && orderInput.OpenType != UserRightOpenType.AdminOpenHightGrade) {
                        return Tuple.Create(ServiceResult.FailedWithMessage("��ͨ��ʽ�д�,Ӧ��Ϊ��������"), 0m);
                    }
                } else {
                    if (orderInput.OpenType != UserRightOpenType.OpenSelf) {
                        return Tuple.Create(ServiceResult.FailedWithMessage("��ͨ��ʽ�д�,Ӧ��Ϊ������"), 0m);
                    }
                }

                // ����۸�
                var price = orderInput.BuyGrade.Price;
                if (orderInput.OpenType == UserRightOpenType.Upgrade) {
                    price = orderInput.BuyGrade.Price - orderInput.CurrentGrade.Price;
                }

                if (price <= 0) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("����ȼ��۸�����Ϊ0"), 0m);
                }

                if (price != orderInput.Price) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("ǰ��̨����۸�һ��"), 0m);
                }

                return Tuple.Create(result, price);
            }
        }

        #endregion ��ȡʵ�ʼ۸�

        #region ����˿�ͨ

        /// <summary>
        ///     ����˿�ͨ
        /// </summary>
        /// <param name="orderBuyInput"></param>
        /// <returns></returns>
        public async Task<Tuple<ServiceResult, OrderBuyOutput>> OpenToOther(UserRightsOrderInput orderBuyInput) {
            var result = ServiceResult.Success;
            var orderBuyOutput = new OrderBuyOutput();

            // ����û�������
            var userGrades = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var userRight = GetSingle(r => r.UserId == orderBuyInput.User.Id && r.GradeId == orderBuyInput.GradeId);
            if (userRight == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage($"����{orderBuyInput.BuyGrade?.Name}����˿�"),
                    orderBuyOutput);
            }

            if (userRight.TotalCount - userRight.TotalUseCount < 1) {
                return Tuple.Create(ServiceResult.FailedWithMessage($"��{orderBuyInput.BuyGrade?.Name}����˿ڣ�������"),
                    orderBuyOutput);
            }

            if (orderBuyInput.BuyUser == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("�»�Աδע��ɹ�"), orderBuyOutput);
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

                // ����Kpi��¼��ʹ��Kpi ��¼�����¼����
                Resolve<IKpiService>().Add(kpi);

                // ʹ����+1
                userRight.TotalUseCount += 1;
                Update(userRight);

                // �޸ĵȼ�
                var updateUser = Resolve<IUserService>().GetSingle(r => r.Id == orderBuyInput.BuyUser.Id);
                updateUser.GradeId = orderBuyInput.GradeId;
                Resolve<IUserService>().Update(updateUser);

                // �������Ȩ��
                // ����û�Ȩ��
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

                //��ͨ�ɹ��޸�UserDetail���ַ
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

        #endregion ����˿�ͨ

        #region �Լ���ͨ������,֧��Ǯ�����ѿ�ͨ������Ա��ͨӪ������

        public async Task<Tuple<ServiceResult, OrderBuyOutput>> OpenSelfOrUpgrade(UserRightsOrderInput orderInput) {
            var result = ServiceResult.Success;
            var orderBuyOutput = new OrderBuyOutput();
            //if (orderInput.GradeId == Guid.Parse("72be65e6-3000-414d-972e-1a3d4a366001"))
            //{
            //    result = ServiceResult.FailedWithMessage("��׼�̼Ҳ����Լ�֧����ͨ");
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
                // ������չ����
                var userRightOrder = AutoMapping.SetValue<UserRightsOrderInput>(orderInput);

                order.OrderExtension = new OrderExtension {
                    // �۸���Ϣ
                    OrderAmount = new OrderAmount {
                        TotalProductAmount = payPrice.Item2, // ��Ʒ�ܼ�
                        ExpressAmount = 0m, // �ʷ�
                        FeeAmount = 0m // �����
                    },
                    AttachContent = userRightOrder.ToJsons(),
                    User = orderInput.BuyUser, // �µ��û�
                    Store = null
                };

                order.AccountPay = order.AccountPayPair.ToJsons();
                order.Extension = order.OrderExtension.ToJsons();

                // ��������˲�Ϊ��

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
                    // ֧����¼���ʧ�ܣ��ع�
                    context.RollbackTransaction();
                    return Tuple.Create(payResult.Item1, new OrderBuyOutput());
                }

                //���������֧����¼Id
                var orderIds = orderList.Select(e => e.Id).ToList();
                Resolve<IOrderService>().Update(r => { r.PayId = payResult.Item2.Id; }, e => orderIds.Contains(e.Id));

                // �����ֵ
                orderBuyOutput.PayAmount = payResult.Item2.Amount;
                orderBuyOutput.PayId = payResult.Item2.Id;
                orderBuyOutput.OrderIds = orderList.Select(r => r.Id).ToList();

                //��ͨ�ɹ��޸�UserDetail���ַ
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
            // ɾ������
            Resolve<IUserService>().DeleteUserCache(orderInput.User.Id, orderInput.User.UserName);
            Resolve<IUserService>().DeleteUserCache(orderInput.BuyUser.Id, orderInput.BuyUser.UserName);
            return new Tuple<ServiceResult, OrderBuyOutput>(result, orderBuyOutput);
        }

        #endregion �Լ���ͨ������,֧��Ǯ�����ѿ�ͨ������Ա��ͨӪ������

        #region ��ȡ��ʾ��ͼ

        /// <summary>
        ///     ��ȡ��ʾ��ͼ
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<UserRightsOutput> GetView(long userId) {
            var isAdmin = Resolve<IUserService>().IsAdmin(userId);
            var userRightsOutputList = GetViewByCache(isAdmin);
            if (userId > 0) {
                var user = Resolve<IUserService>().GetSingle(r => r.Id == userId); //  �ǻ����ж�ȡ

                if (user != null && user.Status == Status.Normal) {
                    // ɾ������
                    Resolve<IUserService>().DeleteUserCache(user.Id, user.UserName);
                    var userRightsConfigs = Resolve<IAutoConfigService>().GetList<UserRightsConfig>();
                    var userGrades = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
                    var userGrade = userGrades.FirstOrDefault(r => r.Id == user.GradeId);
                    userRightsOutputList.Foreach(r => {
                        r.CurrnetGradeName = userGrade?.Name;
                        r.UserName = user.GetUserName();
                    });

                    // �������
                    if (userRightsConfigs.Select(r => r.GradeId).Contains(user.GradeId)) {
                        var userRightsList = Resolve<IUserRightsService>().GetList(r => r.UserId == user.Id);

                        if (userGrade != null) {
                            foreach (var userRightOutputItem in userRightsOutputList) {
                                var itemGrade = userGrades.FirstOrDefault(r => r.Id == userRightOutputItem.GradeId);
                                if (itemGrade == null) {
                                    continue;
                                }
                                var gradeConfig = userRightsConfigs.Single(s => s.GradeId == itemGrade.Id);
                                //�Ƿ���ʾ�õȼ�,ǰ���Ѿ��ж��� �����޷������Ƿ���ʾ
                                //if (gradeConfig != null && !gradeConfig.IsShowGradePage)
                                //{
                                //    continue;
                                //}
                                userRightOutputItem.SalePrice = itemGrade.Price;
                                //�Ƿ񿪷Ű�ť
                                if (userRightOutputItem.IsOpen) {
                                    //������Ա
                                    if (itemGrade.Price > userGrade.Price) {
                                        //�ж��Ƿ��Ѿ��Ǽǵ�ַ
                                        var userDetail = Resolve<IUserDetailService>().GetSingle(u => u.UserId == userId);
                                        if (userDetail.RegionId <= 0) {
                                            userRightOutputItem.IsRegion = false;
                                        }

                                        if (gradeConfig.CanUpgradeBySelf) {
                                            userRightOutputItem.OpenType = UserRightOpenType.Upgrade;
                                            userRightOutputItem.Price = itemGrade.Price - userGrade.Price;
                                            userRightOutputItem.ButtonText = $"��{itemGrade.Price - userGrade.Price}����{userRightOutputItem.Name}";
                                        }
                                    } else {
                                        //�����ѿ�ͨ
                                        var userRightItem = userRightsList.FirstOrDefault(r => r.GradeId == userRightOutputItem.GradeId);
                                        long remainCount = 0; // ʣ������
                                        if (userRightItem != null) {
                                            userRightOutputItem.TotalCount = userRightItem.TotalCount;
                                            remainCount = userRightItem.TotalCount - userRightItem.TotalUseCount;
                                        }
                                        //�ж��Ƿ��Ѿ��Ǽǵ�ַ
                                        userRightOutputItem.IsRegion = false;

                                        userRightOutputItem.RemainCount = remainCount;
                                        userRightOutputItem.TotalUseCount = userRightItem == null ? 0 : userRightItem.TotalUseCount;

                                        //// 20190403 ��׼�̼�ֱ���ö˿ڿ�ͨ
                                        //if (remainCount > 0 && userRightOutputItem.GradeId == Guid.Parse("72be65e6-3000-414d-972e-1a3d4a366001"))
                                        //{
                                        //    userRightOutputItem.OpenType = UserRightOpenType.OpenToOtherByRight;
                                        //    userRightOutputItem.Price = 0m;
                                        //    userRightOutputItem.ButtonText = $"ȷ�Ͽ�ͨ{userRightOutputItem.Name}";
                                        //}
                                        //else
                                        //{
                                        //    userRightOutputItem.OpenType = UserRightOpenType.OpenToOtherByPay;
                                        //    userRightOutputItem.Price = itemGrade.Price;
                                        //    userRightOutputItem.ButtonText =
                                        //        $"֧��{itemGrade.Price}Ԫ�����ѿ�ͨ{userRightOutputItem.Name}";
                                        //}

                                        //�ж˿ڵ�ʱ��
                                        if (remainCount > 0) {
                                            if (!gradeConfig.IsHavePortNeedToPay) {
                                                //ʹ�ö˿���������ѿ�ͨ
                                                userRightOutputItem.OpenType = UserRightOpenType.OpenToOtherByRight;
                                                userRightOutputItem.ButtonText = $"�����ѿ�ͨ{itemGrade?.Name}";
                                            } else {
                                                //֧���������ѿ�ͨ
                                                userRightOutputItem.OpenType = UserRightOpenType.OpenToOtherByPay;
                                                userRightOutputItem.Price = itemGrade.Price;
                                                userRightOutputItem.ButtonText = $"֧��{itemGrade.Price}Ԫ�����ѿ�ͨ{userRightOutputItem.Name}";
                                            }
                                        } else {
                                            //�޶˿�
                                            if (gradeConfig.HaveNotPortCanOpen) {
                                                //֧���������ѿ�ͨ
                                                userRightOutputItem.OpenType = UserRightOpenType.OpenToOtherByPay;
                                                userRightOutputItem.Price = itemGrade.Price;
                                                userRightOutputItem.ButtonText = $"֧��{itemGrade.Price}Ԫ�����ѿ�ͨ{userRightOutputItem.Name}";
                                            } else {
                                                userRightOutputItem.ButtonText = null;
                                            }
                                        }
                                        if (gradeConfig.IsCanOnlyAdminOpen) {
                                            userRightOutputItem.OpenType = UserRightOpenType.AdminOpenHightGrade;
                                            userRightOutputItem.ButtonText = $"����ԱȨ�޿�ͨ";
                                        }

                                        ////// ��Ҫ��ȫ��ҪǮ(2019.04.02)
                                        //userRightOutputItem.OpenType = UserRightOpenType.OpenToOtherByPay;
                                        //userRightOutputItem.Price = itemGrade.Price;
                                        //userRightOutputItem.ButtonText =
                                        //    $"֧��{itemGrade.Price}Ԫ�����ѿ�ͨ{userRightOutputItem.Name}";
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return userRightsOutputList;
        }

        #endregion ��ȡ��ʾ��ͼ

        #region

        /// <summary>
        ///     ��ȡ��ʾ��ͼ
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

        #region ��ȡû��userId������

        /// <summary>
        ///     ��ȡû��userId������
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
                //������ǹ���Ա���ж��Ƿ���ʾ�õȼ�
                if (!isAdmin) {
                    //�Ƿ���ʾ�õȼ�
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
                    //�Ƿ񿪷Ű�ť
                    if (userRightsOutput.IsOpen) {
                        if (item.CanUpgradeBySelf) {
                            // �޵�¼�û�
                            userRightsOutput.OpenType = UserRightOpenType.OpenSelf;
                            //if (userRightsOutput.OpenType == UserRightOpenType.OpenToOtherByPay)
                            //    userRightsOutput.ButtonText = $"֧��{itemGrade?.Price}Ԫ��ͨ{itemGrade?.Name}";
                            //else if (userRightsOutput.OpenType == UserRightOpenType.OpenToOtherByRight)
                            //    userRightsOutput.ButtonText = $"�����ѿ�ͨ{itemGrade?.Name}";
                            //else if (userRightsOutput.OpenType == UserRightOpenType.Upgrade)
                            userRightsOutput.ButtonText = $"֧��{itemGrade?.Price}Ԫ����Ϊ{itemGrade?.Name}";
                        }
                    }

                    //else
                    //{
                    //    userRightsOutput.OpenType = UserRightOpenType.None;
                    //    userRightsOutput.ButtonText = $"������ͨ�������ڴ�";
                    //}

                    // ��ȡȨ��
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

        #endregion ��ȡû��userId������

        public List<string> ExcecuteSqlList(List<object> entityIdList) {
            // ��ԱȨ�棬����Idֻ��һ��

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
            // ����û�Ȩ��
            var sql = string.Empty;
            var userRightConfigList = Resolve<IAutoConfigService>().GetList<UserRightsConfig>();
            // �����Ժ���û�Ȩ��
            var upgradeUserRightConfig = userRightConfigList.FirstOrDefault(r => r.GradeId == userRightsOrder.GradeId);
            // ��ǰ�û���Ȩ��
            var currentUserRightConfig =
                userRightConfigList.FirstOrDefault(r => r.GradeId == order.OrderExtension.User.GradeId);
            if (upgradeUserRightConfig != null) {
                foreach (var rightItem in upgradeUserRightConfig.UserRightItems) {
                    var userRightItem = userRightsList.FirstOrDefault(r => r.GradeId == rightItem.GradeId);
                    if (userRightItem != null) {
                        // �����ӵĶ˿�����
                        var count = rightItem.Count;
                        // ��ǰ��Ա�ĵȼ���Ȩ��
                        var currentRightItem =
                            currentUserRightConfig?.UserRightItems?.FirstOrDefault(r =>
                                r.GradeId == userRightItem.GradeId);
                        if (currentRightItem != null) {
                            // ������Ҫ���ӵĶ˿���
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

            // �޸ĵȼ�
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
                        var beforeGrade = Resolve<IGradeService>().GetGrade(buyUser.GradeId); // ��ǰ�ȼ�
                        var afterGrade = Resolve<IGradeService>().GetGrade(upgradeRecord.AfterGradeId);// ������ȼ�
                        if (afterGrade.Id == afterGrade.Id) {
                            //throw new ValidException("�Ƿ�����");
                            Resolve<IUserRightsService>().Log($"������:{userRightsOrder.BuyUser?.Id},order:{order.Id}=>{afterGrade.Id + "==" + afterGrade.Id}�Ƿ�����!");
                            return;
                        }
                        if (afterGrade.Contribute <= beforeGrade.Contribute) {
                            Resolve<IUserRightsService>().Log($"������:{userRightsOrder.BuyUser?.Id},order:{order.Id}=>{afterGrade.Contribute + "<=" + beforeGrade.Contribute}�Ƿ�����!");
                            //throw new ValidException("�Ƿ�����");
                            return;
                        }
                        // ��Ҫ�����п�ͨ, ��Ҫ��Ǯ�ֿ۶˿� 2019.04.02
                        if (true) {
                            // ���˿�, ����û�������
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

                                // ����Kpi��¼��ʹ��Kpi ��¼�����¼����
                                Resolve<IKpiService>().Add(kpi);

                                // ʹ����+1
                                userRight.TotalUseCount += 1;
                                Update(userRight);
                            }
                        }

                        Resolve<IUpgradeRecordService>().Add(upgradeRecord);
                        // ɾ������
                        Resolve<IUserService>().DeleteUserCache(buyUser.Id, buyUser.UserName);
                        // ���Ͷ���
                        SendMessage(buyUser, userRightsOrder.GradeId);
                    }
                }
            }
        }

        /// <summary>
        ///  ���Ͷ���
        /// </summary>
        /// <param name="buyUser"></param>
        /// <param name="gradeId"></param>
        private void SendMessage(User buyUser, Guid gradeId) {
            if (buyUser != null) {
                var buyGrade = Resolve<IGradeService>().GetGrade(gradeId);
                var message = $"�𾴵��û�����,���ѳ�Ϊ{buyGrade.Name},���¼ϵͳ�鿴��ص�Ȩ�����Ϥ��ز���";
                Resolve<IOpenService>().SendRaw(buyUser.Mobile, message);

                message = GetUserRightIntro(buyUser.Id);
                if (!message.IsNullOrEmpty()) {
                    Resolve<IOpenService>().SendRaw(buyUser.Mobile, message);
                }

                // �����Ƽ���
                var parentUser = Resolve<IUserService>().GetSingle(buyUser.ParentId);
                if (parentUser != null) {
                    buyGrade = Resolve<IGradeService>().GetGrade(gradeId);
                    message = $"�𾴵��û�����,���Ƽ����̻�{buyUser.Mobile}�ѳ�Ϊ{buyGrade.Name},��Ϥ֪����ָ���̼���Ϥ{buyGrade.Name}��ص�Ȩ��";
                    Resolve<IOpenService>().SendRaw(parentUser.Mobile, message);

                    message = GetUserRightIntro(parentUser.Id);
                    if (!message.IsNullOrEmpty()) {
                        Resolve<IOpenService>().SendRaw(parentUser.Mobile, message);
                    }
                }
            }
        }

        /// <summary>
        /// ��ȡ�û�Ȩ��������Ϣ
        /// </summary>
        /// <param name="userId"></param>
        private string GetUserRightIntro(long userId) {
            var userRights = GetList(r => r.UserId == userId);
            var message = string.Empty;
            if (userRights.Count > 0) {
                message = $"����ǰ��������Ȩ��˿ڣ�";
                var grades = Resolve<IGradeService>().GetUserGradeList();
                foreach (var item in userRights) {
                    var grade = grades.FirstOrDefault(r => r.Id == item.GradeId);
                    message +=
                        $"{grade.Name}��:{item.TotalCount},��ʹ��:{item.TotalUseCount},ʣ��:{item.TotalCount - item.TotalUseCount} ";
                }
            }
            return message;
        }

        #region ��̨��ӻ�༭

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
            // ��֤�û�
            var result = Resolve<IValidService>().VerifyUser(view.UserName);
            if (!result.Succeeded) {
                return result;
            }
            // ��֤�ȼ�
            result = Resolve<IValidService>().VerifyUserGrade(view.GradeId);
            if (!result.Succeeded) {
                return result;
            }

            var user = Resolve<IUserService>().GetSingle(view.UserName);
            var find = GetSingle(view.Id);
            if (find != null) {
                if (find.GradeId != view.GradeId) {
                    return ServiceResult.FailedWithMessage("���û��õȼ�Ȩ���Ѵ��ڣ���ѡ���༭");
                }
                if (find.UserId != user.Id) {
                    return ServiceResult.FailedWithMessage("�����ܱ༭�����û�Ȩ�棬��������ȷ���û���");
                }

                find.TotalUseCount = view.TotalUseCount.ConvertToLong();
                find.TotalCount = view.TotalCount.ConvertToLong();
                if (!Update(find)) {
                    return ServiceResult.FailedWithMessage("�༭Ȩ��ʧ��");
                }
            } else {
                var model = new UserRights {
                    TotalUseCount = view.TotalUseCount.ConvertToLong(),
                    TotalCount = view.TotalCount.ConvertToLong(),
                    UserId = user.Id,
                    GradeId = view.GradeId
                };
                if (!Add(model)) {
                    return ServiceResult.FailedWithMessage("���ʧ��");
                }
            }

            return ServiceResult.Success;
        }

        #endregion ��̨��ӻ�༭
    }
}