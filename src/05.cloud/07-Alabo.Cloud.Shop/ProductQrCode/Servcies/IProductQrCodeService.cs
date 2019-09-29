using Alabo.Domains.Services;

namespace Alabo.Cloud.Shop.ProductQrCode.Servcies
{
    public interface IProductQrCodeService : IService
    {
        /// <summary>
        ///     生成商品二维码
        /// </summary>
        /// <param name="productId"></param>
        void CreateQrcode(long productId);
    }
}