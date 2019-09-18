using System;
using Xunit;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Tasks.Domain.Services
{
    public class ITaskQueueServiceTests : CoreTest
    {
        // [Fact]
        [TestMethod("GetPageList_Object")]
        public void GetPageList_Object_test()
        {
            object parparameter = null;
            var result = Resolve<ITaskQueueService>().GetPageList(parparameter);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Add_Int64_Guid_DateTime_Object")]
        [TestIgnore]
        public void Add_Int64_Guid_DateTime_Object_test()
        {
            ////var userId = 0;
            ////var moduleId =Guid.Empty ;
            ////DateTime executionTime = null;
            ////Object parameter = null;
            //Service<ITaskQueueService>().Add( userId, moduleId, executionTime, parameter);
        }

        [Fact]
        [TestMethod("Add_Int64_Guid_Object")]
        [TestIgnore]
        public void Add_Int64_Guid_Object_test()
        {
            //var userId = 0;
            //var moduleId = Guid.Empty;
            //Object parameter = null;
            //Service<ITaskQueueService>().Add(userId, moduleId, parameter);
        }

        [Fact]
        [TestMethod("Add_Int64_Guid_TaskQueueType_DateTime_Int32_Object")]
        [TestIgnore]
        public void Add_Int64_Guid_TaskQueueType_DateTime_Int32_Object_test()
        {
            //var userId = 0;
            //var moduleId =Guid.Empty ;
            //var type = (Alabo.App.Core.Tasks.Domain.Entities.TaskQueueType)0;
            //DateTime executionTime = null;
            //var executionTimes = 0;
            //Object parameter = null;
            //Service<ITaskQueueService>().Add( userId, moduleId, type, executionTime, executionTimes, parameter);
        }

        [Fact]
        [TestMethod("Count_Guid")]
        [TestIgnore]
        public void Count_Guid_test()
        {
            //var moduleId = Guid.Empty;
            //var result = Service<ITaskQueueService>().Count(moduleId);
            //Assert.True(result > 0);
        }

        [Fact]
        [TestMethod("Delete_Int64_Guid")]
        public void Delete_Int64_Guid_test()
        {
            var userId = 0;
            var moduleId = Guid.Empty;
            Resolve<ITaskQueueService>().Delete(userId, moduleId);
        }

        [Fact]
        [TestMethod("GetAllTaskModuleAttribute")]
        public void GetAllTaskModuleAttribute_test()
        {
            var result = Resolve<ITaskQueueService>().GetAllTaskModuleAttribute();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAllUnhandledList")]
        [TestIgnore]
        public void GetAllUnhandledList_test()
        {
            //var result = Service<ITaskQueueService>().GetAllUnhandledList();
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetList_Guid_Boolean")]
        public void GetList_Guid_Boolean_test()
        {
            var ModuleId = Guid.Empty;
            var IsHandled = false;
            var result = Resolve<ITaskQueueService>().GetList(ModuleId, IsHandled);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetList_IPredicateQuery1")]
        [TestIgnore]
        public void GetList_IPredicateQuery1_test()
        {
            //var result = Service<ITaskQueueService>().GetList(null);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPagedList_IPageQuery1")]
        [TestIgnore]
        public void GetPagedList_IPageQuery1_test()
        {
            //var result = Service<ITaskQueueService>().GetPagedList(null);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Guid")]
        [TestIgnore]
        public void GetSingle_Guid_test()
        {
            //var moduleId = Guid.Empty;
            //var result = Service<ITaskQueueService>().GetSingle(moduleId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Int32")]
        [TestIgnore]
        public void GetSingle_Int32_test()
        {
            //var id = 0;
            //var result = Service<ITaskQueueService>().GetSingle(id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetTaskModuleAttribute_Guid")]
        [TestIgnore]
        public void GetTaskModuleAttribute_Guid_test()
        {
            //var moduleId = Guid.Empty;
            //var result = Service<ITaskQueueService>().GetTaskModuleAttribute(moduleId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetTaskModuleAttribute")]
        [TestIgnore]
        public void GetTaskModuleAttribute_test()
        {
            //var result = Service<ITaskQueueService>().GetTaskModuleAttribute();
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Handle_Int32")]
        public void Handle_Int32_test()
        {
            var id = 0;
            Resolve<ITaskQueueService>().Handle(id);
        }

        /*end*/
    }
}