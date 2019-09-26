using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Dtos.Bill;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Repositories;
using Alabo.App.Core.Finance.ViewModels.Account;
using Alabo.App.Core.Finance.ViewModels.Bill;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Mapping;

namespace Alabo.App.Core.Finance.Domain.Services {

    /// <summary>
    ///     Class FinanceAdminService.
    /// </summary>
    public class FinanceAdminService : ServiceBase, IFinanceAdminService {

        /// <summary>
        ///     The account repository
        /// </summary>
        private readonly IAccountRepository _accountRepository;

        /// <summary>
        ///     The bill repository
        /// </summary>
        private readonly IBillRepository _billRepository;

        /// <summary>
        ///     The 会员 repository
        /// </summary>
        private readonly IUserRepository _userRepository;

        public FinanceAdminService(IUnitOfWork unitOfWork) : base(unitOfWork) {
            _accountRepository = Repository<IAccountRepository>();
            _billRepository = Repository<IBillRepository>();
            _userRepository = Repository<IUserRepository>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="FinanceAdminService" /> class.
        /// </summary>
        /// <summary>
        ///     获取s the API bill 分页 list.
        /// </summary>
        /// <param name="billApiInput">The bill API input.</param>
        /// <param name="count">The count.</param>
        public IList<Bill> GetApiBillPageList(BillApiInput billApiInput, out long count) {
            var billList = _billRepository.GetApiBillList(billApiInput, out count);
            return billList;
        }

        /// <summary>
        ///     获取s the 视图 bill 分页 list.
        /// </summary>
        /// <param name="userInput">The 会员 input.</param>
        public PagedList<ViewAdminBill> GetViewBillPageList(BillInput userInput) {
            if (!userInput.Serial.IsNullOrEmpty() && userInput.Serial.Length > 8) {
                userInput.Id = long.Parse(userInput.Serial.Substring(1, userInput.Serial.Length - 1).TrimStart('0'));
            }

            var billList = _billRepository.GetBillList(userInput, out var count);

            var userIds = billList.Select(r => r.UserId).Distinct().ToList();
            var otherUserIds = billList.Select(r => r.OtherUserId).Distinct().ToList();
            userIds.AddRange(otherUserIds);
            var users = _userRepository.GetList(userIds);
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            IList<ViewAdminBill> result = new List<ViewAdminBill>();
            foreach (var item in billList) {
                var viewUser = new ViewAdminBill {
                    Bill = item,
                    Id = item.Id
                };
                var user = users.FirstOrDefault(r => r.Id == item.UserId);
                if (user != null) {
                    viewUser.User = user;
                    viewUser.UserId = viewUser.User.Id;
                    viewUser.UserName = Resolve<IUserService>().GetUserStyle(user);
                } else {
                    viewUser.User = new Users.Entities.User();
                }

                var otherUser = users.FirstOrDefault(r => r.Id == item.OtherUserId);
                if (otherUser != null) {
                    viewUser.OtherUser = otherUser;
                    viewUser.OtherUserName = Resolve<IUserService>().GetUserStyle(otherUser);
                    viewUser.OtherUserId = viewUser.OtherUser.Id;
                }

                viewUser.Serial = item.Serial;
                viewUser.MoneyTypeName = moneyTypes.FirstOrDefault(e => e.Id == item.MoneyTypeId)?.Name;
                viewUser.FlowAmoumtStr = item.Flow.GetHtmlName();
                viewUser.Amount = item.Amount;
                viewUser.AfterAmount = item.AfterAmount;
                viewUser.BillTypeName = item.Type.GetHtmlName();
                viewUser.CreatimeStr = item.CreateTime.ToString();
                viewUser.Intro = item.Intro;
                result.Add(viewUser);
            }

            return PagedList<ViewAdminBill>.Create(result, count, userInput.PageSize, userInput.PageIndex);
        }

        public BillViewApiOutput GetBillOutput(ViewAdminBill view) {
            BillViewApiOutput apiOutput = new BillViewApiOutput {
                Amount = view.Bill.Amount.ToDecimal(),
                AfterAmount = view.Bill.AfterAmount.ToDecimal(),
                UserName = view.User.GetUserName(),
                OtherUserName = view.OtherUser?.GetUserName(),
                CreateTime = view.Bill.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                SerialNum = view.Bill.Serial,
                MoneyTypeName = view.MoneyTypeConfig.Name,
                AcctionType = view.Bill.Type.GetDisplayName(),
                Flow = view.Bill.Flow.GetDisplayName(),
                Intro = view.Bill.Intro
            };

            return apiOutput;
        }

        /// <summary>
        ///     获取s the 视图 bill single.
        /// </summary>
        /// <param name="id">Id标识</param>
        public ViewAdminBill GetViewBillSingle(long id) {
            var bill = Resolve<IBillService>().GetSingle(r => r.Id == id);
            if (bill == null) {
                return null;
            }

            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var grades = Resolve<IGradeService>().GetUserGradeList();
            var viewAdminBill = new ViewAdminBill {
                Bill = bill,
                User = Resolve<IUserService>().GetSingle(bill.UserId),
                OtherUser = Resolve<IUserService>().GetSingle(bill.OtherUserId),
                MoneyTypeConfig = moneyTypes.FirstOrDefault(r => r.Id == bill.MoneyTypeId)
            };
            viewAdminBill.UserGrade = grades.FirstOrDefault(r => r.Id == viewAdminBill.User?.GradeId);
            viewAdminBill.OtherUserGrade = grades.FirstOrDefault(r => r.Id == viewAdminBill.OtherUser?.GradeId);

            //下一条账单
            viewAdminBill.NextBill = Resolve<IBillService>().Next(bill);
            viewAdminBill.PrexBill = Resolve<IBillService>().Prex(bill);

            return viewAdminBill;
        }

        /// <summary>
        ///     获取s the 视图 会员 分页 list.
        /// </summary>
        /// <param name="userInput">The 会员 input.</param>
        public PagedList<ViewUserAccounts> GetViewUserPageList(UserInput userInput) {
            var viewUserList = _userRepository.GetViewUserList(userInput, out var count);
            var userIds = viewUserList.Select(r => r.Id).Distinct().ToList();
            var accounts = _accountRepository.GetAccountByUserIds(userIds);

            IList<ViewUserAccounts> result = new List<ViewUserAccounts>();

            foreach (var item in viewUserList) {
                var viewUser = new ViewUserAccounts {
                    User = item,
                    Accounts = accounts.Where(r => r.UserId == item.Id).ToList()
                };
                result.Add(viewUser);
            }

            return PagedList<ViewUserAccounts>.Create(result, count, userInput.PageSize, userInput.PageIndex);
        }

        /// <summary>
        ///     获取s the bill 分页 list.
        /// </summary>
        /// <param name="query">查询</param>
        public PagedList<ViewAdminBill> GetBillPageList(object query) {
            var dic = query.ToObject<Dictionary<string, string>>();
            var userInput = AutoMapping.SetValue<BillInput>(dic);
            var model = GetViewBillPageList(userInput);
            return model;
        }
    }
}