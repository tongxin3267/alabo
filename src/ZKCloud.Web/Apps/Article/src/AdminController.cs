using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Web.Apps.Article.src.Domains.Services;
using ZKCloud.Web.Apps.Article.src.Entities;
using ZKCloud.Web.Mvc;

namespace ZKCloud.Web.Apps.Article.src
{   
    [App("article")]
    public class AdminController: BaseController
    {

        //public Entities.Article get(int id)
        //{
        //    return new Entities.Article();
        //}
        //public EasyuiGridResult<Entities.Article> list(EasyuiGridPageInfo pageinfo)
        //{
        //    EasyuiGridResult<Entities.Article> list = Resolve<IArticleServices>().query(pageinfo,-1,"");
        //    return list;
        //}
        //public AjaxResult add(Entities.Article entity)
        //{
        //    try
        //    {
        //        Resolve<IArticleServices>().AddSingle(entity);
        //        return new AjaxResult { status = true, message = "保存成功" };
        //    }
        //    catch(Exception ex)
        //    {
        //        return new AjaxResult { status = true, message = "保存失败："+ex.Message };
        //    }
            
        //}

        //public AjaxResult update(Entities.Article entity)
        //{
        //    return new AjaxResult { status = true, message = "修改成功" };
        //}

        public AjaxResult delete(string ids)
        {
            try
            {
           
                Resolve<IArticleServices>().deletes(ids);

                return new AjaxResult() { status = true, message = "删除成功" };
            }
            catch (Exception ex)
            {
                return new AjaxResult() { status = false, message = "删除失败：" + ex.Message };
            }

        }
    }
}
