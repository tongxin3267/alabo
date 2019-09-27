using Alabo.Data.People.Stores.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Domains.Services;
using Alabo.Industry.Shop.Deliveries.ViewModels;
using MongoDB.Bson;

namespace Alabo.Data.People.Stores.Domain.Services
{
    public interface IStoreService : IService<Store, ObjectId>
    {
        /// <summary>
        ///     ��ȡ��Ӫ����
        ///     ƽ̨���̣���̨��ӵ�ʱ��Ϊƽ̨��Ʒ
        /// </summary>
        Store PlanformStore();

        /// <summary>
        ///     ��ȡs the ��Ա store.
        ///     ��ȡ��Ա����
        /// </summary>
        /// <param name="UserId">��ԱId</param>
        Store GetUserStore(long UserId);

        /// <summary>
        ///     ���s the �� ����.
        /// </summary>
        /// <param name="store">The store.</param>
        ServiceResult AddOrUpdate(ViewStore store);

        /// <summary>
        ///     ��ȡs the ��ͼ store ��ҳ list.
        /// </summary>
        /// <param name="dto">The dto.</param>
        PagedList<ViewStore> GetViewStorePageList(PagedInputDto dto);

        /// <summary>
        ///     ��ȡs the ��ͼ store.
        /// </summary>
        /// <param name="Id">Id��ʶ</param>
        ViewStore GetViewStore(long Id);

        /// <summary>
        ///     ��ȡs the ��ҳ list.
        /// </summary>
        /// <param name="query">��ѯ</param>
        PagedList<ViewStore> GetPageList(object query);

        /// <summary>
        /// ��ȡ��ͼ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ViewStore GetView(long id);
    }
}