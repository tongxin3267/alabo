using System.Text;

namespace Alabo.Core.Helpers
{
    public class UserNameHelper
    {
        /// <summary>
        ///     在指定的字符串列表CnStr中检索符合拼音索引字符串
        /// </summary>
        /// <param name="CnStr">汉字字符串</param>
        /// <returns>相对应的汉语拼音首字母串</returns>
        /// <summary>
        ///     得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母
        /// </summary>
        /// <param name="CnChar">单个汉字</param>
        /// <returns>单个大写字母</returns>

        //
        public string ConvertLetters(string chineseStr)
        {
            try
            {
                var buffer = new char[chineseStr.Length];
                for (var i = 0; i < chineseStr.Length; i++) {
                    buffer[i] = ConvertChar(chineseStr[i]);
                }

                return new string(buffer).Replace(" ", string.Empty);
            }
            catch
            {
                return string.Empty;
            }
        }

        public char ConvertChar(char chinese)
        {
            try
            {
                var gb2312 = Encoding.GetEncoding("GB2312");
                var unicode = Encoding.Unicode;
                var unicodeBytes = unicode.GetBytes(new[] {chinese});
                var asciiBytes = Encoding.Convert(unicode, gb2312, unicodeBytes);
                if (asciiBytes.Length < 2) {
                    return chinese; //待转换的字符在ascii码表示范围内
                }

                var n = asciiBytes[0] << 8;
                n += asciiBytes[1];
                //   根据汉字区域码获取拼音声母
                if (In(0xB0A1, 0xB0C4, n)) {
                    return 'a';
                }

                if (In(0XB0C5, 0XB2C0, n)) {
                    return 'b';
                }

                if (In(0xB2C1, 0xB4ED, n)) {
                    return 'c';
                }

                if (In(0xB4EE, 0xB6E9, n)) {
                    return 'd';
                }

                if (In(0xB6EA, 0xB7A1, n)) {
                    return 'e';
                }

                if (In(0xB7A2, 0xB8c0, n)) {
                    return 'f';
                }

                if (In(0xB8C1, 0xB9FD, n)) {
                    return 'g';
                }

                if (In(0xB9FE, 0xBBF6, n)) {
                    return 'h';
                }

                if (In(0xBBF7, 0xBFA5, n)) {
                    return 'j';
                }

                if (In(0xBFA6, 0xC0AB, n)) {
                    return 'k';
                }

                if (In(0xC0AC, 0xC2E7, n)) {
                    return 'l';
                }

                if (In(0xC2E8, 0xC4C2, n)) {
                    return 'm';
                }

                if (In(0xC4C3, 0xC5B5, n)) {
                    return 'n';
                }

                if (In(0xC5B6, 0xC5BD, n)) {
                    return 'o';
                }

                if (In(0xC5BE, 0xC6D9, n)) {
                    return 'p';
                }

                if (In(0xC6DA, 0xC8BA, n)) {
                    return 'q';
                }

                if (In(0xC8BB, 0xC8F5, n)) {
                    return 'r';
                }

                if (In(0xC8F6, 0xCBF0, n)) {
                    return 's';
                }

                if (In(0xCBFA, 0xCDD9, n)) {
                    return 't';
                }

                if (In(0xCDDA, 0xCEF3, n)) {
                    return 'w';
                }

                if (In(0xCEF4, 0xD188, n)) {
                    return 'x';
                }

                if (In(0xD1B9, 0xD4D0, n)) {
                    return 'y';
                }

                if (In(0xD4D1, 0xD7F9, n)) {
                    return 'z';
                }

                return ' ';
            }
            catch
            {
                return ' ';
            }
        }

        private bool In(int lp, int hp, int value)
        {
            return value <= hp && value >= lp;
        }
    }
}