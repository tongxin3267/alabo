//TODO �����ع� 2019��9��25��

//using System;
//using System.Collections.Generic;
//using Alabo.Domains.Repositories.EFCore;
//using Alabo.Domains.Repositories.Model;
//using System.Linq;
//using MongoDB.Bson;
//using Alabo.Domains.Services;
//using Alabo.Datas.UnitOfWorks;
//using Alabo.Domains.Repositories;
//using Alabo.App.Asset.Withdraws.Domain.Entities;
//using Alabo.App.Core.Common.Domain.CallBacks;
//using Alabo.App.Core.Common.Domain.Services;
//using Alabo.App.Core.Finance.Domain.CallBacks;
//using Alabo.App.Core.Finance.Domain.Dtos.WithDraw;
//using Alabo.App.Core.Finance.Domain.Entities;
//using Alabo.App.Core.Finance.Domain.Entities.Extension;
//using Alabo.App.Core.Finance.Domain.Enums;
//using Alabo.App.Core.Finance.Domain.Repositories;
//using Alabo.App.Core.Finance.Domain.Services;
//using Alabo.App.Core.Finance.ViewModels.WithDraw;
//using Alabo.App.Core.User;
//using Alabo.App.Core.User.Domain.Services;
//using Alabo.Framework.Core.Enums.Enum;
//using Alabo.Framework.Core.Extensions;
//using Alabo.Domains.Entities;
//using Alabo.Domains.Enums;
//using Alabo.Domains.Query;
//using Alabo.Extensions;
//using Alabo.Mapping;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Routing.Constraints;

//namespace Alabo.App.Asset.Withdraws.Domain.Services {
//    public class WithdrawService : ServiceBase<Withdraw, long>, IWithdrawService {
//        public WithdrawService(IUnitOfWork unitOfWork, IRepository<Withdraw, long> repository) : base(unitOfWork,
//            repository) {
//        }

//        / <summary>
//        /     ����
//        /     �������֣�
//        /     1. ֧���������ֵĻ������ͣ�����������Ͳ������֣����ֹ����
//        /     2. ����֮ǰ��WithdRawConfig�������ã�������֤
//        /     3. ����ʱҪ�����ʽ�
//        / </summary>
//        / <param name = "view" > �������� </ param >
//        public ServiceResult Add(WithDrawInput view) {
//            var serviceResult = ServiceResult.Success;

//            #region ��ȫ��֤

//            if (view.MoneyTypeId.IsGuidNullOrEmpty()) {
//                return ServiceResult.FailedWithMessage("��ѡ�������˻�");
//            }

//            var user = Resolve<IUserService>().GetUserDetail(view.UserId);
//            if (user == null) {
//                return ServiceResult.FailedWithMessage("�û�������");
//            }

//            if (user.Status != Status.Normal) {
//                return ServiceResult.FailedWithMessage("�û�״̬������");
//            }

//            if (view.PayPassword.ToMd5HashString() != user.Detail.PayPassword) {
//                return ServiceResult.FailedWithMessage("֧�����벻��ȷ");
//            }

//            var adminConfig = Resolve<IAutoConfigService>().GetValue<AdminCenterConfig>();
//            if (!adminConfig.IsAllowUserWitraw) {
//                return ServiceResult.FailedWithMessage("��ǰʱ���ϵͳ�ڽ����ڲ�����,�벻Ҫ���֡����Ժ����ԣ����������Ĳ�����б�Ǹ!");
//            }

//            if (view.Amount <= 0) {
//                return ServiceResult.FailedWithMessage("���ֶ�ȱ������0");
//            }

//            var moneyType = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>()
//                .FirstOrDefault(r => r.Id == view.MoneyTypeId && r.Status == Status.Normal && r.IsWithDraw);
//            if (moneyType == null) {
//                return ServiceResult.FailedWithMessage("��ǰ�˻���֧������"); //��״̬�����������ڿ�������н�������
//            }

