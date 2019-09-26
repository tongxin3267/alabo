using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Alabo.Datas.Queries.Enums;
using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Domains.Query.Dto;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Linq.Dynamic;
using Alabo.Mapping;
using Alabo.Web.ViewFeatures;
using ZKCloud.Open.DynamicExpression;
using Lambda = Alabo.Linq.Lambda;

namespace Alabo.Domains.Services.Page
{
    public abstract class GetPageBase<TEntity, TKey> : GetPageAsyncBase<TEntity, TKey>, IGetPage<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected GetPageBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public PagedList<TEntity> GetPagedList(IPageQuery<TEntity> query)
        {
            return Store.GetPagedList(query);
        }

        public PagedList<TEntity> GetPagedList(object parmater, Expression<Func<TEntity, bool>> predicate)
        {
            return GetPagedList(parmater, typeof(TEntity), predicate);
        }

        public long PageCount(int pageSize, Expression<Func<TEntity, bool>> predicate)
        {
            return Store.PageCount(pageSize, predicate);
        }

        public long PageCount(int pageSize)
        {
            return Store.PageCount(pageSize);
        }

        public IEnumerable<TEntity> GetListByPage(int pageSize, int pageIndex,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            return Store.GetListByPage(predicate, pageSize, pageIndex);
        }

        public IEnumerable<TEntity> GetListByPageDesc(int pageSize, int pageIndex,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            return Store.GetListByPageDesc(predicate, pageSize, pageIndex);
        }

        public PagedList<TEntity> GetPagedList(object paramater, Dictionary<string, string> dictionary)
        {
            var dic = paramater.DeserializeJson<Dictionary<string, string>>();
            dic.AddRange(dictionary);
            return GetPagedList(dic.ToJson(), typeof(TEntity));
        }

        public PageResult<TOutput> GetApiPagedList<TOutput>(object paramater,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            var result = GetPagedList<TOutput>(paramater, predicate);

            if (result == null) return null;

            var apiRusult = PageResult<TOutput>.Convert(result);

            return apiRusult;
        }

        public PagedList<TEntity> GetPagedList(object paramater)
        {
            return GetPagedList(paramater, typeof(TEntity));
        }

        public PagedList<TOutput> GetPagedList<TOutput>(object paramater,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            var pageList = GetPagedList(paramater, typeof(TEntity), predicate);
            var resultList = AutoMapping.ConverPageList<TOutput, TEntity>(pageList);
            return resultList;
        }

