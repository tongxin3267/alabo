using Alabo.App.Core.ApiStore;
using Alabo.App.Core.ApiStore.CallBacks;
using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.Finance.Domain.Dtos.Pay;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Entities.Extension;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Finance.Domain.Repositories;
using Alabo.App.Core.Finance.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Alabo.AutoConfigs;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Randoms;
using ZKCloud.Open.ApiStore.Payment.Modules.Alipay;
using ZKCloud.Open.ApiStore.Payment.Modules.Alipay.Domain;
using ZKCloud.Open.ApiStore.Payment.Modules.Alipay.Request;
using ZKCloud.Open.ApiStore.Payment.Modules.Alipay.Response;
using ZKCloud.Open.ApiStore.Payment.Modules.WeChatPay;
using ZKCloud.Open.ApiStore.Payment.Modules.WeChatPay.Request;
using ZKCloud.Open.ApiStore.Payment.Modules.WeChatPay.Response;
using ZKCloud.Open.Security;
using Convert = System.Convert;

namespace Alabo.App.Core.Finance.Domain.Services {

    /// <summary>
    ///     支付数据服务
    /// </summary>
    public class PayService : ServiceBase<Pay, long>, IPayService {
        private readonly IPayRepository _payRepository;

        public PayService(IUnitOfWork unitOfWork, IRepository<Pay, long> repository) : base(unitOfWork, repository) {
            _payRepository = Repository<IPayRepository>();
        }

        /// <summary>
        ///     通过Id获取支付记录
        /// </summary>
        /// <param name="id">主键ID</param>
        public Pay GetSingle(long id) {
            var pay = GetSingle(r => r.Id == id);
            if (pay != null) {
                pay.AccountPayPair = pay.AccountPay.DeserializeJson<List<KeyValuePair<Guid, decimal>>>();
                pay.PayExtension = pay.Extensions.DeserializeJson<PayExtension>();
            }

            return pay;
        }

        /// <summary>
        ///     更新支付记录处理
        /// </summary>
        /// <param name="payInput"></param>
        /// <param name="httpContext"></param>
        public Tuple<ServiceResult, PayOutput> Pay(PayInput payInput, HttpContext httpContext = null) {
            var result = ServiceResult.Failed;
            var payOutput = new PayOutput();
            var payResult = new Tuple<ServiceResult, PayOutput>(result, payOutput);
            var orderIds = new List<object>();
            try {
                // 获取前台Url，实现跳转支付完成后跳转功能
                var url = HttpWeb.ClientHost;
                // 后台服务端Url
                var serviceUrl = HttpWeb.ServiceHost;

                #region 订单支付安全验证

                #region 通用安全验证

                if (payInput.Amount < 0) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("支付金额不能小于0"), payOutput);
                }

