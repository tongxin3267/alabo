using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Repositories;
using ZKCloud.Domain.Services;
using ZKCloud.Web.Apps.Article.src.Entities;
using ZKCloud.Web.Apps.Article.src.Domains.Repositories;
using Microsoft.AspNet.Mvc;
using ZKCloud.Web.Apps.Article.src.Domains.Enums;
using System.Linq.Expressions;
using System.Diagnostics;

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
        //=======================================================================
        public Entities.Article get(long id)
        {
            return Repository<ArticleRepositories>().ReadSingle(c => c.Id == id);
        }


        public AjaxResult add(Entities.Article  entity)
        {
            try
            {
                entity.CreateTime = DateTime.Now;
                Repository<ArticleRepositories>().AddSingle(entity);
                return new AjaxResult() { status = true, message = "新增成功" };
            }
            catch (Exception ex)
            {
                return new AjaxResult() { status = false, message = "新增失败:"+ex.Message };
            }
        }

        public AjaxResult edit(Entities.Article entity)
        {
            try
            {
                //这里更新要注意，如果页面上没有保持所有字段的值，那么这里应该先从数据库获取对象，在更新这个对象的相应的值,
                
                entity.LastUpdated = DateTime.Now;
                Repository<ArticleRepositories>().UpdateSingle(entity);
                Repository<ArticleRepositories>().RepositoryContext.SaveChanges();
                return new AjaxResult() { status = true, message = "修改成功" };
            }
            catch (Exception ex)
            {
                return new AjaxResult() { status = false, message = "修改失败:" + ex.Message };
            }
        }

        public AjaxResult deletes(string ids)
        {
            try
            {
                var arrayId = ids.Split(',');
                List<long> ListId = new List<long>();
                foreach (var item in arrayId)
                {
                    if (!string.IsNullOrEmpty(item))
                        ListId.Add(long.Parse(item));
                }
                //Repository<ArticleRepositories>().UpdateMany(c => ListId.Contains(c.Id), e=> e.State = ArticleStates.Delete);
                List<Entities.Article> ups = Repository<ArticleRepositories>().ReadMany(c => ListId.Contains(c.Id)).ToList();
                ups.ForEach((c) =>
                {
                    c.State = ArticleStates.Delete;
                    Repository<ArticleRepositories>().UpdateSingle(c);
                });
                Repository<ArticleRepositories>().RepositoryContext.SaveChanges();
                return new AjaxResult() { status = true, message = "删除成功" };
            }
            catch (Exception ex)
            {
                return new AjaxResult() { status = false, message = "删除失败：" + ex.Message };
            }

        }

        public EasyuiGridResult<Entities.Article> query(EasyuiGridPageInfo pageinfo, int state = -1, string sreach = null)
        {
            try
            {


                Expression<Func<Entities.Article, bool>> exp = null;
                if (state == -1)
                {
                    exp = c => true;
                    if (!string.IsNullOrEmpty(sreach))
                    {
                        exp = c => c.Title.Contains(sreach);
                    }
                }
                else
                {
                    exp = c => c.State == (ArticleStates)state;
                    if (!string.IsNullOrEmpty(sreach))
                    {
                        exp = c => c.State == (ArticleStates)state && c.Title.Contains(sreach);
                    }
                }

                Debug.WriteLine("sreach:" + sreach);
                PagedList<Entities.Article> pagelist = new ArticlePageRepositories().ReadMany(exp, pageinfo.rows, pageinfo.page);
                return new EasyuiGridResult<Entities.Article>(pagelist.RecordCount, pagelist.ToList());
            }
            catch(Exception ex)
            {
                Debug.WriteLine("查询数据内部异常：" + ex.Message);
                Debug.WriteLine("调用堆栈：" + ex.StackTrace.ToString());
                return new EasyuiGridResult<Entities.Article>(0, new List<Entities.Article>());
            }
        }        
    }
}
