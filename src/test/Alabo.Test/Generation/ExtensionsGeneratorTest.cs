using Xunit;

namespace Alabo.Test.Generation
{
    public class ExtensionsGeneratorTest
    {
        [Theory]
        [InlineData("core\\ZKCloud\\Extensions", "Alabo.Extensions")]
        public void Start(string folder, string fullName)
        {
            StrartSingleFolder(folder, fullName);

            //var allServiceType = Service<ITypeService>().GetAllEntityService(); //.Where(o=>!o.IsAbstract);

            //foreach (var type in allServiceType)
            //    try
            //    {
            //        var startupTest = new StartupTest();
            //        startupTest.StartSingleServieCode(type, true);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
        }

        public void StrartSingleFolder(string folder, string fullName)
        {
            //var host = new TestHostingEnvironment();
            //var root = host.TestRootPath.Replace("test", "");
            //var path = root + folder;
            //var files = DirectoryHelper.GetFileInfo(path);
            //var startupTest = new StartupTest();
            //foreach (var item in files) {
            //    var typeFullName = fullName + "." + item.Name;
            //    typeFullName = typeFullName.Replace(".cs", "");
            //    var type = typeFullName.GetTypeByFullName();

            //    if (type != null) startupTest.StartSingleServieCode(type, false, true);
            //}
        }
    }
}