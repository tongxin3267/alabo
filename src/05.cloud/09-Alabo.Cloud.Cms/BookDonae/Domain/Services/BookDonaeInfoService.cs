using Alabo.Cloud.Cms.BookDonae.Domain.Dtos;
using Alabo.Cloud.Cms.BookDonae.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Alabo.Cloud.Cms.BookDonae.Domain.Services
{
    public class BookDonaeInfoService : ServiceBase<BookDonaeInfo, ObjectId>, IBookDonaeInfoService
    {
        public BookDonaeInfoService(IUnitOfWork unitOfWork, IRepository<BookDonaeInfo, ObjectId> repository) : base(
            unitOfWork, repository)
        {
        }

        /// <summary>
        ///     导入
        /// </summary>
        public void Init(BookPathHost pathHost)
        {
            var directoryInfo = new DirectoryInfo(pathHost.Path);

            var directories = directoryInfo.GetDirectories();

            foreach (var directoryItem in directories)
            {
                var bookClass = new BooksClass
                {
                    Name = directoryItem.Name.Trim(),
                    Host = pathHost.Host
                };
                // 分类
                var findClass = Resolve<IBooksClassService>().GetSingle(r => r.Name == bookClass.Name);
                if (findClass == null)
                {
                    Resolve<IBooksClassService>().Add(bookClass);
                    findClass = bookClass;
                    Console.WriteLine($"成功添加分类:{bookClass.Name}");
                }

                // 书籍处理
                var bookList = new List<BookDonaeInfo>();
                var filePath = $@"{pathHost.Path}\{bookClass.Name}";
                var directory = new DirectoryInfo(filePath);
                var books = directory.GetFiles();
                var findBooks = GetList(r => r.ClassId == findClass.Id);
                foreach (var bookItem in books)
                {
                    var view = new BookDonaeInfo
                    {
                        Name = bookItem.Name.Trim().Replace(".pdf", ""),
                        IsOnSale = true,
                        ClassName = findClass.Name,
                        Url = $"{pathHost.Host}/{findClass.Name}/{bookItem.Name}",
                        ClassId = findClass.Id
                    };
                    var find = findBooks.FirstOrDefault(r => r.Name == view.Name);
                    if (find == null) bookList.Add(view);
                }

                Resolve<IBookDonaeInfoService>().AddMany(bookList);
                Console.WriteLine($"分类:{bookClass.Name}下新增：{bookList.Count}本书籍");
            }
        }
    }
}