using System;
using System.Linq.Expressions;
using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Extensions;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.UI.AutoReports.Enums;

namespace Alabo.Linq
{
    /// <summary>
    ///     构建实体动态查询条件
    ///     目前只支持单个查询提交，后期可拓展成支持多个
    /// </summary>
    public class EntityQueryCondition
    {
        /// <summary>
        ///     实体类型,比如User,Order
        /// </summary>
        public string EntityType { get; set; }

        /// <summary>
        ///     字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        ///     运算符
        /// </summary>
        public OperatorCompare Operator { get; set; }

        /// <summary>
        ///     比较值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        ///     时间统计方式
        /// </summary>
        public TimeType TimeType { get; set; }

        /// <summary>
        ///     基准时间
        ///     基准时间和TimeType组成StartTime，和EndTime
        /// </summary>
        public DateTime ReferTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     自定义开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        ///     自定义结束时间
        /// </summary>
        public DateTime EndTime { get; set; }


        /// <summary>
        ///     获取表名
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            var typeFind = EntityType.GetTypeByName();
            var instanceFind = EntityType.GetInstanceByName();

            var table = Ioc.Resolve<ITableService>().GetSingle(r => r.Key == typeFind.Name);

            return table.TableName;
        }

        /// <summary>
        ///     转换成Sql查询语句,Sql数据库使用Sql查询
        /// </summary>
        /// <returns></returns>
        public string ToSqlWhere(ReportStyle reportStyle = ReportStyle.Count)
        {
            //  以下为示例代码
            var sqlWhere = " WHERE 1 = 1 ";
            if (Field.IsNotNullOrEmpty() && Value.IsNotNullOrEmpty())
            {
                var operatorMark = Operator.GetFieldAttribute().Mark;
                sqlWhere += $" AND {Field} {operatorMark} '{Value}'"; // 此处需要根据类型做额外的处理
            }

            if (reportStyle == ReportStyle.Count || reportStyle == ReportStyle.Sum ||
                reportStyle == ReportStyle.Avg || reportStyle == ReportStyle.Min || reportStyle == ReportStyle.Max)
            {
                // 根据timeType获取条件,获取季度，年度、月份等条件,构建时间查询语句
                var timeTypeCondition = DataRecordExtension.GetTimeTypeCondition(TimeType, ReferTime);
                if (!timeTypeCondition.IsNullOrEmpty()) {
                    sqlWhere += $" And {timeTypeCondition}";
                }
            }

            return sqlWhere;
        }


        /// <summary>
        ///     转换成Linq查询条件
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public Expression<Func<TEntity, bool>> ToLinq<TEntity, TKey>()
            where TEntity : class, IAggregateRoot<TEntity, TKey>
        {
            Expression<Func<TEntity, bool>> predicate = null;
            if (Field.IsNotNullOrEmpty() && Value.IsNotNullOrEmpty()) {
                switch (Operator)
                {
                    case OperatorCompare.Equal:
                        predicate = Lambda.Equal<TEntity>(Field, Value);
                        break;

                    case OperatorCompare.Greater:
                        predicate = Lambda.Greater<TEntity>(Field, Value);
                        break;

                    case OperatorCompare.GreaterEqual:
                        predicate = Lambda.GreaterEqual<TEntity>(Field, Value);
                        break;

                    case OperatorCompare.Less:
                        predicate = Lambda.Less<TEntity>(Field, Value);
                        break;

                    case OperatorCompare.LessEqual:
                        predicate = Lambda.LessEqual<TEntity>(Field, Value);
                        break;

                    case OperatorCompare.NotEqual:
                        predicate = Lambda.NotEqual<TEntity>(Field, Value);
                        break;
                }
            }

            // 根据时间和基准时间构建表达式
            predicate = TimeType.GetPredicate<TEntity, TKey>(ReferTime, predicate);

            return predicate;
        }
    }
}