using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Alabo.App.Core.Common.Domain.CallBacks;
using Alabo.App.Core.Common.ViewModels;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.AutoConfigs;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Files;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Helpers;
using Alabo.Runtime;
using Alabo.Schedules;
using Alabo.Users.Entities;
using File = System.IO.File;

namespace Alabo.App.Core.User.Domain.Services {

    public class UserQrCodeService : ServiceBase<UserDetail, long>, IUserQrCodeService {

        public UserQrCodeService(IUnitOfWork unitOfWork, IRepository<UserDetail, long> repository) : base(unitOfWork, repository) {
        }

        public PagedList<ViewImagePage> QrCodePageList(object query) {
            var userResult = Resolve<IUserService>().GetPagedList(query);

            var list = new List<ViewImagePage>();
            userResult.ForEach(r => {
                list.Add(new ViewImagePage() {
                    Id = r.Id,
                    Title = r.GetUserName(),
                    ImageUrl = $"/wwwroot/qrcode/{r.Id}.jpeg"
                });
            });
            return PagedList<ViewImagePage>.Create(list, userResult.RecordCount, userResult.PageSize, userResult.PageIndex);
        }

        /// <summary>
        /// 生成二维码任务
        /// </summary>
        public void CreateCodeTask() {
            var backJobParameter = new BackJobParameter {
                ModuleId = TaskQueueModuleId.UserQrcodeCreate,
                CheckLastOne = true,
                ServiceName = typeof(IUserQrCodeService).Name,
                Method = "CreateAllUserQrCode"
            };
            //TODO 9月重构注释
            //  Resolve<ITaskQueueService>().AddBackJob(backJobParameter);
        }

        /// <summary>
        /// 更新所有会员的二维码
        /// </summary>
        public void CreateAllUserQrCode() {
            var maxUserId = Resolve<IUserService>().MaxUserId();
            for (var i = 0; i < maxUserId + 1; i++) {
                var user = Ioc.Resolve<IUserService>().GetUserDetail(i);
                if (user != null) {
                    CreateCode(user);
                }
            }
        }

        /// <summary>
        ///     生成会员二维码
        /// </summary>
        /// <param name="userId">用户Id</param>
        public string QrCore(long userId) {
            var path = $"/wwwroot/qrcode/{userId}.jpeg";
            var qrcodePath = FileHelper.RootPath + path;

            if (File.Exists(qrcodePath)) {
                return Resolve<IApiService>().ApiImageUrl(path);
            }

            var user = Resolve<IUserService>().GetSingle(userId);
            CreateCode(user); // 如果不存在则继续生成二维码
            return Resolve<IApiService>().ApiImageUrl(path);
        }

        /// <summary>
        ///     生成用户的二维码，并且把二维码保存到本地
        /// </summary>
        /// <param name="user">用户</param>
        public void CreateCode(Users.Entities.User user) {
            //文件夹不存在，重新生成文件夹
            FileHelper.CreateDirectory(FileHelper.WwwRootPath + "//qrcode");
            //二维码图片地址
            var qrcodePath = $"/wwwroot/qrcode/{user.Id}.jpeg";
            var qrConfig = Resolve<IAutoConfigService>().GetValue<QrCodeConfig>();
            if (qrConfig != null) {
                if (qrConfig.BgPicture.IsNullOrEmpty()) {
                    qrConfig.BgPicture = "/wwwroot/assets/mobile/images/qrcode/01.png";
                }

                qrConfig.BgPicture.Replace('/', '\\');
            }

            if (qrConfig.QrCodeBig <= 50) {
                qrConfig.QrCodeBig = 300;
            }

            var webSite = Resolve<IAutoConfigService>().GetValue<WebSiteConfig>();
            //二维码网址
            // 前端/from负责将UserName写入cookis或者流量器缓存
            // userName 首字母必须小写
            // from页面根据二维码设置，完成两种客户需求：1.跳转需求，2 展示具体的内容，比如公司介绍等等
            //var url = $@"{webSite.DomainName}/user/reg?ParentUserName={user.UserName}";
            var url = $@"{webSite.DomainName}/pages/index?path=user_reg&usercode={user.UserName}";
            if (!url.Contains("http://") || !url.Contains("https://")) {
                url = $"https://{url}";
            }

            try {
                var qrGenerator = new QRCodeGenerator();
                var qrCodeData =
                    qrGenerator.CreateQrCode(url.ToEncoding(), QRCodeGenerator.ECCLevel.H); // 使用utf-8编码，解决某些浏览器不能识别问题
                var qrCode = new QRCode(qrCodeData);
                var bmp = qrCode.GetGraphic(100);
                bmp = new Bitmap(bmp, qrConfig.QrCodeBig, qrConfig.QrCodeBig);

                var bgPath = RuntimeContext.Current.Path.RootPath + qrConfig.BgPicture;
                var bg = Image.FromFile(bgPath);
                var g = Graphics.FromImage(bg);
                bmp = ImageHelper.GetThumbnail(bmp, qrConfig.QrCodeBig, qrConfig.QrCodeBig);

                g.DrawImage(bmp, new Point(qrConfig.PositionLeft, qrConfig.PositionTop));

                if (qrConfig.IsDisplayUserInformation) {
                    var avatorPath = RuntimeContext.Current.Path.RootPath +
                                     Resolve<IApiService>().ApiUserAvator(user.Id);
                    if (!File.Exists(avatorPath)) {
                        avatorPath = RuntimeContext.Current.Path.WebRootPath + @"\static\images\avator\Man_48.png";
                    }

                    var avatorWidth = 150;
                    var avator = new Bitmap(Image.FromFile(avatorPath), avatorWidth, avatorWidth);
                    var pen = new Pen(Color.Azure, 4);
                    g.DrawArc(pen, (850 - avatorWidth) / 2, 100, avatorWidth, avatorWidth, 0, 360);
                    g.DrawImage(avator, (850 - avatorWidth) / 2, 101);
                    var UserName = " " + user.GetUserName();
                    var font = new Font("微软雅黑", 26, FontStyle.Regular);
                    var length = g.MeasureString(UserName, font);
                    g.DrawString(UserName, font, Brushes.Azure, (850 - length.Width) / 2, 270);
                    //获取等级
                    var grade = Resolve<IGradeService>().GetGrade(user.GradeId);
                    var gradeName = string.Empty;
                    if (grade != null) {
                        gradeName = grade.Name;
                    }

                    // gradeName = "匠心" + gradeName;
                    length = g.MeasureString(gradeName, font);
                    g.DrawString(gradeName, font, Brushes.Azure, (850 - length.Width) / 2, 310);
                }

                var result = new MemoryStream();
                bg.Save(result, ImageFormat.Jpeg);
                bg.Save(FileHelper.RootPath + qrcodePath, ImageFormat.Jpeg);
                result.Flush();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public PagedList<ViewImagePage> GetQrCodeList(PagedInputDto parameter) {
            var users = Resolve<IUserService>()
                .GetListByPageDesc(15, (int)parameter.PageIndex, u => u.ParentId == parameter.UserId);
            var usersCount = Resolve<IUserService>().GetList(u => u.ParentId == parameter.UserId).Count();
            var list = new List<ViewImagePage>();
            users.ToList().ForEach(r => {
                list.Add(new ViewImagePage() {
                    Id = r.Id,
                    Title = r.GetUserName(),
                    ImageUrl = $"/wwwroot/qrcode/{r.Id}.jpeg"
                });
            });
            return PagedList<ViewImagePage>.Create(list, usersCount, 15, parameter.PageIndex);
        }
    }
}