using System.Collections.Generic;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.Extensions;

namespace Alabo.App.Core.Themes.Extensions {

    public static class TaskQueueParameterExtension {

        public static string ToJson(this TaskQueueParameter parameter) {
            if (parameter == null) {
                return "{}";
            }

            return parameter.Parameters.ToJson();
            //var res = new Dictionary<string, TaskQueueParameterItem>();
            //foreach (var key in parameter.GetKeys())
            //{
            //    res.Add(key, parameter[key]);
            //}
            //return res.ToJson();
        }

        public static TaskQueueParameter FromJson(this string str) {
            if (str.IsNullOrEmpty()) {
                return null;
            }

            IDictionary<string, TaskQueueParameterItem> parameters =
                str.DeserializeJson<Dictionary<string, TaskQueueParameterItem>>();
            return new TaskQueueParameter(parameters);
        }
    }
}