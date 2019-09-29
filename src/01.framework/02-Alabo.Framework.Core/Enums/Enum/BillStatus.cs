using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum
{
    /// <summary>
    ///     账单状态 通过此字段来完成转账、提现、充值等流程
    ///     提现流程：
    ///     流程一：管理员初审不通过：
    ///     操作流程：会员申请(进行中)--->管理员初审失败(失败)
    ///     状态变化：0(InProcess)----->4(Failured)
    ///     流程二：管理员初审通过：
    ///     操作流程：会员申请(进行中)--->管理员初审通过（等待审核)
    ///     状态变化：0(InProcess)----->1(WaitCheck)
    ///     流程二：管理员二审不通过：
    ///     操作流程：会员申请(进行中)--->管理员初审通过（等待审核)--->管理员二审失败(失败）
    ///     状态变化：0(InProcess)----->1(WaitCheck)-->4(Failured)
    ///     流程三：管理员二审通过：
    ///     操作流程：会员申请(进行中)--->管理员初审通过（等待审核)--->管理员二审通过(等待付款）
    ///     状态变化：0(InProcess)----->1(WaitCheck)-->2(WaitPay)
    ///     流程四：付款失败：
    ///     操作流程：会员申请(进行中)--->管理员初审通过（等待审核)--->管理员二审通过(等待付款）--->转账付款失败(失败）
    ///     状态变化：0(InProcess)----->1(WaitCheck)-->2(WaitPay)--4(Failured)
    ///     流程四：提现成功：
    ///     操作流程：会员申请(进行中)--->管理员初审通过（等待审核)--->管理员二审通过(等待付款）--->转账付款成功(成功）
    ///     状态变化：0(InProcess)----->1(WaitCheck)-->2(WaitPay)--3(Success)
    ///     流程五：维权流程：
    ///     前提：根据提现设置，进行操作
    ///     操作流程：会员申请维权后，管理员可以修改该账单为任何一个状态
    ///     状态变化：4(Failured)----->5(Activist)
    ///     充值流程：
    ///     流程一：管理员初审不通过：
    ///     操作流程：会员申请(进行中)--->管理员初审失败(失败)
    ///     状态变化：0(InProcess)----->4(Failured)
    ///     流程二：管理员初审通过：
    ///     操作流程：会员申请(进行中)--->管理员初审通过（等待审核)
    ///     状态变化：0(InProcess)----->1(WaitCheck)
    ///     流程二：管理员二审不通过：
    ///     操作流程：会员申请(进行中)--->管理员初审通过（等待审核)--->管理员二审失败(失败）
    ///     状态变化：0(InProcess)----->1(WaitCheck)-->4(Failured)
    ///     流程三：管理员二审通过：
    ///     操作流程：会员申请(进行中)--->管理员初审通过（等待审核)--->管理员二审通过(等待付款）
    ///     状态变化：0(InProcess)----->1(WaitCheck)-->2(WaitPay)
    ///     流程四：付款失败：
    ///     操作流程：会员申请(进行中)--->管理员初审通过（等待审核)--->管理员二审通过(等待付款）--->转账付款失败(失败）
    ///     状态变化：0(InProcess)----->1(WaitCheck)-->2(WaitPay)--4(Failured)
    ///     流程四：提现成功：
    ///     操作流程：会员申请(进行中)--->管理员初审通过（等待审核)--->管理员二审通过(等待付款）--->转账付款成功(成功）
    ///     状态变化：0(InProcess)----->1(WaitCheck)-->2(WaitPay)--3(Success)
    ///     流程五：维权流程：
    ///     前提：根据充值设置，进行操作
    ///     操作流程：会员申请维权后，管理员可以修改该账单为任何一个状态
    ///     状态变化：4(Failured)----->5(Activist)
    /// </summary>
    [ClassProperty(Name = "账单状态和充值流程")]
    public enum BillStatus
    {
        /// <summary>
        ///     进行中
        ///     会员创建记录，或者会员前台进行申请
        ///     等待管理员初审
        /// </summary>
        [Display(Name = "进行中")]
        [LabelCssClass("m-badge--primary")]
        InProcess = 0,

        /// <summary>
        ///     等待审核
        /// </summary>
        [Display(Name = "等待审核")]
        [LabelCssClass(BadgeColorCalss.Success)]
        WaitCheck = 1,

        /// <summary>
        ///     等待付款
        /// </summary>
        [Display(Name = "等待付款")]
        [LabelCssClass(BadgeColorCalss.Success)]
        WaitPay = 2,

        /// <summary>
        ///     成功
        /// </summary>
        [Display(Name = "成功")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Success = 3,

        /// <summary>
        ///     失败
        /// </summary>
        [Display(Name = "失败")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        Failured = 4,

        /// <summary>
        ///     维权
        /// </summary>
        [Display(Name = "维权")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        Activist = 5,

        /// <summary>
        ///     付款成功
        /// </summary>
        [Display(Name = "付款成功")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Successful = 6
    }
}