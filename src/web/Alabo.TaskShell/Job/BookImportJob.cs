﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Alabo.App.Market.BookDonae.Domain.Dtos;
using Alabo.App.Market.BookDonae.Domain.Services;
using Alabo.Dependency;
using Alabo.Schedules.Job;

namespace Alabo.Web.TaskShell.Job {

    public class BookImportJob : JobBase {

        public override TimeSpan? GetInterval() {
            return TimeSpan.FromDays(3);
        }

        protected override async Task Execute(IJobExecutionContext context, IScope scope) {
            var list = new List<BookPathHost>();
            BookPathHost host = new BookPathHost();
            host.Path = @"D:\服务器01(t1.dpnfs.com)";
            host.Host = "http://t1.dpnfs.com:809";

            list.Add(host);

            host = new BookPathHost();
            host.Path = @"D:\服务器02(t2.dpnfs.com)";
            host.Host = "http://t2.dpnfs.com:809";

            list.Add(host);
            foreach (var item in list) {
                scope.Resolve<IBookDonaeInfoService>().Init(item);
            }
            Console.WriteLine("所有书籍导入成功");
        }
    }
}