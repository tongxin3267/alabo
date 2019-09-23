using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Market.BookDonae.Domain.Entities;
using System.IO;
using System.Collections.Generic;
using Alabo.App.Market.BookDonae.Domain.Dtos;

namespace Alabo.App.Market.BookDonae.Domain.Services {

    public class BooksClassService : ServiceBase<BooksClass, ObjectId>, IBooksClassService {

        public BooksClassService(IUnitOfWork unitOfWork, IRepository<BooksClass, ObjectId> repository) : base(unitOfWork, repository) {
        }

        public void Init(BookPathHost pathHost) {
            var directoryInfo = new DirectoryInfo(pathHost.Path);
            var directories = directoryInfo.GetDirectories();
            //∑÷¿‡list
            var list = new List<BooksClass>();
            foreach (var item in directories) {
                var view = new BooksClass {
                    Name = item.Name
                };
                list.Add(view);
            }
            Resolve<IBooksClassService>().AddMany(list);
        }
    }
}