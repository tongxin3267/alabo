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
        ///��ȡ��Ʒ�߱༭��ͼ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        GoodsLineOutput GetEditView(string id);

        /// <summary>
        /// ��Ʒ�߱༭
        /// </summary>
        /// <param name="goodsLine"></param>
        /// <returns></returns>
        ServiceResult Edit(GoodsLine goodsLine);
    }
}