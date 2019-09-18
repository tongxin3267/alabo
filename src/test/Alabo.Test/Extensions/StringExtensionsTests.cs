using Xunit;
using ZKCloud.Extensions;
using ZKCloud.Test.Base.Core;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Extensions
{
    public class StringExtensionsTests : CoreTest
    {
        [Theory]
        [InlineData("Tessssssss", "s", 8)]
        [InlineData("Tesssssss", "s", 7)]
        [InlineData("张友军", "友", 1)]
        [InlineData("AccountFlow", "c", 2)]
        [TestMethod("CountChar_String_Char")]
        public void CountChar_String_Char_test(string self, string target, int count)
        {
            var result = self.CountChar(char.Parse(target));
            Assert.Equal(result, count);
        }

        [Theory]
        [InlineData("AccountFlow", "cc", "Flow", "ount")]
        [InlineData("岁加适量的开发商的发电量法世纪东方", "cc", "Flow", "")]
        [InlineData("岁加适量的开发商的发电量法世纪东方", "适量的", "东方", "开发商的发电量法世纪")]
        [InlineData("张友军", "张", "军", "友")]
        [InlineData("张友军", "张", "友", "")]
        [TestMethod("CutString_String_String_String")]
        public void CutString_String_String_String_test(string str, string beginStr, string endStr, string cutString)
        {
            var result = str.CutString(beginStr, endStr);
            Assert.Equal(result, cutString);
        }

        //  [Fact]
        [TestMethod("Filter_String")]
        public void Filter_String_test()
        {
            var sInput = "";
            var result = sInput.Filter();
            Assert.NotNull(result);
        }

        //   [Fact]
        [TestMethod("FormatWith_String_Object")]
        public void FormatWith_String_Object_test()
        {
            var self = "";
            object[] args = null;
            var result = self.FormatWith(args);
            Assert.NotNull(result);
        }

        //   [Fact]
        [TestMethod("FuzzyContains_String_String")]
        public void FuzzyContains_String_String_test()
        {
            var source = "";
            var target = "";
            var result = source.FuzzyContains(target);
            Assert.True(result);
        }

        //  [Fact]
        [TestMethod("GetBitValue_Byte_Int32")]
        public void GetBitValue_Byte_Int32_test()
        {
            byte[] bytes = null;
            var bitIndex = 0;
            var result = bytes.GetBitValue(bitIndex);
            Assert.NotNull(result);
        }

        //   [Fact]
        [TestMethod("IsEmail_String")]
        public void IsEmail_String_test()
        {
            var input = "";
            var result = input.IsEmail();
            Assert.True(result);
        }

        //  [Fact]
        [TestMethod("IsNumber_String")]
        public void IsNumber_String_test()
        {
            var input = "";
            var result = input.IsNumber();
            Assert.True(result);
        }

        //    [Fact]
        [TestMethod("IsUrl_String")]
        public void IsUrl_String_test()
        {
            var url = "";
            var result = url.IsUrl();
            Assert.True(result);
        }

        //   [Fact]
        [TestMethod("ParseUrl_String_String")]
        public void ParseUrl_String_String_test()
        {
            var url = "";
            string baseUrl = null;
            var result = StringExtensions.ParseUrl(url, out baseUrl);
            Assert.NotNull(result);
        }

        //[Fact]
        [TestMethod("ReplaceRegex_String_String_String")]
        public void ReplaceRegex_String_String_String_test()
        {
            var input = "";
            var oldString = "";
            var newString = "";
            var result = input.ReplaceRegex(oldString, newString);
            Assert.NotNull(result);
        }

        // [Fact]
        [TestMethod("SetBitValue_Byte_Int32_Boolean_Action1")]
        public void SetBitValue_Byte_Int32_Boolean_Action1_test()
        {
            byte[] bytes = null;
            var bitIndex = 0;
            var value = false;
            bytes.SetBitValue(bitIndex, value, null);
        }

        //  [Fact]
        [TestMethod("ToBoolean_Object")]
        public void ToBoolean_Object_test()
        {
            object val = null;
            var result = val.ToBoolean();
            Assert.True(result);
        }

        //   [Fact]
        [TestMethod("ToEncoding_String_String")]
        public void ToEncoding_String_String_test()
        {
            var str = "";
            var charset = "";
            var result = str.ToEncoding(charset);
            Assert.NotNull(result);
        }

        //  [Fact]
        [TestMethod("ToGuid_Object")]
        public void ToGuid_Object_test()
        {
            object obj = null;
            var result = obj.ToGuid();
            Assert.True(result.IsGuidNullOrEmpty());
        }

        // [Fact]
        [TestMethod("ToInt_String_Boolean")]
        public void ToInt_String_Boolean_test()
        {
            var input = "";
            var throwExceptionIfFailed = false;
            var result = input.ToInt(throwExceptionIfFailed);
            Assert.True(result > 0);
        }

        //  [Fact]
        [TestMethod("ToInt64_Object")]
        public void ToInt64_Object_test()
        {
            object val = null;
            var result = val.ToInt64();
            Assert.True(result > 0);
        }

        //  [Fact]
        [TestMethod("ToLong_String_Int64")]
        public void ToLong_String_Int64_test()
        {
            var input = "";
            var defaultValue = 0;
            var result = input.ToLong(defaultValue);
            Assert.True(result > 0);
        }

        //   [Fact]
        [TestMethod("ToSafeObjectId_String")]
        public void ToSafeObjectId_String_test()
        {
            var str = "";
            var result = str.ToSafeObjectId();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("DecodeAsBase64_String_Byte")]
        public void DecodeAsBase64_String_Byte_test()
        {
            var self = "";
            byte[] defaultValue = null;
            var result = self.DecodeAsBase64(defaultValue);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("DecodeAsHexdigest_String_Byte")]
        public void DecodeAsHexdigest_String_Byte_test()
        {
            var self = "";
            byte[] defaultValue = null;
            var result = self.DecodeAsHexdigest(defaultValue);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Deserialize_String_T")]
        public void Deserialize_String_T_test()
        {
            //var str = "";
            //T t = null;
            //var result = str.Deserialize(t);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Deserialize_String")]
        public void Deserialize_String_test()
        {
            //var str = "";
            //var result = StringExtensions.Deserialize(str);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("EncodeToBase64_Byte")]
        public void EncodeToBase64_Byte_test()
        {
            byte[] self = null;
            var result = self.EncodeToBase64();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("EncodeToHexdigest_Byte")]
        public void EncodeToHexdigest_Byte_test()
        {
            byte[] self = null;
            var result = self.EncodeToHexdigest();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("FuzzyEquals_String_String")]
        public void FuzzyEquals_String_String_test()
        {
            var source = "";
            var target = "";
            var result = source.FuzzyEquals(target);
            Assert.True(result);
        }

        [Fact]
        [TestMethod("InputTexts_String")]
        public void InputTexts_String_test()
        {
            var text = "";
            var result = text.InputTexts();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("IsDeserialize_String")]
        public void IsDeserialize_String_test()
        {
            //var str = "";
            //var result = StringExtensions.IsDeserialize(str);
            //Assert.True(result);
        }

        [Fact]
        [TestMethod("IsGuidNullOrEmpty_Object")]
        public void IsGuidNullOrEmpty_Object_test()
        {
            object str = null;
            var result = str.IsGuidNullOrEmpty();
            Assert.True(result);
        }

        [Fact]
        [TestMethod("IsNullOrEmpty_Object")]
        public void IsNullOrEmpty_Object_test()
        {
            object str = null;
            var result = str.IsNullOrEmpty();
            Assert.True(result);
        }

        [Fact]
        [TestMethod("IsNullOrEmpty_ObjectId")]
        public void IsNullOrEmpty_ObjectId_test()
        {
            //ObjectId objectId = null;
            //var result = objectId.IsNullOrEmpty();
            //Assert.True(result);
        }

        [Fact]
        [TestMethod("IsNullOrEmpty_String")]
        public void IsNullOrEmpty_String_test()
        {
            var str = "";
            var result = str.IsNullOrEmpty();
            Assert.True(result);
        }

        [Fact]
        [TestMethod("IsObjectIdNullOrEmpty_Object")]
        public void IsObjectIdNullOrEmpty_Object_test()
        {
            object str = null;
            var result = str.IsObjectIdNullOrEmpty();
            Assert.True(result);
        }

        [Fact]
        [TestMethod("Join_IEnumerable1_String_String")]
        public void Join_IEnumerable1_String_String_test()
        {
            //var quotes = "";
            //var separator = "";
            //var result = StringExtensions.Join(null, quotes, separator);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("LeftString_String_Int32_String")]
        public void LeftString_String_Int32_String_test()
        {
            var str = "";
            var maxLength = 0;
            var suffix = "";
            var result = str.LeftString(maxLength, suffix);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ReplaceHtmlTag_String_Int32")]
        public void ReplaceHtmlTag_String_Int32_test()
        {
            var html = "";
            var length = 0;
            var result = html.ReplaceHtmlTag(length);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ReplaceSpace_String")]
        public void ReplaceSpace_String_test()
        {
            var source = "";
            var result = source.ReplaceSpace();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("SafeString_Object")]
        public void SafeString_Object_test()
        {
            object input = null;
            var result = input.SafeString();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("SplitBlank_String_Int32")]
        public void SplitBlank_String_Int32_test()
        {
            var text = "";
            var count = 0;
            var result = text.SplitBlank(count);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("SplitLine_String")]
        public void SplitLine_String_test()
        {
            var str = "";
            var result = str.SplitLine();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("SplitList_String_Char")]
        public void SplitList_String_Char_test()
        {
            var source = "";
            char[] splitChar = null;
            var result = source.SplitList(splitChar);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("SplitString_Object_String")]
        public void SplitString_Object_String_test()
        {
            object source = null;
            var delimiter = "";
            var result = source.SplitString(delimiter);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("SplitString_String_String_Int32")]
        public void SplitString_String_String_Int32_test()
        {
            var source = "";
            var spliter = "";
            var limit = 0;
            var result = source.SplitString(spliter, limit);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("StrSub_String_Int32")]
        public void StrSub_String_Int32_test()
        {
            var str = "";
            var strLength = 0;
            var result = str.StrSub(strLength);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Substring_String_Int32")]
        public void Substring_String_Int32_test()
        {
            var soucreStr = "";
            var len = 0;
            var result = StringExtensions.Substring(soucreStr, len);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("SubStringEnd_String_String")]
        public void SubStringEnd_String_String_test()
        {
            var str = "";
            var beginStr = "";
            var result = str.SubStringEnd(beginStr);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("SubStringEndLast_String_String")]
        public void SubStringEndLast_String_String_test()
        {
            var str = "";
            var beginStr = "";
            var result = str.SubStringEndLast(beginStr);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("SubStringLastEnd_String_String")]
        public void SubStringLastEnd_String_String_test()
        {
            var str = "";
            var beginStr = "";
            var result = str.SubStringLastEnd(beginStr);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToBytes_String")]
        public void ToBytes_String_test()
        {
            var str = "";
            var result = str.ToBytes();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToCamelCaseString_String")]
        public void ToCamelCaseString_String_test()
        {
            var s = "";
            var result = s.ToCamelCaseString();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToDate_String_Boolean")]
        public void ToDate_String_Boolean_test()
        {
            var input = "";
            var throwExceptionIfFailed = false;
            var result = input.ToDate(throwExceptionIfFailed);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToDateTime_Object")]
        public void ToDateTime_Object_test()
        {
            object val = null;
            var result = val.ToDateTime();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToDecimal_Object")]
        public void ToDecimal_Object_test()
        {
            object val = null;
            var result = val.ToDecimal();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToDouble_String_Boolean")]
        public void ToDouble_String_Boolean_test()
        {
            var input = "";
            var throwExceptionIfFailed = false;
            var result = input.ToDouble(throwExceptionIfFailed);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToFristUpper_String_Boolean")]
        public void ToFristUpper_String_Boolean_test()
        {
            var str = "";
            var isLower = false;
            var result = str.ToFristUpper(isLower);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToInt16_Object")]
        public void ToInt16_Object_test()
        {
            object val = null;
            var result = val.ToInt16();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToObjectId_Object")]
        public void ToObjectId_Object_test()
        {
            object input = null;
            var result = input.ToObjectId();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToObjectId_String")]
        public void ToObjectId_String_test()
        {
            var str = "";
            var result = str.ToObjectId();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToReplace_String")]
        public void ToReplace_String_test()
        {
            var str = "";
            var result = str.ToReplace();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToSplitList_Object_String")]
        public void ToSplitList_Object_String_test()
        {
            object source = null;
            var delimiter = "";
            var result = source.ToSplitList(delimiter);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToStr_Object")]
        public void ToStr_Object_test()
        {
            object input = null;
            var result = input.ToStr();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToUrlDecode_String")]
        public void ToUrlDecode_String_test()
        {
            var str = "";
            var result = str.ToUrlDecode();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToUrlEncode_String")]
        public void ToUrlEncode_String_test()
        {
            var str = "";
            var result = str.ToUrlEncode();
            Assert.NotNull(result);
        }

        /*end*/
    }
}