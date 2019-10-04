using System.Collections.Generic;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Framework.Basic.PostRoles.Domain.Entities;
using Alabo.Framework.Basic.PostRoles.Dtos;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.PostRoles.Domain.Services
{
    public interface IPostRoleService : IService<PostRole, ObjectId>
    {
        /// <summary>
        ///     ��ȡȨ�޲˵���
        /// </summary>
        /// <returns></returns>
        List<PlatformRoleTreeOutput> RoleTrees();

        /// <summary>
        ///     ��ȡ��̨����ģ��˵�
        /// </summary>
        /// <returns></returns>
        List<ThemeOneMenu> GetAdminThemeMenus();

        /// <summary>
        ///     ������޸�Ȩ��
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResult Edit(PostRoleInput model);

        /// <summary>
        ///     ��ʼ����λȨ�����ݣ��Լ�Ĭ�ϳ�������Ա
        /// </summary>
        void Init();
    }
}