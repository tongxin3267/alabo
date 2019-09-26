using System;

namespace Alabo.Industry.Offline.RechargeAccount.Domain.Dtos {
   public  class RechargeAccountInput {

       public string  UserName { get; set; }

       public  Guid Id { get; set; }


       /// <summary>
       /// 是否管理员充值
       /// </summary>
       public bool IsAdmin { get; set; }
   }
}
