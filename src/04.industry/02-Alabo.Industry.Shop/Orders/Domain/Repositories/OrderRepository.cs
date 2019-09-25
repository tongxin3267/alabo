using System;
using System.Collections.Generic;
using System.Data;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;

namespace Alabo.App.Shop.Order.Domain.Repositories {

    public class OrderRepository : RepositoryEfCore<Entities.Order, long>, IOrderRepository {

        public List<Entities.Order> GetOrders(int? day = 2) {
            List<Entities.Order> result = new List<Entities.Order>();
            var sql = $"select OrderStatus,Id,CreateTime from  Shop_order where OrderStatus= {Convert.ToInt16(OrderStatus.WaitingBuyerPay)} and DATEDIFF(DAY, Createtime,GETDATE())={day}";
            using (var dr = RepositoryContext.ExecuteDataReader(sql)) {
                while (dr.Read()) {
                    result.Add(ReadOrder(dr));
                }
            }
            return result;
        }

        private Entities.Order ReadOrder(IDataReader dr) {
            var result = new Entities.Order {
                Id = dr["Id"].ConvertToLong(),
                OrderStatus = (OrderStatus)dr["OrderStatus"],
                CreateTime = dr["CreateTime"].ToDateTime()
            };
            return result;
        }

        public OrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }
    }
}