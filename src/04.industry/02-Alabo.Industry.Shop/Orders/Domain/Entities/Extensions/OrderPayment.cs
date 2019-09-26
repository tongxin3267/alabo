using System;
using Alabo.Domains.Entities.Extensions;

namespace Alabo.Industry.Shop.Orders.Domain.Entities.Extensions
{
    /// <summary>
    ///     PayRecord 类为订单支付记录类
    /// </summary>
    public class OrderPayment : EntityExtension
    {
        /// <summary>
        ///     获取或设置支付的支付记录号码
        /// </summary>
        public Guid PayNO { get; set; }

        /// <summary>
        ///     获取或设置支付的订单 ID
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        ///     获取或设置订单号
        /// </summary>
        public string Serial { get; set; }

        /// <summary>
        ///     获取或设置用户支付的 ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     获取或设置支付的金额
        /// </summary>
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     获取或设置支付的时间
        /// </summary>
        public DateTime PayTime { get; set; }

        /// <summary>
        ///     获取或设置用户支付的方式
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        ///     获取或设置支付记录的 Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     工厂方法创建 OrderPayment 类的一个新实例
        /// </summary>
        /// <param name="order"></param>
        /// <param name="payType"></param>
        public static OrderPayment Create(Order order, string payType)
        {
            return Create(order.Id, order.Serial, order.UserId, order.PaymentAmount, payType);
        }

        public static OrderPayment Create(long orderId, string serial, long UserId, decimal paymentAmount,
            string payType)
        {
            return new OrderPayment
            {
                OrderId = orderId,
                UserId = UserId,
                Serial = serial,
                PaymentAmount = paymentAmount,
                PayType = payType,
                PayTime = DateTime.Now,
                PayNO = Guid.NewGuid()
            };
        }
    }
}