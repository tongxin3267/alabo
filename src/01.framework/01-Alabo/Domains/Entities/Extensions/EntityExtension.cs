using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Domains.Entities.Extensions {

    /// <summary>
    ///     数据库扩展属性，一般以JSon的方式重新保存
    /// </summary>
    public abstract class EntityExtension : BaseViewModel, IEntityExtension {
    }
}