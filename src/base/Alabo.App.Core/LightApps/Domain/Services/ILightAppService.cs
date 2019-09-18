using MongoDB.Bson;
using System.Collections.Generic;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.LightApps.Domain.Services {

    /// <summary>
    /// 自动数据接口
    /// </summary>
    public interface ILightAppService : IService {

        /// <summary>
        /// Table的数据总数
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        long Count(string tableName);

        /// <summary>
        /// 添加自动数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="dataJson">传入等保存的json数据</param>
        /// <returns></returns>
        ServiceResult Add(string tableName, string dataJson);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataJson"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResult Update(string tableName, string dataJson, ObjectId id);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResult Delete(string tableName, ObjectId id);

        /// <summary>
        /// 按ObjectId获取单条记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        dynamic GetSingle(string tableName, ObjectId id);

        /// <summary>
        /// 按FieldName和FieldValue获取单条记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        dynamic GetSingle(string tableName, string fieldName, string fieldValue);

        dynamic GetSingle(string tableName, Dictionary<string, string> query);

        /// <summary>
        /// 按FieldName和FieldValue获取List记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        List<dynamic> GetList(string tableName, string fieldName = "", string fieldValue = "");

        /// <summary>
        /// // Get,GetList等接口，动态参数查询，字段名与数据库一致，不区分大小写
        //  == ：Operator.Equal（等于），可省去，默认
        //  << ：Operator.Less（小于）
        //  <= ：Operator.LessEqual（小于等于）
        //  >> ：Operator.Greater（大于）
        //  >= ：Operator.GreaterEqual（大于等于）
        //  != ：Operator.NotEqual（不等于）
        //  c% ：Operator.Contains（包含）
        //  const para= {
        //    userId: '>=10',
        //    userName: 'c%admin',
        //    mobile: '13989646465'
        //  }
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        List<dynamic> GetList(string tableName, object query);

        List<dynamic> GetList(string tableName, Dictionary<string, string> query);

        /// <summary>
        /// 按UserId获取List记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<dynamic> GetListByUserId(string tableName, long userId);

        /// <summary>
        /// 按ClassId获取List记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        List<dynamic> GetListByClassId(string tableName, long classId);

        /// <summary>
        /// 按Query条件获取List记录
        //  == ：Operator.Equal（等于），可省去，默认
        //  << ：Operator.Less（小于）
        //  <= ：Operator.LessEqual（小于等于）
        //  >> ：Operator.Greater（大于）
        //  >= ：Operator.GreaterEqual（大于等于）
        //  != ：Operator.NotEqual（不等于）
        //  c% ：Operator.Contains（包含）
        //  const para= {
        //    userId: '>=10',
        //    userName: 'c%admin',
        //    mobile: '13989646465'
        //  }
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        PagedList<dynamic> GetPagedList(string tableName, object query);
    }
}