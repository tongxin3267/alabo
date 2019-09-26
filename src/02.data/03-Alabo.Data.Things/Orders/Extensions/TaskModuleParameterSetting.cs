using System;
using System.Collections.Generic;

namespace Alabo.Data.Things.Orders.Extensions {

    public static class TaskModuleParameterSetting {
        private static readonly IDictionary<Guid, Type> _moduleParameterSetting = new Dictionary<Guid, Type>();

        static TaskModuleParameterSetting() {
            //_moduleParameterSetting.Add(new Guid(UserGradeUpgradeModule.Id), typeof(UserAssetsChangeQueueParameter));
            //_moduleParameterSetting.Add(new Guid(UpgradeBySubordinateNumberModule.Id), typeof(UserAssetsChangeQueueParameter));
        }

        public static void AddTaskModule(Guid id, Type parameterType) {
            if (_moduleParameterSetting.ContainsKey(id)) {
                return;
            }

            _moduleParameterSetting.Add(id, parameterType);
        }

        public static bool ContainsModule(Guid moduleId) {
            return _moduleParameterSetting.ContainsKey(moduleId);
        }

        public static bool TryGetParameterType(Guid id, out Type type) {
            return _moduleParameterSetting.TryGetValue(id, out type);
        }

        public static void AddTaskModule(object p, Type type) {
            throw new NotImplementedException();
        }
    }
}