using Alabo.Extensions;
using Alabo.Framework.Basic.Address.Domain.Entities;
using Alabo.Framework.Basic.Address.Domain.Services;
using Alabo.Framework.Basic.Address.Dtos;
using Alabo.Framework.Basic.Regions.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Regexs;
using Alabo.UI.Design.AutoForms;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Data.People.Users.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/UserAddress/[action]")]
    public class ApiUserAddressController : ApiBaseController<UserAddress, ObjectId>
    {
        public ApiUserAddressController()
        {
            BaseService = Resolve<IUserAddressService>();
        }

        /// <summary>
        ///     Gets the reg form.
        /// </summary>
        [HttpGet]
        [Display(Description = "��ȡ��ӵ�ַ��ͼ")]
        public ApiResult<AutoForm> GetAddAddressForm()
        {
            var view = AutoFormMapping.Convert<UserAddress>();
            return null;
        }

        /// <summary>
        ///     ���¡���ӡ������ջ���ַ
        /// </summary>
        [HttpPost]
        [Display(Description = "���¡���ӡ������ջ���ַ")]
        [ApiAuth]
        public ApiResult SaveOrderAddress([FromBody] AddressInput parameter)
        {
            if (parameter == null) return ApiResult.Failure("���ڵ�������Ϊ��");

            var regex = RegexHelper.ChinaMobile.IsMatch(parameter.Mobile);
            if (!regex) return ApiResult.Failure("��ϵ�绰��ʽ����ȷ");
            if (parameter.Mobile.IsNullOrEmpty()) return ApiResult.Failure("��ϵ�绰����Ϊ��");

            if (parameter.Address.IsNullOrEmpty()) return ApiResult.Failure("��ϸ��ַ����Ϊ��");

            if (parameter.Name.IsNullOrEmpty()) return ApiResult.Failure("�ջ�����������Ϊ��");

            if (!this.IsFormValid())
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);

            var serviceResult = Resolve<IUserAddressService>().SaveOrderAddress(parameter);
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     ���¡���ӡ������Ա������ַ(��Ա���ĵ�ַ)
        /// </summary>
        [HttpPost]
        [Display(Description = "������ַ�޸�")]
        [ApiAuth]
        public ApiResult SaveUserInfoAddress([FromBody] UserInfoAddressInput parameter)
        {
            if (!this.IsFormValid())
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);

            parameter.Type = AddressLockType.UserInfoAddress;
            var serviceResult = Resolve<IUserAddressService>().SaveUserInfoAddress(parameter);
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     ��ȡ�û����е�ַ,����������ַ���ջ���ַ��������ַ
        /// </summary>
        [HttpGet]
        [Display(Description = "��ȡ�û���ַ����")]
        [ApiAuth]
        public ApiResult<List<UserAddress>> Get([FromQuery] long loginUserId)
        {
            var result = Resolve<IUserAddressService>().GetAllList(loginUserId)
                .Where(x => x.Type != AddressLockType.CustomeShopOrderAddress).ToList(); // �Զ����̳��Զ��µ��ĵ�ַ�ų�������ʾ
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     ɾ���û���ַ
        /// </summary>
        [HttpDelete]
        [Display(Description = "ɾ���û���ַ")]
        [ApiAuth]
        public ApiResult Delete([FromQuery] long loginUserId, string id)
        {
            if (!this.IsFormValid())
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            var serviceResult = Resolve<IUserAddressService>().Delete(loginUserId, id.ToObjectId());
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     ����Ĭ�ϵ�ַ
        /// </summary>
        [HttpPost]
        [Display(Description = "����Ĭ�ϵ�ַ")]
        [ApiAuth]
        public ApiResult SetDefault([FromBody] AddressDefaultInput parameter)
        {
            if (!this.IsFormValid())
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);

            var serviceResult = Resolve<IUserAddressService>().SetDefault(parameter.UserId, parameter.Id.ToObjectId());
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     IdֵΪ�ջ�ȡĬ�ϵ�ַ  ���û��Ĭ�ϵ�ַ�򷵻ؿ�   Idֵ�������ȡ��Id��ͬ�ĵ�ַ
        /// </summary>
        /// <param name="id">Id��ʶ</param>
        /// <param name="loginUserId">��¼��ԱId</param>
        [ApiAuth]
        [HttpGet]
        [Display(Description = "idֵΪ�ջ�ȡĬ�ϵ�ַ ���û��Ĭ�ϵ�ַ�򷵻�ֵΪ�� id ֵ�������ȡ��id��ͬ�ĵ�ַ")]
        public ApiResult Single([FromQuery] string id, long loginUserId)
        {
            var result = Resolve<IUserAddressService>().GetUserAddress(id.ToObjectId(), loginUserId);
            if (result != null)
            {
                var regionName = Resolve<IRegionService>().GetSingle(u => u.RegionId == result.RegionId);
                result.AddressDescription = regionName.Name;
                return ApiResult.Success(result);
            }

            return ApiResult.Failure("û������");
        }
    }
}