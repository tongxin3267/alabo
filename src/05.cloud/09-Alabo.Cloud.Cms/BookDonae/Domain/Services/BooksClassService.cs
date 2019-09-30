using Alabo.Cloud.Cms.BookDonae.Domain.Dtos;
using Alabo.Cloud.Cms.BookDonae.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;
using System.Collections.Generic;
using System.IO;

namespace Alabo.Cloud.Cms.BookDonae.Domain.Services
{
    public class BooksClassService : ServiceBase<BooksClass, ObjectId>, IBooksClassService
    {
        public BooksClassService(IUnitOfWork unitOfWork, IRepository<BooksClass, ObjectId> repository) : base(
            unitOfWork, repository)
        {
        }

        public void Init(BookPathHost pathHost)
        {
            var directoryInfo = new DirectoryInfo(pathHost.Path);
            var directories = directoryInfo.GetDirectories();
            //∑÷¿‡list
            var list = new List<BooksClass>();
            foreach (var item in directories)
            {
                var view = new BooksClass
                {
                    Name = item.Name
                };
                list.Add(view);
            }

            Resolve<IBooksClassService>().AddMany(list);
        }
    }
}