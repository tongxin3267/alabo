using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Web.Apps.Article.src.Entities
{
    /// <summary>
    /// Easyui DataGrid 分页请求参数
    /// </summary>
    public class EasyuiGridPageInfo
    {
        private int _rows = 10;
        private int _page = 1;
        public int rows {
            get {
                if (this._rows == 0) this._rows=10;
                return _rows;
            }
            set
            {
                this._rows = value;
            }
        }
        public int page {
            get {
                if (this._page == 0) return this._page=1;
                return this._page;
            }
            set {
                this._page = value;
            }
        }
    }
}
