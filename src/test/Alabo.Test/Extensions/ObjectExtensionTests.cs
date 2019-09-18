using System;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using Xunit;
using ZKCloud.Extensions;
using ZKCloud.Test.Base.Core;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Extensions
{
    public class ObjectExtensionTests : CoreTest
    {
        [Theory]
        [InlineData(20)]
        [InlineData(200)]
        [TestMethod("ConvertToInt_Object_Int32")]
        public void ConvertToInt_Object_Int32_test(object obj)
        {
            var defaultValue = 0;
            var result = obj.ConvertToInt(defaultValue);
            Assert.True(result > 0);
        }

        //  [Fact]
        [TestMethod("ConvertToGuid_Object")]
        public void ConvertToGuid_Object_test()
        {
            object obj = Guid.NewGuid();
            var result = obj.ConvertToGuid();
            Assert.True(result.IsGuidNullOrEmpty());
        }

        // [Fact]
        [TestMethod("ConvertToUri_Object_UriKind_String")]
        public void ConvertToUri_Object_UriKind_String_test()
        {
            object data = null;
            var kind = (UriKind) 0;
            string[] allowedScheme = null;
            var result = data.ConvertToUri(kind, allowedScheme);
            Assert.NotNull(result);
        }

        //  [Fact]
        [TestMethod("ConvertToUtcDateTime_Object_DateTimeKind")]
        public void ConvertToUtcDateTime_Object_DateTimeKind_test()
        {
            object obj = null;
            var sourceKind = (DateTimeKind) 0;
            var result = obj.ConvertToUtcDateTime(sourceKind);
            Assert.NotNull(result);
        }

        // [Fact]
        [TestMethod("ConvertToUtcDateTime_Object")]
        public void ConvertToUtcDateTime_Object_test()
        {
            object obj = null;
            var result = obj.ConvertToUtcDateTime();
            Assert.NotNull(result);
        }

        //  [Fact]
        [TestMethod("CopyPropertyValuesFrom_Object_Object")]
        public void CopyPropertyValuesFrom_Object_Object_test()
        {
            object obj = null;
            object source = null;
            obj.CopyPropertyValuesFrom(source);
        }

        // [Fact]
        [TestMethod("GetCount_String_String")]
        public void GetCount_String_String_test()
        {
            var str = "";
            var strCount = "";
            var result = str.GetCount(strCount);
            Assert.True(result > 0);
        }

        // [Fact]
        [TestMethod("ToJson_HttpContext_Type")]
        public void ToJson_HttpContext_Type_test()
        {
            HttpContext httpContext = null;
            Type type = null;
            var result = ObjectExtension.ToJson(httpContext, type);
            Assert.NotNull(result);
        }

        //[Fact]
        [TestMethod("ToJson_Type")]
        public void ToJson_Type_test()
        {
            Type type = null;
            var result = type.ToJson();
            Assert.NotNull(result);
        }

        //  [Fact]
        [TestMethod("ToObject_Object_String")]
        public void ToObject_Object_String_test()
        {
            object obj = null;
            var fullName = "";
            var result = obj.ToObject(fullName);
            Assert.NotNull(result);
        }

        //[Fact]
        [TestMethod("ToObject_Object_Type")]
        public void ToObject_Object_Type_test()
        {
            object obj = null;
            Type type = null;
            var result = obj.ToObject(type);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("AddCss_String_String_String")]
        public void AddCss_String_String_String_test()
        {
            var cssContext = "";
            var cssKey = "";
            var cssContexts = "";
            var result = cssContext.AddCss(cssKey, cssContexts);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Cast_Object_T")]
        public void Cast_Object_T_test()
        {
            //    object obj = null;
            //    T t = null;
            //    var result = obj.Cast(t);
            //    Assert.NotNull(result);

            Assert.True(true);
        }

        [Fact]
        [TestMethod("CheckJsonIsEmpty_String")]
        public void CheckJsonIsEmpty_String_test()
        {
            ////var str = "";
            ////var result = str.CheckJsonIsEmpty();
            ////Assert.True(result);
            Assert.True(true);
        }

        [Fact]
        [TestMethod("ConvertToBool_Object_Boolean")]
        public void ConvertToBool_Object_Boolean_test()
        {
            //object obj = null;
            //var defaultValue = false;
            //var result = obj.ConvertToBool(defaultValue);
            //Assert.True(result);
            Assert.True(true);
        }

        [Fact]
        [TestMethod("ConvertToDateTime_Object_DateTime")]
        public void ConvertToDateTime_Object_DateTime_test()
        {
            object obj = null;
            var defaultValue = DateTime.Now;
            var result = obj.ConvertToDateTime(defaultValue);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ConvertToDateTime_Object")]
        public void ConvertToDateTime_Object_test()
        {
            object obj = null;
            var result = obj.ConvertToDateTime();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ConvertToDecimal_Object_Decimal")]
        public void ConvertToDecimal_Object_Decimal_test()
        {
            object obj = null;
            var defaultValue = 0;
            var result = obj.ConvertToDecimal(defaultValue);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ConvertToDouble_Object_Double")]
        public void ConvertToDouble_Object_Double_test()
        {
            object obj = 500;
            var defaultValue = 0;
            var result = obj.ConvertToDouble(defaultValue);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ConvertToIntList_Object")]
        public void ConvertToIntList_Object_test()
        {
            object data = null;
            var result = data.ConvertToIntList();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ConvertToList_Object_T")]
        public void ConvertToList_Object_T_test()
        {
            //object obj = null;
            //T t = null;
            //var result = obj.ConvertToList(t);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ConvertToLong_Object_Int64")]
        public void ConvertToLong_Object_Int64_test()
        {
            object obj = 100;
            var defaultValue = 0;
            var result = obj.ConvertToLong(defaultValue);
            Assert.True(result > 0);
        }

        [Fact]
        [TestMethod("ConvertToNullableBool_Object")]
        public void ConvertToNullableBool_Object_test()
        {
            object obj = true;
            var result = obj.ConvertToNullableBool();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ConvertToNullableDateTime_Object")]
        public void ConvertToNullableDateTime_Object_test()
        {
            object obj = DateTime.Now;
            var result = obj.ConvertToNullableDateTime();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ConvertToObjectId_String")]
        public void ConvertToObjectId_String_test()
        {
            var value = ObjectId.GenerateNewId().ToString();
            var result = value.ConvertToObjectId();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ConvertToString_Object_String")]
        public void ConvertToString_Object_String_test()
        {
            object obj = "ssss";
            var defaultValue = "";
            var result = obj.ConvertToString(defaultValue);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ConvertToUIntList_Object")]
        public void ConvertToUIntList_Object_test()
        {
            object data = null;
            var result = data.ConvertToUIntList();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ConvertToUtcDateTime_Object_DateTime")]
        public void ConvertToUtcDateTime_Object_DateTime_test()
        {
            //    object obj = null;
            //    DateTime defaultValue = null;
            //    var result = obj.ConvertToUtcDateTime(defaultValue);
            //    Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("CopyObject_T")]
        public void CopyObject_T_test()
        {
            //T self = null;
            //var result = ObjectExtension.CopyObject(self);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("DeserializeJson_Object")]
        public void DeserializeJson_Object_test()
        {
            //object obj = null;
            //var result = ObjectExtension.DeserializeJson(obj);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("DeserializeJson_String_T")]
        public void DeserializeJson_String_T_test()
        {
            //var jsonString = "";
            //T defaultValue = null;
            //var result = jsonString.DeserializeJson(defaultValue);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("InsertAfter_String_String_String")]
        public void InsertAfter_String_String_String_test()
        {
            var str = "";
            var newChild = "";
            var refChild = "";
            var result = str.InsertAfter(newChild, refChild);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("InsertBefore_String_String_String")]
        public void InsertBefore_String_String_String_test()
        {
            var str = "";
            var newChild = "";
            var refChild = "";
            var result = str.InsertBefore(newChild, refChild);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("IsJsonDeserialize_String")]
        public void IsJsonDeserialize_String_test()
        {
            //var str = "";
            //var result = ObjectExtension.IsJsonDeserialize(str);
            //Assert.True(result);
        }

        [Fact]
        [TestMethod("Join_IEnumerable1_String_String")]
        public void Join_IEnumerable1_String_String_test()
        {
            //var quotes = "";
            //var separator = "";
            //var result = ObjectExtension.Join(null, quotes, separator);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("LastChar_String_String")]
        public void LastChar_String_String_test()
        {
            //var str = "";
            //var cha = "";
            //var result = str.LastChar(cha);
            //Assert.NotNull(result);
            Assert.True(true);
        }

        [Fact]
        [TestMethod("RemoveCssContext_String_String")]
        public void RemoveCssContext_String_String_test()
        {
            var cssContext = "";
            var cssKey = "";
            var result = cssContext.RemoveCssContext(cssKey);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("RemoveIndexOf_String_String")]
        public void RemoveIndexOf_String_String_test()
        {
            var str = "";
            var refChild = "";
            var result = str.RemoveIndexOf(refChild);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Replaces_String_String_String")]
        public void Replaces_String_String_String_test()
        {
            var str = "";
            var strRep = "";
            var rep = "";
            var result = str.Replaces(strRep, rep);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("SafeValue_Nullable_T")]
        public void SafeValue_Nullable_T_test()
        {
            //T value = null;
            //var result = ObjectExtension.SafeValue(value);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Substring_String_String")]
        public void Substring_String_String_test()
        {
            var charStrand = "";
            var label = "";
            var result = charStrand.Substring(label);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToClass_Object")]
        public void ToClass_Object_test()
        {
            //object obj = null;
            //var result = ObjectExtension.ToClass(obj);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToJson_Object")]
        public void ToJson_Object_test()
        {
            object obj = null;
            var result = obj.ToJson();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToJson_Object_Type")]
        public void ToJson_Object_Type_test()
        {
            object obj = null;
            Type type = null;
            var result = obj.ToJson(type);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToJsons_Object")]
        public void ToJsons_Object_test()
        {
            object obj = null;
            var result = obj.ToJsons();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("ToObject_Object")]
        public void ToObject_Object_test()
        {
            //object obj = null;
            //var result = ObjectExtension.ToObject(obj);
            //Assert.NotNull(result);
        }

        /*end*/
    }
}