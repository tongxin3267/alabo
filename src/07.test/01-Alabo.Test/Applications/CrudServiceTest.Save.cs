﻿using System;using ZKCloud.Domains.Repositories.EFCore;using ZKCloud.Domains.Repositories.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using NSubstitute;
using Xunit;
using ZKCloud.Dependency;
using ZKCloud.Exceptions;
using ZKCloud.Extensions;
using ZKCloud.Helpers;
using ZKCloud.Test.Samples;
using ZKCloud.Test.XUnitHelpers;

namespace ZKCloud.Test.Applications {
    /// <summary>
    /// 增删改查服务测试
    /// </summary>
    public partial class CrudServiceTest {
        /// <summary>
        /// Id
        /// </summary>
        private Guid _id;
        /// <summary>
        /// Id2
        /// </summary>
        private Guid _id2;
        /// <summary>
        /// 实体
        /// </summary>
        private EntitySample _entity;
        /// <summary>
        /// 实体2
        /// </summary>
        private EntitySample _entity2;
        /// <summary>
        /// 数据传输对象
        /// </summary>
        private DtoSample _dto;
        /// <summary>
        /// 数据传输对象2
        /// </summary>
        private DtoSample _dto2;
        /// <summary>
        /// 增删改查服务
        /// </summary>
        private CrudServiceSample _service;
        /// <summary>
        /// 工作单元
        /// </summary>
        private ZKCloud.Datas.UnitOfWorks.IUnitOfWork _unitOfWork;
        /// <summary>
        /// 仓储
        /// </summary>
        private IRepositorySample _repository;

        /// <summary>
        /// 测试初始化
        /// </summary>
        public CrudServiceTest() {
            _id = Guid.NewGuid();
            _id2 = Guid.NewGuid();
            _entity = new EntitySample( _id ) { Name = "A" };
            _entity2 = new EntitySample( _id2 ) { Name = "B" };
            _dto = new DtoSample{Id = _id.ToString(),Name = "A"};
            _dto2 = new DtoSample { Id = _id2.ToString(), Name = "B" };
            _unitOfWork = Substitute.For<ZKCloud.Datas.UnitOfWorks.IUnitOfWork>();
            _repository = Substitute.For<IRepositorySample>();
            _service = new CrudServiceSample( _unitOfWork,_repository );
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        private List<EntitySample> GetEntities() {
            return new List<EntitySample> { _entity, _entity2 };
        }

        /// <summary>
        /// 通过AOP进行DTO验证
        /// </summary>
        [Fact]
        public void TestSave_ValidateDto() {
            var container = Ioc.CreateContainer( new IocConfig() );
            var service = container.Create<ICrudServiceSample>();

            //有效
            service.Save( _dto );

            //无效
            AssertHelper.Throws<Warning>( () => {
                service.Save( new DtoSample() );
            }, "名称不能为空" );
        }

        /// <summary>
        /// 测试添加
        /// </summary>
        [Fact]
        public void TestSave_Add() {
            _service.Save( new DtoSample{Name = "a"} );
            _repository.Received().Add( Arg.Is<EntitySample>( t => t.Name == "a" ) );
            _repository.DidNotReceive().Update( Arg.Any<EntitySample>() );
        }

        /// <summary>
        /// 测试修改
        /// </summary>
        [Fact]
        public void TestSave_Update() {
            _repository.Find( _id ).Returns( t => new EntitySample( _id ) );
            _service.Save( new DtoSample { Id =_id.ToString(), Name = "b" } );
            _repository.DidNotReceive().Add( Arg.Any<EntitySample>() );
            _repository.Received().Update( Arg.Is<EntitySample>( t => t.Name == "b" ) );
        }

        /// <summary>
        /// 测试添加
        /// </summary>
        [Fact]
        public async Task TestSaveAsync_Add() {
            await _service.SaveAsync( new DtoSample { Name = "a" } );
            await _repository.Received().AddAsync( Arg.Is<EntitySample>( t => t.Name == "a" ) );
        }

        /// <summary>
        /// 测试修改
        /// </summary>
        [Fact]
        public async Task TestSaveAsync_Update() {
            _repository.FindAsync( _id ).Returns( t => new EntitySample( _id ) );
            await _service.SaveAsync( new DtoSample { Id = _id.ToString(), Name = "b" } );
            await _repository.DidNotReceive().AddAsync( Arg.Any<EntitySample>() );
            await _repository.Received().UpdateAsync( Arg.Is<EntitySample>( t => t.Name == "b" ) );
        }

        /// <summary>
        /// 测试删除
        /// </summary>
        [Fact]
        public void TestDelete() {
            var ids = new[] {_id, _id2};
            _repository.FindByIds( ids.Join() ).Returns( GetEntities() );
            _service.Delete( ids.Join() );
            _repository.Received().Remove( Arg.Is<List<EntitySample>>( t => t.All( d => ids.Contains( d.Id ) ) ) );
        }

        /// <summary>
        /// 测试删除 - id无效
        /// </summary>
        [Fact]
        public void TestDelete_IdInvalid() {
            var ids = new[] { Guid.NewGuid(), Guid.NewGuid() };
            _repository.FindByIds( ids.Join() ).Returns( new List<EntitySample>() );
            _service.Delete( ids.Join() );
            _repository.DidNotReceive().Remove( Arg.Any<List<EntitySample>>() );
        }

        /// <summary>
        /// 测试删除
        /// </summary>
        [Fact]
        public async Task TestDeleteAsync() {
            var ids = new[] { _id, _id2 };
            _repository.FindByIdsAsync( ids.Join() ).Returns( GetEntities() );
            await _service.DeleteAsync( ids.Join() );
            await _repository.Received().RemoveAsync( Arg.Is<List<EntitySample>>( t => t.All( d => ids.Contains( d.Id ) ) ) );
        }
    }

    /// <summary>
    /// Ioc配置
    /// </summary>
    public class IocConfig : ConfigBase {
        protected override void Load( ContainerBuilder builder ) {
            builder.AddScoped<ZKCloud.Datas.UnitOfWorks.IUnitOfWork, UnitOfWorkSample>();
            builder.AddScoped<IRepositorySample, RepositorySample>();
            builder.AddScoped<ICrudServiceSample, CrudServiceSample>();
        }
    }
}
