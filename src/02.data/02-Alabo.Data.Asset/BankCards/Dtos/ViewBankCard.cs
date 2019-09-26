using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Asset.BankCards.Domain.Entities;
using Alabo.App.Asset.BankCards.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Mapping;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;

namespace Alabo.App.Asset.BankCards.Dtos
{
    public class ViewBankCard : UIBase, IAutoTable<ViewBankCard>
    {
        public ObjectId Id { get; set; }

        /// <summary>
        ///     持卡人姓名
        /// </summary>
        [Display(Name = "持卡人姓名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Width = "120", IsShowAdvancedSerach = true,
            ListShow = true, SortOrder = 5)]
        public string Name { get; set; }

        /// <summary>
        ///     银行类型
        /// </summary>
        [Display(Name = "银行类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "120",
            ListShow = true, SortOrder = 5)]
        public string BankCardTypeName { get; set; }

        public string BankLogo { get; set; }

        /// <summary>
        ///     银行卡号
        /// </summary>
        [Display(Name = "银行卡号")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Width = "220", IsShowAdvancedSerach = true,
            ListShow = true, SortOrder = 7)]
        [StringLength(20, ErrorMessage = "银行卡号不能超过20位")]
        public string Number { get; set; }

        /// <summary>
        ///     开户行
        /// </summary>
        [Display(Name = "开户行")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 8)]
        public string Address { get; set; }

        public List<TableAction> Actions()
        {
            //return new List<TableAction>() {
            //       ToLinkAction("删除", "/Api/BankCard/DeleteBankCard",ActionLinkType.Delete,TableActionType.ColumnAction)
            //};
            return new List<TableAction>();
        }

        /// <summary>
        /// </summary>
        public PageResult<ViewBankCard> PageTable(object query, AutoBaseModel autoModel)
        {
            var userInput = ToQuery<ViewBankCardInput>();
            var view = new PagedList<ViewBankCard>();

            if (autoModel.Filter == FilterType.Admin)
            {
                var temp = new ExpressionQuery<BankCard>
                {
                    EnablePaging = true,
                    PageIndex = (int) userInput.PageIndex,
                    PageSize = 15
                };
                if (!userInput.Name.IsNullOrEmpty()) temp.And(u => u.Name.Contains(userInput.Name));

                if (!userInput.Number.IsNullOrEmpty()) temp.And(u => u.Number.Contains(userInput.Number));

                var model = Resolve<IBankCardService>().GetPagedList(temp);
                foreach (var item in model)
                {
                    var outPut = AutoMapping.SetValue<ViewBankCard>(item);
                    outPut.BankCardTypeName = item.Type.GetDisplayName();
                    view.Add(outPut);
                }

                return ToPageResult(view);
            }

            if (autoModel.Filter == FilterType.User)
            {
                userInput.UserId = autoModel.BasicUser.Id;
                var model = Resolve<IBankCardService>().GetUserBankCardOutputs(userInput);
                foreach (var item in model)
                {
                    var outPut = AutoMapping.SetValue<ViewBankCard>(item);
                    outPut.BankCardTypeName = item.Type.GetDisplayName();
                    view.Add(outPut);
                }

                return ToPageResult(view);
            }

            throw new ValidException("类型权限不正确");
        }
    }
}