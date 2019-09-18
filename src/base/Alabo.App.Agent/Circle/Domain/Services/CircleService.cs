using System;
using System.Collections.Generic;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using AutoMapper;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Agent.Circle.Domain.Entities;
using Alabo.App.Core.ApiStore.AMap.CircleMap;
using Alabo.Mapping;

namespace Alabo.App.Agent.Circle.Domain.Services {

    public class CircleService : ServiceBase<Entities.Circle, ObjectId>, ICircleService {

        public CircleService(IUnitOfWork unitOfWork, IRepository<Entities.Circle, ObjectId> repository) : base(unitOfWork, repository) {
        }

        public void Init() {
            if (!Exists()) {
                var client = new CircleMapClient();
                var circleList = client.GetCircleList();
                var result = new List<Entities.Circle>();
                foreach (var item in circleList) {
                    var view = AutoMapping.SetValue<Entities.Circle>(item);
                    result.Add(view);
                }

                AddMany(result);
            }
        }
    }
}