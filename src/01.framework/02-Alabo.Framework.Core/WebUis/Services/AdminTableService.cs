using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Files;
using Alabo.Framework.Core.Reflections.Services;
using Alabo.Linq.Dynamic;
using Alabo.Mapping;
using Alabo.Reflections;
using Alabo.Web.ViewFeatures;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using FileInfo = System.IO.FileInfo;

namespace Alabo.Framework.Core.WebUis.Domain.Services
{
    public class AdminTableService : ServiceBase, IAdminTableService
    {
        public AdminTableService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        ///     导出表格
        ///     //实现步骤：1.先实现基础的导出功能，2.用户可以通过界面选择导出方式，3.保存用户习惯,4.扩展到其他所有的表格
        /// </summary>
        /// <param name="key">导出表格key，通过key从缓存中读取用户的设置</param>
        /// <param name="service">获取数据的服务</param>
        /// <param name="method">获取数据的方法</param>
        /// <param name="query">Url参数，用户通过界面选择传递</param>
        public Tuple<ServiceResult, string, string> ToExcel(string key, string service, string method, object query)
        {
            var type = key.GetTypeByFullName();
            if (type == null)
                return Tuple.Create(ServiceResult.FailedWithMessage($"类型不存在，请输入正确的{key}"), string.Empty, string.Empty);

            var classDescription = new ClassDescription(type);
            //获取excel显示字段
            var excelPropertys = Resolve<IClassService>().GetListPropertys(type.FullName);

            // 动态获取数据
            var resultList = DynamicService.ResolveMethod(service, method, query);
            FileHelper.CreateDirectory(FileHelper.WwwRootPath + "//excel");
            var file = new FileInfo(Path.Combine(FileHelper.WwwRootPath, "excel",
                $"{classDescription.ClassPropertyAttribute.Name}_{DateTime.Now.ConvertDateTimeFileString()}.xlsx"));
            using (var package = new ExcelPackage(file))
            {
                // 添加worksheet
                var worksheet = package.Workbook.Worksheets.Add(type.Name);
                worksheet.DefaultRowHeight = 35; //行高25
                //worksheet.Cells.Style.Font.Size = 9;
                for (var i = 1; i < excelPropertys.Count() + 1; i++)
                {
                    var width = excelPropertys.ToList()[i - 1].FieldAttribute.Width; // 宽度
                    worksheet.Cells[1, i].Value = excelPropertys.ToList()[i - 1].DisplayAttribute.Name;
                    worksheet.Cells[1, i].Style.Font.Bold = true; //加粗
                    worksheet.Cells[1, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //水平居中
                    worksheet.Cells[1, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center; //垂直居中
                    worksheet.Cells[1, i].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //边框
                    worksheet.Cells[1, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, i].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    worksheet.Column(i).Width = width.ToDouble() / 8; //固定宽度
                }

                //添加值
                var recordIndex = 2;
                var pageList = new List<object>();
                if (resultList.Item2 != null)
                    try
                    {
                        pageList = ((IEnumerable<object>) resultList.Item2).ToList();
                    }
                    catch
                    {
                        Tuple.Create(ServiceResult.FailedMessage("该类型不支持Excel导出"), file.FullName, file.Name);
                    }

                foreach (var data in pageList)
                {
                    for (var i = 1; i < excelPropertys.Count() + 1; i++)
                    {
                        var property = excelPropertys.ToList()[i - 1];
                        var value = property.Property.GetPropertyValue(data);
                        var chageResult = AutoMapping.TryChangeHtmlValue(value, property.Property.PropertyType);
                        var excelText = !chageResult.Item1 ? string.Empty : chageResult.Item2;

                        worksheet.Cells[recordIndex, i].Value = excelText.ToString().InputTexts();
                        worksheet.Cells[recordIndex, i].Style.Border
                            .BorderAround(ExcelBorderStyle.Thin, Color.Black); //边框
                        worksheet.Cells[recordIndex, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[recordIndex, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[recordIndex, i].Style.Font.Size = 9;
                    }

                    recordIndex++;
                }

                package.Save();
            }

            return Tuple.Create(ServiceResult.Success, file.FullName, file.Name);
        }

        public Tuple<ServiceResult, string, string> ToExcel(string type, object query)
        {
            throw new NotImplementedException();
        }

        public Tuple<ServiceResult, string, string> ToExcel(Type type, dynamic inputData)
        {
            //var service = "IAlaboUserService";
            //var method = "GetViewUserPageList";
            //var query = "";
            //var type = autoTable.GetType();

            var classDescription = new ClassDescription(type);
            //获取excel显示字段
            var excelPropertys = Resolve<IClassService>().GetListPropertys(type.FullName);

            // 动态获取数据
            var resultList = inputData.Result as IEnumerable<object>;
            //var resultList = DynamicService.ResolveMethod(service, method, query);
            FileHelper.CreateDirectory(FileHelper.WwwRootPath + "//excel");
            var file = new FileInfo(Path.Combine(FileHelper.WwwRootPath, "excel",
                $"{classDescription.ClassPropertyAttribute.Name}_{DateTime.Now.ConvertDateTimeFileString()}.xlsx"));
            using (var package = new ExcelPackage(file))
            {
                // 添加worksheet
                var worksheet = package.Workbook.Worksheets.Add(type.Name);
                worksheet.DefaultRowHeight = 35; //行高25
                //worksheet.Cells.Style.Font.Size = 9;
                for (var i = 1; i < excelPropertys.Count() + 1; i++)
                {
                    var width = excelPropertys.ToList()[i - 1].FieldAttribute.Width; // 宽度
                    worksheet.Cells[1, i].Value = excelPropertys.ToList()[i - 1].DisplayAttribute.Name;
                    worksheet.Cells[1, i].Style.Font.Bold = true; //加粗
                    worksheet.Cells[1, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; //水平居中
                    worksheet.Cells[1, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center; //垂直居中
                    worksheet.Cells[1, i].Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black); //边框
                    worksheet.Cells[1, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, i].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    worksheet.Column(i).Width = width.ToDouble() / 8; //固定宽度
                }

                //添加值
                var recordIndex = 2;
                var pageList = new List<object>();
                //if (resultList.Item2 != null)
                //{
                //    try
                //    {
                //        pageList = ((IEnumerable<object>)resultList.Item2).ToList();
                //    }
                //    catch
                //    {
                //        Tuple.Create(ServiceResult.FailedMessage("该类型不支持Excel导出"), file.FullName, file.Name);
                //    }
                //}

                foreach (var data in resultList)
                {
                    for (var i = 1; i < excelPropertys.Count() + 1; i++)
                    {
                        var property = excelPropertys.ToList()[i - 1];
                        var value = property.Property.GetPropertyValue(data);
                        var chageResult = AutoMapping.TryChangeHtmlValue(value, property.Property.PropertyType);
                        var excelText = !chageResult.Item1 ? string.Empty : chageResult.Item2;

                        worksheet.Cells[recordIndex, i].Value = excelText.ToString().InputTexts();
                        worksheet.Cells[recordIndex, i].Style.Border
                            .BorderAround(ExcelBorderStyle.Thin, Color.Black); //边框
                        worksheet.Cells[recordIndex, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[recordIndex, i].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[recordIndex, i].Style.Font.Size = 9;
                    }

                    recordIndex++;
                }

                package.Save();
            }

            return Tuple.Create(ServiceResult.Success, file.FullName, file.Name);
        }
    }
}