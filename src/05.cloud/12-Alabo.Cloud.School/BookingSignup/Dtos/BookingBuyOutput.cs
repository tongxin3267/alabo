using Alabo.Domains.Query.Dto;
using MongoDB.Bson;

namespace Alabo.Cloud.School.BookingSignup.Dtos
{
    public class BookingBuyOutput : EntityDto
    {
        /// </summary>
        public decimal PayAmount { get; set; }

        /// <summary>
        ///     支付Id
        /// </summary>
        public long PayId { get; set; }

        public ObjectId OrderId { get; set; }
    }
}
