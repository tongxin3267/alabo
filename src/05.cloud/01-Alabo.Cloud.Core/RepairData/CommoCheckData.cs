using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Core.Reflections.Interfaces;
using Alabo.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace _01_Alabo.Cloud.Core.RepairData
{
    public class CommoCheckData : ICheckData
    {
        public async Task ExcuteAsync()
        {
        }

        public void Execute()
        {
            #region 默认货币类型不存在，设置人民币为默认货币类型

            var moneyTypes = Ioc.Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>(r => r.Status == Status.Normal)
                .OrderBy(r => r.SortOrder).ToList();
            if (moneyTypes.FirstOrDefault(r => r.IsDefault) == null)
            {
                moneyTypes.ForEach(r =>
                {
                    if (r.Currency == Currency.Cny) {
                        r.IsDefault = true;
                    }
                });
                Ioc.Resolve<IAutoConfigService>().AddOrUpdate<MoneyTypeConfig>(moneyTypes);
            }

            #endregion 默认货币类型不存在，设置人民币为默认货币类型
        }
    }
}