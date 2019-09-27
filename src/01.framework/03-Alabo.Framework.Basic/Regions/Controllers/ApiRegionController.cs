using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using Alabo.Extensions;
using Alabo.Framework.Basic.Regions.Domain.Entities;
using Alabo.Framework.Basic.Regions.Domain.Services;
using Alabo.Framework.Basic.Regions.Dtos;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Basic.Regions.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/Region/[action]")]
    public class ApiRegionController : ApiBaseController<Region, ObjectId>
    {
        public ApiRegionController()
        {
            BaseService = Resolve<IRegionService>();
        }

        /// <summary>
        ///     国家区域树形结构
        /// </summary>
        [HttpGet]
        [Display(Description = "国家区域树形结构")]
        public ApiResult<List<RegionTree>> Tree()
        {
            var result = Resolve<IRegionService>().RegionTrees().ToList();
            return ApiResult.Success(result);
        }

        /// <summary>
        /// </summary>
        [HttpGet]
        [Display(Description = "国家区域树形结构")]
        public ApiResult<List<RegionWithChild>> all()
        {
            var list = Resolve<IRegionService>().RegionTrees().Select(r => new RegionWithChild
            {
                Id = r.Id,
                Name = r.Name,
                ParentId = r.ParentId
            }).ToList();

            var dict = list.ToDictionary(r => r.Id);

            list.Foreach(r =>
            {
                if (dict.ContainsKey(r.ParentId)) dict[r.ParentId].Children.Add(r);
            });

            var ret = list.Where(r => !dict.ContainsKey(r.ParentId));

            ret.Foreach(reg => { reg.Children.ForEach(child => child.Children = new List<RegionWithChild>()); });

            return ApiResult.Success(ret.ToList());
        }

        /// <summary>
        ///     根据传入枚举返回对应的区域数据
        /// </summary>
        [HttpGet]
        public ApiResult GetRegionData(RegionLevel level)
        {
            var result = Resolve<IRegionService>().GetRegionData(level);
            return ApiResult.Success(result);
        }

        [HttpGet]
        [Display(Description = "国家区域信息")]
        public ApiResult<PagedList<Region>> RegionList([FromQuery] PagedInputDto parameter)
        {
            var model = Resolve<IRegionService>().GetPagedList(Query);
            return ApiResult.Success(model);
        }
    }
}