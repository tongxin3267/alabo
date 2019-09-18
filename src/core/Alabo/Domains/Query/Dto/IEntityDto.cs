namespace Alabo.Domains.Query.Dto
{
    /// <summary>
    ///     将input/output参数命名为MethodNameInput和 MethodNameOutput，
    ///     并为每个应用服务方法定义一个单独的input和output DTO。即使你的方法只需要或返回一个参数，
    ///     最好也创建一个DTO类。这样，你的代码回更具有扩展性。以后你可以添加更多的属性而不用改变方法的签名
    ///     ，而且也不用使已存在的客户端应用发生重大变化
    /// </summary>
    public interface IEntityDto : IEntityDto<long>
    {
    }

    public interface IEntityDto<TPrimaryKey>
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        //TPrimaryKey Id { get; set; }
    }
}