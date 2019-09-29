using System.Collections.Generic;
using System.Linq;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Domain.Services
{
    public class AboutService : ServiceBase<About, ObjectId>, IAboutService
    {
        public AboutService(IUnitOfWork unitOfWork, IRepository<About, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }

        /// <summary>
        ///     插入默认数据
        ///     默认数据参考：http://www.womanfushi.com/about-96.html
        /// </summary>
        public void InitialData()
        {
            var list = GetList();
            if (list.ToList().Count == 0)
            {
                var sources = new List<About>();
                var abount = new About
                {
                    Name = "关于我们",
                    Content = "关于我们"
                };
                sources.Add(abount);

                abount = new About
                {
                    Name = "网店代理",
                    Content = "网店代理"
                };
                sources.Add(abount);

                abount = new About
                {
                    Name = "实体店批发",
                    Content = "实体店批发"
                };
                sources.Add(abount);

                abount = new About
                {
                    Name = "来样加工",
                    Content = "来样加工"
                };
                sources.Add(abount);

                abount = new About
                {
                    Name = "实体店加盟",
                    Content = "实体店加盟"
                };
                sources.Add(abount);

                abount = new About
                {
                    Name = "公司介绍",
                    Content = "公司介绍"
                };
                sources.Add(abount);

                abount = new About
                {
                    Name = "企业合作",
                    Content = "企业合作"
                };
                sources.Add(abount);

                abount = new About
                {
                    Name = "联系我们",
                    Content = "联系我们"
                };
                sources.Add(abount);

                AddMany(sources);
            }
        }
    }
}