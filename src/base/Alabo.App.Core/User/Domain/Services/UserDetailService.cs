using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.Common.Domain.CallBacks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.App.Core.User.ViewModels;
using Alabo.Core.Extensions;
using Alabo.Core.Files;
using Alabo.Core.Regex;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Linq.Dynamic;
using Alabo.Mapping;
using Alabo.Maps;
using Alabo.Runtime;
using Alabo.Schedules;
using Alabo.UI;
using File = System.IO.File;

namespace Alabo.App.Core.User.Domain.Services {

    public class UserDetailService : ServiceBase<UserDetail, long>, IUserDetailService {
        private readonly IUserDetailRepository _userDetailRepository;

        public UserDetailService(IUnitOfWork unitOfWork, IRepository<UserDetail, long> repository) : base(unitOfWork,
            repository) {
            _userDetailRepository = Repository<IUserDetailRepository>();
        }

        /// <summary>
        /// </summary>
        /// <param name="userDetail"></param>
        public bool UpdateSingle(UserDetail userDetail) {
            userDetail.ModifiedTime = DateTime.Now;
            var result = Resolve<IUserDetailService>().Update(userDetail);
            if (result) {
                var user = Resolve<IUserService>().GetSingle(userDetail.UserId);
                if (!userDetail.NickName.IsNullOrEmpty()) {
                    user.Name = userDetail.NickName;
                    Resolve<IUserService>().Update(user);
                }

                Resolve<IUserService>().DeleteUserCache(user.Id);
            }

            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="userId">用户Id</param>
        public bool IsServiceCenter(long userId) {
            var userDetail = GetSingle(r => r.UserId == userId);
            if (userDetail != null) {
                return userDetail.IsServiceCenter;
            }

            return false;
        }

        public ServiceResult ConfirmPayPassword(string payPassWord, long loginUserId) {
            var model = Resolve<IUserDetailService>().GetSingle(u => u.UserId == loginUserId);
            if (model == null) {
                return ServiceResult.FailedWithMessage("您访问的用户不存在");
            }

            var passWordHash = payPassWord.ToMd5HashString();

            if (model.PayPassword == passWordHash) {
                return ServiceResult.Success;
            }
            return ServiceResult.FailedWithMessage("密码不正确");
        }

        /// <summary>
        /// </summary>
        /// <param name="passwordInput"></param>
        /// <param name="checkLastPassword"></param>
        public ServiceResult ChangePassword(PasswordInput passwordInput, bool checkLastPassword = true) {
            var userDetail = Resolve<IUserService>().GetUserDetail(passwordInput.UserId);
            if (userDetail == null) {
                return ServiceResult.FailedWithMessage("您访问的用户不存在");
            }

            var result = ServiceResult.Failed;
            if (passwordInput.Password.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("密码不能为空");
            }

            if (passwordInput.Password.Length < 6) {
                return ServiceResult.FailedWithMessage("密码长度不能小于6");
            }

            if (passwordInput.Password != passwordInput.ConfirmPassword) {
                return ServiceResult.FailedWithMessage("确认密码与确认密码不相同");
            }

            // 检查老密码
            if (checkLastPassword) {
                if (passwordInput.Type == PasswordType.LoginPassword) {
                    if (!passwordInput.LastPassword.ToMd5HashString()
                        .Equals(userDetail.Detail.Password, StringComparison.OrdinalIgnoreCase)) {
                        return ServiceResult.FailedWithMessage("原始登录密码不正确");
                    }
                }

                if (passwordInput.Type == PasswordType.PayPassword) {
                    if (!passwordInput.LastPassword.ToMd5HashString().Equals(userDetail.Detail.PayPassword,
                        StringComparison.OrdinalIgnoreCase)) {
                        return ServiceResult.FailedWithMessage("原始支付密码不正确");
                    }
                }
            }

            if (passwordInput.Type == PasswordType.LoginPassword) {
                if (_userDetailRepository.ChangePassword(passwordInput.UserId,
                    passwordInput.Password.ToMd5HashString())) {
                    Resolve<IUserService>().DeleteUserCache(userDetail.Id, userDetail.UserName);
                    return ServiceResult.Success;
                }
            }

            if (passwordInput.Type == PasswordType.PayPassword) {
                if (!RegexHelper.CheckPayPasswrod(passwordInput.Password)) {
                    return ServiceResult.FailedWithMessage("支付密码必须为六位数字");
                }

                if (_userDetailRepository.ChangePayPassword(passwordInput.UserId,
                    passwordInput.Password.ToMd5HashString())) {
                    Resolve<IUserService>().DeleteUserCache(userDetail.Id, userDetail.UserName);
                    return ServiceResult.Success;
                }
            }

            return result;
        }

        /// <summary>
        ///     找回密码
        /// </summary>
        /// <param name="findPassword"></param>
        public ServiceResult FindPassword(FindPasswordInput findPassword) {
            //var user = Resolve<IUserService>().GetUserDetail(findPassword.UserName);
            var user = Resolve<IUserService>().GetSingle(u => u.Mobile == findPassword.Mobile);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户名不存在");
            }

            if (user.Mobile != findPassword.Mobile) {
                return ServiceResult.FailedWithMessage("用户名与手机不匹配");
            }

            if (findPassword.Password.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("密码不能为空");
            }

            if (findPassword.Password.Length < 6) {
                return ServiceResult.FailedWithMessage("密码长度不能小于6");
            }

            if (findPassword.Password != findPassword.ConfirmPassword) {
                return ServiceResult.FailedWithMessage("确认密码与确认密码不相同");
            }

            var userDetail = GetSingle(r => r.UserId == user.Id);
            userDetail.Password = findPassword.Password.ToMd5HashString();
            if (UpdateSingle(userDetail)) {
                return ServiceResult.Success;
            }

            return ServiceResult.FailedWithMessage("服务异常");
        }

