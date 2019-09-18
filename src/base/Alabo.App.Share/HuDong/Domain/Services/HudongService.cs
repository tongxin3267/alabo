using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Open.HuDong.Domain.Entities;
using Alabo.App.Open.HuDong.Domain.Enums;
using Alabo.App.Open.HuDong.Dtos;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;

namespace Alabo.App.Open.HuDong.Domain.Services {

    public class HudongService : ServiceBase<Hudong, ObjectId>, IHudongService {

        public HudongService(IUnitOfWork unitOfWork, IRepository<Hudong, ObjectId> repository) : base(unitOfWork,
            repository) {
        }

        /// <summary>
        /// �����༭
        /// </summary>
        /// <param name="huDong"></param>
        /// <returns></returns>
        public ServiceResult Edit(Hudong huDong) {
            var findType = huDong.Key.GetInstanceByName();
            if (findType == null) {
                return ServiceResult.FailedWithMessage("�������Ͳ�����");
            }
            var find = GetSingle(huDong.Id, huDong.Key);
            if (find != null) {
                if (!Update(huDong)) {
                    return ServiceResult.FailedWithMessage("�����ʧ��");
                }
            } else {
                // ��ֵ����
                if (!Add(huDong)) {
                    return ServiceResult.FailedWithMessage("����ʧ��");
                }
            }
            return ServiceResult.Success;
        }

        /// <summary>
        /// ��ȡ��Ʒ��������ѡ��
        /// </summary>
        /// <returns></returns>
        public List<EnumKeyValue> GetAwardType() {
            List<EnumKeyValue> list = new List<EnumKeyValue>();

            foreach (var e in Enum.GetValues(typeof(HudongAwardType))) {
                EnumKeyValue enumKeyValue = new EnumKeyValue();
                enumKeyValue.Key = Convert.ToInt32(e);
                enumKeyValue.Value = e.GetDisplayName();
                list.Add(enumKeyValue);
            }
            return list;
        }

        /// <summary>
        /// ��ȡһ����¼
        /// </summary>
        /// <param name="id"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public Hudong GetSingle(ObjectId id, string key) {
            var find = GetSingle(id);
            if (find == null && !key.IsNullOrEmpty()) {
                // Ŀǰһ�ֻֻ֧�ִ���һ����¼
                find = GetSingle(r => r.Key == key);
            }
            return find;
        }

        /// <summary>
        /// ��ȡ���ͼ
        /// </summary>
        /// <param name="viewInput"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, Hudong> GetView(HuDongViewInput viewInput) {
            var view = GetSingle(viewInput.Id, viewInput.Key);

            if (viewInput.Key.IsNullOrEmpty()) {
                return new Tuple<ServiceResult, Hudong>(ServiceResult.FailedWithMessage("�������Ͳ���Ϊ��"), view);
            }

            var findType = viewInput.Key.GetInstanceByName();
            if (findType == null) {
                return new Tuple<ServiceResult, Hudong>(ServiceResult.FailedWithMessage("�������Ͳ�����"), view);
            }

            if (view == null) {
                view = new Hudong {
                    Key = findType.GetType().FullName
                };
                if (findType is IHuDong set) {
                    view.Setting = set.Setting();
                    view.Awards = set.DefaultAwards();
                }
            }

