using System;
using Alabo.Domains.Repositories.Mongo.Extension;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Alabo.Domains.Entities.Core
{
    /**
    * DateTime values in MongoDB are always saved as UTC
    * DateTime类型在MongoDB存储格式是UTC格式。
    *
    * =====可用属性定义 http://www.cnblogs.com/Johnzhang/p/3326820.html
    * [BsonId]
    * [BsonElementAttribute("fn")]
    * [BsonIgnore] 设置属性不映射到数据库，在读写数据库的时候，该属性就会被忽略掉
    * [BsonIgnoreExtraElements] 忽略元素不匹配目标类型的任何字段或属性
    * [BsonIgnoreIfNull] 默认为空的值序列化到 BSON 文档时对应的 BSON字段为 Null。替代方法是序列化时忽略掉具有null值的字段或属性
    * [BsonDefaultValue("abc")] 指定一个字段或属性的默认值
    * [BsonIgnoreIfDefault] 是否将默认值序列化（默认值为是）。不序列化属性默认值，搭配BsonDefaultValue时使用。
    * [BsonRequired] 确定必须字段
    *
    * 支持的数据类型
    * String : 这是最常用的数据类型来存储数据。在MongoDB中的字符串必须是有效的UTF-8。
    * Integer : 这种类型是用来存储一个数值。整数可以是32位或64位，这取决于您的服务器。
    * Boolean : 此类型用于存储一个布尔值 (true/ false) 。
    * Double : 这种类型是用来存储浮点值。
    * Min/ Max keys : 这种类型被用来对BSON元素的最低和最高值比较。
    * Arrays : 使用此类型的数组或列表或多个值存储到一个键。
    * Timestamp : 时间戳。这可以方便记录时的文件已被修改或添加。
    * Object : 此数据类型用于嵌入式的文件。
    * Null : 这种类型是用来存储一个Null值。
    * Symbol : 此数据类型用于字符串相同，但它通常是保留给特定符号类型的语言使用。
    * Date : 此数据类型用于存储当前日期或时间的UNIX时间格式。可以指定自己的日期和时间，日期和年，月，日到创建对象。
    * Object ID : 此数据类型用于存储文档的ID。
    * Binary data : 此数据类型用于存储二进制数据。
    * Code : 此数据类型用于存储到文档中的JavaScript代码。
    * Regular expression : 此数据类型用于存储正则表达式
    */

    /// <summary>
    ///     Mongo数据表接口
    /// </summary>
    public interface IMongoEntity : IEntity<ObjectId>
    {
        /// <summary>
        ///     唯一Id
        /// </summary>
        [JsonConverter(typeof(ObjectIdConverter))]
        new ObjectId Id { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        new DateTime CreateTime { get; set; }

        /// <summary>
        ///     ObjectId是否为空
        /// </summary>
        bool IsObjectIdEmpty();
    }
}