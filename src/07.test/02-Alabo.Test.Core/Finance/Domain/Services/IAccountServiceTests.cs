using System.Linq;
using Xunit;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Randoms;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Finance.Domain.Services
{
    public class IAccountServiceTests : CoreTest
    {
        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetAccount_Int64_Guid")]
        public void GetAccount_Int64_Guid_test(long userId)
        {
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            moneyTypes = moneyTypes.Where(r => r.Status == Status.Normal).ToList();
            var user = Resolve<IUserService>().GetRandom(userId);
            foreach (var money in moneyTypes)
            {
                var result = Resolve<IAccountService>().GetAccount(user.Id, money.Id);
                Assert.NotNull(result);
                Assert.True(result.Amount >= 0);
            }
        }

        [TestMethod("GetAccount_Int64")]
        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        public void GetAccount_Int64_test(long id)
        {
            var account = Resolve<IAccountService>().GetRandom(id);

            var result = Resolve<IAccountService>().GetAccount(account.Id);
            Assert.NotNull(result);
            Assert.True(result.Amount >= 0);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetAccount_Int64_Currency")]
        public void GetAccount_Int64_Currency_test(long userId)
        {
            var user = Resolve<IUserService>().GetRandom(userId);
            var cnReuslt = Resolve<IAccountService>().GetAccount(user.Id, Currency.Cny);
            Assert.NotNull(cnReuslt);
            Assert.True(cnReuslt.Amount >= 0);

            var poiontReulst = Resolve<IAccountService>().GetAccount(user.Id, Currency.Point);
            Assert.NotNull(poiontReulst);
            Assert.True(poiontReulst.Amount >= 0);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetUserAllAccount_Int64")]
        public void GetUserAllAccount_Int64_test(long userId)
        {
            var user = Resolve<IUserService>().GetRandom(userId);
            var result = Resolve<IAccountService>().GetUserAllAccount(user.Id);
            Assert.NotNull(result);
            Assert.True(result.Count > 1);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetAccountAmount_Int64_Guid")]
        public void GetAccountAmount_Int64_Guid_test(long userId)
        {
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            moneyTypes = moneyTypes.Where(r => r.Status == Status.Normal).ToList();
            var user = Resolve<IUserService>().GetRandom(userId);
            foreach (var money in moneyTypes)
            {
                var result = Resolve<IAccountService>().GetAccountAmount(user.Id, money.Id);
                Assert.True(result >= 0);
            }
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("ChangeAccountAmount_Int64_Guid_Decimal")]
        public void ChangeAccountAmount_Int64_Guid_Decimal_test(long userId)
        {
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            moneyTypes = moneyTypes.Where(r => r.Status == Status.Normal).ToList();
            var user = Resolve<IUserService>().GetRandom(userId);
            foreach (var money in moneyTypes)
            {
                var userAccount = Resolve<IAccountService>().GetAccount(user.Id, money.Id);
                decimal amount = RandomHelper.Number(1, 100);
                var result = Resolve<IAccountService>().ChangeAccountAmount(user.Id, money.Id, amount);
                Assert.True(result);

                var newAccount = Resolve<IAccountService>().GetAccount(user.Id, money.Id);
                Assert.Equal(userAccount.Amount + amount, newAccount.Amount);

                // ��ԭ
                result = Resolve<IAccountService>().ChangeAccountAmount(user.Id, money.Id, -amount);
                Assert.True(result);
                newAccount = Resolve<IAccountService>().GetAccount(user.Id, money.Id);
                Assert.Equal(userAccount.Amount, newAccount.Amount);
            }
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("UpdateAccountAmount_Int64_Guid_Decimal")]
        public void UpdateAccountAmount_Int64_Guid_Decimal_test(long userId)
        {
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            moneyTypes = moneyTypes.Where(r => r.Status == Status.Normal).ToList();
            var user = Resolve<IUserService>().GetRandom(userId);
            foreach (var money in moneyTypes)
            {
                var userAccount = Resolve<IAccountService>().GetAccount(user.Id, money.Id);
                decimal amount = RandomHelper.Number(1, 100);
                var result = Resolve<IAccountService>().UpdateAccountAmount(user.Id, money.Id, amount);
                Assert.True(result);

                var newAccount = Resolve<IAccountService>().GetAccount(user.Id, money.Id);
                Assert.Equal(userAccount.Amount + amount, newAccount.Amount);

                // ��ԭ
                result = Resolve<IAccountService>().UpdateAccountAmount(user.Id, money.Id, -amount);
                Assert.True(result);
                newAccount = Resolve<IAccountService>().GetAccount(user.Id, money.Id);
                Assert.Equal(userAccount.Amount, newAccount.Amount);
            }
        }

        [Theory]
        [InlineData(2)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("InitSingleUserAccount_Int64")]
        public void InitSingleUserAccount_Int64_test(long userId)
        {
            var user = Resolve<IUserService>().GetRandom(userId);
            if (user != null)
            {
                Resolve<IAccountService>().InitSingleUserAccount(user.Id);
            }
        }

        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<IAccountService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<IAccountService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(100)]
        [InlineData(200)]
        [InlineData(403)]
        [InlineData(504)]
        [InlineData(200)]
        [InlineData(403)]
        [InlineData(504)]
        [InlineData(501)]
        [InlineData(502)]
        [InlineData(303)]
        [InlineData(-1)]
        [TestMethod("GetToken_Int64_MoneyTypeConfig")]
        public void GetToken_Int64_MoneyTypeConfig_test(long entityId)
        {
            var model = Resolve<IAccountService>().GetRandom(entityId);
            if (model != null)
            {
                var config = Resolve<IAutoConfigService>().MoneyTypes()
                    .FirstOrDefault(r => r.Id == model.MoneyTypeId);
                if (config != null)
                {
                    var result = Resolve<IAccountService>().GetToken(model.UserId, config);
                    Assert.NotNull(result);

                    Assert.Equal(34, result.Length);
                }
                else
                {
                    var result = Resolve<IAccountService>().GetToken(model.UserId, config);
                    Assert.Null(result);
                }
            }
        }

        [Fact]
        [TestMethod("CreateAccount_Int64_MoneyTypeConfig")]
        public void CreateAccount_Int64_MoneyTypeConfig_test()
        {
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            moneyTypes = moneyTypes.Where(r => r.Status == Status.Normal).ToList();

            var user = Resolve<IUserService>().LastOrDefault();
            var config = moneyTypes.FirstOrDefault();
            var account = Resolve<IAccountService>().GetAccount(user.Id, config.Id);
            if (account != null)
            {
                Resolve<IAccountService>().Delete(r => r.Id == account.Id);
            }

            var result = Resolve<IAccountService>().CreateAccount(user.Id, config);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAccount_DbTransaction_Int64_Currency")]
        public void GetAccount_DbTransaction_Int64_Currency_test()
        {
            //DbTransaction transation = null;
            //var userid = 0;
            //var currency = (Currency) 0;
            //var result = Service<IAccountService>().GetAccount(transation, userid, currency);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAccount_DbTransaction_Int64_Guid")]
        [TestIgnore]
        public void GetAccount_DbTransaction_Int64_Guid_test()
        {
            //DbTransaction transation = null;
            //var userId = 0;
            //var moneyTypeId =Guid.Empty ;
            //var result = Service<IAccountService>().GetAccount( transation, userId, moneyTypeId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetBlockChainList_Object")]
        public void GetBlockChainList_Object_test()
        {
            object query = null;
            var result = Resolve<IAccountService>().GetBlockChainList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("InitToken")]
        public void InitToken_test()
        {
            Resolve<IAccountService>().InitToken();
        }

        /*end*/
    }
}