            view.Name = $"{findType?.GetType()?.FullName?.GetClassDescription()?.ClassPropertyAttribute?.Name}����";
            return new Tuple<ServiceResult, Hudong>(ServiceResult.Success, view);
        }

        /// <summary>
        /// ��ȡ���ͼ
        /// </summary>
        /// <param name="viewInput"></param>
        /// <returns></returns>
        public Tuple<ServiceResult, Hudong> GetAwards(HuDongViewInput viewInput) {
            Hudong view = GetSingle(viewInput.Id, viewInput.Key);

            if (!string.IsNullOrEmpty(viewInput.userId))//����û�δ��¼���޷���ȡ������Ϣ
            {
                if (viewInput.userId == "0") {
                    view.DrawCount = 0;
                } else {
                    var beginTime = DateTime.Now.GetDayBegin();
                    var endTime = DateTime.Now.GetDayFinish();

                    var drawCount = Resolve<IHudongRecordService>().GetList(p => p.UserId == viewInput.userId.ToLong() && (p.CreateTime > beginTime && p.CreateTime < endTime)).Count;//��ѯ�Ƿ��г齱��¼,ֻ��ѯ����

                    if (view != null) {
                        if (view.IsEnable == false) {
                            return new Tuple<ServiceResult, Hudong>(ServiceResult.FailedWithMessage("���δ����"), view);
                        }
                        if (view.DrawCount - drawCount <= 0) {
                            view.DrawCount = 0;
                        } else {
                            view.DrawCount = view.DrawCount - drawCount;
                        }
                    } else {
                        return new Tuple<ServiceResult, Hudong>(ServiceResult.FailedWithMessage("���δ����"), view);
                    }
                }
            } else {
                view.DrawCount = 0;
            }

            if (viewInput.Key.IsNullOrEmpty()) {
                return new Tuple<ServiceResult, Hudong>(ServiceResult.FailedWithMessage("�������Ͳ���Ϊ��"), view);
            }

            var findType = viewInput.Key.GetInstanceByName();
            if (findType == null) {
                return new Tuple<ServiceResult, Hudong>(ServiceResult.FailedWithMessage("�������Ͳ�����"), view);
            }

            if (view == null) {
                view = new Hudong {
                    Key = findType.GetType().FullName
                };
                if (findType is IHuDong set) {
                    view.Setting = set.Setting();
                    view.Awards = set.DefaultAwards();
                }
            }

            view.Name = $"{findType?.GetType()?.FullName?.GetClassDescription()?.ClassPropertyAttribute?.Name}����";
            return new Tuple<ServiceResult, Hudong>(ServiceResult.Success, view);
        }

        /// <summary>
        /// �齱����
        /// ������д�ֵ1���Ż�ȯ4������ȯ2���һ�ȯ3��ֱ�Ӵ洢���û��˻���ͬʱ��¼���н���¼��
        /// ��Ҫ����һ���н���û�ʣ��ɳ齱����
        /// </summary>
        /// <param name="drawInput"></param>
        /// <returns></returns>
        public ServiceResult Draw(DrawInput drawInput) {
            if (drawInput.DrawCount <= 0) {
                return ServiceResult.FailedWithMessage("�齱���������꣡");
            }
            if (drawInput.Id == null) {
                return ServiceResult.FailedWithMessage("��ȡ����Ϊ�գ�");
            }

            Hudong model = GetSingle(ObjectId.Parse(drawInput.Id), drawInput.Key);
            if (model == null) {
                return ServiceResult.FailedWithMessage("��ȡ����Ϊ�գ�");
            }

            List<HudongAward> list = model.Awards;

            Dictionary<Guid, int> rateDic = new Dictionary<Guid, int>();
            list.ForEach(a => {
                if (a.Count > 0) {
                    rateDic.Add(a.AwardId, (int)a.Rate * 100);
                }
            });

            Dictionary<Guid, int> resultsDic = Lottery(rateDic);//��ȡ�齱���
            var results = "";
            foreach (var d in resultsDic) {
                results = d.Key.ToString();
            }

            if (!string.IsNullOrEmpty(results)) {
                var winList = model.Awards.Where(a => a.AwardId == results.ToGuid());
                var awardWin = winList.FirstOrDefault(u => u.AwardId == results.ToGuid());
                var updateResult = true;
                //�н���Ҫ��ȥ��Ӧ�Ľ�Ʒ����
                if (awardWin.Type != HudongAwardType.None)//����н���Ϊ��δ�н������ø�������
                {
                    List<HudongAward> awardList = new List<HudongAward>();
                    foreach (var award in model.Awards) {
                        HudongAward awardModel = new HudongAward();
                        if (award.AwardId == results.ToGuid()) {
                            awardModel.Count = award.Count - 1;
                        } else {
                            awardModel.Count = award.Count;
                        }
                        awardModel.AwardId = award.AwardId;
                        awardModel.img = award.img;
                        awardModel.Grade = award.Grade;
                        awardModel.Rate = award.Rate;
                        awardModel.Type = award.Type;
                        awardModel.worth = award.worth;
                        awardList.Add(awardModel);
                    }
                    model.Awards = awardList;
                    updateResult = Update(model);
                }
                //�����н�����
                if (updateResult) {
                    //�����н���awardID��ѯ��Ӧmodel
                    var drawType = awardWin.Type.Value();
                    var intro = awardWin.Intro.IsNullOrEmpty() ? "" : awardWin.Intro.ToString();
                    var worth = awardWin.worth;
                    var count = awardWin.Count;

                    //ÿ�γ齱����Ҫһ���齱��¼
                    HudongRecord recordModel = new HudongRecord();
                    recordModel.UserId = drawInput.UserId;
                    recordModel.HuDongType = HuDongEnums.BigWheel;
                    recordModel.Intro = intro;
                    recordModel.HuDongStatus = AwardStatus.NotExchange;
                    recordModel.Grade = awardWin.Grade;
                    recordModel.HuDongActivityType = awardWin.Type;

                    MoneyTypeConfig moneyTypes = new MoneyTypeConfig();

                    var moneyType = MoneyTypeConfig.Credit;
                    if (drawType == HudongAwardType.StoreValue.Value())//��ֵ��Ӧ�����
                    {
                        moneyTypes.Id = MoneyTypeConfig.CNY;
                        recordModel.HuDongStatus = AwardStatus.Exchage;
                    } else if (drawType == HudongAwardType.Discount.Value()) //�������Ѷ�
                      {
                        moneyTypes.Id = MoneyTypeConfig.ShopAmount;
                        recordModel.HuDongStatus = AwardStatus.Exchage;
                    } else if (drawType == HudongAwardType.Integral.Value()) //����
                      {
                        moneyTypes.Id = MoneyTypeConfig.Point;
                        recordModel.HuDongStatus = AwardStatus.Exchage;
                    } else if (drawType == HudongAwardType.Coupon.Value())//�Ż�ȯ
                      {
                        moneyTypes.Id = MoneyTypeConfig.Preferential;
                        recordModel.HuDongStatus = AwardStatus.Exchage;
                    }

                    var recordResult = Resolve<IHudongRecordService>().AddRecord(recordModel);  //�齱��¼

                    if (recordResult.Succeeded && awardWin.Type != HudongAwardType.None && recordModel.HuDongStatus == AwardStatus.Exchage) {
                        var user = Resolve<IUserService>().GetSingle(drawInput.UserId);

                        Resolve<IBillService>().Increase(user, moneyTypes, worth, "��ת�̳齱�н����");
                    }
                }
            }
            return ServiceResult.SuccessWithObject(results);
        }

        /// <summary>
        /// �����н��������ȡ��һ���н���Ϣ
        /// </summary>
        /// <param name="orignalRates"></param>
        /// <returns></returns>
        public Dictionary<Guid, int> Lottery(Dictionary<Guid, int> orignalRates) {
            Dictionary<Guid, int> resultDic = new Dictionary<Guid, int>();
            if (orignalRates == null || orignalRates.IsNullOrEmpty()) {
                return null;
            }

            int maxSize = orignalRates.Count();
            int maxProbabilityNum = 0;
            Dictionary<Guid, string> dic = new Dictionary<Guid, string>();

            foreach (var rate in orignalRates) {
                dic.Add(rate.Key, maxProbabilityNum + "-" + (maxProbabilityNum + rate.Value));
                maxProbabilityNum += rate.Value;
            }

            int random = new Random().Next(maxProbabilityNum) + 1;

            foreach (var d in dic) {
                int min = int.Parse(d.Value.Split('-')[0]);
                int max = int.Parse(d.Value.Split('-')[1]);

                if (random > min && random <= max) {
                    resultDic.Add(d.Key, orignalRates[d.Key].ToInt16() / 100);
                    return resultDic;
                }
            }
            return resultDic;
        }
    }
}