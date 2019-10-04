using System;

namespace Alabo.Web.Mvc.Attributes {

    /// <summary>
    ///     字段是否为主字段，一个实体里头只能有一个字段
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class MainAttribute : Attribute {
    }
}