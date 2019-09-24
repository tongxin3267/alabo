using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using Alabo.Domains.Query.Dto;

namespace Alabo.App.Market.BookingSignup.Dtos
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
