using Alabo.Randoms;
using Alabo.Web.Mvc.Attributes;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Alabo.Helpers
{
    public static class PasswordHelper
    {
        /// <summary>
        ///     获取指定数据的PBKDF2校验值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="slat">参与校验的随机数据，长度需要等于8</param>
        /// <param name="iterations">计算循环次数，越长强度越高但越耗费性能</param>
        /// <param name="hashLength">校验值长度</param>
        public static byte[] Pbkdf2Sum(
            byte[] data, byte[] slat, int iterations = 1024, int hashLength = 32)
        {
            var hash = new Rfc2898DeriveBytes(data, slat, iterations).GetBytes(hashLength);
            return hash;
        }

        /// <summary>
        ///     获取指定数据的Md5校验值
        public static byte[] Md5Sum(byte[] data)
        {
            return MD5.Create().ComputeHash(data);
        }

        /// <summary>
        ///     获取指定数据的Sha1校验值
        public static byte[] Sha1Sum(byte[] data)
        {
            return SHA1.Create().ComputeHash(data);
        }
    }

    /// <summary>
    ///     密码信息
    /// </summary>
    public class PasswordInfo
    {
        /// <summary>
        ///     密码类型
        /// </summary>
        public PasswordHashType Type { get; set; } = PasswordHashType.Pbkdf2;

        /// <summary>
        ///     参与校验的随机数据（base64）
        /// </summary>
        public string Slat { get; set; }

        /// <summary>
        ///     密码校验值（base64）
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        ///     检查密码，返回密码是否正确
        /// </summary>
        /// <param name="password"></param>
        public bool Check(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;

            var slat = Slat == null ? null : System.Convert.FromBase64String(Slat);
            var info = FromPassword(password, slat, Type);
            return Hash == info.Hash;
        }

        /// <summary>
        ///     从密码创建密码信息
        /// </summary>
        /// <param name="password">密码</param>
        /// <param name="slat">参与校验的随机数据，不指定时使用默认值</param>
        /// <param name="type">密码类型</param>
        public static PasswordInfo FromPassword(string password,
            byte[] slat = null, PasswordHashType type = PasswordHashType.Pbkdf2)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password can't be empty");

            var info = new PasswordInfo { Type = type };
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            if (type == PasswordHashType.Pbkdf2)
            {
                slat = slat ?? RandomHelper.SystemRandomBytes();
                info.Slat = System.Convert.ToBase64String(slat);
                info.Hash = System.Convert.ToBase64String(PasswordHelper.Pbkdf2Sum(passwordBytes, slat));
            }
            else if (type == PasswordHashType.Md5)
            {
                info.Hash = System.Convert.ToBase64String(PasswordHelper.Md5Sum(passwordBytes));
            }
            else if (type == PasswordHashType.Sha1)
            {
                info.Hash = System.Convert.ToBase64String(PasswordHelper.Sha1Sum(passwordBytes));
            }

            return info;
        }
    }

    /// <summary>
    ///     密码类型
    /// </summary>
    [ClassProperty(Name = "密码类型")]
    public enum PasswordHashType
    {
        /// <summary>
        ///     默认，等于PBKDF2
        /// </summary>
        Default = Pbkdf2,

        /// <summary>
        ///     PBKDF2
        /// </summary>
        Pbkdf2 = 0,

        /// <summary>
        ///     Md5
        /// </summary>
        Md5 = 1,

        /// <summary>
        ///     Sha1
        /// </summary>
        Sha1 = 2
    }
}