                if (payInput.Amount > Convert.ToDecimal(99999999)) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("支付金额不能大于100000"), payOutput);
                }

                var pay = GetSingle(payInput.PayId);
                pay.PayExtension = pay.Extensions.ToObject<PayExtension>();
                if (pay == null && pay.Status != PayStatus.WaiPay) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("支付记录不存在，或已处理"), payOutput);
                }

                if (pay.EntityId == null) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("实体订单已不存在"), payOutput);
                }

                try {
                    orderIds = pay.EntityId.DeserializeJson<List<object>>();
                    if (orderIds == null || orderIds.Count < 1) {
                        return Tuple.Create(ServiceResult.FailedWithMessage("实体订单已不存在"), payOutput);
                    }
                } catch {
                    return Tuple.Create(ServiceResult.FailedWithMessage("实体订单序列化出错"), payOutput);
                }

                var user = Resolve<IUserService>().GetSingle(payInput.LoginUserId);
                if (user == null && user.Status != Status.Normal) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("用户不存在，或状态不正常"), payOutput);
                }

                if (pay.PayType != PayType.AdminPay) {
                    // 非管理员支付，验证用户Id
                    if (user.Id != pay.UserId) {
                        if (user.Id != pay.PayExtension.OrderUser.Id) {
                            return Tuple.Create(ServiceResult.FailedWithMessage("用户Id和支付记录Id不一样"), payOutput);
                        }
                    }
                }

                #endregion 通用安全验证

                #region 支付金额校验

                // 如果订单方式商城订单，则和商城订单表Shop_order的金额进行核对验证
                if (pay.Type == CheckoutType.Order) {
                    var payShopOrders = _payRepository.GetOrderPayAccount(orderIds);
                    if (!pay.Amount.EqualsDigits(payShopOrders.Sum(r => r.PaymentAmount))) {
                        return Tuple.Create(ServiceResult.FailedWithMessage("支付金额与订单金额不一致"), payOutput);
                    }

                    foreach (var payPair in pay.AccountPayPair) {
                        var value = 0.0m;
                        foreach (var order in payShopOrders) {
                            value += order.AccountPayPair.Where(r => r.Key == payPair.Key).Sum(r => r.Value);
                        }

                        if (!value.EqualsDigits(payPair.Value)) {
                            return Tuple.Create(ServiceResult.FailedWithMessage("账户支付金额不一致"), payOutput);
                        }
                    }
                }

                ////安全验证 如果是充值 ordersIds   为tradeId 2018524
                //if (pay.Type == CheckoutType.Recharge) {
                //    var tradeIds = pay.EntityId.DeserializeJson<List<long>>();
                //    if (tradeIds == null || tradeIds.Count < 1) {
                //        return Tuple.Create(ServiceResult.FailedWithMessage("实体交易已不存在"), payOutput);
                //    }

                //    var payTrades = Resolve<ITradeService>().GetList(r => tradeIds.Contains(r.Id));
                //    if (!pay.Amount.EqualsDigits(payTrades.Sum(r => r.Amount))) {
                //        return Tuple.Create(ServiceResult.FailedWithMessage("支付金额与订单金额不一致"), payOutput);
                //    }
                //}

                #endregion 支付金额校验

                #endregion 订单支付安全验证

                #region 支付之前处理事务

                //包括1. 冻结客户的虚拟资产（人民币资产除外），防止客户进行其他的操作
                //在支付之前冻结账户支付资产,不能和支付成功的步骤放到一起
                TreezeBeforePay(pay, user);

                #endregion 支付之前处理事务

                #region 开始支付

                switch (payInput.PayType) {
                    case PayType.BalancePayment: {
                            result = BalancePayment(ref pay); // 现金账户余额支付
                            if (result.Succeeded) {
                                pay.PayType = PayType.BalancePayment;

                                var callback = _payRepository.AfterPay(orderIds, pay, result.Succeeded);
                                if (!callback.Succeeded) {
                                    payOutput.Message += "回调失败!";
                                }

                                if (orderIds.Count > 1) {
                                    payOutput.Url = "/pages/user?path=order_index";
                                } else {
                                    payOutput.Url = $"/pages/index?path=order_show&id={orderIds.FirstOrDefault()}";
                                    payOutput.Message = $"/pages/index?path=order_show&id={orderIds.FirstOrDefault()}";
                                }
                                // 如果前台跳转不为空
                                if (!pay.PayExtension.RedirectUrl.IsNullOrEmpty()) {
                                    payOutput.Url = pay.PayExtension.RedirectUrl;
                                }
                            }

                            break;
                        }

                    case PayType.AlipayWeb:
                        payResult = AlipayWebPayment(ref pay, url, serviceUrl, orderIds.FirstOrDefault()); // 支付宝电脑端支付
                        result = payResult.Item1;
                        payOutput = payResult.Item2;
                        //payResult = AlipayWapPayment(ref pay, url, serviceUrl);// 支付宝PC端支付
                        //result = payResult.Item1;
                        //payOutput = payResult.Item2;
                        break;

                    case PayType.AlipayWap:
                        payResult = AlipayWapPayment(ref pay, url, serviceUrl, orderIds.FirstOrDefault()); // 支付宝手机端支付
                        result = payResult.Item1;
                        payOutput = payResult.Item2;
                        break;

                    case PayType.AlipayApp://AlipayApp

                        payResult = AlipayAppPayment(ref pay, url, serviceUrl, orderIds.FirstOrDefault()); // 支付宝手机APP支付
                        result = payResult.Item1;
                        payOutput = payResult.Item2;

                        break;

                    //case PayType.AliPayQrCode:
                    //    break;

                    //case PayType.AliPayBar:
                    //    break;

                    case PayType.WeChatPayPublic:
                        payResult = WechatPublicPayment(ref pay, payInput.OpenId, url, serviceUrl); // 微信公众号支付
                        result = payResult.Item1;
                        payOutput = payResult.Item2;
                        break;

                    //case PayType.WeChatPayApp:
                    //   var wechatAppResult = WechatAppPayment(ref pay,  url, serviceUrl);//微信APP支付
                    //    result = wechatAppResult.Item1;
                    //    payOutput = wechatAppResult.Item2;

                    //    break;

                    //case PayType.WeChatPayBar:
                    //    break;

                    //case PayType.WeChatPayQrCode:
                    //    break;

                    //case PayType.WeChatPayWap:
                    //    break;

                    case PayType.WeChatPayLite:
                        payResult = WechatLitePayment(ref pay, payInput.OpenId, url); // 微信公众号支付
                        //Resolve<IPayService>().Log(payResult.ToJson(),LogsLevel.Warning);
                        result = payResult.Item1;
                        payOutput = payResult.Item2;
                        break;

                    //case PayType.QPayBar:
                    //    break;

                    //case PayType.JdPayBar:
                    //    break;

                    //case PayType.YeePayBar:
                    //    break;

                    //case PayType.EPayBar:
                    //    break;

                    //case PayType.PaypalWeb:
                    //    break;

                    //case PayType.PaypalWap:
                    //    break;

                    case PayType.AdminPay: {
                            result = BalancePayment(ref pay); // 管理员代付
                            if (result.Succeeded) {
                                pay.PayType = PayType.AdminPay;
                                result = _payRepository.AfterPay(orderIds, pay, result.Succeeded);
                                if (orderIds.Count > 1) {
                                    payOutput.Url = $"/pages/index?path=order_list";
                                } else {
                                    payOutput.Url = $"/pages/index?path=order_show&id={orderIds.FirstOrDefault()}";
                                }
                            }

                            break;
                        }
                }

                #endregion 开始支付

                payOutput.EntityIds = orderIds;
                payOutput.PayId = payInput.PayId;
                if (!pay.PayExtension.RedirectUrl.IsNullOrEmpty()) {
                    payOutput.Url = pay.PayExtension.RedirectUrl;
                }
            } catch (Exception ex) {
                Resolve<IPayService>().Log("支付异常:" + ex.Message);
            }

            return Tuple.Create(result, payOutput);
        }

        /// <summary>
        /// </summary>
        /// <param name="pay"></param>
        /// <param name="isSucess"></param>
        public ServiceResult AfterPay(Pay pay, bool isSucess) {

            #region 通用安全验证

            if (pay == null && pay.Status != PayStatus.WaiPay) {
                return ServiceResult.FailedWithMessage("支付记录不存在，或已处理");
            }

            if (pay.EntityId == null) {
                return ServiceResult.FailedWithMessage("实体订单已不存在");
            }

            var orderIds = pay.EntityId.DeserializeJson<List<object>>();
            if (orderIds == null || orderIds.Count < 1) {
                return ServiceResult.FailedWithMessage("实体订单已不存在");
            }

            if (pay.Type == CheckoutType.Recharge) {
                //var trade = Resolve<ITradeService>().GetList(e => orderIds.Contains(e.Id)).FirstOrDefault();
                //return _payRepository.AfterPay(pay, isSucess, trade);
            }

            return _payRepository.AfterPay(orderIds, pay, isSucess);

            #endregion 通用安全验证
        }

        /// <summary>
        ///     Gets the page list.
        /// </summary>
        /// <param name="query">查询</param>
        /// <returns>
        ///     PagedList&lt;Pay&gt;.
        /// </returns>
        public PagedList<Pay> GetPageList(object query) {
            var PageList = Resolve<IPayService>().GetPagedList(query);
            return PageList;
        }

        #region 在支付之前冻结资产

        /// <summary>
        ///     在支付之前冻结资产
        ///     防止用户进行其他的操作
        /// </summary>
        /// <param name="pay"></param>
        /// <param name="user">用户</param>
        private ServiceResult TreezeBeforePay(Pay pay, Users.Entities.User user) {
            var result = ServiceResult.Success;
            var moneyTypes = Resolve<IAutoConfigService>().MoneyTypes();
            var context = Repository<IBillRepository>().RepositoryContext;
            context.BeginTransaction();
            try {
                foreach (var payPair in pay.AccountPayPair) {
                    var moneyConfig = moneyTypes.FirstOrDefault(r => r.Id == payPair.Key);
                    if (payPair.Value > 0) {
                        result = Resolve<IBillService>().TreezeSingle(user, moneyConfig, payPair.Value,
                            $"支付订单(编号{pay.PayExtension.TradeNo})前冻结{moneyConfig.Name}资产");
                        if (!result.Succeeded) {
                            return result;
                        }
                    }
                }

                context.SaveChanges();
                context.CommitTransaction();
            } catch (Exception ex) {
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("虚拟资产冻结失败:" + ex.Message);
            } finally {
                context.DisposeTransaction();
            }

            return result;
        }

        #endregion 在支付之前冻结资产

        #region 现金账户余额支付

        /// <summary>
        ///     现金账户余额支付
        /// </summary>
        private ServiceResult BalancePayment(ref Pay pay) {
            var userId = pay.UserId;
            //var userAccount = Resolve<IAccountService>().GetAccount(pay.UserId, Currency.Cny); //获取现金账户
            var userAccount = Resolve<IAccountService>().GetSingle(r =>
                r.UserId == userId && r.MoneyTypeId == Guid.Parse("E97CCD1E-1478-49BD-BFC7-E73A5D699000"));
            if (pay.Amount < 0) {
                return ServiceResult.FailedWithMessage("支付金额不能小于0");
            }

            if (userAccount == null) {
                return ServiceResult.FailedWithMessage("现金账户不存在，或者余额不足");
            }

            if (userAccount.Amount < pay.Amount) {
                return ServiceResult.FailedWithMessage("余额不足，请充值");
            }

            var actionType = BillActionType.Shopping;
            if (pay.Type == CheckoutType.Recharge) {
                actionType = BillActionType.Recharge;
            }

            if (pay.Type == CheckoutType.GradeBuy) {
                actionType = BillActionType.Declaration;
            }

            if (pay.Type == CheckoutType.Customer) {
                actionType = BillActionType.Custommer;
            }

            var bill = Resolve<IBillService>()
                .CreateBill(userAccount, -pay.Amount, actionType, $"支付订单(编号{pay.PayExtension.TradeNo})");
            var context = Repository<IBillRepository>().RepositoryContext;
            context.BeginTransaction();
            try {
                userAccount.Amount = userAccount.Amount - pay.Amount;
                Resolve<IAccountService>().Update(userAccount); // 减少支出
                Resolve<IBillService>().Add(bill);

                context.SaveChanges();
                context.CommitTransaction();

                // 如果支付成功，重新赋值pay
                pay.ResponseSerial = bill.Serial;
                pay.Status = PayStatus.Success;
                pay.ResponseTime = DateTime.Now;
            } catch (Exception ex) {
                //支付失败
                pay.Status = PayStatus.Failured;
                pay.ResponseTime = DateTime.Now;
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage($"支付失败:{ex.Message}");
            } finally {
                context.DisposeTransaction();
            }

            return ServiceResult.Success;
        }

        #endregion 现金账户余额支付

        #region 支付宝手机端支付

        /// <summary>
        ///     支付宝wap支付
        /// </summary>
        private Tuple<ServiceResult, PayOutput> AlipayWebPayment(ref Pay pay, string url, string serviceUrl, object orderId) {
            var serverUrl = "https://openapi.alipay.com/gateway.do";
            var payOutput = new PayOutput();
            var config = Ioc.Resolve<IAutoConfigService>().GetValue<AlipayPaymentConfig>();
            if (!config.IsEnable) {
                return Tuple.Create(ServiceResult.FailedWithMessage("支付宝配置未启用，请在后台开启"), payOutput);
            }

            if (config.RsaPrivateKey.IsNullOrEmpty() || config.RsaAlipayPublicKey.IsNullOrEmpty()) {
                return Tuple.Create(ServiceResult.FailedWithMessage("支付宝私钥或支付宝秘钥为空，请在后台配置"), payOutput);
            }

            var client = new AlipayClient(serverUrl, config.AppId, config.RsaPrivateKey);
            //var request = new AlipayTradeWapPayRequest();
            var request = new AlipayTradePagePayRequest();

            request.SetBizModel(new AlipayTradePagePayModel {
                OutTradeNo = pay.PayExtension.TradeNo,
                ProductCode = "FAST_INSTANT_TRADE_PAY",
                TotalAmount = pay.Amount.ToString(),
                Subject = $"支付订单(编号{pay.PayExtension.TradeNo})",
                PassbackParams = pay.Id.ToString()
            });

            var orderIds = pay.EntityId.DeserializeJson<List<object>>();
            if (pay.Type == CheckoutType.Order) {
                if (orderIds.Count > 1) {
                    request.SetReturnUrl($"{url}/pages/index?path=order_list");
                } else {
                    request.SetReturnUrl($"{url}/pages/index?path=order_show&id={orderIds.FirstOrDefault()}");
                }
            }

            if (pay.Type == CheckoutType.Recharge) {
                request.SetReturnUrl($"{url}{pay.PayExtension.RedirectUrl}");
                request.SetNotifyUrl($"{serviceUrl}/Pay/WapPayAsync");
            } else {
                if (!pay.PayExtension.RedirectUrl.IsNullOrEmpty()) {
                    request.SetReturnUrl($"{url}{pay.PayExtension.RedirectUrl}");
                    request.SetNotifyUrl($"{serviceUrl}/Api/BookingSignup/WapPayAsync");
                } else {
                    request.SetNotifyUrl($"{serviceUrl}/Pay/WapPayAsync");
                }
            }
            var response = new AlipayTradePagePayResponse();
            try {
                Task.Run(async () => {
                    response = await client.SdkExecuteAsync(request);
                })
                    .GetAwaiter()
                    .GetResult();
            } catch (Exception ex) {
                return Tuple.Create(ServiceResult.FailedWithMessage("支付宝请求异常" + ex.Message), payOutput);
            }

            payOutput = new PayOutput {
                Message = $"{serverUrl}?{response.Body}",
                OrderId = orderId,
                Url = $"{serverUrl}?{response.Body}", // 前台跳转网址
                Status = PayStatus.WaiPay
            };
            return Tuple.Create(ServiceResult.Success, payOutput);
        }

        /// <summary>
        ///     支付宝wap支付
        /// </summary>
        private Tuple<ServiceResult, PayOutput> AlipayWapPayment(ref Pay pay, string url, string serviceUrl, object orderId) {
            var serverUrl = "https://openapi.alipay.com/gateway.do";
            var payOutput = new PayOutput();
            var config = Ioc.Resolve<IAutoConfigService>().GetValue<AlipayPaymentConfig>();
            if (!config.IsEnable) {
                return Tuple.Create(ServiceResult.FailedWithMessage("支付宝配置未启用，请在后台开启"), payOutput);
            }

            if (config.RsaPrivateKey.IsNullOrEmpty() || config.RsaAlipayPublicKey.IsNullOrEmpty()) {
                return Tuple.Create(ServiceResult.FailedWithMessage("支付宝私钥或支付宝秘钥为空，请在后台配置"), payOutput);
            }

            var client = new AlipayClient(serverUrl, config.AppId, config.RsaPrivateKey);
            var request = new AlipayTradeWapPayRequest();
            request.SetBizModel(new AlipayTradeWapPayModel {
                TotalAmount = pay.Amount.ToString(),
                Subject = $"支付订单(编号{pay.PayExtension.TradeNo})",
                OutTradeNo = pay.PayExtension.TradeNo,
                PassbackParams = pay.Id.ToString()
            });

            var orderIds = pay.EntityId.DeserializeJson<List<object>>();
            if (pay.Type == CheckoutType.Order) {
                if (orderIds.Count > 1) {
                    request.SetReturnUrl($"{url}/pages/index?path=order_list");
                } else {
                    request.SetReturnUrl($"{url}/pages/index?path=order_show&id={orderIds.FirstOrDefault()}");
                }
            }

            if (pay.Type == CheckoutType.Recharge) {
                if (orderIds.Count > 1) {
                    request.SetReturnUrl($"{url}/pages/index?path=Asset_recharge_list");
                } else {
                    request.SetReturnUrl($"{url}/pages/index?path=Asset_recharge_list");
                }
                request.SetNotifyUrl($"{serviceUrl}/Pay/WapPayAsync");
            } else {
                if (!pay.PayExtension.RedirectUrl.IsNullOrEmpty()) {
                    request.SetReturnUrl($"{url}{pay.PayExtension.RedirectUrl}");
                    request.SetNotifyUrl($"{serviceUrl}/Api/BookingSignup/WapPayAsync");
                } else {
                    request.SetNotifyUrl($"{serviceUrl}/Pay/WapPayAsync");
                }
            }
            var response = new AlipayTradeWapPayResponse();
            try {
                Task.Run(async () => {
                    response = await client.SdkExecuteAsync(request);
                })
                    .GetAwaiter()
                    .GetResult();
            } catch (Exception ex) {
                return Tuple.Create(ServiceResult.FailedWithMessage("支付宝请求异常" + ex.Message), payOutput);
            }

            payOutput = new PayOutput {
                Message = $"{serverUrl}?{response.Body}",
                OrderId = orderId,
                Url = $"{serverUrl}?{response.Body}", // 前台跳转网址
                Status = PayStatus.WaiPay
            };
            return Tuple.Create(ServiceResult.Success, payOutput);
        }

        /// <summary>
        ///     支付宝手机APP支付
        /// </summary>
        private Tuple<ServiceResult, PayOutput> AlipayAppPayment(ref Pay pay, string url, string serviceUrl, object orderId) {
            var serverUrl = "https://openapi.alipay.com/gateway.do";
            var payOutput = new PayOutput();
            var config = Ioc.Resolve<IAutoConfigService>().GetValue<AlipayPaymentConfig>();
            if (!config.IsEnable) {
                return Tuple.Create(ServiceResult.FailedWithMessage("支付宝配置未启用，请在后台开启"), payOutput);
            }

            if (config.RsaPrivateKey.IsNullOrEmpty() || config.RsaAlipayPublicKey.IsNullOrEmpty()) {
                return Tuple.Create(ServiceResult.FailedWithMessage("支付宝私钥或支付宝秘钥为空，请在后台配置"), payOutput);
            }
            var request = new AlipayTradeAppPayRequest();

            var orderIds = pay.EntityId.DeserializeJson<List<object>>();
            //if (pay.Type == CheckoutType.Order)
            //{
            //    if (orderIds.Count > 1)
            //    {
            //        request.SetReturnUrl($"{url}/pages/index?path=order_list");
            //    }
            //    else
            //    {
            //        request.SetReturnUrl($"{url}/pages/index?path=order_show&id={orderIds.FirstOrDefault()}");
            //    }
            //}

            if (pay.Type == CheckoutType.Recharge) {
                //app支付没有 Returnurl

                //if (orderIds.Count > 1)
                //{
                //    request.SetReturnUrl($"{url}/pages/index?path=Asset_recharge_list");
                //}
                //else
                //{
                //    request.SetReturnUrl($"{url}/pages/index?path=Asset_recharge_list");
                //}
                request.SetNotifyUrl($"{serviceUrl}/Pay/WapPayAsync");
            } else {
                if (!pay.PayExtension.RedirectUrl.IsNullOrEmpty()) {
                    //request.SetReturnUrl($"{url}{pay.PayExtension.RedirectUrl}");
                    request.SetNotifyUrl($"{serviceUrl}/Api/BookingSignup/WapPayAsync");
                } else {
                    request.SetNotifyUrl($"{serviceUrl}/Pay/WapPayAsync");
                }
            }
            var client = new AlipayClient(serverUrl, config.AppId, config.RsaPrivateKey);
            // var client = new AlipayClient(serverUrl, config.AppId, config.RsaPrivateKey, "json", "1.0", "RSA2", config.RsaAlipayPublicKey);

            //request = new AlipayTradeAppPayRequest();

            request.SetBizModel(new AlipayTradeAppPayModel {
                TotalAmount = pay.Amount.ToString("0.00"),
                Subject = $"APP Order(No:{pay.PayExtension.TradeNo})",//支付宝app支付有中文转义乱码问题
                OutTradeNo = pay.PayExtension.TradeNo,
                PassbackParams = pay.Id.ToString(),
                ProductCode = "QUICK_MSECURITY_PAY",
                //TotalAmount= pay.Amount.ToString("0.00")
            });

            request.SetNotifyUrl($"{serviceUrl}/Pay/WapPayAsync");

            var response = new AlipayTradeAppPayResponse();
            try {
                Task.Run(async () => {
                    response = await client.SdkExecuteAsync(request);
                })
                    .GetAwaiter()
                    .GetResult();
            } catch (Exception ex) {
                return Tuple.Create(ServiceResult.FailedWithMessage("支付宝请求异常" + ex.Message), payOutput);
            }

            payOutput = new PayOutput {
                Message = response.Body,// response.Body.Split('&').ToDictionary(k => k.Split('=')[0], v => v.Split('=')[1]).ToJson(),// $"{serverUrl}?{response.Body}",
                OrderId = orderId,
                Url = $"{serverUrl}?{response.Body}", //
                Status = PayStatus.WaiPay
            };
            return Tuple.Create(ServiceResult.Success, payOutput);
        }

        #endregion 支付宝手机端支付

        #region 微信公众号支付

        /// <summary>
        ///     微信公众号支付
        /// </summary>
        private Tuple<ServiceResult, PayOutput> WechatPublicPayment(ref Pay pay, string openId, string url,
            string serviceUrl) {
            //Resolve<IPayService>().Log($"微信公众支付参数pay:{Newtonsoft.Json.JsonConvert.SerializeObject(pay)},openId:{openId},url:{url},serviceUrl:{serviceUrl}");

            if (openId.IsNullOrEmpty()) {
                var userId = pay.UserId;
                var userDetail = Resolve<IUserDetailService>().GetSingle(r => r.UserId == userId);
                if (userDetail != null) {
                    openId = userDetail.OpenId;
                }
                if (openId.IsNullOrEmpty()) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("OpenId未空"), new PayOutput());
                }
            }
            var payOutput = new PayOutput();
            var config = Resolve<IAutoConfigService>().GetValue<WeChatPaymentConfig>();
            if (!config.IsEnable) {
                return Tuple.Create(ServiceResult.FailedWithMessage("微信配置未启用，请在后台开启"), payOutput);
            }

            if (config.AppSecret.IsNullOrEmpty() || config.APISecretKey.IsNullOrEmpty()) {
                return Tuple.Create(ServiceResult.FailedWithMessage("微信公众号AppSecret或商户号API秘钥为空，请在后台配置"), payOutput);
            }

            try {
                var returnUrl = string.Empty;
                var orderIds = pay.EntityId.DeserializeJson<List<object>>();
                if (orderIds.Count > 1) {
                    returnUrl = $"{url}/pages/index?path=order_list";
                } else {
                    returnUrl = $"{url}/pages/index?path=order_show&id={orderIds.FirstOrDefault()}";
                }
                //Resolve<IPayService>().Log($"returnUrl:{returnUrl}");

                var client = new WeChatPayClient(config.AppId, config.AppSecret, config.MchId, config.APISecretKey);
                serviceUrl = config.CallBackUrl;

                var request = new WeChatPayUnifiedOrderRequest {
                    TotalFee = (int)(pay.Amount * 100),
                    TradeType = "JSAPI",
                    Body = $"支付订单(编号{pay.PayExtension.TradeNo})",
                    OutTradeNo = pay.PayExtension.TradeNo + RandomHelper.Number(1000, 9999),
                    SpbillCreateIp = HttpWeb.Ip,
                    NotifyUrl = $"{HttpWeb.ServiceHost}/Pay/PublicPayAsync",
                    OpenId = openId,
                    Attach = pay.Id.ToString()
                };
                //购买活动设置回调
                if (!pay.PayExtension.ReturnUrl.IsNullOrEmpty()) {
                    request.NotifyUrl = $"{HttpWeb.ServiceHost}/Api/BookingSignup/PublicPayAsync";
                }

                Resolve<IPayService>().Log("微信公众支付请求参数:" + request.ToJson(), LogsLevel.Warning);
                WeChatPayDictionary sortedTxtParams = new WeChatPayDictionary(request.GetParameters())
                {
                    {
                        "appid",
                        config.AppId
                    },
                    {
                        "mch_id",
                        config.MchId
                    },
                    {
                        "nonce_str",
                        Guid.NewGuid().ToString("N")
                    }
                };
                sortedTxtParams.Add("sign",
                    Md5.GetMD5WithKey((SortedDictionary<string, string>)sortedTxtParams, config.APISecretKey, true));

                var response = new WeChatPayUnifiedOrderResponse();
                try {
                    Task.Run(async () => {
                        response = await client.ExecuteAsync(request);
                    })
                        .GetAwaiter()
                        .GetResult();

                    //Resolve<IPayService>().Log("微信公众支付返回" + response.ToJson());
                } catch (Exception ex) {
                    Resolve<IPayService>().Log("微信请求异常" + ex.Message);
                    return Tuple.Create(ServiceResult.FailedWithMessage("微信请求异常" + ex.Message), payOutput);
                }

                // 组合"调起支付接口"所需参数 :
                var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var timeStamp = (long)(DateTime.Now - unixEpoch).TotalSeconds;

                var dic = new WeChatPayDictionary
                {
                    {"appId", config.AppId},
                    {"timeStamp", timeStamp.ToString()},
                    {"nonceStr", Guid.NewGuid().ToString("N")},
                    {"package", $"prepay_id={response.PrepayId}"},
                    {"signType", "MD5"}
                };
                dic.Add("paySign", Md5.GetMD5WithKey(dic, config.APISecretKey));
                var result = dic.ToJson();

                payOutput = new PayOutput {
                    Message = result, // response.ToJson(), 如果调试前端出错时，尝试将信息输入到前端
                    Url = returnUrl,
                    Status = PayStatus.WaiPay
                };

                //Resolve<IPayService>().Log($"微信公众支付返回:{Newtonsoft.Json.JsonConvert.SerializeObject(payOutput)}");
            } catch (Exception ex) {
                Resolve<IPayService>().Log(ex.Message);
            }
            return Tuple.Create(ServiceResult.Success, payOutput);
        }

        #endregion 微信公众号支付

        #region 微信app支付

        /// <summary>
        ///     微信公众号支付
        /// </summary>
        public Tuple<ServiceResult, object> WechatAppPayment(ref Pay pay, string url,
            string serviceUrl) {

            #region openid

            //Resolve<IPayService>().Log($"微信公众支付参数pay:{Newtonsoft.Json.JsonConvert.SerializeObject(pay)},openId:{openId},url:{url},serviceUrl:{serviceUrl}");
            //app 不判断openid

            //if (openId.IsNullOrEmpty())
            //{
            //    var userId = pay.UserId;
            //    var userDetail = Resolve<IUserDetailService>().GetSingle(r => r.UserId == userId);
            //    if (userDetail != null)
            //    {
            //        openId = userDetail.OpenId;
            //    }
            //    if (openId.IsNullOrEmpty())
            //    {
            //        return Tuple.Create(ServiceResult.FailedWithMessage("OpenId未空"), new PayOutput());
            //    }
            //}

            #endregion openid

            var orderInfo = new object();
            var config = Resolve<IAutoConfigService>().GetValue<AppWeChatPaymentConfig>();
            if (!config.IsEnable) {
                return Tuple.Create(ServiceResult.FailedWithMessage("微信配置未启用，请在后台开启"), orderInfo);
            }

            if (config.AppSecret.IsNullOrEmpty() || config.APISecretKey.IsNullOrEmpty()) {
                return Tuple.Create(ServiceResult.FailedWithMessage("微信AppSecret或商户号API秘钥为空，请在后台配置"), orderInfo);
            }
            var noncestr = Guid.NewGuid().ToString("N");
            try {
                var returnUrl = string.Empty;
                var orderIds = pay.EntityId.DeserializeJson<List<object>>();
                if (orderIds.Count > 1) {
                    returnUrl = $"{url}/pages/index?path=order_list";
                } else {
                    returnUrl = $"{url}/pages/index?path=order_show&id={orderIds.FirstOrDefault()}";
                }
                //Resolve<IPayService>().Log($"returnUrl:{returnUrl}");

                var client = new WeChatPayClient(config.AppId, config.AppSecret, config.MchId, config.APISecretKey);
                //serviceUrl = config.CallBackUrl;

                var request = new WeChatPayUnifiedOrderRequest {
                    TotalFee = (int)(pay.Amount * 100),
                    TradeType = "APP",
                    Body = $"支付订单(编号{pay.PayExtension.TradeNo})",
                    OutTradeNo = pay.PayExtension.TradeNo + RandomHelper.Number(1000, 9999),
                    SpbillCreateIp = HttpWeb.Ip,
                    NotifyUrl = $"{HttpWeb.ServiceHost}/Pay/PublicPayAsync",
                    // OpenId = openId,
                    Attach = pay.Id.ToString()
                };

                //购买活动设置回调
                if (!pay.PayExtension.ReturnUrl.IsNullOrEmpty()) {
                    request.NotifyUrl = $"{HttpWeb.ServiceHost}/Api/BookingSignup/PublicPayAsync";
                }
                //Resolve<IPayService>().Log("微信公众支付请求参数:" + request.ToJson());
                WeChatPayDictionary sortedTxtParams = new WeChatPayDictionary(request.GetParameters())
            {
                {
                    "appid",
                    config.AppId
                },
                {
                    "mch_id",
                    config.MchId
                },
                {
                    "nonce_str",
                    noncestr
                }
            };
                sortedTxtParams.Add("sign",
                    Md5.GetMD5WithKey((SortedDictionary<string, string>)sortedTxtParams, config.APISecretKey, true));

                var response = new WeChatPayUnifiedOrderResponse();
                try {
                    Task.Run(async () => {
                        response = await client.ExecuteAsync(request);
                    })
                        .GetAwaiter()
                        .GetResult();

                    //Resolve<IPayService>().Log("微信公众支付返回" + response.ToJson());
                } catch (Exception ex) {
                    Resolve<IPayService>().Log("微信请求异常" + ex.Message);
                    return Tuple.Create(ServiceResult.FailedWithMessage("微信请求异常" + ex.Message), new object());
                }

                // 组合"调起支付接口"所需参数 :
                var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var timeStamp = (long)(DateTime.Now - unixEpoch).TotalSeconds;

                var dic = new WeChatPayDictionary
                {
                    //{"appId", config.AppId},
                    //{"timeStamp", timeStamp.ToString()},
                    //{"nonceStr", noncestr},
                    //{"package", $"prepay_id={response.PrepayId}"},
                    //{"signType", "MD5"}
                    { "appid" , config.AppId },
                    { "noncestr" , noncestr },
                    { "package" , "Sign=WXPay" },
                    { "partnerid" , config.MchId },
                    { "prepayid" , response.PrepayId },
                    { "timestamp" , timeStamp },
                };
                //dic.Add("paySign", Md5.GetMD5WithKey(dic, config.APISecretKey));
                // var result = dic.ToJson();

                //payOutput = new PayOutput
                //{
                //    Message = result, // response.ToJson(), 如果调试前端出错时，尝试将信息输入到前端
                //    Url = returnUrl,
                //    Status = PayStatus.WaiPay
                //};
                orderInfo = new {
                    appid = config.AppId,
                    noncestr = noncestr,
                    package = "Sign=WXPay",
                    partnerid = config.MchId,
                    prepayid = response.PrepayId,
                    timestamp = timeStamp,
                    sign = Md5.GetMD5WithKey(dic, config.APISecretKey)
                };
                //Resolve<IPayService>().Log($"微信公众支付返回:{Newtonsoft.Json.JsonConvert.SerializeObject(payOutput)}");
            } catch (Exception ex) {
                Resolve<IPayService>().Log(ex.Message);
            }
            return Tuple.Create(ServiceResult.Success, orderInfo);
        }

        #endregion 微信app支付

        #region 微信小程序支付

        /// <summary>
        ///     微信公众号支付
        /// </summary>
        private Tuple<ServiceResult, PayOutput> WechatLitePayment(ref Pay pay, string openId, string url) {
            if (openId.IsNullOrEmpty()) {
                var userId = pay.UserId;
                var userDetail = Resolve<IUserDetailService>().GetSingle(r => r.UserId == userId);
                if (userDetail != null) {
                    openId = userDetail.OpenId;
                }
                if (openId.IsNullOrEmpty()) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("OpenId未空"), new PayOutput());
                }
            }
            var payOutput = new PayOutput();
            var mpConfig = Resolve<IAutoConfigService>().GetValue<MiniProgramConfig>();
            var weixinConfig = Resolve<IAutoConfigService>().GetValue<WeChatPaymentConfig>();
            if (!mpConfig.IsEnable) {
                return Tuple.Create(ServiceResult.FailedWithMessage("微信配置未启用，请在后台开启"), payOutput);
            }

            if (mpConfig.AppSecret.IsNullOrEmpty() || weixinConfig.APISecretKey.IsNullOrEmpty()) {
                return Tuple.Create(ServiceResult.FailedWithMessage("微信公众号AppSecret或商户号API秘钥为空，请在后台配置"), payOutput);
            }

            var returnUrl = string.Empty;
            var orderIds = pay.EntityId.DeserializeJson<List<object>>();
            if (orderIds.Count > 1) {
                returnUrl = $"{url}/order/list";
            } else {
                returnUrl = $"{url}/pages/index?path=order_show&id={orderIds.FirstOrDefault()}";
            }

            var client = new WeChatPayClient(mpConfig.AppID, mpConfig.AppSecret, weixinConfig.MchId, weixinConfig.APISecretKey);

            var webSite = Resolve<IAutoConfigService>().GetValue<WebSiteConfig>();

            var request = new WeChatPayUnifiedOrderRequest {
                TotalFee = (int)(pay.Amount * 100),
                TradeType = "JSAPI",
                Body = $"支付订单(编号{pay.PayExtension.TradeNo})",
                OutTradeNo = pay.PayExtension.TradeNo + RandomHelper.Number(1000, 9999),
                SpbillCreateIp = HttpWeb.Ip,
                NotifyUrl = $"{HttpWeb.ServiceHost}/Pay/PublicPayAsync",
                OpenId = openId,
                Attach = pay.Id.ToString()
            };

            //购买活动设置回调
            if (!pay.PayExtension.ReturnUrl.IsNullOrEmpty()) {
                request.NotifyUrl = $"{HttpWeb.ServiceHost}/Api/BookingSignup/PublicPayAsync";
            }

            WeChatPayDictionary sortedTxtParams = new WeChatPayDictionary(request.GetParameters())
            {
                {
                    "appid",
                    mpConfig.AppID
                },
                {
                    "mch_id",
                    weixinConfig.MchId
                },
                {
                    "nonce_str",
                    Guid.NewGuid().ToString("N")
                }
            };
            sortedTxtParams.Add("sign",
                Md5.GetMD5WithKey((SortedDictionary<string, string>)sortedTxtParams, weixinConfig.APISecretKey, true));

            //Resolve<IPayService>().Log("微信公众支付参数" + sortedTxtParams.ToJson());
            var response = new WeChatPayUnifiedOrderResponse();
            try {
                Task.Run(async () => {
                    response = await client.ExecuteAsync(request);
                })
                    .GetAwaiter()
                    .GetResult();

                //Resolve<IPayService>().Log("微信公众支付返回" + response.ToJson());
            } catch (Exception ex) {
                return Tuple.Create(ServiceResult.FailedWithMessage("微信请求异常" + ex.Message), payOutput);
            }

            // 组合"调起支付接口"所需参数 :
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var timeStamp = (long)(DateTime.Now - unixEpoch).TotalSeconds;

            var dic = new WeChatPayDictionary
            {
                {"appId", mpConfig.AppID},
                {"timeStamp", timeStamp.ToString()},
                {"nonceStr", Guid.NewGuid().ToString("N")},
                {"package", $"prepay_id={response.PrepayId}"},
                {"signType", "MD5"}
            };
            dic.Add("paySign", Md5.GetMD5WithKey(dic, weixinConfig.APISecretKey));
            var result = dic.ToJson();

            payOutput = new PayOutput {
                Message = result, // response.ToJson(), 如果调试前端出错时，尝试将信息输入到前端
                Url = returnUrl,
                Status = PayStatus.WaiPay
            };
            return Tuple.Create(ServiceResult.Success, payOutput);
        }

        #endregion 微信小程序支付

        #region 退款

        /// <summary>
        /// 微信退款
        /// </summary>
        /// <param name="pay"></param>
        /// <param name="fee">退款金额,单位:分</param>
        /// <param name="refundNo">退款id</param>
        /// <returns></returns>
        public Tuple<ServiceResult, string> Refund(ref Pay pay, int fee, string refundNo) {
            if (pay.PayType == PayType.WeChatPayApp
                || pay.PayType == PayType.WeChatPayBar
                || pay.PayType == PayType.WeChatPayLite
                || pay.PayType == PayType.WeChatPayPublic
                || pay.PayType == PayType.WeChatPayQrCode) {
                var wechatRefundResult = WechatRefund(ref pay, fee, refundNo);

                //请求结果  <return_code><![CDATA[SUCCESS]]></return_code>
                if (wechatRefundResult.Item2.ToUpper().IndexOf("<return_code><![CDATA[SUCCESS]]></return_code>".ToUpper()) <= -1) {
                    return Tuple.Create(ServiceResult.FailedMessage("请求微信退款失败"), string.Empty);
                }
                //判断返回退款是否有异常     <result_code><![CDATA[SUCCESS]]></result_code>
                if (wechatRefundResult.Item2.ToUpper().IndexOf("<result_code><![CDATA[FAIL]]></result_code>".ToUpper()) >= 0) {
                    return Tuple.Create(ServiceResult.FailedMessage("微信退款失败"), string.Empty);
                }

                return wechatRefundResult;
            } else if (pay.PayType == PayType.BalancePayment) {
                return BalanceRefund(ref pay, fee, refundNo);
            } else if (pay.PayType == PayType.AlipayApp
                  || pay.PayType == PayType.AliPayBar
                  || pay.PayType == PayType.AliPayQrCode
                  || pay.PayType == PayType.AlipayWap
                  || pay.PayType == PayType.AlipayWeb
                  ) {
                return AliRefund(ref pay, fee, refundNo);
            } else {
                return Tuple.Create(ServiceResult.FailedMessage("暂不支持该退款方式"), string.Empty);
            }
        }

        /// <summary>
        /// 余额退款
        /// </summary>
        /// <param name="pay"></param>
        /// <param name="fee">退款金额,单位:分</param>
        /// <param name="refundNo">退款id</param>
        /// <returns></returns>
        public Tuple<ServiceResult, string> BalanceRefund(ref Pay pay, int fee, string refundNo) {
            var result = ServiceResult.Success;

            var moneyConfigs = Resolve<IAutoConfigService>().MoneyTypes();
            var moneyConfig = moneyConfigs.FirstOrDefault(s => s.Id == MoneyTypeConfig.CNY);

            var user = Resolve<IUserService>().GetSingle(pay.UserId);

            var refundMsg = $"会员订单{pay.PayExtension.TradeNo}退款";

            var res = Resolve<IBillService>().Increase(user, moneyConfig, (fee / 100m), refundMsg);
            if (res.Succeeded) {
                return Tuple.Create(res, refundMsg);
            } else {
                return Tuple.Create(ServiceResult.FailedMessage("余额退款失败"), refundMsg);
            }
        }

        /// <summary>
        /// 微信退款
        /// </summary>
        /// <param name="pay"></param>
        /// <param name="fee">退款金额,单位:分</param>
        /// <param name="refundNo">退款id</param>
        /// <returns></returns>
        public Tuple<ServiceResult, string> WechatRefund(ref Pay pay, int fee, string refundNo) {
            var result = ServiceResult.Success;
            var mpConfig = Resolve<IAutoConfigService>().GetValue<MiniProgramConfig>();
            var weixinConfig = Resolve<IAutoConfigService>().GetValue<WeChatPaymentConfig>();
            if (!mpConfig.IsEnable) {
                return Tuple.Create(ServiceResult.FailedMessage("微信配置未启用，请在后台开启"), string.Empty);
            }

            if (mpConfig.AppSecret.IsNullOrEmpty() || weixinConfig.APISecretKey.IsNullOrEmpty()) {
                return Tuple.Create(ServiceResult.FailedMessage("微信公众号AppSecret或商户号API秘钥为空，请在后台配置"), string.Empty);
            }

            var data = string.Empty;

            var nonce_str = Guid.NewGuid().ToString("N");

            var messageExtension = pay.Message.ToObject<WxMessage>();
            var out_trade_no = messageExtension?.OutTradeNo;

            var par = $@"appid={mpConfig.AppID}&mch_id={weixinConfig.MchId}&nonce_str={nonce_str}&out_refund_no={refundNo}&out_trade_no={out_trade_no}&refund_fee={fee}&total_fee={Convert.ToInt32(pay.Amount * 100)}&key={weixinConfig.APISecretKey}";//  &transaction_id={pay.ResponseSerial}

            var sign = Md5.GetMD5(par);

            //拼接data xml参数
            data = $@"<xml>
                       <appid>{mpConfig.AppID}</appid>
                       <mch_id>{weixinConfig.MchId}</mch_id>
                       <nonce_str>{nonce_str}</nonce_str>
                       <out_refund_no>{refundNo}</out_refund_no>
                       <out_trade_no>{out_trade_no}</out_trade_no>
                       <refund_fee>{fee}</refund_fee>
                       <total_fee>{Convert.ToInt32(pay.Amount * 100)}</total_fee>
                       <sign>{sign}</sign>
                    </xml>";//<transaction_id>{pay.ResponseSerial}</transaction_id>
                            // <out_trade_no>{pay.ord}</out_trade_no>

            string fileDir = Environment.CurrentDirectory;
            var cert = Path.Combine(fileDir, "apiclient_cert.p12");//证书位置

            //string rootdir = AppContext.BaseDirectory;
            //DirectoryInfo Dir = Directory.GetParent(rootdir);
            //string root = Dir.Parent.Parent.FullName;
            // cert = rootdir + "/apiclient_cert.p12"; //证书位置
            string password = "1534102441";// "1522644291";//证书密码

            string refundUrl = "https://api.mch.weixin.qq.com/secapi/pay/refund";//请求地址
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            X509Certificate2 cer = new X509Certificate2(cert, password);
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(refundUrl);
            webrequest.ClientCertificates.Add(cer);
            byte[] bs = Encoding.UTF8.GetBytes(data);

            webrequest.Method = "POST";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            webrequest.ContentLength = bs.Length;
            //提交请求数据
            Stream reqStream = webrequest.GetRequestStream();
            reqStream.Write(bs, 0, bs.Length);
            reqStream.Close();
            //接收返回的页面，必须的，不能省略
            WebResponse wr = webrequest.GetResponse();
            System.IO.Stream respStream = wr.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(respStream, System.Text.Encoding.GetEncoding("utf-8"));
            string t = reader.ReadToEnd();
            wr.Close();

            //打印微信返回
            Resolve<IPayService>().Log($"微信退款返回结果:证书路径=>{cert},请求参数=> { HttpUtility.UrlEncode(data)},返回结果=>{HttpUtility.UrlEncode(t)}", LogsLevel.Error);

            return Tuple.Create(result, t);
        }

        /// <summary>
        /// 支付宝退款
        /// </summary>
        /// <param name="pay"></param>
        /// <param name="fee">退款金额,单位:分</param>
        /// <param name="refundNo">退款id</param>
        /// <returns></returns>
        public Tuple<ServiceResult, string> AliRefund(ref Pay pay, int fee, string refundNo) {
            //var serverUrl = "https://openapi.alipay.com/gateway.do";
            //var payOutput = new PayOutput();
            //var config = Ioc.Resolve<IAutoConfigService>().GetValue<AlipayPaymentConfig>();
            //if (!config.IsEnable) {
            //    return Tuple.Create(ServiceResult.FailedWithMessage("支付宝配置未启用，请在后台开启"), string.Empty);
            //}

            //if (config.RsaPrivateKey.IsNullOrEmpty() || config.RsaAlipayPublicKey.IsNullOrEmpty()) {
            //    return Tuple.Create(ServiceResult.FailedWithMessage("支付宝私钥或支付宝秘钥为空，请在后台配置"), string.Empty);
            //}
            //string retStr = string.Empty;
            //var client = new DefaultAopClient(serverUrl,
            //                                  config.AppId,
            //                                  config.RsaPrivateKey,
            //                                  "json",
            //                                  "1.0",
            //                                  "RSA2",
            //                                  config.RsaAlipayPublicKey);
            ////实例化客户端
            ////IAlipayClient client = new AlipayClient(serverUrl, config.AppId, config.RsaPrivateKey, "json", "1.0", "RSA2", config.RsaAlipayPublicKey);

            ////实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称：alipay.trade.refund
            //var request = new Aop.Api.Request.AlipayTradeRefundRequest {
            //    //创建API对应的request类
            //    //SDK已经封装掉了公共参数，这里只需要传入业务参数。以下方法为sdk的model入参方式(model和biz_content同时存在的情况下取biz_content)。

            //    //var model=new AlipayTradeRefundModel()
            //    //{
            //    //     OutRequestNo = Guid.NewGuid().ToString(),
            //    //     RefundReason = $"Order refund:{pay.PayExtension.TradeNo}",
            //    //     // 商户订单号
            //    //     OutTradeNo = pay.PayExtension.TradeNo,
            //    //     // TradeNo = ,//支付宝订单号
            //    //     //model.RefundReason= //退款原因
            //    //     RefundAmount = (fee / 100m).ToString(),//分转元 全部退款-如果是部分退款需要退款单号，可详细阅读文档

            //    // };
            //    // request.SetBizModel(model);

            //    BizContent =
            //    "{" +
            //    $"\"out_request_no\":\"{Guid.NewGuid().ToString()}\"," +
            //    $"\"refund_reason\":\"Order refund:{pay.PayExtension.TradeNo}\"," +
            //    // 商户订单号" +
            //    $"\"out_trade_no\":\"{pay.PayExtension.TradeNo}\"," +
            //    // trade_no = ,//支付宝订单号" +
            //    //RefundReason= //退款原因";
            //    $"\"refund_amount\":\"{(fee / 100m).ToString()}\"" +//分转元 全部退款-如果是部分退款需要退款单号，可详细阅读文档
            //    "}"
            //};

            //var response = new Aop.Api.Response.AlipayTradeRefundResponse();
            //try {
            //    response = client.Execute(request);

            //    //Task.Run(async () =>
            //    //{
            //    //    response = await client.SdkExecuteAsync(request);
            //    //})
            //    //   .GetAwaiter()
            //    //   .GetResult();

            //    if (response.Code == "10000") {
            //        //状态返回成功
            //        return Tuple.Create(ServiceResult.Success, $"request:{request.ToJsons()},response:{response.ToJsons()}");
            //    } else {
            //        return Tuple.Create(ServiceResult.FailedMessage(response.SubMsg), $"request:{request.ToJsons()},response:{response.ToJsons()}");
            //    }
            //} catch (Exception ex) {
            //    return Tuple.Create(ServiceResult.FailedWithMessage(ex.Message), $"request:{request.ToJsons()},response:{response.ToJsons()},退款发生异常!{refundNo} 退款失败");
            //}
            return null;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) {
            if (errors == SslPolicyErrors.None) {
                return true;
            }

            return false;
        }

        #endregion 退款

        #region 获取所有的支付方式

        /// <summary>
        ///     根据传入的参数
        ///     和 PayType 中的特性获取能够使用的支付方式
        /// </summary>
        /// <param name="parameter"></param>
        public Tuple<ServiceResult, PayTypeOutput> GetPayType(ClientInput parameter) {
            var pay = GetSingle(parameter.PayId);
            if (pay == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("支付账单不存在"), new PayTypeOutput());
            }

            if (pay.Status != PayStatus.WaiPay) {
                return Tuple.Create(ServiceResult.FailedWithMessage("支付账单已支付或取消"), new PayTypeOutput());
            }

            if (!pay.Amount.EqualsDigits(parameter.Amount)) {
                return Tuple.Create(ServiceResult.FailedWithMessage("支付账单金额不正确"), new PayTypeOutput());
            }

            if (pay.UserId != parameter.LoginUserId) {
                if (pay.PayExtension?.OrderUser?.Id != parameter.LoginUserId) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("您无权访问别人账单"), new PayTypeOutput());
                }
            }

            var user = Resolve<IUserService>().GetSingle(parameter.LoginUserId);
            if (user == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("该用户已不存在"), new PayTypeOutput());
            }

            if (user.Status != Status.Normal) {
                return Tuple.Create(ServiceResult.FailedWithMessage("用户状态不正常"), new PayTypeOutput());
            }

            var payTypeOutput = new PayTypeOutput {
                PayId = parameter.PayId,
                Amount = parameter.Amount,
                Note = pay.PayExtension.Note
            };

            if (pay.PayExtension.IsFromOrder) {
                // 线下转账给卖家
                foreach (var clientType in GetAllPayClientTypeAttribute()) {
                    if (clientType.PayType == PayType.OffineToSeller) {
                        var itemOutput = new PayTypeList {
                            Name = clientType.Name,
                            Icon = clientType.Icon,
                            Intro = clientType.Intro,
                            PayType = clientType.PayType,
                            Method = clientType.Method,
                            Id = clientType.Id
                        };
                        payTypeOutput.PayTypeList.Add(itemOutput);
                    }
                }
            } else {
                var alipayPaymentConfig = Resolve<IAutoConfigService>().GetValue<AlipayPaymentConfig>();
                var weChatPaymentConfig = Resolve<IAutoConfigService>().GetValue<WeChatPaymentConfig>();
                foreach (var clientType in GetAllPayClientTypeAttribute()) {
                    if (clientType.AllowClient.Split(',').Contains(parameter.BrowserType.ToString())) {
                        //if (parameter.BrowserType == ClientType.WeChat)
                        //{
                        //    if (clientType.AllowClient.Contains("WeChat") && clientType.PayType != PayType.WeChatPayPublic)
                        //    {  // 启用微信公众号登录
                        //        continue;
                        //    }
                        //}

                        var itemOutput = new PayTypeList {
                            Name = clientType.Name,
                            Icon = clientType.Icon,
                            Intro = clientType.Intro,
                            PayType = clientType.PayType,
                            Method = clientType.Method,
                            Id = clientType.Id
                        };
                        //储值不返回余额支付
                        if (pay.Type == CheckoutType.Recharge && itemOutput.PayType == PayType.BalancePayment) {
                            continue;
                        }

                        //阿里支付
                        if (clientType.PayType == PayType.AlipayWap && alipayPaymentConfig?.IsEnable == false) {
                            continue;
                        }
                        //微信支付
                        if (clientType.PayType == PayType.WeChatPayPublic && weChatPaymentConfig?.IsEnable == false) {
                            continue;
                        }
                        //如果使用余额支付，则获取该账户人民币的余额
                        if (itemOutput.PayType == PayType.BalancePayment) {
                            var amount = Resolve<IAccountService>().GetAccount(parameter.LoginUserId, Currency.Cny);
                            var moneyType = Resolve<IAutoConfigService>().MoneyTypes()
                                .FirstOrDefault(r => r.Currency == Currency.Cny);
                            itemOutput.Intro = $"{moneyType.Name}余额{amount.Amount}{moneyType.Unit}";
                        }
                        payTypeOutput.PayTypeList.Add(itemOutput);
                    }
                }
            }

            return Tuple.Create(ServiceResult.Success, payTypeOutput);
        }

        /// <summary>
        ///     获取所有支付方式的特性
        ///     通过缓存方式获取
        /// </summary>
        public IList<ClientTypeAttribute> GetAllPayClientTypeAttribute() {
            var cacheKey = "AllPayClientTypeAttribute";
            if (!ObjectCache.TryGet(cacheKey, out IList<ClientTypeAttribute> clientTypeAttributeList)) {
                clientTypeAttributeList = new List<ClientTypeAttribute>();
                foreach (PayType item in Enum.GetValues(typeof(PayType))) {
                    var clientType = item.GetCustomAttr<ClientTypeAttribute>();
                    clientType.PayType = item;
                    clientType.Name = item.GetDisplayName();
                    clientType.Id = item.GetFieldId();
                    if (item.GetFieldAttribute().IsDefault) {
                        clientTypeAttributeList.Add(clientType);
                    }
                }

                ObjectCache.Set(cacheKey, clientTypeAttributeList);
            }

            return clientTypeAttributeList;
        }

        #endregion 获取所有的支付方式
    }
}