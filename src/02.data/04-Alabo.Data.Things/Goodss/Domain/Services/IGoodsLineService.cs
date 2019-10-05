using Alabo.Data.Things.Goodss.Domain.Entities;
using Alabo.Data.Things.Goodss.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Data.Things.Goodss.Domain.Services
{
    public interface IGoodsLineService : IService<GoodsLine, ObjectId>
    {
        /// <summary>
        ///     获取产品线编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GoodsLineOutput GetEditView(string id);

        /// <summary>
        ///     产品线编辑
        /// </summary>
        /// <param name="goodsLine"></param>
        /// <returns></returns>
        ServiceResult Edit(GoodsLine goodsLine);
    }
}