using MongoDB.Bson;
using System.Collections.Generic;
using Alabo.App.Core.Employes.Domain.Dtos;
using Alabo.App.Core.Employes.Domain.Entities;
using Alabo.App.Core.Themes.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Employes.Domain.Services {

    public interface IPostRoleService : IService<PostRole, ObjectId> {

        /// <summary>
        /// ��ȡȨ�޲˵���
        /// </summary>
        /// <returns></returns>
        List<PlatformRoleTreeOutput> RoleTrees();

        /// <summary>
        /// ��ȡ��̨����ģ��˵�
        /// </summary>
        /// <returns></returns>
        List<ThemeOneMenu> GetAdminThemeMenus();

        /// <summary>
        /// ������޸�Ȩ��
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ServiceResult Edit(PostRoleInput model);

        /// <summary>
        /// ��ʼ����λȨ�����ݣ��Լ�Ĭ�ϳ�������Ա
        /// </summary>
        void Init();
    }
}