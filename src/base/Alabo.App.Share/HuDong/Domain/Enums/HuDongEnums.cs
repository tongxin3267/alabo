using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Open.HuDong.Domain.Enums {

    public enum HuDongEnums {

        /// <summary>
        /// 大转盘
        /// </summary>
        [Display(Name = "大转盘")]
        BigWheel = 1,

        /// <summary>
        /// 超级扭蛋机
        /// </summary>
        [Display(Name = "超级扭蛋机")]
        EggMachine = 2,

        /// <summary>
        /// 刮刮乐
        /// </summary>
        [Display(Name = "刮刮乐")]
        GuaGuaLe = 3,

        /// <summary>
        /// 多功能签到
        /// </summary>
        [Display(Name = "多功能签到")]
        MultiSign = 4,

        /// <summary>
        /// 红包雨
        /// </summary>
        [Display(Name = "红包雨")]
        RedPack = 5,

        /// <summary>
        /// 摇一摇
        /// </summary>
        [Display(Name = "摇一摇")]
        ShakeShake = 6,
    }
}