//            var withDrawConfig = Resolve<IAutoConfigService>().GetValue<WithddrawConfig>();
//            if (withDrawConfig == null) {
//                return ServiceResult.FailedWithMessage("���������쳣");
//            }

//            if (!withDrawConfig.CanWithdRaw) {
//                return ServiceResult.FailedWithMessage(withDrawConfig.CanNotWithdrawIntro);
//            }

//            var userAcount = Resolve<IAccountService>()
//                .GetSingle(e => e.MoneyTypeId == view.MoneyTypeId && e.UserId == view.UserId);
//            if (userAcount.Amount < view.Amount) {
//                return ServiceResult.FailedWithMessage("���ֶ�Ȳ���");
//            }

//            if (withDrawConfig.IsIdentity) {
//                if (user.Detail.IdentityStatus != IdentityStatus.IsChecked) {
//                    return ServiceResult.FailedWithMessage($"����ǰ��ʵ����֤");
//                }
//            }

//            if (withDrawConfig.MinAmount > view.Amount) {
//                return ServiceResult.FailedWithMessage($"���ֶ�Ȳ���С��{withDrawConfig.MinAmount}");
//            }

//            if (withDrawConfig.MaxAmount < view.Amount) {
//                return ServiceResult.FailedWithMessage($"���ֶ�Ȳ��ܴ���{withDrawConfig.MaxAmount}");
//            }

//            if (withDrawConfig.IsMinAmountMultiple != 0 && withDrawConfig.MinAmount != 0 &&
//                view.Amount % withDrawConfig.MinAmount != 0) {
//                return ServiceResult.FailedWithMessage($"���ֶ�ȱ���Ϊ{withDrawConfig.MinAmount}�ı���");
//            }

//            if (!withDrawConfig.CanSerialWithDraw) {
//                if (Resolve<ITradeService>().GetList(e =>
//                        e.Type == TradeType.Withdraw && e.Status != TradeStatus.Failured &&
//                        e.Status != TradeStatus.Success).Count() > 0) {
//                    return ServiceResult.FailedWithMessage("����һ���������ڴ����У����������ѱ�ȡ����");
//                }
//            }

//            #endregion ��ȫ��֤

//            var trade = AutoMapping.SetValue<Trade>(view);
//            trade.Type = TradeType.Withdraw;
//            ���п���Ϣ����
//            var backCard = Resolve<IBankCardService>().GetSingle(u => u.Number == view.BankCardId);
//            var backCard = new BankCard();
//            trade.TradeExtension.TradeRemark = new TradeRemark { UserRemark = view.UserRemark };
//            trade.TradeExtension.WithDraw = new TradeWithDraw {
//                BankCard = backCard,
//                CheckAmount = view.Amount * (1 - withDrawConfig.Fee),
//                Fee = view.Amount * withDrawConfig.Fee
//            };

//            var userBill = Resolve<IBillService>().CreateBill(userAcount, -view.Amount, BillActionType.Treeze);
//            userBill.Intro = $"�û�{user.GetUserName()}�������ֲ���,{moneyType.Name}����{view.Amount}";

//            userAcount.HistoryAmount = userAcount.Amount;
//            userAcount.Amount -= view.Amount;
//            userAcount.FreezeAmount += view.Amount;

//            trade.TradeExtension.WithDraw.AfterAmount = userBill.AfterAmount;
//            trade.Extension = trade.TradeExtension.ToJson();

//            var context = Repository<ITradeRepository>().RepositoryContext;
//            context.BeginTransaction();
//            try {
//                Resolve<ITradeService>().Add(trade);
//                Resolve<IAccountService>().Update(userAcount);
//                userBill.EntityId = trade.Id;
//                Resolve<IBillService>().Add(userBill);
//                context.SaveChanges();
//                context.CommitTransaction();
//            } catch (Exception ex) {
//                context.RollbackTransaction();
//                return ServiceResult.FailedWithMessage("����ʧ��:" + ex.Message);
//            } finally {
//                context.DisposeTransaction();
//            }

