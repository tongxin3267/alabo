using System.ComponentModel.DataAnnotations;
using Alabo.Validations;

namespace Alabo.App.Share.HuDong.Dtos
{
    public class DrawInput
    {
        /// <summary>
        /// </summary>
        [Display(Name = "类型不能为空")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Key { get; set; }

        /// <summary>
        ///     转盘Id
        /// </summary>
        //[JsonConverter(typeof(ObjectIdConverter))]
        public string Id { get; set; }

        /// <summary>
        ///     抽奖用户id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     剩余可抽奖次数
        /// </summary>
        public int DrawCount { get; set; }
    }
}