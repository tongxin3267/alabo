using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.UI.Domain.Services;
using Alabo.Schedules;
using Alabo.UI.AutoTasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.IO;
using Alabo.Core.WebApis.Controller;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.UI.Controllers {

    /// <summary>
    /// 自动任务
    /// </summary>
    [Route("Api/Task/[action]")]
    public class ApiTaskController : ApiBaseController {

        public ApiTaskController() : base() {
        }

        /// <summary>
        /// 执行.bat脚本
        /// </summary>
        /// <param name="batPath">bat文件的路径,如果固定文件路径给默认值即可</param>
        /// <param name="arguments">可执行文件参数,"para1 para2 para3" 参数为字符串形式，每一个参数用空格隔开</param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult RunBat([FromQuery]string batPath = "", string arguments = "232323") {
            Process pro = new Process();
            try {
                var runPath = new {
                    working = string.Empty,
                    fileName = string.Empty
                };
                FileInfo file = new FileInfo(batPath);
                if (string.IsNullOrEmpty(batPath)) {
                    runPath = new {
                        working = AppDomain.CurrentDomain.BaseDirectory,
                        fileName = "1.bat"
                    };
                } else {
                    runPath = new {
                        working = file.Directory.FullName,
                        fileName = file.Name
                    };
                }

                pro.StartInfo.WorkingDirectory = runPath.working;
                pro.StartInfo.FileName = Path.Combine(runPath.working, runPath.fileName);
                // 当我们需要给可执行文件传入参数时候可以设置这个参数
                // "para1 para2 para3" 参数为字符串形式，每一个参数用空格隔开
                if (!string.IsNullOrEmpty(arguments)) {
                    pro.StartInfo.Arguments = arguments;
                }

                pro.StartInfo.UseShellExecute = false;   //不使用shell壳运行
                pro.StartInfo.CreateNoWindow = true;//创建不可见window

                pro.Start();
                pro.WaitForExit();
                return ApiResult.Success();
            } catch (Exception ex) {
                return ApiResult.Failure(ex.Message);
            } finally {
                if (pro != null) {
                    pro.Close();
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="type">类型：示范值UserMapAutoTask</param>

        [HttpPost]
        public ApiResult Exceute([FromBody]string type) {
            Type typeFind = null;
            object instanceFind = null;
            var checkType = Resolve<IUIBaseService>().CheckType(type, ref typeFind, ref instanceFind);
            if (!checkType.Succeeded) {
                //  return new Tuple<ServiceResult, AutoForm>(checkType, new AutoForm());
            }
            if (instanceFind is IAutoTask set) {
                var autoTask = set.Init();
                var backJobParameter = new BackJobParameter {
                    ModuleId = autoTask.ModuleId,
                    CheckLastOne = true,
                    ServiceName = autoTask.ServcieType.Name,
                    Method = autoTask.Method
                };
                //  Resolve<ITaskQueueService>().AddBackJob(backJobParameter);
            }
            return ApiResult.Success();
        }

        ///// <summary>
        ///// 获取最近的执行任务
        ///// </summary>
        ///// <param name="type"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ApiResult<List<TaskQueue>> List(string type) {
        //    Type typeFind = null;
        //    object instanceFind = null;
        //    var checkType = Resolve<IUIBaseService>().CheckType(type, ref typeFind, ref instanceFind);
        //    if (!checkType.Succeeded) {
        //        //  return new Tuple<ServiceResult, AutoForm>(checkType, new AutoForm());
        //    }
        //    if (instanceFind is IAutoTask set) {
        //        var autoTask = set.Init();
        //        var list = Resolve<ITaskQueueService>().GetList(r => r.ModuleId == autoTask.ModuleId);
        //    }
        //    //    return ApiResult.Success
        //    // 根据ModuldId，读取出最近10条
        //    return null;
        //}

        public ApiResult<AutoTask> GetView(string type) {
            Type typeFind = null;
            object instanceFind = null;
            var checkType = Resolve<IUIBaseService>().CheckType(type, ref typeFind, ref instanceFind);
            if (!checkType.Succeeded) {
                //  return new Tuple<ServiceResult, AutoForm>(checkType, new AutoForm());
            }
            if (instanceFind is IAutoTask set) {
                var autoTask = set.Init();
                return ApiResult.Success(autoTask);
            }

            return null;
        }
    }
}