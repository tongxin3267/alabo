using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Tables.Dtos;
using Convert = System.Convert;

namespace Alabo.Tables.Domain.Services
{
    public class SmsService : ServiceBase, ISmsService
    {
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public SmsService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public SmsEntity Sent(string phone, string content)
        {
            var res = Send(phone, $"【企牛牛】{content}");
            return res.ToObject<SmsEntity>();
        }

        private static string Send(string phone, string content)
        {
            try
            {
                var appId = "GZDL4G51N7299516832"; //账号
                var key = "8A8E1D851D816964721A3010A426636B"; //秘钥 "0OIFS";
                var sign = AESEncrypts(currentTimeMillis().ToString(), key);
                var smsdata = $"[{{\"mobile\":\"{phone}\",\"smscontent\":\"{content}\"}}]";
                var smsBody = AESEncrypts(smsdata, key);
                var body = $"appId={appId}&sign={sign}&smsData={smsBody}";
                return SendByHttpPost("https://sdk.gzdlinfo.cn/dlcpInterface/sendsms", body);
            }
            catch (Exception ex)
            {
                Ioc.Resolve<ITableService>().Log(ex.Message, LogsLevel.Error);
            }

            return string.Empty;
        }

        private static string SendByHttpPost(string Url, string postDataStr)
        {
            try
            {
                ServicePointManager.Expect100Continue = false;

                var request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postDataStr.Length;

                var write = new StreamWriter(request.GetRequestStream(), Encoding.ASCII);
                write.Write(postDataStr);
                write.Flush();
                var response = (HttpWebResponse)request.GetResponse();
                var encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1) encoding = "UTF-8";

                var reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                var retstring = reader.ReadToEnd();
                reader.Close();
                response.Close();
                return retstring;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //系统当前时间毫秒数的生成方法
        private static long currentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        /// <summary>
        ///     AES加密
        /// </summary>
        /// <param name="text">加密字符</param>
        /// <param name="password">加密的密码</param>
        /// <param name="iv">密钥</param>
        /// <returns></returns>
        private static string AESEncrypts(string Data, string Key)
        {
            // 256-AES key
            var keyArray = HexStringToBytes(Key); //UTF8Encoding.ASCII.GetBytes(Key);  //
            var toEncryptArray = Encoding.UTF8.GetBytes(Data);

            var rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;

            var cTransform = rDel.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0,
                toEncryptArray.Length);

            return BytesToHexString(resultArray);
        }

        private static string BytesToHexString(byte[] bytes)
        {
            var returnStr = new StringBuilder();
            if (bytes != null || bytes.Length == 0)
                for (var i = 0; i < bytes.Length; i++)
                    returnStr.Append(bytes[i].ToString("X2"));

            return returnStr.ToString();
        }

        /// <summary>
        ///     16 hex string converted to byte array
        /// </summary>
        /// <param name="hexString">16 hex string</param>
        /// <returns>byte array</returns>
        private static byte[] HexStringToBytes(string hexString)
        {
            if (hexString == null || string.IsNullOrEmpty(hexString)) return null;

            var length = hexString.Length / 2;
            if (hexString.Length % 2 != 0) return null;

            var d = new byte[length];
            for (var i = 0; i < length; i++) d[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);

            return d;
        }
    }
}