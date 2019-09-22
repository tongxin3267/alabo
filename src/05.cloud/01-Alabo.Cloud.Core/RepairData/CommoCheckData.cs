﻿using System.Linq;
using System.Threading.Tasks;
using Alabo.App.Core.Admin;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Enums;
using Alabo.Helpers;

namespace Alabo.App.Core.Common {

    public class CommoCheckData : ICheckData {

        public async Task ExcuteAsync() {
        }

        public void Execute() {

            #region 默认货币类型不存在，设置人民币为默认货币类型

            var moneyTypes = Ioc.Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>(r => r.Status == Status.Normal)
                .OrderBy(r => r.SortOrder).ToList();
            if (moneyTypes.FirstOrDefault(r => r.IsDefault) == null) {
                moneyTypes.ForEach(r => {
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