//            return serviceResult;
//        }

//        / <summary>
//        /     �û�ɾ�����֣���ȡ�����֣���˳ɹ������ּ�¼����ɾ��
//        /     ɾ�����֣�ֻ�ܶ�δ��˵����ֽ���ɾ��
//        /     ɾ������ʱ��������ʽ���Ҫ�ⶳ
//        / </summary>
//        / <param name = "userId" > ��ԱId </ param >
//        / < param name="id">Id��ʶ</param>
//        public ServiceResult Delete(long userId, long id) {
//            var serviceResult = ServiceResult.Success;
//            var trade = Resolve<ITradeService>()
//                .GetSingle(e => e.Type == TradeType.Withdraw && e.UserId == userId && e.Id == id);
//            var user = Resolve<IUserService>().GetSingle(userId);
//            var userAcount = Resolve<IAccountService>()
//                .GetSingle(e => e.UserId == userId && e.MoneyTypeId == trade.MoneyTypeId);
//            var moneyType = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>()
//                .FirstOrDefault(r => r.Id == trade.MoneyTypeId && r.Status == Status.Normal);
//            if (trade != null) {
//                if (trade.Status == TradeStatus.Pending) {
//                    var userBill = Resolve<IBillService>()
//                        .CreateBill(userAcount, trade.Amount, BillActionType.DeductTreeze);
//                    userBill.Intro = $"�û�{user.GetUserName()}�������ֲ���,{moneyType.Name}�ⶳ{trade.Amount}";
//                    userBill.EntityId = trade.Id;

//                    userAcount.HistoryAmount = userAcount.Amount;
//                    userAcount.Amount += trade.Amount;
//                    userAcount.FreezeAmount -= trade.Amount;

//                    var context = Repository<ITradeRepository>().RepositoryContext;
//                    context.BeginTransaction();
//                    try {
//                        Resolve<ITradeService>().Delete(e => e.Id == trade.Id);
//                        Resolve<IBillService>().Add(userBill);
//                        context.SaveChanges();
//                        context.CommitTransaction();
//                    } catch (Exception ex) {
//                        context.RollbackTransaction();
//                        return ServiceResult.FailedWithMessage("����ʧ��:" + ex.Message);
//                    } finally {
//                        context.DisposeTransaction();
//                    }
//                }

//                if (trade.Status == TradeStatus.Failured) {
//                    Resolve<ITradeService>().Delete(e => e.Id == trade.Id);
//                    return serviceResult;
//                }

//                if (trade.Status == TradeStatus.Success) {
//                    return ServiceResult.FailedWithMessage("���ֳɹ���¼������ɾ��!");
//                }

//                return ServiceResult.FailedWithMessage("����������ּ�¼������ɾ��!");
//            }

//            return ServiceResult.FailedWithMessage("��ǰ���ּ�¼������!");
//        }

//        / <summary>
//        /     ��ȡ��������
//        / </summary>
//        / <param name = "userId" > ��ԱId </ param >
//        / < param name="id">Id��ʶ</param>
//        public WithDrawShowOutput GetSingle(long userId, long id) {
//            var trade = Resolve<ITradeService>()
//                .GetSingle(r => r.Id == id && r.UserId == userId && r.Type == TradeType.Withdraw);
//            var monenyTypeConfigs = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
//            if (trade != null) {
//                var user = Resolve<IUserService>().GetSingle(userId);
//                var moneyConfigs = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
//                var withDrawOutput = AutoMapping.SetValue<WithDrawShowOutput>(trade);
//                withDrawOutput.Fee = trade.TradeExtension.WithDraw.Fee;
//                withDrawOutput.UserName = user.UserName;
//                withDrawOutput.PayTime = trade.PayTime.ToString("yyyy-MM-dd HH:mm:ss");
//                withDrawOutput.CreateTime = trade.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
//                withDrawOutput.Status = trade.Status.GetDisplayName();
//                withDrawOutput.MoneyTypeConfig = monenyTypeConfigs.SingleOrDefault(e => e.Id == trade.MoneyTypeId);
//                withDrawOutput.Remark = trade.TradeExtension.TradeRemark.Remark;
//                withDrawOutput.UserRemark = trade.TradeExtension.TradeRemark.UserRemark;
//                withDrawOutput.BankCardInfo =
//                    $"�ֿ���:[{trade.TradeExtension.WithDraw.BankCard.Name}]����:[{trade.TradeExtension.WithDraw.BankCard.Number}]{trade.TradeExtension.WithDraw.BankCard.Type.GetDisplayName()}";
//                return withDrawOutput;
//            }

