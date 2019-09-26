using System;
using System.Collections.Generic;
using System.Data;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Industry.Shop.Orders.Domain.Entities;
using Alabo.Industry.Shop.Orders.Domain.Enums;

namespace Alabo.Industry.Shop.Orders.Domain.Repositories
{
    public class OrderRepository : RepositoryEfCore<Order, long>, IOrderRepository
    {
        public OrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<Order> GetOrders(int? day = 2)
        {
            var result = new List<Order>();
            var sql =
                $"select OrderStatus,Id,CreateTime from  Shop_order where OrderStatus= {Convert.ToInt16(OrderStatus.WaitingBuyerPay)} and DATEDIFF(DAY, Createtime,GETDATE())={day}";
            using (var dr = RepositoryContext.ExecuteDataReader(sql))
            {
                while (dr.Read()) result.Add(ReadOrder(dr));
            }

            return result;
        }

        private Order ReadOrder(IDataReader dr)
        {
            var result = new Order
            {
                Id = dr["Id"].ConvertToLong(),
                OrderStatus = (OrderStatus) dr["OrderStatus"],
                CreateTime = dr["CreateTime"].ToDateTime()
            };
            return result;
        }
    }
}