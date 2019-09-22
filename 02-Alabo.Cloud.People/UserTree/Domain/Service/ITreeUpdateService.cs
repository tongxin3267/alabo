using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Core.User.ViewModels;
using Alabo.Domains.Entities;
using _01_Alabo.Cloud.Core.UserTree.Domain.UI;

namespace _01_Alabo.Cloud.Core.UserTree.Domain.Service {

    public interface ITreeUpdateService {

        /// <summary>
        /// 更新所有会员的组织架构图
        /// </summary>
        void UpdateAllUserMap();

        /// <summary>
        /// 检查所有的会员关系图是否正确
        /// </summary>

        ServiceResult CheckOutUserMap();

        /// <summary>
        /// 检查所有的会员关系图是否正确
        /// </summary>
        void ParentMapTaskQueue();
    }
}