//            return null;
//        }

//        public PagedList<Trade> GetUserWithDrawList(WithDrawApiInput parameter) {
//            var query = new ExpressionQuery<Trade> {
//                PageIndex = (int)parameter.PageIndex,
//                PageSize = (int)parameter.PageSize
//            };
//            query.And(e => e.UserId == parameter.LoginUserId);
//            query.And(e => e.Type == TradeType.Withdraw);
//            query.OrderByDescending(r => r.Id);
//            var model = Resolve<ITradeService>().GetPagedList(query);
//            return model;
//        }

//        / <summary>
//        /     ��̨��ҳ
//        / </summary>
//        / <param name = "query" > ��ѯ </ param >
//        public PagedList<WithDrawOutput> GetPageList(object query) {
//            ��ȡ��ǰ��Ӧ�̵����ж���
//           var list = Resolve<ITradeService>().GetPagedList(query, r => r.Type == TradeType.Withdraw);
//            var moneyType = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
//            var users = Resolve<IUserService>().GetList();
//            var resultList = new List<WithDrawOutput>();
//            var model = new WithDrawOutput();
//            foreach (var item in list) {
//                model = AutoMapping.SetValue<WithDrawOutput>(item);
//                if (item.TradeExtension != null) {
//                    model.Fee = item.TradeExtension.WithDraw.Fee;
//                    model.UserRemark = item.TradeExtension.TradeRemark.UserRemark;
//                }

//                model.MoneyTypeName = moneyType.FirstOrDefault(u => u.Id == item.MoneyTypeId)?.Name;
//                model.UserName = users.FirstOrDefault(u => u.Id == item.UserId)?.UserName;
//                model.BankName = item.TradeExtension?.WithDraw?.BankCard?.Address;
//                model.CardId = item.TradeExtension?.WithDraw?.BankCard?.Number;
//                model.RealName = item.TradeExtension?.WithDraw?.BankCard?.Name;

//                resultList.Add(model);
//            }

//            return PagedList<WithDrawOutput>.Create(resultList, list.Count, list.PageSize, list.PageIndex);
//        }

//        / <summary>
//        /     ��̨��ҳ
//        / </summary>
//        / <param name = "query" > ��ѯ </ param >
//        public PagedList<ViewAdminWithDraw> GetAdminPageList(object query) {
//            ��ȡ��ǰ��Ӧ�̵����ж���
//           var list = Resolve<ITradeService>().GetPagedList(query, r => r.Type == TradeType.Withdraw);
//            var moneyType = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
//            var users = Resolve<IUserService>().GetList();
//            var resultList = new List<ViewAdminWithDraw>();
//            var model = new ViewAdminWithDraw();
//            foreach (var item in list) {
//                model = AutoMapping.SetValue<ViewAdminWithDraw>(item);
//                if (item.TradeExtension != null) {
//                    model.Fee = item.TradeExtension.WithDraw.Fee;
//                    model.UserRemark = item?.TradeExtension?.TradeRemark?.UserRemark;
//                }

