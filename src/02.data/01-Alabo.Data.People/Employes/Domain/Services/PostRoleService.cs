using Alabo.Data.People.Employes.Domain.Entities;
using Alabo.Data.People.Employes.Dtos;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Themes.Domain.Entities;
using Alabo.Framework.Themes.Domain.Enums;
using Alabo.Framework.Themes.Domain.Services;
using Alabo.Framework.Themes.Dtos;
using Alabo.Maps;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.Data.People.Employes.Domain.Services
{
    public class PostRoleService : ServiceBase<PostRole, ObjectId>, IPostRoleService
    {
        public PostRoleService(IUnitOfWork unitOfWork, IRepository<PostRole, ObjectId> repository) : base(unitOfWork,
            repository)
        {
        }

        /// <summary>
        ///     ��ȡ��̨����ģ��˵�
        /// </summary>
        /// <returns></returns>
        public List<ThemeOneMenu> GetAdminThemeMenus()
        {
            var clientPageInput = new ClientPageInput
            {
                ClientType = ClientType.PcWeb,
                Type = ThemeType.Admin
            };
            var defaultAdminTemplage = Resolve<IThemeService>().GetDefaultTheme(clientPageInput);
            var menus = defaultAdminTemplage?.Menu?.Menus;
            return menus;
        }

        /// <summary>
        ///     ��ȡȨ�޲˵���
        /// </summary>
        /// <returns></returns>
        public List<PlatformRoleTreeOutput> RoleTrees()
        {
            var adminThemeMenus = GetAdminThemeMenus();
            // ���еĸ�λ��������ҳȨ�ޣ������ܷ���
            adminThemeMenus = adminThemeMenus
                .Where(r => !r.Url.Contains("admin/index", StringComparison.OrdinalIgnoreCase)).ToList();

            var result = adminThemeMenus?.Select(s =>
            {
                var res = new PlatformRoleTreeOutput
                {
                    Id = s.Id,
                    IsEnable = s.IsEnable,
                    Name = s.Name,
                    Url = s.Url,
                    AppItems = s.Menus?.Select(ss =>
                    {
                        var res1 = new PlatformRoleTreeOutput
                        {
                            Id = ss.Id,
                            IsEnable = ss.IsEnable,
                            Name = ss.Name,
                            Url = ss.Url,
                            AppItems = ss.Menus?.Select(sss =>
                            {
                                var res2 = new PlatformRoleTreeOutput
                                {
                                    Id = sss.Id,
                                    IsEnable = sss.IsEnable,
                                    Name = sss.Name,
                                    Url = sss.Url,
                                    AppItems = null
                                };
                                return res2;
                            }).ToList()
                        };
                        return res1;
                    }).ToList()
                };

                return res;
            }).ToList();

            return result;
        }

        /// <summary>
        ///     Ȩ��
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ServiceResult Edit(PostRoleInput model)
        {
            var postRole = model.MapTo<PostRole>();
            if (postRole.RoleIds == null) postRole.RoleIds = new List<ObjectId>();

            // �����ҳȨ��Id
            var adminThemeMenus = GetAdminThemeMenus();
            var indexMenu =
                adminThemeMenus.FirstOrDefault(r => r.Url.Contains("admin/index", StringComparison.OrdinalIgnoreCase));
            if (indexMenu != null) postRole.RoleIds.Add(indexMenu.Id);
            // ��Ϣҳ��
            indexMenu = adminThemeMenus.FirstOrDefault(r =>
                r.Url.Contains("admin/message", StringComparison.OrdinalIgnoreCase));
            if (indexMenu != null) postRole.RoleIds.Add(indexMenu.Id);

            if (model.RoleIds == null) model.RoleIds = new List<string>();
            foreach (var item in model.RoleIds)
            {
                var roleId = item.ToObjectId();
                if (roleId != ObjectId.Empty) postRole.RoleIds.Add(roleId);
            }

            postRole.RoleIds = postRole.RoleIds;

            if (!AddOrUpdate(postRole, s => s.Id == postRole.Id)) return ServiceResult.FailedMessage("����ʧ��");
            return ServiceResult.Success;
        }

        #region ��ʼ����λȨ��

        /// <summary>
        ///     ��ʼ����λȨ��
        /// </summary>
        public void Init()
        {
            #region ��Ӹ�λȨ��

            if (!Exists())
            {
                var list = new List<PostRole>();
                var postRole = new PostRole
                {
                    Id = ObjectId.Parse("a008029a8600000752803001"),
                    Name = "��������Ա",
                    Summary = "�߱��������й����Ȩ��"
                };
                list.Add(postRole);

                postRole = new PostRole
                {
                    Id = ObjectId.Parse("a008029a8600000752803002"),
                    Name = "�ͷ�",
                    Summary = "�߱����������߿ͷ��������鿴��Ʒ�б�ͷ��顱�����鿴�������޸Ķ�����ע���ӳ�����������ȡ���������ĵ��̹���Ȩ�ޡ�"
                };
                list.Add(postRole);

                postRole = new PostRole
                {
                    Id = ObjectId.Parse("a008029a8600000752803003"),
                    Name = "����",
                    Summary = "�߱����ʲ���������ع��ܵĹ���Ȩ��"
                };
                list.Add(postRole);

                postRole = new PostRole
                {
                    Id = ObjectId.Parse("a008029a8600000752803004"),
                    Name = "��Ӫ",
                    Summary = "�߱����ͻ�����,��Ӫ������, ��������ѯ��,���ŵꡱ, ����Ʒ������ع��ܵĹ���Ȩ�� "
                };
                list.Add(postRole);

                postRole = new PostRole
                {
                    Id = ObjectId.Parse("a008029a8600000752803005"),
                    Name = "��ͨ����Ա",
                    Summary = "�߱��������ɾ��Ա�����޸Ľ�ɫ��ɾ�����ݡ��ʲ�������֮�⣬�������е��̹���Ȩ��"
                };
                list.Add(postRole);

                postRole = new PostRole
                {
                    Id = ObjectId.Parse("a008029a8600000752803006"),
                    Name = "�ֹ�",
                    Summary = "�߱���Ʒ����,�����������ع��ܵĹ���Ȩ��"
                };
                list.Add(postRole);

                AddMany(list);
            }

            #endregion ��Ӹ�λȨ��

            if (!Resolve<IEmployeeService>().Exists())
            {
                var user = Resolve<IUserService>().GetSingle("admin");
                if (user != null)
                {
                    var employee = new Employee
                    {
                        PostRoleId = ObjectId.Parse("a008029a8600000752803001"),
                        Name = "��������Ա",
                        Intro = "ϵͳĬ�ϳ�������Ա",
                        IsEnable = true,
                        UserId = user.Id,
                        IsSuperAdmin = true
                    };
                    Resolve<IEmployeeService>().Add(employee);
                }
            }
        }

        #endregion ��ʼ����λȨ��
    }
}