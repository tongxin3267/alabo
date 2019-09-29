using Alabo.Data.People.Employes.Domain.Entities;
using Alabo.Data.People.Employes.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Framework.Themes.Domain.Entities;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Alabo.Data.People.Employes.Domain.Services
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