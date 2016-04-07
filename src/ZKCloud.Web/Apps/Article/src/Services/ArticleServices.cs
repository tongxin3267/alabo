using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Entities;
using ZKCloud.Domain.Repositories;
using ZKCloud.Domain.Services;
using ZKCloud.Web.Apps.Article.src.Entities;
using ZKCloud.Web.Apps.Article.src.Domains.Repositories;

namespace ZKCloud.Web.Apps.Article.src.Domains.Services {
	public class ArticleServices : ServiceBase, IArticleServices {
		public void AddSingle(Entities.Article article) {
			Repository<ArticleRepositories>().AddSingle(article);
        }

		public void DelSingle(string ids) {
			if (!string.IsNullOrEmpty(ids))
			{
				var arrayId = ids.Split(',');
				List<long> ListId = new List<long>();
				foreach (var item in arrayId) {
					if (!string.IsNullOrEmpty(item))
						ListId.Add(long.Parse(item));
				}
				Repository<ArticleRepositories>().Delete(x=> ListId.Contains(x.Id));
            }
		}

		public void UpdateSingle(Entities.Article article) {
			Repository<ArticleRepositories>().UpdateSingle(article);
		}

		public List<Entities.Article> GetList() {
			return Repository<ArticleRepositories>().ReadMany(e=>true).ToList();
        }

		public Entities.Article GetSingle(long id) {
			return Repository<ArticleRepositories>().ReadSingle(e => e.Id == id);
        }

		public Entities.Article Read(long id) {
			throw new NotImplementedException();
		}

		public void Delete(params int[] ids) {
			throw new NotImplementedException();
		}

		public IList<Entities.Article> ReadMany() {
			return Repository<ArticleRepositories>().ReadMany(e => true).ToList();
		}

		public PagedList<Entities.Article> ReadPage(int pageIndex = 1, int pageSize = 10) {
			return new ArticlePageRepositories().ReadMany(e => true, pageSize, pageIndex);
        } 
    }
}
