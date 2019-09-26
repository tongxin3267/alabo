using System.ComponentModel.DataAnnotations;

namespace Alabo.Cloud.Asset.SecondBuy.Domain.Enums
{
    public enum SecondBuyOrderStatus
    {
        [Display(Name = "未发货")] NotDeliver = 1,

        [Display(Name = "已发货")] IsDeliver = 2,

        [Display(Name = "关闭")] IsClose = 3
    }
}