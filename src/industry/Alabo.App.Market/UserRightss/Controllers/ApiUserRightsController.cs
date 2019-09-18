using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Market.UserRightss.Domain.Dtos;
using Alabo.App.Market.UserRightss.Domain.Entities;
using Alabo.App.Market.UserRightss.Domain.Services;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Models;

using Alabo.RestfulApi;

using Alabo.Helpers;
using System;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.Domains.Services.Bulk;
using Alabo.Core.Extensions;
using Alabo.App.Core.Employes.Domain.Services;

namespace Alabo.App.Market.UserRightss.Controllers {

    [ApiExceptionFilter]
    [Route("Api/UserRights/[action]")]
    public class ApiUserRightsController : ApiBaseController<UserRights, long> {

        public ApiUserRightsController() : base() {
            BaseService = Resolve<IUserRightsService>();
        }

        /// <summary>
        /// ��ͨҳ
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "��ͨҳ")]
        [ApiAuth]
        public ApiResult<UserRightsOutput> OpenPage([FromQuery] long loginUserId, System.Guid gradeId) {
            var user = Ioc.Resolve<IUserService>().GetSingle(r => r.Id == loginUserId);
            var result = Resolve<IUserRightsService>().GetView(loginUserId).FirstOrDefault(x => x.GradeId == gradeId);
            //�����ͨ׼Ӫ�����ĺ�Ӫ������ְ���ǹ���Ա�����Ƽ���ͨ
            // ׼Ӫ�����ģ���Ӫ�����ĵĿ�ֻͨ���ǹ���Ա
            //if (gradeId == Guid.Parse("f2b8d961-3fec-462d-91e8-d381488ea972") || gradeId == Guid.Parse("cc873faa-749b-449b-b85a-c7d26f626feb"))
            //{
            //������ǹ���Ա��ʾ��Ȩ��
            //if (result.OpenType == Domain.Enums.UserRightOpenType.AdminOpenHightGrade && !Resolve<IUserService>().IsAdmin(user.Id))
            //{
            //    return ApiResult.Failure<UserRightsOutput>("����Ȩ��ͨ");
            //}
            //else
            //{
            //    //����Ա�����˿�ͨ
            //    result.OpenType = Domain.Enums.UserRightOpenType.AdminOpenHightGrade;
            //}
            //}

            return ApiResult.Success(result);
        }

        /// <summary>
        ///     ��ȡ�̼�Ȩ��
        /// </summary>
        [HttpGet]
        [Display(Description = "�̼�Ȩ��")]
        [ApiAuth]
        public ApiResult<IList<UserRightsOutput>> GetView([FromQuery] long loginUserId) {
            var result = Resolve<IUserRightsService>().GetView(loginUserId);
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     �̼ҷ��񶩹�
        /// </summary>
        /// <param name="parameter"></param>
        [HttpPost]
        [Display(Description = "�̼ҷ��񶩹�")]
        [ApiAuth]
        public async Task<ApiResult<OrderBuyOutput>> Buy([FromBody] UserRightsOrderInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<OrderBuyOutput>(this.FormInvalidReason(),
                    MessageCodes.ParameterValidationFailure);
            }
            // var usr = Resolve<IUserService>().GetByIdNoTracking(parameter.UserId);

            var result = await Resolve<IUserRightsService>().Buy(parameter);
            if (!result.Item1.Succeeded) {
                return ApiResult.Failure<OrderBuyOutput>(result.Item1.ToString(), MessageCodes.ServiceFailure);
            }
            var user = Resolve<IUserService>().GetSingle(s => s.Mobile == parameter.Mobile);
            //������û��Ƽ���Ϊ�� ��ֱ�Ӱ󶨵�ǰ��½���˻�
            if (user != null && user.ParentId <= 0) {
                user.ParentId = parameter.UserId;
                Resolve<IUserService>().Update(user);
            }
            return ApiResult.Success(result.Item2);
        }

        /// <summary>
        ///     ��ȡ�û���Ϣ
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "��ȡ�û���Ϣ")]
        [ApiAuth]
        public ApiResult<string> GetUserIntro([FromQuery] string mobile) {
            var user = Resolve<IUserService>().GetSingleByUserNameOrMobile(mobile);
            if (user == null) {
                var result = $"�ֻ���Ϊ{mobile}���û������ڣ���ע��Ϊ���û�";
                return ApiResult.Success<string>(result);
            }

            var userGrades = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var buyGrade = userGrades.FirstOrDefault(r => r.Id == user.GradeId);

            if (buyGrade?.Price == 0) {
                var result = $"����:{user.Name}����ǰ����{buyGrade?.Name},�ɼ���";
                return ApiResult.Success<string>(result);
            } else {
                var result = $"����:{user.Name}����ǰ����{buyGrade?.Name},���ɼ���";
                return ApiResult.Success<string>(result);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="modelInput"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiAuth]
        public ApiResult AddPorts([FromBody]AddPortsInput modelInput) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure("������֤��ͨ��");
            }

            if (modelInput.LoginUserId < 1) {
                return ApiResult.Failure("��¼��ԱIdû�д������");
            }

            var loginUser = Ioc.Resolve<IUserService>().GetSingle(r => r.Id == modelInput.LoginUserId);
            if (loginUser == null) {
                return ApiResult.Failure("��ӦID��Ա������");
            }

            var forUser = Ioc.Resolve<IUserService>().GetSingleByUserNameOrMobile(modelInput.Mobile);
            if (forUser == null) {
                //forUser = new Core.User.Domain.Entities.User {
                //    UserName = modelInput.Mobile,
                //    Name = modelInput.Mobile,
                //    Mobile = modelInput.Mobile,
                //    Status = Domains.Enums.Status.Normal,
                //};

                //forUser.Detail = new Core.User.Domain.Entities.UserDetail {
                //    Password = "111111".ToMd5HashString(), //��¼����
                //    PayPassword = "222222".ToMd5HashString() //֧������
                //};

                //var result = Resolve<IUserBaseService>().Reg(forUser, false);
                //if (!result.Result.Succeeded) {
                //    return ApiResult.Failure("�½��û�ʧ��!");
                //}
            }

            var loginUserIsAdmin = Resolve<IUserService>().IsAdmin(modelInput.LoginUserId);
            if (!loginUserIsAdmin) {
                return ApiResult.Failure("��¼�û�����Admin, ����ִ�ж˿�����!");
            }

            var rightsConfigList = Resolve<IUserRightsService>().GetView(modelInput.LoginUserId);
            var forUserExistRights = Resolve<IUserRightsService>().GetList(x => x.UserId == forUser.Id);

            for (int i = 1; i <= 5; i++) {
                var forCount = 0L;
                var forGradeId = "";
                switch (i) {
                    case 1:
                        forCount = modelInput.Grade1;
                        forGradeId = "72be65e6-3000-414d-972e-1a3d4a366001";
                        break;

                    case 2:
                        forCount = modelInput.Grade2;
                        forGradeId = "6f7c8477-4d9a-486b-9fc7-8ce48609edfc";
                        break;

                    case 3:
                        forCount = modelInput.Grade3;
                        forGradeId = "72be65e6-3000-414d-972e-1a3d4a366002";
                        break;

                    case 4:
                        forCount = modelInput.Grade4;
                        forGradeId = "f2b8d961-3fec-462d-91e8-d381488ea972";
                        break;

                    case 5:
                        forCount = modelInput.Grade5;
                        forGradeId = "cc873faa-749b-449b-b85a-c7d26f626feb";
                        break;
                }

                if (forCount < 1) {
                    continue;
                }

                var rightItem = forUserExistRights.FirstOrDefault(x => x.GradeId == Guid.Parse(forGradeId));
                if (rightItem == null) {
                    rightItem = new UserRights {
                        GradeId = rightsConfigList[i - 1].GradeId,
                        TotalCount = forCount,
                        UserId = forUser.Id,
                    };
                    var rs = Resolve<IUserRightsService>().Add(rightItem);
                } else {
                    rightItem.TotalCount += forCount;
                    var rs = Resolve<IUserRightsService>().Update(rightItem);
                }
            }

            return ApiResult.Success("���Ͷ˿ڳɹ�!");
        }

        /// <summary>
        ///     �Ƿ�Ǽ�����
        /// </summary>
        [HttpGet]
        public ApiResult IsRegion(long UserId) {
            var model = Resolve<IUserDetailService>().GetSingle(u => u.UserId == UserId);
            if (model == null) {
                return ApiResult.Failure("δ�ҵ��û�");
            }
            if (model.RegionId <= 0) {
                return ApiResult.Failure("δ�Ǽ�����");
            }
            return ApiResult.Success();
        }

        /// <summary>
        ///     ���ǻ�Ա��������
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult UserRightsAddRegion(UserRightsRegion view) {
            if (view == null) {
                return ApiResult.Failure("��������Ϊ��");
            }

            if (view.RegionId <= 0) {
                return ApiResult.Failure("��ѡ����������");
            }
            var model = Resolve<IUserDetailService>().GetSingle(u => u.UserId == view.UserId);
            if (model == null) {
                return ApiResult.Failure("�û�������");
            }

            model.RegionId = view.RegionId;
            var result = Resolve<IUserDetailService>().Update(model);
            if (!result) {
                return ApiResult.Failure("�޸�ʧ��");
            }
            return ApiResult.Success();
        }
    }
}