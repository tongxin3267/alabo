using ZKCloud.Domain.Repositories;

namespace ZKCloud.Web.Apps.Article.src.Domains.Repositories {
	/// <summary>
	/// 文章数据仓储
	/// </summary>
	public class ArticleRepositories : ReadWriteRepositoryBase<src.Entities.Article>{

	}
	public class ArticlePageRepositories : PagedReadRepositoryBase<src.Entities.Article> {

	}
}