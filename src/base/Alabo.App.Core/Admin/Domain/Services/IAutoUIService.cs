using System.Collections.Generic;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Admin.Domain.Services {

    /// <summary>
    /// 自动UI相关的服务
    /// </summary>
    public interface IAutoUIService : IService {

        /// <summary>
        /// 根据类型返回UI表单
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        ServiceResult GetAutoComponents(string type);

        /// <summary>
        /// 获取Pc端所有的URL调试地址
        /// </summary>
        /// <returns></returns>
        List<KeyValue> GetAllPcUrl();

        /// <summary>
        /// 自动表格的KeyValue对象
        /// </summary>
        /// <returns></returns>
        List<KeyValue> AutoTableKeyValues();

        /// <summary>
        /// 自动表单的KeyValue对象
        /// </summary>
        /// <returns></returns>
        List<KeyValue> AutoFormKeyValues();

        /// <summary>
        /// 自动列表的KeyValue对象
        /// </summary>
        /// <returns></returns>
        List<KeyValue> AutoListKeyValues();

        /// <summary>
        /// 自动预览的KeyValue对象
        /// </summary>
        /// <returns></returns>
        List<KeyValue> AutoPreviewKeyValues();

        /// <summary>
        /// 新闻列表的KeyValue对象
        /// </summary>
        /// <returns></returns>
        List<KeyValue> AutoNewsKeyValues();

        /// <summary>
        /// 文章列表的KeyValue对象
        /// </summary>
        /// <returns></returns>
        List<KeyValue> AutoArticleKeyValues();

        /// <summary>
        /// 统计列表的KeyValue对象
        /// </summary>
        /// <returns></returns>
        List<KeyValue> AutoReportKeyValues();

        /// <summary>
        /// 常见问题的KeyValue对象
        /// </summary>
        /// <returns></returns>
        List<KeyValue> AutoFaqsKeyValues();

        /// <summary>
        /// 自动图片的KeyValue对象
        /// </summary>
        /// <returns></returns>
        List<KeyValue> AutoImagesKeyValues();

        /// <summary>
        /// 自动索引的KeyValue对象
        /// </summary>
        /// <returns></returns>
        List<KeyValue> AutoIndexKeyValues();

        /// <summary>
        /// 自动索引的KeyValue对象
        /// </summary>
        /// <returns></returns>
        List<KeyValue> AutoIntroKeyValues();

        /// <summary>
        /// 自动任务的KeyValue对象
        /// </summary>
        /// <returns></returns>
        List<KeyValue> AutoTaskKeyValues();

        /// <summary>
        /// 自动视频的KeyValue对象
        /// </summary>
        /// <returns></returns>
        List<KeyValue> AutoVideoKeyValues();
    }
}