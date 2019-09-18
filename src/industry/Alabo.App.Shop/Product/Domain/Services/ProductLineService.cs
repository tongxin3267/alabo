using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Shop.Product.Controllers;
using Alabo.App.Shop.Product.Domain.CallBacks;
using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.App.Shop.Product.ViewModels;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;
using ProductModel = Alabo.App.Shop.Product.Domain.Entities.Product;

namespace Alabo.App.Shop.Product.Domain.Services {

    /// <summary>
    ///     Class ProductLineService.
    /// </summary>
    public class ProductLineService : ServiceBase<ProductLine, long>, IProductLineService {

        /// <summary>
        ///     获取s the line models.
        /// </summary>
        /// <param name="id">Id标识</param>
        /// <param name="pageSize">Size of the 分页.</param>
        /// <param name="pageIndex">Index of the 分页.</param>
        public PagedList<ProductLineModel> GetLineModels(long id, int? pageSize, int? pageIndex) {
            var productLine = Resolve<IProductLineService>().GetSingle(e => e.Id.Equals(id));
            var productIds = new List<long>();
            if (!productLine.ProductIds.IsNullOrEmpty()) {
                var tags = productLine.ProductIds.Deserialize(new { ProductId = 1 });
                foreach (var item in tags) {
                    productIds.Add(item.ProductId);
                }
            }

            var list = new List<ProductLineModel>();
            var query = new ExpressionQuery<ProductModel> {
                PageIndex = pageIndex ?? 1,
                PageSize = pageSize ?? 30
            };
            query.And(e => productIds.Contains(e.Id));
            var model = Resolve<IProductService>().GetPagedList(query);
            foreach (var item in productIds) {
                var product = model.FirstOrDefault(u => u.Id == item);
                if (product == null) {
                    continue;
                }

                var productLineModel = AutoMapping.SetValue<ProductLineModel>(product);
                var priceStyles = Alabo.Helpers.Ioc.Resolve<IAutoConfigService>()
                    .GetList<PriceStyleConfig>(r => r.Status == Status.Normal);
                if (priceStyles != null) {
                    priceStyles = priceStyles.OrderBy(r => r.SortOrder).ToList();
                }

                productLineModel.PriceStyleName = priceStyles.FirstOrDefault(u => u.Id == product.PriceStyleId).Name;
                list.Add(productLineModel);
            }

            return PagedList<ProductLineModel>.Create(list, list.Count, model.PageSize, model.PageIndex);
        }

        /// <summary>
        ///     获取s the line models.
        /// </summary>
        /// <param name="pname">The pname.</param>
        /// <param name="pageSize">Size of the 分页.</param>
        /// <param name="pageIndex">Index of the 分页.</param>
        public PagedList<ProductLineModel> GetLineModels(string pname, int? pageSize, int? pageIndex) {
            var query = new ExpressionQuery<ProductModel>();
            if (!pname.IsNullOrEmpty()) {
                query.And(r => r.Name.Contains(pname));
            }

            query.EnablePaging = true;
            query.PageSize = pageSize ?? 10;
            query.PageIndex = pageIndex ?? 1;
            var list = Resolve<IProductService>().GetPagedList(query);
            var plinelist = Resolve<IProductLineService>().GetList();
            var products = new List<ProductModel>();
            IList<ProductLineModel> productLineModels = new List<ProductLineModel>();
            foreach (var item in list) {
                var productIds = new List<long>();
                var plModel = AutoMapping.SetValue<ProductLineModel>(item);
                foreach (var temp in plinelist) {
                    if (!temp.ProductIds.IsNullOrEmpty()) {
                        var tags = temp.ProductIds.Deserialize(new { ProductId = 1 });
                        foreach (var tag in tags) {
                            if (tag.ProductId == item.Id) {
                                if (!plModel.Plinestring.IsNullOrEmpty()) {
                                    plModel.Plinestring = plModel.Plinestring + "," + temp.Name;
                                } else {
                                    plModel.Plinestring = temp.Name;
                                }
                            }
                        }
                    }
                }

                productLineModels.Add(plModel);
            }

            return PagedList<ProductLineModel>.Create(productLineModels, list.RecordCount, list.PageSize,
                list.PageIndex);
        }

        /// <summary>
        ///     获取s the 分页 list.
        /// </summary>
        /// <param name="query">查询</param>
        public PagedList<ProductLine> GetPageList(object query) {
            var list = Resolve<IProductLineService>().GetPagedList(query);
            var productLines = new List<ProductLine>();
            foreach (var item in list) {
                var line = AutoMapping.SetValue<ProductLine>(item);
                productLines.Add(line);
            }

            return PagedList<ProductLine>.Create(productLines, productLines.Count, list.PageSize, list.PageIndex);
        }

        /// <summary>
        ///     获取s the product ids.
        ///     根据产品线Id，获取所有的商品Id
        /// </summary>
        /// <param name="ids">The ids.</param>
        public List<long> GetProductIds(List<long> ids) {
            var list = GetList(r => ids.Contains(r.Id));
            var productIdList = new List<long>();
            foreach (var item in list) {
                var productIds = item.ProductIds.ToObject<List<ProductLineNode>>();
                productIdList.AddRange(productIds.Select(r => r.ProductId).ToList());
            }

            return productIdList;
        }

        /// <summary>
        ///     删除s the specified identifier.
        /// </summary>
        /// <param name="id">Id标识</param>
        public bool Delete(long id) {
            var result = ServiceResult.Success;
            var productLine = Resolve<IProductLineService>().GetSingle(u => u.Id == id);
            if (productLine == null) {
                return false;
            }

            Resolve<IProductLineService>().Delete(u => u.Id == id);
            return true;
        }

        public ProductLineService(IUnitOfWork unitOfWork, IRepository<ProductLine, long> repository) : base(unitOfWork, repository) {
        }
    }

    public class ProductLineNode {
        public long ProductId { get; set; }
    }
}