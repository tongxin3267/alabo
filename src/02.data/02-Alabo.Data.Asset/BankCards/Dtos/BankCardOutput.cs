using MongoDB.Bson;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoTables;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.Domain.Entities {

    /// <summary>
    ///     银行卡
    /// </summary>
    [ClassProperty(Name = "银行卡")]
    public class BankCardOutput : UIBase, IAutoTable<BankCardOutput> {
        public ObjectId Id { get; set; }

        /// <summary>
        ///     持卡人姓名
        /// </summary>
        [Display(Name = "持卡人姓名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Width = "120", IsShowAdvancedSerach = true,
            ListShow = true, SortOrder = 2)]
        public string Name { get; set; }

        /// <summary>
        ///     银行类型
        /// </summary>
        [Display(Name = "银行类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.DropdownList, DataSource = "Alabo.Framework.Core.Enums.Enum.BankType", Width = "120", ListShow = false, SortOrder = 6)]
        public BankType Type { get; set; }

        /// <summary>
        /// 银行类型名称
        /// </summary>
        [Display(Name = "银行类型")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, Width = "220", IsShowAdvancedSerach = true,
        ListShow = true, SortOrder = 7)]
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

        public List<TableAction> Actions() {
            return new List<TableAction>();
        }

        /// <summary>
        ///
        /// </summary>
        public PageResult<BankCardOutput> PageTable(object query, AutoBaseModel autoModel) {
            var List = Resolve<IBankCardService>().GetPageList(query);

            var result = new PagedList<BankCardOutput>();
            if (List.Count > 0) {
                List.ForEach(p => {
                    var model = new BankCardOutput();
                    model.Address = p.Address;
                    model.Id = p.Id;
                    model.Name = p.Name;
                    model.BankCardTypeName = p.Type.ToString();
                    model.Number = p.Number;
                    model.Type = p.Type;

                    result.Add(model);
                });
                result.RecordCount = List.Count;
            }
            return ToPageResult(result);
        }
    }
}