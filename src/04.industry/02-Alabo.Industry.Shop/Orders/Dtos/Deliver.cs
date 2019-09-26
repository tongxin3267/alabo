using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Orders.Dtos {

    /// <summary>
    /// 快递信息
    /// </summary>
    [Table("Order_Deliver")]
    [ClassProperty(Name = "快递")]
    public class Deliver : AggregateMongodbRoot<Deliver> {

        


        /// <summary>
        /// 快递单号
        /// </summary>
        public string ExpressNum { get; set; }

        /// <summary>
        /// 快递识别号
        /// </summary>
        public string ExpressNo { get; set; }

        /// <summary>
        /// 快递公司
        /// </summary>
        public string ExpressName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }


        /// <summary>
        /// 订单Id
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// 店铺Id
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 收货姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 地区id
        /// </summary>
        public long RegionId { get; set; }
    }
}