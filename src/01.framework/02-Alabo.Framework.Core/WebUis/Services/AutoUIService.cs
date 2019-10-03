using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.Reflections.Services;
using Alabo.Tables.Domain.Services;
using Alabo.UI.Design.AutoArticles;
using Alabo.UI.Design.AutoFaqs;
using Alabo.UI.Design.AutoForms;
using Alabo.UI.Design.AutoImages;
using Alabo.UI.Design.AutoIndexs;
using Alabo.UI.Design.AutoIntros;
using Alabo.UI.Design.AutoLists;
using Alabo.UI.Design.AutoNews;
using Alabo.UI.Design.AutoPreviews;
using Alabo.UI.Design.AutoReports;
using Alabo.UI.Design.AutoTables;
using Alabo.UI.Design.AutoTasks;
using Alabo.UI.Design.AutoVideos;
using System;
using System.Collections.Generic;

namespace Alabo.Framework.Core.WebUis.Services
{
    public class AutoUIService : ServiceBase, IAutoUIService
    {
        public AutoUIService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<KeyValue> AutoFormKeyValues()
        {
            var allAutoFormTypes = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IAutoForm));
            var resultList = new List<KeyValue>();
            foreach (var item in allAutoFormTypes) {
                try
                {
                    var classProperty = item.FullName.GetClassDescription();
                    var keyValue = new KeyValue
                    {
                        Value = item.FullName,
                        Name = $"{classProperty?.ClassPropertyAttribute?.Name} [{item.FullName}]"
                    };
                    resultList.Add(keyValue);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            // 叠加实体类型
            var catalogEntity = Resolve<ITableService>().CatalogEntityKeyValues();
            resultList.AddRange(catalogEntity);

            return resultList;
        }

        public List<KeyValue> AutoTableKeyValues()
        {
            var allAutoTableTypes = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IAutoTable));
            var resultList = new List<KeyValue>();
            foreach (var item in allAutoTableTypes) {
                try
                {
                    var classProperty = item.FullName.GetClassDescription();
                    var keyValue = new KeyValue
                    {
                        Value = item.FullName,
                        Name = $"{classProperty?.ClassPropertyAttribute?.Name} [{item.FullName}]"
                    };
                    resultList.Add(keyValue);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            // 叠加实体类型
            var catalogEntity = Resolve<ITableService>().CatalogEntityKeyValues();
            resultList.AddRange(catalogEntity);

            return resultList;
        }

        public List<KeyValue> AutoListKeyValues()
        {
            var allAutoListTypes = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IAutoList));
            var resultList = new List<KeyValue>();
            foreach (var item in allAutoListTypes) {
                try
                {
                    var classProperty = item.FullName.GetClassDescription();
                    var keyValue = new KeyValue
                    {
                        Value = item.FullName,
                        Name = $"{classProperty?.ClassPropertyAttribute?.Name} [{item.FullName}]"
                    };
                    resultList.Add(keyValue);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return resultList;
        }

        public List<KeyValue> AutoPreviewKeyValues()
        {
            var allAutoPreviewTypes = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IAutoPreview));
            var resultList = new List<KeyValue>();
            foreach (var item in allAutoPreviewTypes) {
                try
                {
                    var classProperty = item.FullName.GetClassDescription();
                    var keyValue = new KeyValue
                    {
                        Value = item.FullName,
                        Name = $"{classProperty?.ClassPropertyAttribute?.Name} [{item.FullName}]"
                    };
                    resultList.Add(keyValue);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return resultList;
        }

        public List<KeyValue> AutoNewsKeyValues()
        {
            var allAutoNewsTypes = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IAutoNews));
            var resultList = new List<KeyValue>();
            foreach (var item in allAutoNewsTypes) {
                try
                {
                    var classProperty = item.FullName.GetClassDescription();
                    var keyValue = new KeyValue
                    {
                        Value = item.FullName,
                        Name = $"{classProperty?.ClassPropertyAttribute?.Name} [{item.FullName}]"
                    };
                    resultList.Add(keyValue);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return resultList;
        }

        public List<KeyValue> AutoArticleKeyValues()
        {
            var allAutoArticleTypes = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IAutoArticle));
            var resultList = new List<KeyValue>();
            foreach (var item in allAutoArticleTypes) {
                try
                {
                    var classProperty = item.FullName.GetClassDescription();
                    var keyValue = new KeyValue
                    {
                        Value = item.FullName,
                        Name = $"{classProperty?.ClassPropertyAttribute?.Name} [{item.FullName}]"
                    };
                    resultList.Add(keyValue);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return resultList;
        }

        public List<KeyValue> AutoReportKeyValues()
        {
            var allAutoReportTypes = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IAutoReport));
            var resultList = new List<KeyValue>();
            foreach (var item in allAutoReportTypes) {
                try
                {
                    var classProperty = item.FullName.GetClassDescription();
                    var keyValue = new KeyValue
                    {
                        Value = item.FullName,
                        Name = $"{classProperty?.ClassPropertyAttribute?.Name} [{item.FullName}]"
                    };
                    resultList.Add(keyValue);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return resultList;
        }

        public List<KeyValue> AutoFaqsKeyValues()
        {
            var allAutoFaqsTypes = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IAutoFaq));
            var resultList = new List<KeyValue>();
            foreach (var item in allAutoFaqsTypes) {
                try
                {
                    var classProperty = item.FullName.GetClassDescription();
                    var keyValue = new KeyValue
                    {
                        Value = item.FullName,
                        Name = $"{classProperty?.ClassPropertyAttribute?.Name} [{item.FullName}]"
                    };
                    resultList.Add(keyValue);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return resultList;
        }

        public List<KeyValue> AutoImagesKeyValues()
        {
            var allAutoImageTypes = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IAutoImage));
            var resultList = new List<KeyValue>();
            foreach (var item in allAutoImageTypes) {
                try
                {
                    var classProperty = item.FullName.GetClassDescription();
                    var keyValue = new KeyValue
                    {
                        Value = item.FullName,
                        Name = $"{classProperty?.ClassPropertyAttribute?.Name} [{item.FullName}]"
                    };
                    resultList.Add(keyValue);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return resultList;
        }

        public List<KeyValue> AutoIndexKeyValues()
        {
            var allAutoIndexTypes = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IAutoIndex));
            var resultList = new List<KeyValue>();
            foreach (var item in allAutoIndexTypes) {
                try
                {
                    var classProperty = item.FullName.GetClassDescription();
                    var keyValue = new KeyValue
                    {
                        Value = item.FullName,
                        Name = $"{classProperty?.ClassPropertyAttribute?.Name} [{item.FullName}]"
                    };
                    resultList.Add(keyValue);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return resultList;
        }

        public List<KeyValue> AutoIntroKeyValues()
        {
            var allAutoIntroTypes = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IAutoIntro));
            var resultList = new List<KeyValue>();
            foreach (var item in allAutoIntroTypes) {
                try
                {
                    var classProperty = item.FullName.GetClassDescription();
                    var keyValue = new KeyValue
                    {
                        Value = item.FullName,
                        Name = $"{classProperty?.ClassPropertyAttribute?.Name} [{item.FullName}]"
                    };
                    resultList.Add(keyValue);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return resultList;
        }

        public List<KeyValue> AutoTaskKeyValues()
        {
            var allAutoTaskTypes = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IAutoTask));
            var resultList = new List<KeyValue>();
            foreach (var item in allAutoTaskTypes) {
                try
                {
                    var classProperty = item.FullName.GetClassDescription();
                    var keyValue = new KeyValue
                    {
                        Value = item.FullName,
                        Name = $"{classProperty?.ClassPropertyAttribute?.Name} [{item.FullName}]"
                    };
                    resultList.Add(keyValue);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return resultList;
        }

        public List<KeyValue> AutoVideoKeyValues()
        {
            var allAutoVideoTypes = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IAutoVideo));
            var resultList = new List<KeyValue>();
            foreach (var item in allAutoVideoTypes) {
                try
                {
                    var classProperty = item.FullName.GetClassDescription();
                    var keyValue = new KeyValue
                    {
                        Value = item.FullName,
                        Name = $"{classProperty?.ClassPropertyAttribute?.Name} [{item.FullName}]"
                    };
                    resultList.Add(keyValue);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return resultList;
        }

        public ServiceResult GetAutoComponents(string type)
        {
            var resultList = new List<string>();
            if (type.IsNullOrEmpty() || type == "undefined") {
                return ServiceResult.FailedWithMessage("类型不能为空");
            }

            var typeIntance = type.GetTypeByName();
            var find = type.GetInstanceByName();
            if (find == null || typeIntance == null) {
                return ServiceResult.FailedWithMessage($"类型不存在，请确认{type}输入是否正确");
            }
            //config
            if (find is IAutoTable) {
                resultList.Add("admin-auto-table");
            }

            if (find is IAutoImage) {
                resultList.Add("admin-auto-image");
            }

            if (find is IAutoArticle) {
                resultList.Add("admin-auto-article");
            }

            if (find is IAutoFaq) {
                resultList.Add("admin-auto-faq");
            }

            if (find is IAutoForm) {
                resultList.Add("admin-auto-form");
            }

            if (find is IAutoIndex) {
                resultList.Add("admin-auto-index");
            }

            if (find is IAutoIntro) {
                resultList.Add("admin-auto-intro");
            }

            if (find is IAutoList) {
                resultList.Add("admin-auto-list");
            }

            if (find is IAutoNews) {
                resultList.Add("admin-auto-news");
            }

            if (find is IAutoPreview) {
                resultList.Add("admin-auto-preview");
            }

            if (find is IAutoReport) {
                resultList.Add("admin-auto-report");
            }

            if (find is IAutoTask) {
                resultList.Add("admin-auto-task");
            }

            if (find is IAutoVideo) {
                resultList.Add("admin-auto-video");
            }

            return ServiceResult.SuccessWithObject(resultList);
        }

        /// <summary>
        ///     返回所有的URL地址
        /// </summary>
        /// <returns></returns>
        public List<KeyValue> GetAllPcUrl()
        {
            var result = new List<KeyValue>();
            var baseUrl = "http://localhost:2008/auto?type={0}";
            var _TypeService = Resolve<ITypeService>();

            var typeList = new List<IEnumerable<Type>>
            {
                _TypeService.GetAllTypeByInterface(typeof(IAutoTable)),
                _TypeService.GetAllTypeByInterface(typeof(IAutoImage)),
                _TypeService.GetAllTypeByInterface(typeof(IAutoArticle)),
                _TypeService.GetAllTypeByInterface(typeof(IAutoFaq)),
                _TypeService.GetAllTypeByInterface(typeof(IAutoForm)),
                _TypeService.GetAllTypeByInterface(typeof(IAutoIndex)),
                _TypeService.GetAllTypeByInterface(typeof(IAutoIntro)),
                _TypeService.GetAllTypeByInterface(typeof(IAutoList)),
                _TypeService.GetAllTypeByInterface(typeof(IAutoNews)),
                _TypeService.GetAllTypeByInterface(typeof(IAutoPreview)),
                _TypeService.GetAllTypeByInterface(typeof(IAutoReport)),
                _TypeService.GetAllTypeByInterface(typeof(IAutoTask)),
                _TypeService.GetAllTypeByInterface(typeof(IAutoVideo))
            };
            typeList.ForEach(type => ProcessUrls(result, baseUrl, type));

            return result;
        }

        private static void ProcessUrls(List<KeyValue> result, string baseUrl, IEnumerable<Type> autoTypeList)
        {
            foreach (var type in autoTypeList) {
                try
                {
                    var classProperty = type.FullName.GetClassDescription();
                    var keyValue = new KeyValue
                    {
                        Value = string.Format(baseUrl, type.Name),
                        Name = $"{classProperty?.ClassPropertyAttribute?.Name}"
                    };
                    result.Add(keyValue);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }
}