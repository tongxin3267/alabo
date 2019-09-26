using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.App.Core.User;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Helpers;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Market.UserCard.UI
{

    [ClassProperty(Name = "会员卡设置")]
    public class UserCardSetting : UIBase, IAutoForm, IAutoTable<UserCardSetting>
    {

        //[Field(ControlsType = ControlsType.Label, SortOrder = 2, ListShow = false, EditShow = false, Width = "40%", IsShowBaseSerach = false)]
        //public decimal Height { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; } = new Guid();

        /// <summary>
        ///     等级名称
        /// </summary>
        [Main]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 2, ListShow = true, EditShow = true, Width = "40%", IsShowBaseSerach = true)]
        [Display(Name = "等级名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("等级名称，比如高级会员，中级会员，钻石会员，黄金股东等等")]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the discount.
        /// </summary>
        /// <value>
        ///     The discount.
        /// </value>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, Width = "40%", GroupTabId = 1)]
        [HelpBlock("商城购物折扣,0.1表示折扣为10%，1表示折扣为0，不同的会员等级折扣不一样")]
        [Range(0.00, 1, ErrorMessage = "折扣范围必须在0~1之间")]
        [Display(Name = "折扣")]
        public decimal Discount { get; set; } = 1;

        /// <summary>
        /// 会员卡背景图
        /// </summary>
        [Field(ControlsType = ControlsType.AlbumUploder, ListShow = true, EditShow = true, SortOrder = 1, IsImagePreview = true)]
        [Display(Name = "会员卡背景图")]
        [HelpBlock("请上传会员卡背景图")]
        public string CardImage { get; set; }

        /// <summary>
        ///     购买价格
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true, Width = "40%")]
        [Display(Name = "购买价格")]
        [HelpBlock("输入购买的价格")]
        public decimal Price { get; set; } = 0;

        /// <summary>
        /// 会员卡介绍
        /// </summary>
        [Display(Name = "会员卡介绍")]
        [HelpBlock("会员卡介绍")]
        [Field(ControlsType = ControlsType.TextArea, ListShow = true, EditShow = true, Width = "40%")]
        public string Intro { get; set; }

        /// <summary>
        ///     默认用户等级
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden, EditShow = true)]
        [Display(Name = "默认等级")]
        //[HelpBlock("一种类型的有且只能有一个默认等级，在设置的是请注意，否则是导致数据出错")]
        public bool IsDefault { get; set; } = false;



        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var list = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var model = list.FirstOrDefault(u => u.Id == id.ToGuid());
            if (model == null)
            {
                return ToAutoForm(new UserCardSetting());
            }

            var autoForm = AutoMapping.SetValue<UserCardSetting>(model);
            if (!string.IsNullOrEmpty(model.Icon))
            {
                autoForm.CardImage = model.Icon;
            }

            return ToAutoForm(autoForm);
        }


        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var view = (UserCardSetting)model;
            var list = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            var userGrade = list.FirstOrDefault(u => u.Id == view.Id);
            if (userGrade == null)
            {
                var addModel = AutoMapping.SetValue<UserGradeConfig>(view);
                addModel.Id = Guid.NewGuid();
                list.Add(addModel);
            }
            else
            {
                if (view.Id.ToString() == "00000000-0000-0000-0000-000000000000")
                {
                    view.Id = Guid.NewGuid();
                }

                var mapList = new List<UserGradeConfig>();
                foreach (var item in list)
                {
                    if (userGrade.Name != item.Name)
                    {
                        mapList.Add(item);
                    }
                }

                userGrade.Id = view.Id;
                userGrade.Discount = view.Discount;
                userGrade.CardImage = view.CardImage;
                userGrade.Name = view.Name;
                userGrade.Price = view.Price;
                userGrade.Intro = view.Intro;
                userGrade.IsDefault = view.IsDefault;
                mapList.Add(userGrade);
                list = mapList;
            }
            Resolve<IAutoConfigService>().AddOrUpdate<UserGradeConfig>(list);
            return ServiceResult.Success;
        }

        public PageResult<UserCardSetting> PageTable(object query, AutoBaseModel autoModel)
        {
            var model = ToQuery<QueryWhere>();

            var userGradeConfig = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();
            if (model.Name.IsNotNullOrEmpty())
            {
                userGradeConfig = Resolve<IAutoConfigService>().GetList<UserGradeConfig>(l => l.Name == model.Name);
            }

            var result = new PagedList<UserCardSetting>();
            var apiService = Resolve<IApiService>();

            userGradeConfig.ForEach(u =>
            {
                var view = AutoMapping.SetValue<UserCardSetting>(u);
                if (!string.IsNullOrEmpty(u.Icon)) {
                    view.CardImage = apiService.ApiImageUrl(u.Icon);
                }

                result.Add(view);
            });

            return ToPageResult(result);
        }

        public List<TableAction> Actions()
        {
            //数据保存到UserGardeConfig中
            var list = new List<TableAction>
            {
                ToLinkAction("增加会员等级","/User/UserCard/Edit",TableActionType.QuickAction),
                ToLinkAction("编辑", "/User/UserCard/Edit",TableActionType.ColumnAction),
              //  ToLinkAction("删除", "/api/AutoConfig/Delete",ActionLinkType.Delete,TableActionType.ColumnAction)
                ToLinkAction("删除", "/api/Auto/DeleteUserCard",ActionLinkType.Delete,TableActionType.ColumnAction)
            };
            return list;
        }


        public class QueryWhere : ApiInputDto
        {
            public string Name { get; set; }
            public decimal Discount { get; set; }
        }


    }
}