using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Alabo.App.Core.Finance.Domain.Dtos.BankCard;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Dtos;
using Alabo.AutoConfigs;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Mapping;

namespace Alabo.App.Core.Finance.Domain.Services {

    public class BankCardService : ServiceBase<BankCard, ObjectId>, IBankCardService {

        public BankCardService(IUnitOfWork unitOfWork, IRepository<BankCard, ObjectId> repository) : base(unitOfWork,
            repository) {
        }

        /// <summary>
        /// ���ӻ��޸����п�
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public ServiceResult AddOrUpdateBankCard(ApiBankCardInput view) {
            //���п�����Ϊ���ֲ�����0��ͷ
            var r = new Regex("^([1-9][0-9]*)$");
            if (!r.IsMatch(view.BankCardId)) {
                return ServiceResult.FailedWithMessage("����ȷ�������п���");
            }

            if (view.BankCardId.Length < 10) {
                return ServiceResult.FailedWithMessage("����ȷ�������п���");
            }

            if (view.Type == 0) {
                return ServiceResult.FailedWithMessage("��ѡ����������");
            }

            #region ʹ�ð���ӿ��ж����п��Ƿ���ȷ

            var checkUrl =
                $"https://ccdcapi.alipay.com/validateAndCacheCardInfo.json?_input_charset=utf-8&cardNo={view.BankCardId}&cardBinCheck=true";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(checkUrl));
            HttpWebResponse httpResponse = null;

            try {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            } catch (WebException ex) {
                httpResponse = (HttpWebResponse)ex.Response;
            }
            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            var readResult = reader.ReadToEnd();
            try {
                var aliResult = readResult.DeserializeJson<AliResult>();
                if (aliResult.Validated != "true") {
                    return ServiceResult.FailedWithMessage("����ȷ�������п���");
                }
            } catch (Exception e) {
                return ServiceResult.FailedWithMessage("����ȷ�������п���");
            }

            #endregion ʹ�ð���ӿ��ж����п��Ƿ���ȷ

            var bankCardList = Resolve<IBankCardService>().GetList(u => u.UserId == view.LoginUserId);
            if (bankCardList.Count >= 10) {
                return ServiceResult.FailedWithMessage("���п����ֻ�ɰ�ʮ��");
            }

            var model = Resolve<IBankCardService>().GetSingle(u => u.Number == view.BankCardId);
            BankCard bankCard = new BankCard {
                Number = view.BankCardId,
                Address = view.Address,
                Name = view.Name,
                Type = view.Type,
                UserId = view.LoginUserId,
                Id = view.Id.ToObjectId()
            };

            if (model == null) {
                var result = Add(bankCard);
                if (!result) {
                    return ServiceResult.Failed;
                }
                return ServiceResult.Success;
            }

            return ServiceResult.FailedWithMessage("�����п��Ѿ���");
        }

        /// <summary>
        /// ��ȡ�û����п��б�
        /// </summary>
        /// <param name="loginUserId"></param>
        /// <returns></returns>
        public IList<KeyValue> GetUserBankCardList(long loginUserId) {
            var bankCardList = Resolve<IBankCardService>().GetList(u => u.UserId == loginUserId);
            var result = new List<KeyValue>();
            foreach (var item in bankCardList) {
                var keyValue = new KeyValue {
                    Value = item.Type.GetDisplayName() + $"({item.Number})",
                    Key = item.Number
                };
                result.Add(keyValue);
            }

            return result;
        }

        public PagedList<BankCardOutput> GetPageList(object query) {
            var model = Resolve<IBankCardService>().GetPagedList(query);
            PagedList<BankCardOutput> result = new PagedList<BankCardOutput>();
            foreach (var item in model) {
                var temp = AutoMapping.SetValue<BankCardOutput>(item);
                result.Add(temp);
            }

            return result;
        }

        /// <summary>
        /// ɾ�����п�
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        public ServiceResult RomoveBankCard(ApiBankCardInput view) {
            var model = Resolve<IBankCardService>().GetSingle(u => u.Number == view.BankCardId);
            Delete(model);
            return ServiceResult.Success;
        }

        public PagedList<BankCard> GetUserBankCardOutputs(ViewBankCardInput parameter) {
            var query = new ExpressionQuery<BankCard> {
                PageIndex = (int)parameter.PageIndex,
                PageSize = (int)parameter.PageSize
            };
            query.And(e => e.UserId == parameter.UserId);

            if (!parameter.Name.IsNullOrEmpty()) {
                query.And(u => u.Name.Contains(parameter.Name));
            }

            if (!parameter.Number.IsNullOrEmpty()) {
                query.And(u => u.Number.Contains(parameter.Number));
            }
            var model = Resolve<IBankCardService>().GetPagedList(query);
            return model;
        }

        public ServiceResult DeleteBankCard(string id) {
            var model = Resolve<IBankCardService>().GetSingle(u => u.Id == id.ToObjectId());
            var result = Delete(model);
            if (result) {
                return ServiceResult.Success;
            }
            return ServiceResult.Failed;
        }

        public List<BankCardOutput> GetBankCardByUserId(long userId) {
            var bankList = Resolve<IBankCardService>().GetList(u => u.UserId == userId);
            var webSite = Resolve<IAutoConfigService>().GetValue<WebSiteConfig>();
            var result = new List<BankCardOutput>();
            foreach (var item in bankList) {
                var model = AutoMapping.SetValue<BankCardOutput>(item);
                model.Id = item.Id;
                model.Number = "**** **** **** " + item.Number.Substring(item.Number.Length - 4);
                model.BankCardTypeName = item.Type.GetDisplayName();
                model.BankLogo = $"{webSite.ApiImagesUrl}/wwwroot/uploads/bankcard/{item.Type.ToString()}.png";
                result.Add(model);
            }

            return result;
        }
    }
}