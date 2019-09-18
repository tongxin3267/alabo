using System.Collections.Generic;

namespace Alabo.Core.Helpers
{
    public class CharHelper
    {
        private static readonly string CnNumber = "零壹贰叁肆伍陆柒捌玖";
        private static readonly string CnUnit = "分角元拾佰仟万拾佰仟亿拾佰仟兆拾佰仟";

        public static string GetUpper(int i)
        {
            string str = null;
            switch (i)
            {
                case 1:
                    str = "一";
                    break;

                case 2:
                    str = "二";
                    break;

                case 3:
                    str = "三";
                    break;

                case 4:
                    str = "四";
                    break;

                case 5:
                    str = "五";
                    break;

                case 6:
                    str = "六";
                    break;

                case 7:
                    str = "七";
                    break;

                case 8:
                    str = "八";
                    break;

                case 9:
                    str = "九";
                    break;

                case 10:
                    str = "十";
                    break;
            }

            return str;
        }

        public static string GetCnString(string moneyString)
        {
            var tmpString = moneyString.Split('.');
            var intString = moneyString; // 默认为整数
            var decString = string.Empty; // 保存小数部分字串
            var rmbCapital = string.Empty; // 保存中文大写字串
            int k;
            int j;
            int n;

            if (tmpString.Length > 1)
            {
                intString = tmpString[0]; // 取整数部分
                decString = tmpString[1]; // 取小数部分
            }

            decString += "00";
            decString = decString.Substring(0, 2); // 保留两位小数位
            intString += decString;

            try
            {
                k = intString.Length - 1;
                if (k > 0 && k < 18)
                {
                    for (var i = 0; i <= k; i++)
                    {
                        j = intString[i] - 48;
                        // rmbCapital = rmbCapital + cnNumber[j] + cnUnit[k-i];     // 供调试用的直接转换
                        n = i + 1 >= k ? intString[k] - 48 : intString[i + 1] - 48; // 等效于 if( ){ }else{ }
                        if (j == 0)
                        {
                            if (k - i == 2 || k - i == 6 || k - i == 10 || k - i == 14)
                            {
                                rmbCapital += CnUnit[k - i];
                            }
                            else
                            {
                                if (n != 0) {
                                    rmbCapital += CnNumber[j];
                                }
                            }
                        }
                        else
                        {
                            rmbCapital = rmbCapital + CnNumber[j] + CnUnit[k - i];
                        }
                    }

                    rmbCapital = rmbCapital.Replace("兆亿万", "兆");
                    rmbCapital = rmbCapital.Replace("兆亿", "兆");
                    rmbCapital = rmbCapital.Replace("亿万", "亿");
                    rmbCapital = rmbCapital.TrimStart('元');
                    rmbCapital = rmbCapital.TrimStart('零');

                    return rmbCapital;
                }

                return string.Empty; // 超出转换范围时，返回零长字串
            }
            catch
            {
                return string.Empty; // 含有非数值字符时，返回零长字串
            }
        }

        public static int GetDays(int i, List<int> models)
        {
            var days = 0;
            for (var j = 0; j <= i; j++) {
                if (j != 0) {
                    days += models[j];
                }
            }

            return days;
        }
    }
}