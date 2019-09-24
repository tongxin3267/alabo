using System;
using System.Collections.Generic;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using MongoDB.Bson;
using Alabo.Domains.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.App.Market.BookDonae.Domain.Entities;
using System.IO;
using Alabo.App.Market.BookDonae.Domain.Dtos;

namespace Alabo.App.Market.BookDonae.Domain.Services {

    public class BookDonaeInfoService : ServiceBase<BookDonaeInfo, ObjectId>, IBookDonaeInfoService {

        public BookDonaeInfoService(IUnitOfWork unitOfWork, IRepository<BookDonaeInfo, ObjectId> repository) : base(unitOfWork, repository) {
        }

        /// <summary>
        ///     ����
        /// </summary>
        public void Init(BookPathHost pathHost) {
            var directoryInfo = new DirectoryInfo(pathHost.Path);

            var directories = directoryInfo.GetDirectories();

            foreach (var directoryItem in directories) {
                var bookClass = new BooksClass {
                    Name = directoryItem.Name.Trim(),
                    Host = pathHost.Host,
                };
                // ����
                var findClass = Resolve<IBooksClassService>().GetSingle(r => r.Name == bookClass.Name);
                if (findClass == null) {
                    Resolve<IBooksClassService>().Add(bookClass);
                    findClass = bookClass;
                    Console.WriteLine($"�ɹ���ӷ���:{bookClass.Name}");
                }

                // �鼮����
                var bookList = new List<BookDonaeInfo>();
                var filePath = $@"{pathHost.Path}\{bookClass.Name}";
                var directory = new DirectoryInfo(filePath);
                var books = directory.GetFiles();
                var findBooks = GetList(r => r.ClassId == findClass.Id);
                foreach (var bookItem in books) {
                    var view = new BookDonaeInfo {
                        Name = bookItem.Name.Trim().Replace(".pdf", ""),
                        IsOnSale = true,
                        ClassName = findClass.Name,
                        Url = $"{pathHost.Host}/{findClass.Name}/{bookItem.Name}",
                        ClassId = findClass.Id
                    };
                    var find = findBooks.FirstOrDefault(r => r.Name == view.Name);
                    if (find == null) {
                        bookList.Add(view);
                    }
                }

                Resolve<IBookDonaeInfoService>().AddMany(bookList);
                Console.WriteLine($"����:{bookClass.Name}��������{bookList.Count}���鼮");
            }
        }
    }
}