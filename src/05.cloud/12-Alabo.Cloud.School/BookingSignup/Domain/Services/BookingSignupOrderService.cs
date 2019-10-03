using Alabo.Cloud.School.BookingSignup.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Alabo.Cloud.School.BookingSignup.Domain.Services
{
    public class BookingSignupOrderService : ServiceBase<BookingSignupOrder, ObjectId>, IBookingSignupOrderService
    {
        public BookingSignupOrderService(IUnitOfWork unitOfWork, IRepository<BookingSignupOrder, ObjectId> repository) :
            base(unitOfWork, repository)
        {
        }

        public ServiceResult UserSign(BookingSignupOrderContact view)
        {
            var orders = Resolve<IBookingSignupOrderService>().GetList(u => u.IsPay);
            var list = new List<BookingSignupOrderContact>();

            foreach (var item in orders) {
                foreach (var temp in item.Contacts) {
                    if (temp.Name == view.Name && temp.Mobile == view.Mobile)
                    {
                        if (temp.IsSign == false)
                        {
                            temp.IsSign = true;
                            var result = Resolve<IBookingSignupOrderService>().Update(item);
                            if (!result) {
                                return ServiceResult.FailedWithMessage("ǩ��ʧ�ܣ����ٴγ���");
                            }

                            return ServiceResult.Success;
                        }

                        return ServiceResult.FailedWithMessage("���Ѿ�ǩ�����ˣ������ظ�ǩ��");
                    }
                }
            }

            return ServiceResult.FailedWithMessage("δ�ҵ�������Ϣ����ȷ��������Ϣ�Ƿ���ȷ");
        }
    }
}