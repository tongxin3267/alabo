using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Offline.Merchants.Domain.Dtos;
using Alabo.App.Offline.Merchants.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;

namespace Alabo.App.Offline.Merchants.Domain.Services {

    public class MerchantStoreService : ServiceBase<MerchantStore, ObjectId>, IMerchantStoreService {

        public MerchantStoreService(IUnitOfWork unitOfWork, IRepository<MerchantStore, ObjectId> repository) : base(unitOfWork, repository) {
        }

        /// <summary>
        /// Get merchant
        /// </summary>
        /// <param name="merchantStoreId"></param>
        /// <returns></returns>
        public Tuple<Merchant, MerchantStore> GetMerchantByMerchantStoreId(string merchantStoreId) {
            //get merchant store
            var merchantStore = Resolve<IMerchantStoreService>().GetSingle(s => s.Id == merchantStoreId.ToObjectId());
            if (merchantStore != null) {
                var merchant = Resolve<IMerchantService>().GetSingle(m => m.Id == merchantStore.MerchantId.ToObjectId());
                return Tuple.Create(merchant, merchantStore);
            }
            return null;
        }

        /// <summary>
        /// Get merchant
        /// </summary>
        /// <param name="merchantStoreId"></param>
        /// <returns></returns>
        public Tuple<Merchant, MerchantStore> GetMerchantByUserId(long userId) {
            //get merchant store
            var merchantStore = Resolve<IMerchantStoreService>().GetSingle(s => s.UserId == userId);
            //if (merchantStore != null)
            //{
            var merchant = Resolve<IMerchantService>().GetSingle(m => m.UserId == userId);
            return Tuple.Create(merchant, merchantStore);
            //}
            //return null;
        }

        /// <summary>
        /// Get merchant stores
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="merchantStoreId"></param>
        /// <returns></returns>
        public List<MerchantStore> GetMerchantStore(long userId) {
            //get merchant store
            //var merchant = Resolve<IMerchantService>().GetSingle(m => m.UserId == userId);
            //if (merchant != null)
            //{
            //    return Resolve<IMerchantStoreService>().GetList(s => s.MerchantId == merchant.Id.ToString()).ToList();
            //}
            //else {
            return Resolve<IMerchantStoreService>().GetList().ToList();//s => s.UserId == userId
            //}
            // return new List<MerchantStore>();
        }

        /// <summary>
        /// save store info
        /// </summary>
        /// <param name="input"></param>
        public void Save(MerchantStoreInput input) {
            //get merchant store
            var isAddStore = true;
            MerchantStore store = null;
            var merchant = Resolve<IMerchantService>().GetSingle(m => m.UserId == input.UserId);
            if (merchant != null) {
                store = Resolve<IMerchantStoreService>().GetSingle(s => s.MerchantId == merchant.Id.ToString());
                if (store != null) {
                    isAddStore = false;
                    store.Name = input.Name;
                    store.Logo = input.Logo;
                    store.Description = input.Description;
                    Resolve<IMerchantStoreService>().Update(store);
                }
            } else {
                Resolve<IMerchantService>().Add(new Merchant {
                    UserId = input.UserId,
                    Name = input.Name,
                    // Status = Core.UserType.Domain.Enums.UserTypeStatus.Success
                });
            }

            if (isAddStore) {
                store = new MerchantStore {
                    Name = input.Name,
                    MerchantId = merchant.Id.ToString(),
                    Logo = input.Logo,
                    Description = input.Description
                };
                Resolve<IMerchantStoreService>().Add(store);
            }
        }
    }
}