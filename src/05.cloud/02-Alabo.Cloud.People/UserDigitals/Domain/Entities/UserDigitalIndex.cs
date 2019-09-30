using Alabo.Domains.Entities;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Cloud.People.UserDigitals.Domain.Entities
{
    /// <summary>
    ///     数字画像索引
    /// </summary>
    [Table("Cloud_People_UserDigitalIndex")]
    public class UserDigitalIndex : AggregateMongodbUserRoot<UserDigitalIndex>
    {
        /// <summary>
        ///     数字画像Id
        /// </summary>
        public ObjectId DigitalId { get; set; }
    }
}