//                model.MoneyTypeConfig = moneyType.FirstOrDefault(u => u.Id == item.MoneyTypeId);
//                model.UserName = users.FirstOrDefault(u => u.Id == item.UserId)?.UserName;
//                model.BankCard = item.TradeExtension?.WithDraw?.BankCard;
//                model.StatusName = item.Status.GetDisplayName();

//                resultList.Add(model);
//            }

//            return PagedList<ViewAdminWithDraw>.Create(resultList, list.Count, list.PageSize, list.PageIndex);
//        }

//        / <summary>
//        /     ��������
//        / </summary>
//        / <param name = "id" > Id��ʶ </ param >
//        public ViewAdminWithDraw GetAdminWithDraw(long id) {
//            var withDraw = Resolve<ITradeService>().GetSingle(e => e.Id == id);
//            if (withDraw == null) {
//                return new ViewAdminWithDraw();
//            }

//            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
//            var model = AutoMapping.SetValue<ViewAdminWithDraw>(withDraw);
//            model.User = Resolve<IUserService>().GetSingle(withDraw.UserId);
//            var grades = Resolve<IGradeService>().GetUserGradeList();
//            if (withDraw.TradeExtension != null) {
//                model.Fee = withDraw.TradeExtension.WithDraw.Fee;
//                model.UserRemark = withDraw.TradeExtension.TradeRemark.UserRemark;
//                model.Remark = withDraw.TradeExtension.TradeRemark.Remark;
//                model.FailuredReason = withDraw.TradeExtension.TradeRemark.FailuredReason;
//            }

//            model.AfterAmount = withDraw.TradeExtension.WithDraw.AfterAmount;

//            Ӧ�������
//            model.CheckAmount = withDraw.TradeExtension.WithDraw.CheckAmount;
//            model.ActualAmount = withDraw.Amount;
//            model.UserGrade = grades.FirstOrDefault(r => r.Id == model.User?.GradeId);
//            model.BankCard = withDraw.TradeExtension.WithDraw.BankCard;
//            model.MoneyTypeConfig = moneyTypes.FirstOrDefault(r => r.Id == withDraw.MoneyTypeId);
//            ��һ���˵�
//            model.NextWithDraw = Resolve<ITradeService>().Next(withDraw);

//            ��һ���˵�

//            model.PrexWithDraw = Resolve<ITradeService>().Prex(withDraw);
//            if (model.PrexWithDraw == null) {
//                model.PrexWithDraw = withDraw;
//            }

//            return model;
//        }

//        / <summary>
//        /     ��ȡ����֧�����ֵĻ�������
//        / </summary>
//        public IList<KeyValue> GetWithDrawMoneys(long userId) {
//            var moneys = Resolve<IAutoConfigService>().MoneyTypes();
//            var userAcounts = Resolve<IAccountService>().GetUserAllAccount(userId);
//            var dic = new Dictionary<string, object>();
//            moneys.Foreach(r => {
//                if (r.IsWithDraw) {
//                    var account = userAcounts.FirstOrDefault(e => e.MoneyTypeId == r.Id);
//                    dic.Add(r.Id.ToString(), r.Name + $"[���:{account.Amount}]");
//                }
//            });

//            var list = new List<KeyValue>();
//            foreach (var item in dic) {
//                var keyValue = new KeyValue {
//                    Value = item.Value,
//                    Key = item.Key
//                };
//                list.Add(keyValue);
//            }

//            return list;
//        }

//        / <summary>
//        /     ������� �������������
//        / </summary>
//        / <param name = "httpContext" > The HTTP context.</param>
//        public ServiceResult Check(DefaultHttpContext httpContext) {
//            var view = AutoMapping.SetValue<ViewWithDrawCheck>(httpContext);

//            return WithDrawCheck(view);
//        }

