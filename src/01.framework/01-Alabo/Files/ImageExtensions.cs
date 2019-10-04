using System.IO;

namespace Alabo.Files {

    public static class ImageExtensions {

        /// <summary>
        ///     获取图片的地址
        /// </summary>
        /// <param name="imageUrl"></param>
        public static string GetSmallUrl(string imageUrl) {
            if (imageUrl != null) {
                if (File.Exists(FileHelper.RootPath + imageUrl)) {
                    return imageUrl;
                }
            } else {
                return "/wwwroot/static/images/nopic.png";
            }

            return "/wwwroot/static/images/nopic.png";
        }
    }
}