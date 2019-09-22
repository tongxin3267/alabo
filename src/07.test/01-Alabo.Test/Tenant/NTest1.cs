using Microsoft.CSharp.RuntimeBinder;
using MongoDB.Bson;

//using Alabo.Extensions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xunit;
using Alabo.App.Core.LightApps.Domain.Services;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Runtime;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Tenant {

    /// <summary>
    /// NEG Tests
    /// </summary>
    public class NTest1 : CoreTest {

        [Fact]
        public void T8_Insert5s() {
            var tableName = "NEG_T1";

            for (int i = 0; i < 5; i++) {
                var model = new {
                    Time = $"Time => {DateTime.Now.ToString("yyyyMMdd HHmmss.ffff")}",
                    Intr = $"Introduce from NEG -> {i}",
                    Age = 12345L + i,
                };

                var dataJson = model.ToJsons();
                var result = Resolve<ILightAppService>().Add(tableName, dataJson);
            }
        }

        [Fact]
        public void T7_Delete() {
            var tableName = "NEG_T1";
            var id = ObjectId.Parse("5d11ea150e630724ca15a715");
            var rs = Resolve<ILightAppService>().Delete(tableName, id);
            Assert.True(rs.Succeeded);
        }

        [Fact]
        public void T6_AutoDataService_Add() {
            var tableName = "NEG_T1";

            var i = 0;
            var model = new {
                Time = $"Time => {DateTime.Now.ToString("yyyyMMdd HHmmss.ffff")}",
                Intr = $"Introduce from NEG -> {i}",
                Age = 12345L + i,
            };

            var dataJson = model.ToJsons();
            var result = Resolve<ILightAppService>().Add(tableName, dataJson);

            Assert.NotNull(result);
        }

        [Fact]
        public void T5_DynamicQueryFromMongo() {
            var ConnectString = RuntimeContext.Current.WebsiteConfig.MongoDbConnection.ConnectionString;
            var url = new MongoUrl(ConnectString);

            var ClientSettings = MongoClientSettings.FromUrl(url);
            ClientSettings.ConnectionMode = ConnectionMode.Direct;
            ClientSettings.ConnectTimeout = new TimeSpan(0, 0, 0, 30, 0); //30秒超时
            ClientSettings.MinConnectionPoolSize = 8; //当链接空闲时,空闲线程池中最大链接数，默认0
            ClientSettings.MaxConnectionPoolSize = 300; //默认100
            ClientSettings.WriteConcern = WriteConcern.Acknowledged;
            var Client = new MongoClient(ClientSettings);
            var rsList = Client.GetDatabase("v12_zy_z000").GetCollection<dynamic>("ABC_O1").AsQueryable();

            var rsWhere = WhereQuery(rsList, "Age", "12345").ToList();
        }

        [Fact]
        public void T4() {
            var i = 0;
            dynamic model = new {
                Time = $"Time => {DateTime.Now.ToString("yyyyMMdd HHmmss.ffff")}",
                Intr = $"Introduce from NEG -> {i}",
                Age = 12345L + i,
            };

            var pis = model.GetType().GetProperties();
        }

        [Fact]
        public void T3_JsonToDynamicOjbect_MongoInsert() {
            var ConnectString = RuntimeContext.Current.WebsiteConfig.MongoDbConnection.ConnectionString;
            var url = new MongoUrl(ConnectString);

            var ClientSettings = MongoClientSettings.FromUrl(url);
            ClientSettings.ConnectionMode = ConnectionMode.Direct;
            ClientSettings.ConnectTimeout = new TimeSpan(0, 0, 0, 30, 0); //30秒超时
            ClientSettings.MinConnectionPoolSize = 8; //当链接空闲时,空闲线程池中最大链接数，默认0
            ClientSettings.MaxConnectionPoolSize = 300; //默认100
            ClientSettings.WriteConcern = WriteConcern.Acknowledged;
            var Client = new MongoClient(ClientSettings);
            var rsList = Client.GetDatabase("v12_zy_z000").GetCollection<dynamic>("ABC_O1").AsQueryable();

            //foreach (var item in rsList)
            //{
            //    Type type = item.GetType();
            //    var rr = GetProperty(item, "Age");
            //    //var vv = GPP(item, "Age");
            //    Trace.WriteLine($"Age = {item.Age} => {JsonConvert.SerializeObject(item)}");
            //}

            //var rsWhere = rsList.Filter("Age", "12345");

            var rsWhere = WhereQuery(rsList, "Age", "12345").ToList();

            var parasList = new List<object>();
            //var rsListT1 = Client.GetDatabase("v12_zy_z000").GetCollection<dynamic>("ABC_O1").AsQueryable()
            //    .Where(x => x.Age == 12345)
            //    .ToList();
            //var ss = Client.GetDatabase("v12_zy_z000").GetCollection<dynamic>("ABC_O1").AsQueryable()
            //    .Where("Age = ");

            //for (int i = 0; i < 5; i++)
            //{
            //var model = new
            //{
            //    Time = $"Time => {DateTime.Now.ToString("yyyyMMdd HHmmss.ffff")}",
            //    Intr = $"Introduce from NEG -> {i}",
            //    Age = 12345L + i,
            //};

            //    // json
            //    var json = model.ToJsons();

            //    // dynamic converter
            //    var convert = new ExpandoObjectConverter();

            //    // dynamic object
            //    var obj = JsonConvert.DeserializeObject<ExpandoObject>(json, convert);

            //    Client.GetDatabase("v12_zy_z000").GetCollection<dynamic>("ABC_O1").InsertOne(obj);
            //}
        }

        public static object GPV(dynamic o, string member) {
            if (o == null) {
                throw new ArgumentNullException("o");
            }

            if (member == null) {
                throw new ArgumentNullException("member");
            }

            Type scope = o.GetType();
            IDynamicMetaObjectProvider provider = o as IDynamicMetaObjectProvider;
            if (provider != null) {
                ParameterExpression param = Expression.Parameter(typeof(object));
                DynamicMetaObject mobj = provider.GetMetaObject(param);
                GetMemberBinder binder = (GetMemberBinder)Microsoft.CSharp.RuntimeBinder.Binder.GetMember(0, member, scope, new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(0, null) });
                DynamicMetaObject ret = mobj.BindGetMember(binder);
                BlockExpression final = Expression.Block(
                    Expression.Label(CallSiteBinder.UpdateLabel),
                    ret.Expression
                );
                LambdaExpression lambda = Expression.Lambda(final, param);
                Delegate del = lambda.Compile();

                return del.DynamicInvoke(o);
            } else {
                return o.GetType().GetProperty(member, BindingFlags.Public | BindingFlags.Instance).GetValue(o, null);
            }
        }

        public static object GetPropertyValue(object o, string member) {
            if (o == null) {
                throw new ArgumentNullException("o");
            }

            if (member == null) {
                throw new ArgumentNullException("member");
            }

            Type scope = o.GetType();
            IDynamicMetaObjectProvider provider = o as IDynamicMetaObjectProvider;
            if (provider != null) {
                ParameterExpression param = Expression.Parameter(typeof(object));
                DynamicMetaObject mobj = provider.GetMetaObject(param);
                GetMemberBinder binder = (GetMemberBinder)Microsoft.CSharp.RuntimeBinder.Binder.GetMember(0, member, scope, new CSharpArgumentInfo[] { CSharpArgumentInfo.Create(0, null) });
                DynamicMetaObject ret = mobj.BindGetMember(binder);
                BlockExpression final = Expression.Block(
                    Expression.Label(CallSiteBinder.UpdateLabel),
                    ret.Expression
                );
                LambdaExpression lambda = Expression.Lambda(final, param);
                Delegate del = lambda.Compile();

                var rs = del.DynamicInvoke(o);
                return del.DynamicInvoke(o);
            } else {
                return o.GetType().GetProperty(member, BindingFlags.Public | BindingFlags.Instance).GetValue(o, null);
            }
        }

        public static IEnumerable<object> WhereQuery(IEnumerable<object> source, string fieldName, string fieldValue) {
            foreach (var item in source) {
                if (GetPropertyValue(item, fieldName).ToString() == fieldValue) {
                    yield return item;
                }
            }
        }

        [Fact]
        public void T2() {
            var sql = $@"SELECT Id FROM ZKShop_Product";

            var rs1 = Ioc.Resolve<IUserRepository>().RepositoryContext.Fetch<long>(sql);
        }

        [Fact]
        public void T1() {
            var count = 10L;

            //var sql = $@"SELECT TOP {count} u.Id, u.Name, u.Mobile, d.Sex, d.Birthday, d.Password FROM User_User u JOIN User_UserDetail d ON u.Id = d.UserId";
            var sql = $@"SELECT TOP {count} u.Id, u.Name, u.Mobile, d.Sex, d.Birthday, d.Password, d.PayPassword, d.LastLoginIp FROM User_User u JOIN User_UserDetail d ON u.Id = d.UserId";
            var rs1 = Ioc.Resolve<IUserRepository>().RepositoryContext.Query<InsTest>(sql);
            var rs2 = Ioc.Resolve<IUserRepository>().RepositoryContext.Fetch<InsTest>(sql);
        }
    }

    public static class Extensions {

        public static List<T> Filter<T>(this List<T> source, string columnName, string compValue) {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            Expression property = Expression.Property(parameter, columnName);
            Expression constant = Expression.Constant(compValue);
            Expression equality = Expression.Equal(property, constant);
            Expression<Func<T, bool>> predicate =
                Expression.Lambda<Func<T, bool>>(equality, parameter);

            Func<T, bool> compiled = predicate.Compile();
            return source.Where(compiled).ToList();
        }
    }

    public class InsTest {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public SexTest Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string Password { get; set; }

        public string Nation { get; set; }
        public string City { get; set; }
        public long Age { get; set; }
    }

    public enum SexTest {
        Male = 1,
        Female = 2,
    }
}