//        / <summary>
//        /     ��ȡ�û������б�
//        / </summary>
//        / <param name = "parameter" > ���� </ param >
//        public PagedList<Trade> GetUserList(WithDrawApiInput parameter) {
//            var query = new ExpressionQuery<Trade> {
//                PageIndex = (int)parameter.PageIndex,
//                PageSize = (int)parameter.PageSize
//            };
//            query.And(e => e.UserId == parameter.LoginUserId);
//            query.And(e => e.Type == TradeType.Withdraw);
//            query.OrderByDescending(r => r.Id);
//            var model = Resolve<ITradeService>().GetPagedList(query);
//            return model;
//        }

//        / <summary>
//        /     �������
//        / </summary>
//        / <param name = "view" > The ��ͼ.</param>
//        public ServiceResult WithDrawCheck(ViewWithDrawCheck view) {
//            var result = ServiceResult.Success;

//            #region ����У��

//            if (view.Status == TradeStatus.Failured) {
//                if (view.FailuredReason.IsNullOrEmpty()) {
//                    return ServiceResult.FailedWithMessage("���ʧ�ܣ�ʧ��ԭ����Ϊ��");
//                }
//            }

//            var withDraw = Resolve<ITradeService>().GetSingle(r => r.Id == view.TradeId);
//            if (withDraw == null) {
//                return ServiceResult.FailedWithMessage("�û�û�����ּ�¼");
//            }

//            var user = Resolve<IUserService>().GetSingle(withDraw.UserId);
//            if (user == null) {
//                return ServiceResult.FailedWithMessage("�����û�������");
//            }

//            var moneyTypeList = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>(r => r.Status == Status.Normal);
//            if (moneyTypeList == null || moneyTypeList.Count == 0) {
//                return ServiceResult.FailedWithMessage("�������ò����ڻ�״̬�����������ڿ�������н�������");
//            }

//            var drawMoneyType = moneyTypeList.FirstOrDefault(r =>
//                r.Id == withDraw.MoneyTypeId && r.Status == Status.Normal && r.IsWithDraw);
//            if (drawMoneyType == null) {
//                return ServiceResult.FailedWithMessage("�����˻��������Ͳ����ڣ���״̬�����������ڿ�������н�������");
//            }

//            var account = Resolve<IAccountService>()
//                .GetSingle(r => r.UserId == user.Id && r.MoneyTypeId == drawMoneyType.Id);
//            if (account == null) {
//                return ServiceResult.FailedWithMessage("�û��޸������˻�");
//            }

//            #endregion ����У��

//            var context = Repository<ITradeRepository>().RepositoryContext;
//            if (view.Status == TradeStatus.Failured) {
//                ����׼��
//                var bill = new Bill {
//                    EntityId = withDraw.Id,
//                    Intro =
//                        $@"{user.GetUserName()}����{drawMoneyType.Name}����{
//                                withDraw.Amount
//                            }������Ա����ͨ�����˻��˻�{withDraw.Amount}",
//                    UserId = withDraw.UserId,
//                    Type = BillActionType.Withdraw,
//                    Flow = AccountFlow.Income,
//                    MoneyTypeId = withDraw.MoneyTypeId,
//                    Amount = withDraw.Amount,
//                    AfterAmount = account.Amount + withDraw.Amount
//                };
//                if (withDraw.Status == TradeStatus.FirstCheckSuccess) {
//                    bill.Intro =
//                        $@"{user.GetUserName()}����{drawMoneyType.Name}����{
//                                withDraw.Amount
//                            }������Ա����ͨ�����˻��˻�{withDraw.Amount}";
//                }

//                withDraw.Status = view.Status;
//                withDraw.TradeExtension.TradeRemark.Remark =
//                    $"{withDraw.TradeExtension.TradeRemark.Remark} {view.Remark}";
//                withDraw.TradeExtension.TradeRemark.FailuredReason = view.FailuredReason;
//                withDraw.Extension = withDraw.TradeExtension.ToJson();
//                account.Amount += withDraw.Amount;
//                account.FreezeAmount -= withDraw.Amount;
//                context.BeginTransaction();

