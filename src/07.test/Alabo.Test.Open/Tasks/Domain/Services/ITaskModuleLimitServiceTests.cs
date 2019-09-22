//using Xunit;
//using Alabo.App.Open.Tasks.Domain.Services;
//using Alabo.Test.Core;
//using System.Linq;
//using System;using Alabo.Domains.Repositories.EFCore;using Alabo.Domains.Repositories.Model;
//using System.Collections.Generic;
//using Alabo.Test.Base.Core.Model;
//using Alabo.App.Open.Tasks.Domain.Entities;
//using  Alabo.Domains.Query;

//namespace Alabo.Test.Open.Tasks.Domain.Services {
//using User = Alabo.App.Core.User.Domain.Entities.User;
//using Product = Alabo.App.Shop.Product.Domain.Entities.Product;
//using Order = Alabo.App.Shop.Order.Domain.Entities.Order;
//using Store = Alabo.App.Shop.Store.Domain.Entities.Store;
//
//using UserType = App.Core.UserType.Domain.Entities.UserType;
//	public class ITaskModuleLimitServiceTests : CoreTest {
//		[Fact]
//		[TestMethod("Initialize_Int64_LimitType_Decimal_String")]
//		public void Initialize_Int64_LimitType_Decimal_String_test () {
//			//var configurationId = 0;
//			//var type = (Alabo.App.Open.Tasks.Domain.Entities.LimitType)0;
//			//var value = 0;
//			//var summary = "";
//			//Service<ITaskModuleLimitService>().Initialize( configurationId, type, value, summary);
//		}

//		[Fact]
//		[TestMethod("ResetValue_Int64_Decimal")]
//		public void ResetValue_Int64_Decimal_test () {
//			//var configurationId = 0;
//			//var value = 0;
//			//Service<ITaskModuleLimitService>().ResetValue( configurationId, value);
//		}

//		[Fact]
//		[TestMethod("AddValue_Int64_Decimal")]
//		public void AddValue_Int64_Decimal_test () {
//			//var configurationId = 0;
//			//var value = 0;
//			//var result = Service<ITaskModuleLimitService>().AddValue( configurationId, value);
//			//Assert.NotNull(result);
//		}

//		[Fact]
//		[TestMethod("ReduceValue_Int64_Decimal")]
//		public void ReduceValue_Int64_Decimal_test () {
//			var configurationId = 0;
//			var value = 0;
//			var result = Service<ITaskModuleLimitService>().ReduceValue( configurationId, value);
//			Assert.NotNull(result);
//		}

//		[Fact]
//		[TestMethod("GetAllowableAndLockValue_Int64_Decimal")]
//		public void GetAllowableAndLockValue_Int64_Decimal_test () {
//			var configurationId = 0;
//			var value = 0;
//			var result = Service<ITaskModuleLimitService>().GetAllowableAndLockValue( configurationId, value);
//			Assert.NotNull(result);
//		}

//		[Fact]
//		[TestMethod("Delete_Int64")]
//		public void Delete_Int64_test () {
//			var configurationId = 0;
//			Service<ITaskModuleLimitService>().Delete( configurationId);
//		}

//		[Fact]
//		[TestMethod("Exist_Int64")]
//		public void Exist_Int64_test () {
//			var configurationId = 0;
//			var result = Service<ITaskModuleLimitService>().Exist( configurationId);
//			Assert.True(result);
//		}

//		[Fact]
//		[TestMethod("GetList_IQuery1")]
//		public void GetList_IQuery1_test () {
//			var result = Service<ITaskModuleLimitService>().GetList( null);
//			Assert.NotNull(result);
//		}

//		[Fact]
//		[TestMethod("GetPagedList_IPageQuery1")]
//		public void GetPagedList_IPageQuery1_test () {
//			var result = Service<ITaskModuleLimitService>().GetPagedList( null);
//			Assert.NotNull(result);
//		}

//		[Fact]
//		[TestMethod("GetSingle_Int64")]
//		public void GetSingle_Int64_test () {
//			var configurationId = 0;
//			var result = Service<ITaskModuleLimitService>().GetSingle( configurationId);
//			Assert.NotNull(result);
//		}

///*end*/

//	}
//}

