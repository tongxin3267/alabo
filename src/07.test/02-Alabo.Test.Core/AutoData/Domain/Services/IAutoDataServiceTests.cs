using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Alabo.Extensions;
using Alabo.Industry.Cms.LightApps.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.AutoData.Domain.Services
{
    public class IAutoDataServiceTests : CoreTest
    {
        string testTableName = "NEG_T1";

        [Fact]
        public void T13_MongoFindTest()
        {
            var ConnectString = "mongodb://zkcloud:Cu6nJEpjxAjgi30lYxDuFu2JqCPqOfuSZj5BFPAM@183.60.143.29:30899/?connectTimeoutMS=30000&authMechanism=SCRAM-SHA-1";
            var url = new MongoUrl(ConnectString);

            var ClientSettings = MongoClientSettings.FromUrl(url);
            ClientSettings.ConnectionMode = ConnectionMode.Direct;
            ClientSettings.ConnectTimeout = new TimeSpan(0, 0, 0, 30, 0); //30秒超时
            ClientSettings.MinConnectionPoolSize = 8; //当链接空闲时,空闲线程池中最大链接数，默认0
            ClientSettings.MaxConnectionPoolSize = 300; //默认100
            ClientSettings.WriteConcern = WriteConcern.Acknowledged;
            var Client = new MongoClient(ClientSettings);
            var rsList = Client.GetDatabase("v12_qnn").GetCollection<dynamic>("NEG_T1").AsQueryable().Count();
            var rsList2 = Client.GetDatabase("v12_qnn").GetCollection<dynamic>("NEG_T1").Find(Builders<dynamic>.Filter.Gt("Age", 12345)).ToList();
            //var rsList3 = Client.GetDatabase("v12_qnn").GetCollection<dynamic>("NEG_T1").Find(Builders<dynamic>.Filter.Eq("Age", "12345")).ToList();

            var filter1 = Builders<dynamic>.Filter.Eq("_id", "5d156dcf0e630724ca1d6354".ToObjectId()) | Builders<dynamic>.Filter.Eq("_id", "5d156e0e0e630724ca1d97c7".ToObjectId());

            var filter2 = Builders<dynamic>.Filter.Eq("_id", "5d156dcf0e630724ca1d6354".ToObjectId());
            filter2 = filter2 | Builders<dynamic>.Filter.Eq("_id", "5d156e0e0e630724ca1d97c7".ToObjectId());

            var rsList7 = Client.GetDatabase("v12_qnn").GetCollection<dynamic>("NEG_T1").Find(filter1).ToList();
            var rsList8 = Client.GetDatabase("v12_qnn").GetCollection<dynamic>("NEG_T1").Find(filter2).ToList();

            var rsList3 = Client.GetDatabase("v12_qnn").GetCollection<dynamic>("NEG_T1")
                .Find(Builders<dynamic>.Filter.Eq("_id", "5d156dcf0e630724ca1d6354".ToObjectId()) | Builders<dynamic>.Filter.Eq("_id", "5d156e0e0e630724ca1d97c7".ToObjectId())).ToList();
            var rsList4 = Client.GetDatabase("v12_qnn").GetCollection<dynamic>("NEG_T1")
                .Find(Builders<dynamic>.Filter.Eq("Age", 12345) & Builders<dynamic>.Filter.Eq("UserId", 1)).ToList();
            var rsList5 = Client.GetDatabase("v12_qnn").GetCollection<dynamic>("NEG_T1")
                .Find(Builders<dynamic>.Filter.Eq("Age", 12345) & Builders<dynamic>.Filter.Eq("UserId", 3)).ToList();
            var rsList6 = Client.GetDatabase("v12_qnn").GetCollection<dynamic>("NEG_T1")
                .Find((Builders<dynamic>.Filter.Eq("Age", 12345) & Builders<dynamic>.Filter.Gte("UserId", 4)) | Builders<dynamic>.Filter.Gte("_id", "5d156dcf0e630724ca1d6354")).ToList();

            //var rsList4 = Client.GetDatabase("v12_qnn").GetCollection<object>("NEG_T1")
            //    .Find(Builders<object>.Filter.Eq("Age", 12345) & Builders<object>.Filter.Eq("UserId", 1)).ToList();
            //var rsList5 = Client.GetDatabase("v12_qnn").GetCollection<object>("NEG_T1")
            //    .Find(Builders<object>.Filter.Eq("Age", 12345) & Builders<object>.Filter.Eq("UserId", 3)).ToList();
            //var rsList6 = Client.GetDatabase("v12_qnn").GetCollection<object>("NEG_T1")
            //    .Find(Builders<object>.Filter.Eq("Age", 12345) & Builders<object>.Filter.Gte("UserId", 4)).ToList();
            //var rsList5 = Client.GetDatabase("v12_qnn").GetCollection<object>("NEG_T1").Find(Builders<object>.Filter.Eq("Age", "12345")).ToList();
        }

        [Fact]
        public void T12()
        {
            var dic = new Dictionary<string, string>
            {
                {"Age", "==12345" },
                {"_id", "==5d1586dd0e630724ca1e0b48" },
                {"UserId", "==2" },
                {"UpdateTime", "==2019-06-28 11:17:55" },
                //{"TableName", "==NEG_T1" },
            };

            var rs = Resolve<ILightAppService>().GetList(testTableName, dic);
        }

        ///// <summary>
        ///// 删除测试默认不执行, 需要再单独执行
        ///// </summary>
        //[Fact]
        //[TestMethod("Delete_String_ObjectId")]
        //public void Delete_String_ObjectId_test()
        //{
        //    ObjectId id = ObjectId.Parse("5d1337430e630724ca16f686");
        //    var modelBeforeDelete = Resolve<IAutoDataService>().GetSingle(testTableName, id);
        //    Assert.NotNull(modelBeforeDelete);

        //    var result = Resolve<IAutoDataService>().Delete(testTableName, id);
        //    var modelAfterDelete = Resolve<IAutoDataService>().GetSingle(testTableName, id);

        //    Assert.Null(modelAfterDelete);
        //}

        /// <summary>
        /// 需要生成测试数据时, 把它标为[Fact]动行一次
        /// </summary>
        [Fact]
        public void GenerateTestData_Insert15Items()
        {
            var count = Resolve<ILightAppService>().Count(testTableName);

            // If TestDataTable count less than 15, insert 15 items.
            if (count < 15)
            {
                for (int n = 0; n < 3; n++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        var model = new
                        {
                            Time = $"Time => {DateTime.Now.ToString("yyyyMMdd HHmmss.ffff")}",
                            Intr = $"Introduce from NEG -> {i}",
                            Age = 12345L + i,
                        };

                        var dataJson = model.ToJsons();
                        var result = Resolve<ILightAppService>().Add(testTableName, dataJson);
                    }
                }
            }
        }

        [Fact]
        [TestMethod("Add_String_String")]
        public void Add_String_String_test()
        {
            var countBefore = Resolve<ILightAppService>().Count(testTableName);

            var i = 0;
            var model = new
            {
                Time = $"Time => {DateTime.Now.ToString("yyyyMMdd HHmmss.ffff")}",
                Intr = $"Introduce from NEG -> {i}",
                Age = 12345L + i,
            };
            var dataJson = model.ToJsons();
            var result = Resolve<ILightAppService>().Add(testTableName, dataJson);
            var countAfter = Resolve<ILightAppService>().Count(testTableName);

            Assert.True(countAfter - countBefore == 1);
        }

        [Fact]
        [TestMethod("Update_String_String_ObjectId")]
        public void Update_String_String_ObjectId_test()
        {
            ObjectId id = ObjectId.Parse("5d1324ba0e630724ca16dd62");
            var modelBeforeUpdate = Resolve<ILightAppService>().GetSingle(testTableName, id);

            var i = 0;
            var model = new
            {
                Time = $"Time => {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}",
                Intr = $"{modelBeforeUpdate.Intr} -===UpdateContent==========> {i}",
                Age = 22345L + i,
            };
            var dataJsong = model.ToJsons();
            var result = Resolve<ILightAppService>().Update(testTableName, dataJsong, id);

            var modelAfterUpdate = Resolve<ILightAppService>().GetSingle(testTableName, id);

            Assert.True(modelBeforeUpdate.Intr != modelAfterUpdate.Intr);
        }

        [Fact]
        [TestMethod("GetSingle_String_ObjectId")]
        public void GetSingle_String_ObjectId_test()
        {
            ObjectId id = ObjectId.Parse("5d1324ba0e630724ca16dd62");
            var result = Resolve<ILightAppService>().GetSingle(testTableName, id);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_String_String_String")]
        public void GetSingle_String_String_String_test()
        {
            var fieldName = "Age";
            var fieldValue = "12345";

            var result = Resolve<ILightAppService>().GetSingle(testTableName, fieldName, fieldValue);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetList_String_String_String")]
        public void GetList_String_String_String_test()
        {
            var fieldName = "Age";
            var fieldValue = "12345";

            var result = Resolve<ILightAppService>().GetList(testTableName, fieldName, fieldValue);
            Assert.True(result.Count > 0);
        }

        [Fact]
        [TestMethod("GetListByUserId_String_Int64")]
        public void GetListByUserId_String_Int64_test()
        {
            var userId = 0;

            var result = Resolve<ILightAppService>().GetListByUserId(testTableName, userId);
            Assert.True(result.Count > 0);
        }

        [Fact]
        [TestMethod("GetListByClassId_String_Int64")]
        public void GetListByClassId_String_Int64_test()
        {
            var classId = 2;

            var result = Resolve<ILightAppService>().GetListByClassId(testTableName, classId);
            Assert.True(result.Count > 0);
        }

        [Fact]
        [TestMethod("GetPagedList_String_Object")]
        public void GetPagedList_String_Object_test()
        {
            var countList = new List<Int32>();

            // 分16页, 跑到最后肯定有0的Count记录
            for (int i = 1; i < 16; i++)
            {
                var query = new
                {
                    PageSize = 4,
                    PageIndex = i,
                };
                var result = Resolve<ILightAppService>().GetPagedList(testTableName, query.ToJsons());
                countList.Add(result.Count);
            }

            Assert.True(countList.Count > 0 && countList.Where(x => x == 0).Count() != countList.Count);
        }
    }
}
