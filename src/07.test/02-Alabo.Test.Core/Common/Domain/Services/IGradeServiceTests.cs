using System;
using System.Linq;
using Xunit;
using Alabo.App.Agent.Citys.Domain.CallBacks;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.UserType.Modules.City;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Common.Domain.Services {

    public class IGradeServiceTests : CoreTest {

        [Theory]
        [TestMethod("GetGradeListByKey_String")]
        [InlineData(typeof(UserGradeConfig))]
        [InlineData(typeof(CityGradeConfig))]
        public void GetGradeListByKey_String_test(Type type) {
            var result = Resolve<IGradeService>().GetGradeListByKey(type.FullName);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId) {
            //var model = Service<IAdminService>().GetRandom(entityId);
            //if (model != null)
            //{
            //    var newModel = Service<IAdminService>().GetSingleFromCache(model.Id);
            //    Assert.NotNull(newModel);
            //    Assert.Equal(newModel.Id, model.Id);
            //}
        } /*end*/

        [Fact]
        [TestMethod("GetGrade_Guid_Guid")]
        [TestIgnore]
        public void GetGrade_Guid_Guid_test() {
            //var userTypeId =Guid.Empty ;
            //var gradeId =Guid.Empty ;
            //var result = Service<IGradeService>().GetGrade( userTypeId, gradeId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetGrade_Guid")]
        public void GetGrade_Guid_test() {
            var grade = Resolve<IGradeService>().GetUserGradeList().FirstOrDefault();
            var result = Resolve<IGradeService>().GetGrade(grade.Id);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetGradeByUserTypeIdAndGradeId_Guid_Guid")]
        [TestIgnore]
        public void GetGradeByUserTypeIdAndGradeId_Guid_Guid_test() {
            //var userTypeId =Guid.Empty ;
            //var gradeId =Guid.Empty ;
            //var result = Service<IGradeService>().GetGradeByUserTypeIdAndGradeId( userTypeId, gradeId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetGradeListByGuid_Guid")]
        public void GetGradeListByGuid_Guid_test() {
            var guid = Guid.Empty;
            var result = Resolve<IGradeService>().GetGradeListByGuid(guid);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserGradeList")]
        public void GetUserGradeList_test() {
            var result = Resolve<IGradeService>().GetUserGradeList();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUsersGradeInfo_IEnumerable1")]
        [TestIgnore]
        public void GetUsersGradeInfo_IEnumerable1_test() {
            //var result = Service<IGradeService>().GetUsersGradeInfo( null);
            //Assert.NotNull(result);
        }
    }
}