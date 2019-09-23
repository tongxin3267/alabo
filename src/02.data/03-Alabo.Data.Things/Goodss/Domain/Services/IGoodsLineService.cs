using System;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Data.Things.Goodss.Domain.Entities;
using Alabo.Data.Things.Goodss.Dtos;
using Alabo.Domains.Entities;

namespace Alabo.Data.Things.Goodss.Domain.Services {

    public interface IGoodsLineService : IService<GoodsLine, ObjectId> {

        /// <summary>
        ///获取产品线编辑视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GoodsLineOutput GetEditView(string id);

        /// <summary>
        /// 产品线编辑
        /// </summary>
        /// <param name="goodsLine"></param>
        /// <returns></returns>
        ServiceResult Edit(GoodsLine goodsLine);
    }
}