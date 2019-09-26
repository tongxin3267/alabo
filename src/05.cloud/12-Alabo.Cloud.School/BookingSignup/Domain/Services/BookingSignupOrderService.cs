using System.Collections.Generic;
using Alabo.Cloud.School.BookingSignup.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Cloud.School.BookingSignup.Domain.Services {

    public class BookingSignupOrderService : ServiceBase<BookingSignupOrder, ObjectId>, IBookingSignupOrderService {

        public BookingSignupOrderService(IUnitOfWork unitOfWork, IRepository<BookingSignupOrder, ObjectId> repository) : base(unitOfWork, repository) {
        }

        public ServiceResult UserSign(BookingSignupOrderContact view) {
            var orders = Resolve<IBookingSignupOrderService>().GetList(u => u.IsPay == true);
            var list = new List<BookingSignupOrderContact>();

            foreach (var item in orders) {
                foreach (var temp in item.Contacts) {
                    if (temp.Name == view.Name && temp.Mobile == view.Mobile) {
                        if (temp.IsSign == false) {
                            temp.IsSign = true;
                            var result = Resolve<IBookingSignupOrderService>().Update(item);
                            if (!result) {
                                return ServiceResult.FailedWithMessage("签到失败，请再次尝试");
                            }
                            return ServiceResult.Success;
                        } else {
                            return ServiceResult.FailedWithMessage("您已经签到过了，请勿重复签到");
                        }
                    }
                }
            }

            return ServiceResult.FailedWithMessage("未找到您的信息，请确认输入信息是否正确");
        }
    }
}