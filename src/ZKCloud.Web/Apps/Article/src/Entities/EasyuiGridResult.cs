using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Web.Apps.Article.src.Entities
{
    /// <summary>
    /// EasyUI  DataGrid 的结果集定义
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EasyuiGridResult<T>
    {
        /// <summary>
        /// 记录总数
        /// </summary>
        public long total { get; set; }
        /// <summary>
        /// 记录结果集
        /// </summary>
        public List<T> rows { get; set; }

        /// <summary>
        /// EasyUI  DataGrid 的结果集定义
        /// </summary>
        /// <param name="_total">待查询数据集总数</param>
        /// <param name="list">当前返回的结果集(当前页)</param>
        public EasyuiGridResult(long _total, List<T> list)
        {
            this.total = _total;
            if(list==null){
                rows = new List<T>();
            }
            else
            {
                rows = list;
            }
        }
    }
}
