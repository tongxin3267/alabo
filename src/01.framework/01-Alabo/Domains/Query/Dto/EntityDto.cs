using Alabo.Web.Mvc.ViewModel;
using System;

namespace Alabo.Domains.Query.Dto {

    /// <summary>
    ///     A shortcut of <see cref="EntityDto{TPrimaryKey}" /> for most used primary key type (<see cref="int" />).
    /// </summary>
    [Serializable]
    public class EntityDto : EntityDto<long>, IEntityDto {
        ///// <summary>
        ///// Creates a new <see cref="EntityDto"/> object.
        ///// </summary>
        ///// <param name="id">Id of the entity</param>
        //public EntityDto(long id)
        //    : base(id) {
        //}
    }

    /// <summary>
    ///     Implements common properties for entity based DTOs.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key</typeparam>
    [Serializable]
    public class EntityDto<TPrimaryKey> : BaseViewModel, IEntityDto<TPrimaryKey> {
        ///// <summary>
        ///// Id of the entity.
        ///// </summary>
        //public TPrimaryKey Id { get; set; }

        ///// <summary>
        ///// Creates a new <see cref="EntityDto{TPrimaryKey}"/> object.
        ///// </summary>
        //public EntityDto() {
        //}

        ///// <summary>
        ///// Creates a new <see cref="EntityDto{TPrimaryKey}"/> object.
        ///// </summary>
        ///// <param name="id">Id of the entity</param>
        //public EntityDto(TPrimaryKey id) {
        //    Id = id;
        //}
    }
}