        /// <summary>
        ///     找回支付密码
        /// </summary>
        /// <param name="findPassword"></param>
        public ServiceResult FindPayPassword(FindPasswordInput findPassword) {
            //var user = Resolve<IUserService>().GetUserDetail(findPassword.UserName);
            var user = Resolve<IUserService>().GetSingle(u => u.Mobile == findPassword.Mobile);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户名不存在");
            }

            if (user.Mobile != findPassword.Mobile) {
                return ServiceResult.FailedWithMessage("用户名与手机不匹配");
            }

            if (findPassword.Password.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("新支付密码不能为空");
            }

            if (findPassword.Password.Length < 6) {
                return ServiceResult.FailedWithMessage("密码长度不能小于6");
            }

            if (findPassword.Password.Length > 6) {
                return ServiceResult.FailedWithMessage("密码长度不能大于6");
            }

            if (findPassword.Password != findPassword.ConfirmPassword) {
                return ServiceResult.FailedWithMessage("新支付密码与确认支付密码不相同");
            }

            var userDetail = GetSingle(r => r.UserId == user.Id);
            userDetail.PayPassword = findPassword.Password.ToMd5HashString();
            if (UpdateSingle(userDetail)) {
                return ServiceResult.Success;
            }

            return ServiceResult.FailedWithMessage("服务异常");
        }

        /// <summary>
        ///     Changes the mobile.
        ///     修改手机号码
        /// </summary>
        /// <param name="view">The 视图.</param>
        public ServiceResult ChangeMobile(ViewChangMobile view) {
            var user = Resolve<IUserService>().GetSingle(r => r.Mobile == view.Mobile && r.UserName == view.UserName);
            if (user == null) {
                return ServiceResult.FailedWithMessage("手机号码和用户名不匹配");
            }

            var newUser = Resolve<IUserService>().GetSingleByMobile(view.NewMobile);
            if (newUser != null) {
                return ServiceResult.FailedWithMessage("手机号码已被占用");
            }

            var result = Resolve<IUserService>().Update(r => {
                r.Mobile = view.NewMobile;
            }, r => r.Id == user.Id);
            if (result) {
                return ServiceResult.Success;
            }

            return ServiceResult.FailedWithMessage("手机号码修改失败");
        }

