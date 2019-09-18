using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Alabo.App.Market.SecondBuy.Domain.Enums {
    public  enum  SecondBuyOrderStatus {

        [Display(Name = "未发货")]
        NotDeliver=1,

        [Display(Name = "已发货")]
        IsDeliver = 2,

        [Display(Name = "关闭")]
        IsClose = 3,
    }
}