        public PagedList<TEntity> GetPagedList(object paramater, Type searchView,
            Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = new ExpressionQuery<TEntity>
            {
                EnablePaging = true
            };

            #region 构建查询页数

            var dictionary = paramater.DeserializeJson<Dictionary<string, string>>();
            if (dictionary != null)
            {
                var pagedInput = AutoMapping.SetValue<PagedInputDto>(dictionary);
                if (pagedInput != null)
                {
                    query.PageIndex = (int) pagedInput.PageIndex;
                    query.PageSize = (int) pagedInput.PageSize;
                }
            }

            #endregion 构建查询页数

            if (predicate != null) query.And(predicate);

            var result = HanderDictionary(ref dictionary); // 获取搜索范围相关的字段
            var rangList = result.Item1;
            var interpreter = new Interpreter();
            var classDescription = new ClassDescription(searchView);
            var propertyResults = searchView.GetPropertyResultFromCache();
            var propertyInfos = propertyResults.Select(r => r.PropertyInfo);

            #region 普通字段搜索

            foreach (var item in dictionary)
            {
                var name = item.Key.Replace("_Start", "").Replace("_End", "");
                var value = item.Value.SafeString();
                if (value.IsNullOrEmpty() || name.IsNullOrEmpty()) continue;

                if (name.Equals("UserName", StringComparison.OrdinalIgnoreCase))
                {
                    #region 搜索用户名

                    var property =
                        propertyInfos.FirstOrDefault(r => r.Name.Equals("UserId", StringComparison.OrdinalIgnoreCase));
                    if (property == null) continue;

                    var user = EntityDynamicService.GetSingleUser(value); // 动态获取用户名
                    if (user != null)
                    {
                        var expression = Lambda.Equal<TEntity>(property.Name, user.Id);
                        query.And(expression);
                    }

                    #endregion 搜索用户名
                }
                else if (name.Equals("Serial", StringComparison.OrdinalIgnoreCase))
                {
                    #region 搜索序号

                    // 序号处理,适合序号是通过Id生成的方式，比如Bill,Order,Reard表
                    var property =
                        propertyInfos.FirstOrDefault(r => r.Name.Equals("Id", StringComparison.OrdinalIgnoreCase));
                    if (property == null) continue;

                    if (value.Length > 7 && value.StartsWith("9")) value = value.Substring(1, value.Length - 1);
                    // 去掉前面的0
                    var reg = new Regex(@"^0+");
                    value = reg.Replace(value, "");
                    var idValue = value.ConvertToLong(0);
                    if (idValue > 0)
                    {
                        var expression = Lambda.Equal<TEntity>(property.Name, idValue);
                        query.And(expression);
                    }

                    #endregion 搜索序号
                }
                else
                {
                    var property =
                        propertyInfos.FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

                    if (property == null) continue;

                    if (property.PropertyType.IsEnum)
                        if (value == "-1")
                            continue;

                    var fieldAttribute = propertyResults.FirstOrDefault(r => r.PropertyInfo.Name == property.Name)
                        .FieldAttribute;
                    var valueList = value.ToSplitList();
                    foreach (var itemValue in valueList)
                        if (!itemValue.IsNullOrEmpty())
                        {
                            Expression<Func<TEntity, bool>> expression = null;
                            if (fieldAttribute != null)
                            {
                                if (fieldAttribute.Operator == Operator.Contains)
                                    expression = Lambda.Contains<TEntity>(property.Name, itemValue);

                                if (fieldAttribute.Operator == Operator.Equal)
                                    expression = Lambda.Equal<TEntity>(property.Name, itemValue);
                            }
                            // 字段中包含name和title，一般都可以模糊搜索
                            else if (property.Name.Contains("name", StringComparison.OrdinalIgnoreCase)
                                     || property.Name.Contains("intro", StringComparison.OrdinalIgnoreCase)
                                     || property.Name.Contains("desc", StringComparison.OrdinalIgnoreCase)
                                     || property.Name.Contains("remark", StringComparison.OrdinalIgnoreCase)
                                     || property.Name.Contains("title", StringComparison.OrdinalIgnoreCase))
                            {
                                expression = Lambda.Contains<TEntity>(property.Name, itemValue);
                            }
                            else
                            {
                                expression = Lambda.Equal<TEntity>(property.Name, itemValue);
                            }

                            if (expression != null) query.And(expression);
                        }
                }
            }

            #endregion 普通字段搜索

            #region 搜索范围字段搜索

            foreach (var item in rangList)
            {
                var property =
                    propertyInfos.FirstOrDefault(r => r.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));
                if (property == null || item.StartValue.IsNullOrEmpty() && item.EndValue.IsNullOrEmpty()) continue;
                // 大于等于
                if (!item.StartValue.IsNullOrEmpty())
                {
                    var expression = Lambda.GreaterEqual<TEntity>(property.Name, item.StartValue);
                    if (expression != null) query.And(expression);
                }

                // 小于等于
                if (!item.EndValue.IsNullOrEmpty())
                {
                    var expression = Lambda.LessEqual<TEntity>(property.Name, item.EndValue);
                    if (expression != null) query.And(expression);
                }
            }

            #endregion 搜索范围字段搜索

            #region 排序

            var sortType = result.Item2;
            var propertySort =
                propertyInfos.FirstOrDefault(r => r.Name.Equals(sortType.Name, StringComparison.OrdinalIgnoreCase));
            if (propertySort != null)
            {
                var selectExpression = $"entity.{propertySort.Name}";
                var expression = interpreter.ParseAsExpression<Func<TEntity, object>>(selectExpression, "entity");
                if (sortType.OrderType == OrderType.Descending)
                    query.OrderByDescending(expression);
                else
                    query.OrderByAscending(expression);
            }

            #endregion 排序

            var pageList = Store.GetPagedList(query);
            pageList = PagedListStyle(pageList);