        /// <summary>
        /// 生成二维码任务
        /// </summary>
        public void CreateCodeTask() {
            var backJobParameter = new BackJobParameter {
                ModuleId = TaskQueueModuleId.UserQrcodeCreate,
                CheckLastOne = true,
                ServiceName = typeof(IUserDetailService).Name,
                Method = "CreateAllUserQrCode"
            };
            Resolve<ITaskQueueService>().AddBackJob(backJobParameter);
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
        public void CreateCode(Entities.User user) {
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
            var url = $@"{webSite.DomainName}/user/reg?usercode={user.UserName}";
            if (!url.Contains("http://")) {
                url = $"http://{url}";
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
                    var grade = Resolve<IUserService>().GetSingle(user.Id).UserGradeConfig;
                    var gradeName = string.Empty;
                    if (grade != null) {
                        gradeName = grade.Name;
                    }

                    if (Resolve<IUserDetailService>().IsServiceCenter(user.Id)) {
                        gradeName = gradeName + "（门店）";
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

        /// <summary>
        ///     获取s the 会员 output.
        ///     前端会员输出模型
        /// </summary>
        /// <param name="userId">会员Id</param>
        public UserOutput GetUserOutput(long userId) {
            //var users = Resolve<IUserService>().GetList();
            var user = Resolve<IUserService>().GetSingle(u => u.Id == userId);
            if (user == null) {
                throw new ValidException("用户不存在");
            }

            var grade = Resolve<IGradeService>().GetGrade(user.GradeId);
            if (grade == null) {
                throw new ValidException("用户等级不存在");
            }

            var detail = Resolve<IUserDetailService>().GetSingle(u => u.UserId == userId);
            if (detail == null) {
                throw new ValidException("用户详细信息不存在");
            }
            user.Detail = detail;
            var userOutput = user.Detail.MapTo<UserOutput>();

            userOutput.GradeName = Resolve<IGradeService>().GetGrade(user.GradeId)?.Name;
            userOutput.Status = user.Status.GetDisplayName();
            userOutput.Avator = Resolve<IApiService>().ApiUserAvator(user.Id);
            if (!user.Detail.Avator.IsNullOrEmpty()) {
                userOutput.Avator = Resolve<IApiService>().ApiImageUrl(user.Detail.Avator);
            }

            userOutput.GradeIcon = Resolve<IApiService>().ApiImageUrl(grade?.Icon);
            userOutput.Sex = user.Detail.Sex.GetDisplayName();

            userOutput.RegionName = Resolve<IRegionService>().GetFullName(user.Detail.RegionId);
            var userConfig = Resolve<IAutoConfigService>().GetValue<UserConfig>();
            userOutput.ExpireTime = DateTime.Now.AddMinutes(userConfig.LoginExpirationTime).ConvertDateTimeInt();

            userOutput.GradeName = grade?.Name;
            userOutput.Status = user.Status.GetDisplayName();

            userOutput.Sex = user.Detail.Sex.GetDisplayName();

            userOutput.UserName = user.UserName;
            userOutput.Email = user.Email;
            userOutput.Name = user.Name;
            userOutput.ParentId = user.ParentId;
            userOutput.GradeId = user.GradeId;
            userOutput.Id = user.Id;
            userOutput.Mobile = user.Mobile;
            userOutput.ParentUserName = Resolve<IUserService>().GetSingle(u => u.Id == user.ParentId)?.UserName;
            userOutput.IdentityStatusName = userOutput.IdentityStatus.GetDisplayName();
            userOutput.OpenId = user.Detail.OpenId;

            var serviceUser = Resolve<IUserService>().GetSingle(user.Detail.ServiceCenterUserId);
            if (serviceUser != null) {
                userOutput.ServiceCenterName = serviceUser.GetUserName();
            }

            var parentUser = Resolve<IUserService>().GetSingle(user.ParentId);
            if (parentUser != null) {
                userOutput.ParentUserName = parentUser.GetUserName();
            }

            if (detail.PayPassword.IsNullOrEmpty()) {
                userOutput.IsNeedSetPayPassword = true;
            }
            userOutput.IsAdmin = Resolve<IUserService>().IsAdmin(user.Id);
            userOutput.Store = EntityDynamicService.GetStore(user.Id);
            userOutput.Token = Resolve<IUserService>().GetUserToken(user);

            return userOutput;
        }

        /// <summary>
        ///     更新s the open identifier.
        ///     更新用户的OpenId
        /// </summary>
        /// <param name="openId">The open identifier.</param>
        /// <param name="userId">会员Id</param>
        public void UpdateOpenId(string openId, long userId) {
            if (!openId.IsNullOrEmpty()) {
                var userDetail = GetSingle(r => r.UserId == userId);
                if (userDetail != null) {
                    userDetail.OpenId = openId;
                    Update(userDetail);
                    Resolve<IUserService>().DeleteUserCache(userId);
                }
            }
        }

        /// <summary>
        ///     获取当前服务中心下的所有会员Id
        /// </summary>
        /// <param name="userId">用户Id</param>
        public IList<long> GetAllServiceCenterUserIds(long userId) {
            var list = Repository<IUserDetailRepository>().GetAllServiceCenterUserIds(userId);
            return list;
        }

        /// <summary>
        /// 验证支付密码
        /// </summary>
        /// <param name="payPassword"></param>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        public bool CheckPayPassword(string payPassword, long loginUserId) {
            if (payPassword.IsNullOrEmpty()) {
                return false;
            }

            if (payPassword.ToMd5HashString() == Ioc.Resolve<IUserDetailService>().GetSingle(x => x.UserId == loginUserId)?.PayPassword) {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取角色
        /// 参数中带有ref ,返回执行结果即可
        /// </summary>
        /// <param name="userOutput"></param>
        /// <param name="filterType"></param>
        /// <returns></returns>
        public ServiceResult GetUserRoles(ref UserOutput userOutput, FilterType filterType) {
            return null;
            //userOutput.Roles = new UserOutputRoles();
            //// 后台管理员判断
            //if (filterType == FilterType.Admin)
            //{
            //    var isAdmin = Resolve<IUserService>().IsAdmin(userOutput.Id);
            //    if (isAdmin == false)
            //    {
            //        return ServiceResult.FailedWithMessage("非管理员不能登录");
            //    }
            //    userOutput.Roles.AdminMenus = Resolve<IEmployeeService>().GetAdminMenus(userOutput.Id);
            //    if (userOutput.Roles.AdminMenus == null)
            //    {
            //        return ServiceResult.FailedWithMessage("非管理员不能登录");
            //    }
            //    userOutput.Roles.FilterType = FilterType.Admin;
            //    return ServiceResult.Success;

            //}
            //else if (filterType == FilterType.Store)// 供应商
            //{
            //    var store = EntityDynamicService.GetStore(userOutput.Id);
            //    if (store == null) return ServiceResult.FailedWithMessage("你的账号并非供应商,请联系管客服或者管理员");
            //    userOutput.Roles.FilterType = FilterType.Store;
            //    return ServiceResult.Success;
            //}
            //else if (filterType == FilterType.City)//城市运营中心
            //{
            //    var city = EntityDynamicService.GetCity(userOutput.Id);
            //    if (city == null) return ServiceResult.FailedWithMessage("你的账号并非城市运营中心,请联系管客服或者管理员");
            //    userOutput.Roles.FilterType = FilterType.City;

            //    return ServiceResult.Success;
            //}
            //else if (filterType == FilterType.Market)//营销中心
            //{
            //    //通过会员等级来判断
            //    //var userGrades = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            //    //var userGradeId = userOutput.GradeId;
            //    //营销中心的id
            //    if (userOutput.GradeId != Guid.Parse("cc873faa-749b-449b-b85a-c7d26f626feb"))
            //        return ServiceResult.FailedWithMessage("你的账号并非营销中心,请联系管客服或者管理员");

            //    userOutput.Roles.FilterType = FilterType.Market;

            //    return ServiceResult.Success;
            //}
            //else
            //{
            //    userOutput.Roles.FilterType = FilterType.User;

            //    return ServiceResult.Success;
            //}
        }
    }
}