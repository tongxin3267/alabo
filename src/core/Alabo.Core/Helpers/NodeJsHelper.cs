using System;
using System.Diagnostics;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Enums;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Helpers;

namespace Alabo.Core.Helpers {

    /// <summary>
    ///     Node相关的服务
    /// </summary>
    public static class NodeJsHelper {

        /// <summary>
        ///     运行Nodejs相关命令
        /// </summary>
        /// <param name="path">路径，相对wwwroot目录</param>
        /// <param name="command">命令</param>
        public static void Run(string path, string command) {
            try {
                if (path.IsNullOrEmpty()) {
                    throw new ValidException("路径不能为空");
                }

                if (command.IsNullOrEmpty()) {
                    throw new ValidException("命令不能为空");
                }
                //var realPath = FileHelper.WwwRootPath + path;
                //FileHelper.CreateDirectory(path);

                var runProcess = new Process();
                var config = new ProcessStartInfo(@"C:\Windows\System32\cmd.exe");
                config.WorkingDirectory = path; //设置路径
                config.RedirectStandardInput = true;
                config.RedirectStandardOutput = true;
                config.UseShellExecute = false;
                config.CreateNoWindow = false;
                runProcess.StartInfo = config;
                WriteLog("SetProps OK!");
                runProcess.OutputDataReceived += runProcess_OutputDataReceived;
                runProcess.Start();
                WriteLog("Start OK!");
                runProcess.StandardInput.WriteLine(command); //设置命令
                WriteLog("SetCMD OK!");
                runProcess.BeginOutputReadLine();
            } catch (Exception exc) {
                var log = new Domains.Base.Entities.Logs {
                    Content = exc.Message,
                    Level = LogsLevel.Error,
                    Type = typeof(NodeJsHelper).Name
                };

                Ioc.Resolve<ILogsService>().Add(log);
            }
        }

        private static void runProcess_OutputDataReceived(object sender, DataReceivedEventArgs e) {
            Console.WriteLine(e.Data);
        }

        private static void WriteLog(string msg) {
            var log = new Domains.Base.Entities.Logs {
                Content = msg,
                Level = LogsLevel.Information,
                Type = typeof(NodeJsHelper).Name
            };

            Ioc.Resolve<ILogsService>().Add(log);
        }
    }
}