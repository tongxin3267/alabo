using System.Globalization;
using System.Linq;
using Alabo.Helpers;

namespace Alabo.Core.Localize
{
    public struct LocalizedString
    {
        public string Text { get; }

        public LocalizedString(string text)
        {
            Text = text;
        }

        /// <summary>
        ///     获取翻译后的文本
        /// </summary>
        /// <param name="s"></param>
        public static implicit operator string(LocalizedString s)
        {
            return s.ToString();
        }

        /// <summary>
        ///     获取翻译后的文本
        /// </summary>
        public override string ToString()
        {
            // 文本是空白时不需要翻译
            if (string.IsNullOrEmpty(Text)) {
                return Text ?? string.Empty;
            }
            // 获取当前线程的语言
            var cluture = CultureInfo.CurrentCulture;
            // 获取翻译提供器并进行翻译
            // 传入 {语言}-{地区}
            var providers = Ioc.ResolveAll<ITranslateProvider>()
                .Where(p => p.CanTranslate(cluture))
                .Reverse()
                .ToArray();
            // 翻译文本，先注册的后翻译
            foreach (var provider in providers)
            {
                var translated = provider.Translate(Text);
                if (translated != null) {
                    return translated;
                }
            }

            // 没有找到翻译，返回原有的文本
            return Text ?? string.Empty;
        }
    }
}