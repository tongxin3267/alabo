using Alabo.Data.People.Employes.Domain.Entities;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;
using System;
using Alabo.Framework.Basic.PostRoles.Dtos;
using GetLoginToken = Alabo.Data.People.Employes.Dtos.GetLoginToken;

namespace Alabo.Data.People.Employes.Domain.Services
{
    public interface IEmployeeService : IService<Employee, ObjectId>
    {
        /// <summary>
        ///     Ա����¼
        /// </summary>
        /// <param name="userOutput"></param>
        /// <returns></returns>
        Tuple<ServiceResult, RoleOuput> Login(UserOutput userOutput);

        /// <summary>
        ///     ��ȡ��¼��Token
        /// </summary>
        /// <returns></returns>
        string GetLoginToken(GetLoginToken loginToken);

        /// <summary>
        ///     ����Token��Ϣ��¼
        /// </summary>
        /// <param name="loginByToken"></param>
        /// <returns></returns>
        LoginInput LoginByToken(GetLoginToken loginByToken);

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        void Init();
    }
}