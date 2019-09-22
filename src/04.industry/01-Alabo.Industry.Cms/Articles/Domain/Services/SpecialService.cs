using MongoDB.Bson;
using System.IO;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.Core.Files;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;

namespace Alabo.App.Cms.Articles.Domain.Services {

    public class SpecialService : ServiceBase<Special, ObjectId>, ISpecialService {

        public ServiceResult AddOrUpdate(Special model) {
            var find = new Special();
            if (model.Id.IsObjectIdNullOrEmpty()) {
                find = GetSingle(r => r.Key == model.Key);
            } else {
                find = GetSingle(r => r.Key == model.Key && r.Id != model.Id);
            }

            if (find != null) {
                return ServiceResult.FailedWithMessage("页面已经存在，请重新输入新的标识");
            }

            AddOrUpdate(model, model.Id.IsObjectIdNullOrEmpty());
            return ServiceResult.Success;
        }

        public string GetPagePath(string key, bool ismobile) {
            var path = "";
            if (ismobile) {
                path = $@"/wwwroot/themes/{ThemeHelper.SeMobile}/Special/{key}.cshtml";
            } else {
                path = $@"/wwwroot/themes/{ThemeHelper.CurrentTheme}/Special/{key}.cshtml";
            }

            var filePath = FileHelper.RootPath + "/" + path;
            if (!File.Exists(filePath)) {
                var diretory = FileHelper.RootPath + "/" + $@"wwwroot/themes/{ThemeHelper.CurrentTheme}/Special/";
                FileHelper.CreateDirectory(diretory);
                FileHelper.Write(filePath, $@"{key} 专题视图创建成功");
            }

            return path;
        }

        public SpecialService(IUnitOfWork unitOfWork, IRepository<Special, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}