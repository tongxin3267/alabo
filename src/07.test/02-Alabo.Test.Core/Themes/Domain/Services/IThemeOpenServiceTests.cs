using Xunit;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Themes.Domain.Services
{
    public class IThemeOpenServiceTests : CoreTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
        }

        [Fact]
        [TestMethod("UpdateThemeDataFromService_ThemeDataInput")]
        public void UpdateThemeDataFromService_ThemeDataInput_test()
        {
            //ThemeDataInput themeDataInput = null;
            //var result = Service<IThemeOpenService>().UpdateThemeDataFromService(themeDataInput);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("UpdateThemePageFromService_ThemePage")]
        public void UpdateThemePageFromService_ThemePage_test()
        {
            //ThemePage themePage = null;
            //var result = Service<IThemeOpenService>().UpdateThemePageFromService(themePage);
            //Assert.NotNull(result);
        } /*end*/
    }
}