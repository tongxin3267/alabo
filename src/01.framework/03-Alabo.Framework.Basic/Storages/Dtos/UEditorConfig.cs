using Alabo.Runtime;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

namespace Alabo.Framework.Basic.Storages.Dtos {

    public class UEditorConfig {
        private static readonly bool noCache = true;

        private static JObject _Items;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UEditorConfig(IHostingEnvironment hosting) {
            _hostingEnvironment = hosting;
        }

        public static JObject Items {
            get {
                if (noCache || _Items == null) {
                    _Items = BuildItems();
                }

                return _Items;
            }
        }

        private static JObject BuildItems() {
            var json = File.ReadAllText(RuntimeContext.Current.Path.WebRootPath +
                                        "\\assets\\lib\\ueditor\\config.json");
            return JObject.Parse(json);
        }

        public static T GetValue<T>(string key) {
            return Items[key].Value<T>();
        }

        public static string[] GetStringList(string key) {
            return Items[key].Select(x => x.Value<string>()).ToArray();
        }

        public static string GetString(string key) {
            return GetValue<string>(key);
        }

        public static int GetInt(string key) {
            return GetValue<int>(key);
        }
    }
}