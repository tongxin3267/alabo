﻿using Alabo.Datas.Sql.Queries.Builders.Abstractions;

namespace Alabo.Datas.Sql.Queries.Builders.Conditions {

    /// <summary>
    ///     Sql小于等于查询条件
    /// </summary>
    public class LessEqualCondition : ICondition {

        /// <summary>
        ///     左操作数
        /// </summary>
        private readonly string _left;

        /// <summary>
        ///     右操作数
        /// </summary>
        private readonly string _right;

        /// <summary>
        ///     初始化Sql小于等于查询条件
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public LessEqualCondition(string left, string right) {
            _left = left;
            _right = right;
        }

        /// <summary>
        ///     获取查询条件
        /// </summary>
        public string GetCondition() {
            return $"{_left}<={_right}";
        }
    }
}