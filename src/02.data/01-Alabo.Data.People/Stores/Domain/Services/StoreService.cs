using Alabo.Data.People.Stores.Domain.Entities;
using Alabo.Data.People.Stores.Domain.Repositories;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.People.UserTypes.Enums;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;
using System;

namespace Alabo.Data.People.Stores.Domain.Services
{
    public class StoreService : ServiceBase<Store, ObjectId>, IStoreService
    {
        public StoreService(IUnitOfWork unitOfWork, IRepository<Store, ObjectId> repository) : base(unitOfWork, repository)
        {
        }

        /// <summary>
        ///     ��ȡ��Ӫ����
        ///     ƽ̨���̣���̨��ӵ�ʱ��Ϊƽ̨��Ʒ
        /// </summary>
        public Store PlatformStore()
        {
            var planformUser = Resolve<IUserService>().PlatformUser();
            if (planformUser == null)
            {
                return null;
            }

            var store = GetSingle(r => r.UserId == planformUser.Id);
            // �������Ϊ�գ����ʼ������ƽ̨����
            if (store == null)
            {
                store = new Store
                {
                    UserId = planformUser.Id,
                    Name = "��Ӫ����",
                    GradeId = Guid.Parse("72BE65E6-3A64-414D-972E-1A3D4A36F701"),
                    CreateTime = DateTime.Now,
                    Status = UserTypeStatus.Success
                };

                var context = Repository<IStoreRepository>().RepositoryContext;
                context.BeginTransaction();
                try
                {
                    Add(store);

                    context.SaveChanges();
                    context.CommitTransaction();
                }
                catch (Exception ex)
                {
                    context.RollbackTransaction();
                }
                finally
                {
                    context.DisposeTransaction();
                }
                store = GetSingle(r => r.UserId == planformUser.Id);
            }
            return store;
        }

        /// <summary>
        ///     ��ȡ��Ա����
        /// </summary>
        /// <param name="userId">��ԱId</param>
        public Store GetUserStore(long userId)
        {
            return GetSingle(r => r.UserId == userId && r.Status == UserTypeStatus.Success);
        }
    }
}