using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Asset.Pays.Domain.Entities;
using Alabo.App.Asset.Pays.Domain.Entities.Extension;
using Alabo.App.Asset.Pays.Domain.Services;
using Alabo.Cloud.School.BookingSignup.Domain.Entities;
using Alabo.Cloud.School.BookingSignup.Dtos;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Linq.Dynamic;
using Alabo.Mapping;
using Alabo.Regexs;
using Alabo.Tables.Domain.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.School.BookingSignup.Domain.Services
{
    public class BookingSignupService : ServiceBase<Entities.BookingSignup, ObjectId>, IBookingSignupService
    {
        public BookingSignupService(IUnitOfWork unitOfWork, IRepository<Entities.BookingSignup, ObjectId> repository) :
            base(unitOfWork, repository)
        {
        }

        public Tuple<BookingBuyOutput, ServiceResult> Buy(BookingBuyInput buyInput)
        {
            // 安验证，Order BookingId
            // order.price =总价/数量 // pay表记录

            #region 验证

            if (buyInput.UserId <= 0)
                return Tuple.Create(new BookingBuyOutput(), ServiceResult.FailedWithMessage("会员不存在"));

            if (buyInput.Count < 1)
                return Tuple.Create(new BookingBuyOutput(), ServiceResult.FailedWithMessage("至少要有一名参与者"));

            if (buyInput.Price != buyInput.TotalPrice / buyInput.Count)
                return Tuple.Create(new BookingBuyOutput(), ServiceResult.FailedWithMessage("价格计算有误"));
            //if (buyInput.BookingId.IsNullOrEmpty()) {
            //    return Tuple.Create(new BookingBuyOutput(), ServiceResult.FailedWithMessage("订单为空"));
            //}

            //var checkModel = Resolve<IBookingSignupOrderService>()
            //    .GetSingle(u => u.BookingId == buyInput.BookingId.ToObjectId());
            //if (checkModel != null) {
            //    return Tuple.Create(new BookingBuyOutput(), ServiceResult.FailedWithMessage("该订单已存在，请勿重复提交"));
            //}

            #endregion 验证

            var user = Resolve<IUserService>().GetSingle(buyInput.UserId);

            var model = AutoMapping.SetValue<BookingSignupOrder>(buyInput);

            #region 手机验证

            model.Contacts = buyInput.Contacts.Deserialize<BookingSignupOrderContact>();
            var i = 0;
            foreach (var item in model.Contacts)
            {
                i++;
                if (item.Name.IsNullOrEmpty())
                    return Tuple.Create(new BookingBuyOutput(), ServiceResult.FailedWithMessage($"第{i}名参与者名字不能为空"));

                if (item.Mobile.IsNullOrEmpty())
                    return Tuple.Create(new BookingBuyOutput(), ServiceResult.FailedWithMessage($"第{i}名参与者手机号不能为空"));

                if (!RegexHelper.CheckMobile(item.Mobile))
                    return Tuple.Create(new BookingBuyOutput(), ServiceResult.FailedWithMessage($"第{i}名参与者手机号不正确"));
            }

            #endregion 手机验证

            model.IsPay = false;
            model.BookingId = buyInput.BookingId.ToObjectId();
            if (Resolve<IBookingSignupOrderService>().Add(model))
            {
                var payExtension = new PayExtension
                {
                    TradeNo = model.Id.ToString(),
                    Body = "您正在企牛牛商城上消费，请认真核对账单信息",
                    UserName = user.GetUserName(),
                    AfterSuccess = new BaseServiceMethod
                    {
                        Method = "AfterPaySuccess",
                        ServiceName = typeof(IBookingSignupService).Name,
                        Parameter = model.Id
                    },
                    BuyerCount = 0,
                    GroupOverId = 0,
                    ReturnUrl = $"/pages/index?path=successful_registration&id={model.Id.ToString()}"
                    //NotifyUrl = //不知道什么支付方式
                    //RedirectUrl = $"/pages/index?path=successful_registration&id={model.Id}",
                };
                payExtension.RedirectUrl = $"/pages/index?path=successful_registration&id={model.Id.ToString()}";
                var pay = new Pay
                {
                    Status = PayStatus.WaiPay,
                    Type = CheckoutType.Customer,
                    Amount = buyInput.TotalPrice,
                    UserId = user.Id
                };
                try
                {
                    var bookingSignup = Resolve<IBookingSignupService>().GetSingle(u => u.Id == model.BookingId);
                    // Resolve<ITradeService>().Add(trade);
                    pay.EntityId = $"[\"{model.Id.ToString()}\"]";
                    payExtension.TradeNo = model.Id.ToString();
                    pay.Extensions = payExtension.ToJsons();
                    Resolve<IPayService>().Add(pay);
                }
                catch (Exception ex)
                {
                    return Tuple.Create(new BookingBuyOutput(), ServiceResult.FailedWithMessage("订单记录失败"));
                }

                var outPut = new BookingBuyOutput
                {
                    PayId = pay.Id,
                    PayAmount = buyInput.TotalPrice,
                    OrderId = model.Id
                };
                return Tuple.Create(outPut, ServiceResult.Success);
            }

            return Tuple.Create(new BookingBuyOutput(), ServiceResult.FailedWithMessage("订单记录失败"));
        }

        ///
        public void AfterPaySuccess(List<object> entityId)
        {
            // var orderId = entityIdList.FirstOrDefault();
            var order = Resolve<IBookingSignupOrderService>().GetSingle(entityId.FirstOrDefault());
            if (order == null) return;

            order.IsPay = true;
            Resolve<IBookingSignupOrderService>().Update(order);
            var bookingSignup = Resolve<IBookingSignupService>().GetSingle(order.BookingId);
            var message =
                $"恭喜您成功报名{bookingSignup.Name}课程，课程将于{bookingSignup.StartTime}在{bookingSignup.Address}开始，预定{bookingSignup.EndTime}结束，望悉知!";
            foreach (var item in order.Contacts) Resolve<IOpenService>().SendRaw(item.Mobile, message);
        }
    }
}