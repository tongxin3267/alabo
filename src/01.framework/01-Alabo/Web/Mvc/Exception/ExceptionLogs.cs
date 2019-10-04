using Alabo.Runtime;
using System;
using System.IO;
using System.Text;

namespace Alabo.Web.Mvc.Exception {

    public static class ExceptionLogs {

        public static void Write(System.Exception exception, string path, string fullPath = "") {
            var rootPath = RuntimeContext.Current.Path.RootPath + "/Logs";
            if (!Directory.Exists(rootPath)) {
                Directory.CreateDirectory(rootPath);
            }

            rootPath = Path.Combine(rootPath, DateTime.Now.ToString("yyyy-MM-dd-HH"));
            if (!Directory.Exists(rootPath)) {
                Directory.CreateDirectory(rootPath);
            }

            var fileName = Path.Combine(rootPath, $"{path}{DateTime.Now:mm_ss}.txt");
            if (!Directory.Exists(rootPath)) {
                Directory.CreateDirectory(rootPath);
            }

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("url:" + path);
            stringBuilder.AppendLine("time:" + DateTime.Now);
            stringBuilder.AppendLine("fullPath:" + fullPath);
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("title:");
            stringBuilder.AppendLine(exception.Message);

            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("intro:");
            stringBuilder.AppendLine(exception.StackTrace);
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)) {
                using (var sw = new BinaryWriter(fs)) {
                    sw.Write(stringBuilder.ToString());
                }
            }
        }
    }
}