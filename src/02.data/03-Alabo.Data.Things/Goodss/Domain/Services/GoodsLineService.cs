using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Data.Things.Goodss.Domain.Entities;
using Alabo.Data.Things.Goodss.Dtos;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Maps;

namespace Alabo.Data.Things.Goodss.Domain.Services {

    public class GoodsLineService : ServiceBase<GoodsLine, ObjectId>, IGoodsLineService {

        public GoodsLineService(IUnitOfWork unitOfWork, IRepository<GoodsLine, ObjectId> repository) : base(unitOfWork, repository) {
        }

        public GoodsLineOutput GetEditView(string id) {
            var goodLine = Resolve<IGoodsLineService>().GetSingle(r => r.Id == id.ToObjectId());
            if (goodLine == null) {
                goodLine = new GoodsLine();
            }
            var productList = Resolve<IGoodsService>().GetList(r => goodLine.ProductIds.Contains(r.Id));
            goodLine.ProductIds = productList.Select(r => r.Id).ToList();
            var view = goodLine.MapTo<GoodsLineOutput>();
            view.GoodsList = productList;
            return view;
        }

        public ServiceResult Edit(GoodsLine view) {
            var goodLine = Resolve<IGoodsLineService>().GetSingle(r => r.Id == view.Id) ?? new GoodsLine();
            goodLine.ProductIds = view.ProductIds;
            goodLine.Name = view.Name;
            goodLine.Intro = view.Intro;
            if (Resolve<IGoodsLineService>().AddOrUpdate(goodLine, !view.Id.IsObjectIdNullOrEmpty())) {
                return ServiceResult.Success;
            } else {
                return ServiceResult.FailedWithMessage("²úÆ·Ïß±à¼­Ê§°Ü");
            }
        }
    }
}