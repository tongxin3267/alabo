using Alabo.Dependency;

namespace Alabo.UI.AutoArticles
{
    public interface IAutoArticle : IScopeDependency
    {
        /// <summary>
        ///     文章详情内容
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        AutoArticleItem ResultList(string Id);

        /// <summary>
        ///     页面设置
        /// </summary>
        AutoSetting Setting();
    }
}