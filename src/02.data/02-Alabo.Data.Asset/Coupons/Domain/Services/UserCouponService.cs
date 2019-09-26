using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Asset.Coupons.Domain.Entities;
using Alabo.App.Asset.Coupons.Domain.Enums;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.App.Asset.Coupons.Domain.Services
{
    public class UserCouponService : ServiceBase<UserCoupon, ObjectId>, IUserCouponService
    {
        public UserCouponService(IUnitOfWork unitOfWork, IRepository<UserCoupon, ObjectId> repository) : base(
            unitOfWork, repository)
        {
        }

        /// <summary>
        /// �ֶ������Ż�ȯ
        /// </summary>
        /// <param name="usersStr"></param>
        /// <param name="couponId"></param>
        /// <returns></returns>
        public ServiceResult Send(string usersStr, string couponId)
        {
            if (string.IsNullOrEmpty(usersStr))
            {
                return ServiceResult.FailedMessage("�����뷢���û�����");
            }
            var userList = usersStr.Split(',');
            if (userList.Length <= 0)
            {
                return ServiceResult.FailedMessage("��������ȷ�ĸ�ʽ��");
            }

            var couponModel = Resolve<ICouponService>().GetSingle(ObjectId.Parse(couponId));
            if (couponModel.TotalCount - couponModel.UsedCount < userList.Length)
            {
                return ServiceResult.FailedMessage("ѡ����û����������Ż�ȯ������");
            }

            var StartTime = DateTime.Now;
            var EndTime = DateTime.Now;

            if (couponModel == null)
            {
                return ServiceResult.FailedMessage("��ǰ�Ż�ȯ�����ڣ�");
            }
            else
            {
                //�����Ż�ȯ��Ч�� 
                if (couponModel.TimeLimit == CouponTimeLimit.Days)
                {
                    var day = couponModel.AfterDays; //��Ч��= ���ŵ���+ AfterDays
                    StartTime = DateTime.Now; //�ӷ��ŵ��쿪ʼ��
                    EndTime = DateTime.Now.AddDays(day);
                }
                else
                {
                    StartTime = couponModel.StartPeriodOfValidity;
                    EndTime = couponModel.EndPeriodOfValidity;
                }
            }
            List<UserCoupon> userCouponList = new List<UserCoupon>();
            StringBuilder strB = new StringBuilder();
       
            foreach (var item in userList)
            {
                var user = Resolve<IUserService>().GetSingle(item);
                if (user == null)
                {
                    strB.Append(item);
                    continue;
                }
                else
                {
                    var model = new UserCoupon
                    {
                        CouponId = ObjectId.Parse(couponId),
                        CreateTime = DateTime.Now,
                        Name = couponModel.Name,
                        StoreId = couponModel.StoreId,
                        MinOrderPrice = couponModel.MinOrderPrice,
                        Type = couponModel.Type,
                        UserId = user.Id,
                        UserName = user.UserName,
                        Value = couponModel.Value,
                        StartValidityTime = StartTime,
                        EndValidityTime = EndTime,
                        CouponStatus = CouponStatus.Normal
                    };
                    userCouponList.Add(model);

                    couponModel.UsedCount += 1;
                }
            }
            if (userCouponList.Count > 0)
            {
                AddMany(userCouponList);

                Resolve<ICouponService>().Update(couponModel);
                if (strB != null)
                {
                    return ServiceResult.SuccessWithObject(strB.ToString() + "�����ڣ������û����ųɹ���");
                }
                return ServiceResult.SuccessWithObject("���ųɹ���");
            }
            return ServiceResult.FailedMessage("����ʧ�ܣ�");
        }
    }
}
