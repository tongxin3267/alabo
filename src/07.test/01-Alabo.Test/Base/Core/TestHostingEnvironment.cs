using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace Alabo.Test.Base.Core
{
    public class TestHostingEnvironment : IHostingEnvironment
    {
        public string TestRootPath;

        public TestHostingEnvironment()
        {
            EnvironmentName = "Service Test Case";
            ApplicationName = "Service Test Case";
            var path = Environment.CurrentDirectory;
            var webRootPath = path.Substring(0, path.IndexOf("src\\", StringComparison.OrdinalIgnoreCase) + 3)
                .Replace("zkcloudopen_v2", "zkcloudv11s");
            TestRootPath = webRootPath + "\\test";
            //ContentRootPath = Environment.CurrentDirectory;
            WebRootPath = webRootPath + "\\07.test\\01-Alabo.Test";
            ContentRootPath = WebRootPath;
        }

        public string EnvironmentName { get; set; }
        public string ApplicationName { get; set; }
        public string WebRootPath { get; set; }
        public IFileProvider WebRootFileProvider { get; set; }
        public string ContentRootPath { get; set; }
        public IFileProvider ContentRootFileProvider { get; set; }
    }
}