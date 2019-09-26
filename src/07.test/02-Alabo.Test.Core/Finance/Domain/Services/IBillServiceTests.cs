using System;
using System.Linq;
using Xunit;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.Finance.ViewModels.Account;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Finance.Domain.Services
{
    public class IBillServiceTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IBillService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IBillService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        } /*end*/

        [Fact]
        [TestMethod("CreateBill_Account_Decimal_BillActionType_String_Int64_String_String")]
        [TestIgnore]
        public void CreateBill_Account_Decimal_BillActionType_String_Int64_String_String_test()
        {
            //Account account = null;
            //var changeAmount = 0;
            //var actionType = (Alabo.Framework.Core.Enums.Enum.BillActionType)0;
            //var intro = "";
            //var targetUserId = 0;
            //var orderSerial = "";
            //var remark = "";
            //var result = Service<IBillService>().CreateBill(account, changeAmount, actionType, intro, targetUserId, orderSerial, remark);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("CreateBill_Account_Decimal_BillActionType_String")]
        [TestIgnore]
        public void CreateBill_Account_Decimal_BillActionType_String_test()
        {
            //Account account = null;
            //var changeAmount = 0;
            //var actionType = (Alabo.Framework.Core.Enums.Enum.BillActionType)0;
            //var prefixIntro = "";
            //var result = Service<IBillService>().CreateBill(account, changeAmount, actionType, prefixIntro);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("DeductTreeze_User_Currency_Decimal_String")]
        public void DeductTreeze_User_Currency_Decimal_String_test()
        {
            Users.Entities.User user = null;
            var currency = (Currency) 0;
            var amount = 0;
            var Intro = "";
            var result = Resolve<IBillService>().DeductTreeze(user, currency, amount, Intro);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("DeductTreeze_User_MoneyTypeConfig_Decimal_String")]
        [TestIgnore]
        public void DeductTreeze_User_MoneyTypeConfig_Decimal_String_test()
        {
            //User user = null;
            //MoneyTypeConfig typeConfig = null;
            //var amount = 0;
            //var Intro = "";
            //var result = Service<IBillService>().DeductTreeze(user, typeConfig, amount, Intro);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserPage_Object")]
        public void GetUserPage_Object_test()
        {
            //Object query = null;
            //var result = Service<IBillService>().GetUserPage( query);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Increase_User_Currency_Decimal_String")]
        [TestIgnore]
        public void Increase_User_Currency_Decimal_String_test()
        {
            //User user = null;
            //var currency = (Alabo.Framework.Core.Enums.Enum.Currency)0;
            //var amount = 0;
            //var Intro = "";
            //var result = Service<IBillService>().Increase(user, currency, amount, Intro);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Increase_User_MoneyTypeConfig_Decimal_String")]
        [TestIgnore]
        public void Increase_User_MoneyTypeConfig_Decimal_String_test()
        {
            Users.Entities.User user = null;
            MoneyTypeConfig typeConfig = null;
            //var amount = 0;
            //var Intro = "";
            //var result = Service<IBillService>().Increase(user, typeConfig, amount, Intro);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Reduce_User_Currency_Decimal_String")]
        public void Reduce_User_Currency_Decimal_String_test()
        {
            Users.Entities.User user = null;
            var currency = (Currency) 0;
            var amount = 0;
            var Intro = "";
            var result = Resolve<IBillService>().Reduce(user, currency, amount, Intro);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Reduce_User_MoneyTypeConfig_Decimal_String")]
        public void Reduce_User_MoneyTypeConfig_Decimal_String_test()
        {
            Users.Entities.User user = null;
            MoneyTypeConfig typeConfig = null;
            var amount = 0;
            var Intro = "";
            var result = Resolve<IBillService>().Reduce(user, typeConfig, amount, Intro);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Transfer_User_User_MoneyTypeConfig_MoneyTypeConfig_Decimal")]
        [TestIgnore]
        public void Transfer_User_User_MoneyTypeConfig_MoneyTypeConfig_Decimal_test()
        {
            //User user = null;
            //User targetUser = null;
            //MoneyTypeConfig typeConfig = null;
            //MoneyTypeConfig targetTypeConfig = null;
            //var amount = 0;
            //var result = Service<IBillService>().Transfer(user, targetUser, typeConfig, targetTypeConfig, amount);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Transfer_User_User_TransferConfig_Decimal")]
        public void Transfer_User_User_TransferConfig_Decimal_test()
        {
            var config = Resolve<IAutoConfigService>().GetList<TransferConfig>()
                .FirstOrDefault(r => r.Status == Status.Normal);
            var user = Resolve<IUserService>().PlanformUser();
            var targetUser = Resolve<IUserService>().GetSingle("admin");

            var result = Resolve<IBillService>().Transfer(user, targetUser, config, 0.01m);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Treeze_User_Currency_Decimal_String")]
        public void Treeze_User_Currency_Decimal_String_test()
        {
            var user = Resolve<IUserService>().PlanformUser();
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>(r => r.Status == Status.Normal)
                .ToList();
            foreach (var moneyType in moneyTypes)
            {
                var typeConfig = moneyType;
                var view = new ViewAccount
                {
                    Amount = new Random().Next(100, 1000).ToDecimal(),
                    ActionType = 1
                };
                var result = Resolve<IBillService>().Treeze(user, moneyType.Currency, 0.01m, "≤‚ ‘∂≥Ω·");
                Assert.True(result.Succeeded);
            }
        }

        [Fact]
        [TestMethod("Treeze_User_MoneyTypeConfig_Decimal_String")]
        public void Treeze_User_MoneyTypeConfig_Decimal_String_test()
        {
            var user = Resolve<IUserService>().PlanformUser();
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>(r => r.Status == Status.Normal)
                .ToList();
            foreach (var moneyType in moneyTypes)
            {
                var typeConfig = moneyType;
                var view = new ViewAccount
                {
                    Amount = new Random().Next(100, 1000).ToDecimal(),
                    ActionType = 1
                };
                var result = Resolve<IBillService>().Treeze(user, typeConfig, 0.01m, "≤‚ ‘∂≥Ω·");
                Assert.True(result.Succeeded);
            }
        }

        [Fact]
        [TestMethod("TreezeSingle_User_MoneyTypeConfig_Decimal_String")]
        public void TreezeSingle_User_MoneyTypeConfig_Decimal_String_test()
        {
            var user = Resolve<IUserService>().PlanformUser();
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>(r => r.Status == Status.Normal)
                .ToList();
            foreach (var moneyType in moneyTypes)
            {
                var typeConfig = moneyType;
                var view = new ViewAccount
                {
                    Amount = new Random().Next(100, 1000).ToDecimal(),
                    ActionType = 1
                };
                var result = Resolve<IBillService>().TreezeSingle(user, typeConfig, 0.01m, "≤‚ ‘∂≥Ω·");
                Assert.True(result.Succeeded);
            }
        }
    }
}