            return pageList;
        }

        #region 辅助方法

        /// <summary>
        ///     Api图片地址
        /// </summary>
        /// <param name="imageUrl"></param>
        public string ApiImageUrl(string imageUrl)
        {
            if (imageUrl != null)
                if (!imageUrl.Contains("http", StringComparison.CurrentCultureIgnoreCase))
                    imageUrl = $"{HttpWeb.ServiceHostUrl}/{imageUrl}";

            if (imageUrl.IsNullOrEmpty()) return string.Empty;
            if (imageUrl.Contains("///")) return imageUrl.Replace("//", "/");

            return imageUrl;
        }

        #endregion 辅助方法

        #region 样式列表格式化

        /// <summary>
        ///     Pageds the list style.
        ///     样式列表格式化
        /// </summary>
        /// <param name="pageList"></param>
        /// <returns>PagedList&lt;TEntity&gt;.</returns>
        private PagedList<TEntity> PagedListStyle(PagedList<TEntity> pageList)
        {
            var entityType = typeof(TEntity);
            var entity = Activator.CreateInstance(typeof(TEntity));
            var propertys = entityType.GetPropertiesFromCache();
            var interpreter = new Interpreter();
            // 如果类型中有UserId，和UserName则 将UserName转换
            if (propertys.FirstOrDefault(r => r.Name == "UserName") != null &&
                propertys.FirstOrDefault(r => r.Name == "UserId") != null)
            {
                var selectExpression = "user.UserId";
                var dynamicSelect = interpreter.ParseAsDelegate<Func<TEntity, long>>(selectExpression, "user");
                var userIds = pageList.Select(dynamicSelect).Distinct().ToList();
                // 根据Id获取用户
                var users = EntityDynamicService.GetUserListByIds(userIds);

                var userResult = new List<TEntity>();
                foreach (var item in pageList)
                {
                    var dynamicItem = (dynamic) item;
                    var userExpression = $"user.UserId=={dynamicItem.UserId} ";
                    users.Foreach(r =>
                    {
                        var dynamicUser = (dynamic) r;
                        if (dynamicUser.Id == dynamicItem.UserId)
                        {
                            var gradeConfig = EntityDynamicService.GetUserGrade(dynamicUser.GradeId);
                            dynamicItem.UserName =
                                $" <img src='{ApiImageUrl(gradeConfig.Icon)}' alt='{gradeConfig.Name}' class='user-pic' style='width:18px;height:18px;' /><a class='primary-link margin-8'  title='{dynamicUser.UserName}({dynamicUser.Name}) 等级:{gradeConfig?.Name}'>{dynamicUser.UserName}({dynamicUser.Name})</a>"; //href='/Admin/User/Edit?id={dynamicUser.Id}'
                        }
                    });
                    userResult.Add(dynamicItem);
                }

                pageList = PagedList<TEntity>.Create(userResult, pageList.RecordCount, pageList.PageSize,
                    pageList.PageIndex);
            }

            return pageList;
        }

        /// <summary>
        ///     从字典中取出值，并且从字典中删除值，不区分大小写
        ///     排序目前只支持一种
        /// </summary>
        /// <param name="dictionary">字典数据</param>
        private Tuple<IList<RangDto>, SortTypeDto> HanderDictionary(ref Dictionary<string, string> dictionary)
        {
            dictionary = dictionary.RemoveKey("PageSize", "PageIndex", "Service", "Method"); // 删除不需要搜索字段
            dictionary = dictionary.RemoveNullOrEmpty(); // 删除值为空或者为null的部分
            IList<RangDto> list = new List<RangDto>();
            var sortTypeDto = new SortTypeDto();
            foreach (var item in dictionary)
            {
                if (item.Key.EndsWith("_Start", StringComparison.OrdinalIgnoreCase))
                {
                    var key = item.Key.Replace("_Start", "", StringComparison.OrdinalIgnoreCase);
                    SetRangValue(key, item.Value, true, ref list);
                    dictionary = dictionary.RemoveKey(item.Key);
                }

                if (item.Key.EndsWith("_End", StringComparison.OrdinalIgnoreCase))
                {
                    var key = item.Key.Replace("_End", "", StringComparison.OrdinalIgnoreCase);
                    SetRangValue(key, item.Value, false, ref list);
                    dictionary = dictionary.RemoveKey(item.Key);
                }

                //排序字段
                if (item.Key.Equals("SortOrder_Name", StringComparison.OrdinalIgnoreCase))
                {
                    sortTypeDto.Name = item.Value;
                    dictionary = dictionary.RemoveKey(item.Key);
                }

                //排序方式
                if (item.Key.Equals("SortOrder_Type", StringComparison.OrdinalIgnoreCase))
                {
                    if (item.Value.StringToEnum(out OrderType orderType)) sortTypeDto.OrderType = orderType;

                    dictionary = dictionary.RemoveKey(item.Key);
                }
            }

            return Tuple.Create(list, sortTypeDto);
        }

        private void SetRangValue(string key, string value, bool isStart, ref IList<RangDto> list)
        {
            var find = list.FirstOrDefault(r => r.Name.Equals(key, StringComparison.OrdinalIgnoreCase));
            if (find == null)
            {
                var rangDto = new RangDto
                {
                    Name = key
                };
                if (isStart)
                    rangDto.StartValue = value;
                else
                    rangDto.EndValue = value;

                list.Add(rangDto);
            }
            else
            {
                list.Foreach(r =>
                {
                    if (r.Name.Equals(key, StringComparison.OrdinalIgnoreCase))
                    {
                        if (isStart)
                            r.StartValue = value;
                        else
                            r.EndValue = value;
                    }
                });
            }
        }

        public PageResult<TEntity> GetApiPagedList(object paramater)
        {
            var result = GetPagedList<TEntity>(paramater);

            if (result == null) return null;

            var apiRusult = PageResult<TEntity>.Convert(result);

            return apiRusult;
        }

        #endregion 样式列表格式化
    }
}