//                try {
//                    Resolve<IAccountService>().Update(account);
//                    Resolve<ITradeService>().Update(withDraw);
//                    Resolve<IBillService>().Add(bill);
//                    context.SaveChanges();
//                    context.CommitTransaction();
//                } catch (Exception ex) {
//                    context.RollbackTransaction();
//                    return ServiceResult.FailedWithMessage("����ʧ��:" + ex.Message);
//                } finally {
//                    context.DisposeTransaction();
//                }
//            }

//            ��˳ɹ�
//            if (view.Status == TradeStatus.Success) {
//                var bill = new Bill {
//                    EntityId = withDraw.Id,
//                    Intro = $@"{user.GetUserName()}����{drawMoneyType.Name}����{withDraw.Amount}�ɹ�",
//                    UserId = withDraw.UserId,
//                    Type = BillActionType.Withdraw,
//                    Flow = AccountFlow.Spending,
//                    MoneyTypeId = withDraw.MoneyTypeId,
//                    Amount = withDraw.Amount,
//                    AfterAmount = account.Amount
//                };
//                withDraw.PayTime = DateTime.Now;
//                withDraw.Status = view.Status;
//                withDraw.TradeExtension.TradeRemark.Remark =
//                    $"{withDraw.TradeExtension.TradeRemark.Remark} {view.Remark}";

//                withDraw.TradeExtension.TradeRemark.Remark = view.Remark;
//                withDraw.Extension = withDraw.TradeExtension.ToJson();

//                account.FreezeAmount -= withDraw.Amount;

//                TODO 9���ع�ע��
//                var shareOrder = new ShareOrder {
//                    UserId = withDraw.UserId,
//                    Amount = withDraw.Amount,
//                    Status = ShareOrderStatus.Pending,
//                    TriggerType = TriggerType.WithDraw,
//                    EntityId = withDraw.Id
//                };

//                context.BeginTransaction();
//                try {
//                    Resolve<IAccountService>().Update(account);
//                    Resolve<ITradeService>().Update(withDraw);
//                    Resolve<IBillService>().Add(bill);
//                    TODO 9���ع�ע��
//                     Resolve<IShareOrderService>().Add(shareOrder);
//                    context.SaveChanges();
//                    context.CommitTransaction();
//                } catch (Exception ex) {
//                    context.RollbackTransaction();
//                    return ServiceResult.FailedWithMessage("����ʧ��:" + ex.Message);
//                } finally {
//                    context.DisposeTransaction();
//                }
//            }

//            if (view.Status == TradeStatus.FirstCheckSuccess) {
//                withDraw.Status = view.Status;
//                withDraw.TradeExtension.TradeRemark.Remark =
//                    $"{withDraw.TradeExtension.TradeRemark.Remark} {view.Remark}";
//                withDraw.TradeExtension.TradeRemark.Remark = view.Remark;
//                withDraw.Extension = withDraw.TradeExtension.ToJson();
//                Resolve<ITradeService>().Update(withDraw);
//            }

//            return result;
//        }

//        / <summary>
//        /     ��Ա�������ּ�¼
//        / </summary>
//        / <param name = "query" ></ param >
//        public PagedList<ViewHomeWithDraw> GetUserPage(object query) {
//            var user = query.ToUserObject();
//            var resultList = Resolve<ITradeService>()
//                .GetPagedList<ViewHomeWithDraw>(query, r => r.Type == TradeType.Withdraw && r.UserId == user.Id);
//            var moenyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
//            resultList.ForEach(r => {
//                r.MoneyTypeName = moenyTypes.FirstOrDefault(u => u.Id == r.MoneyTypeId)?.Name;
//                if (r.TradeExtension != null) {
//                    r.Fee = r.TradeExtension.WithDraw.Fee;
//                }
//            });
//            return resultList;
//        }

//        / <summary>
//        /     ����Edit
//        / </summary>
//        public ViewHomeWithDraw GetView(long id) {
//            var viewHomeWithDraw = new ViewHomeWithDraw();
//            return viewHomeWithDraw;
//        }
//    }
//}