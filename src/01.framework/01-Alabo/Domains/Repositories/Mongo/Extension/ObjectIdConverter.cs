﻿using Alabo.Extensions;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;

namespace Alabo.Domains.Repositories.Mongo.Extension {

    /// <summary>
    ///     [ScriptIgnore]//使用JavaScriptSerializer序列化时不序列化此字段
    ///     [IgnoreDataMember]//使用DataContractJsonSerializer序列化时不序列化此字段
    ///     [JsonIgnore]//使用JsonConvert序列化时不序列化此字段
    /// </summary>
    public sealed class ObjectIdConverter : JsonConverter {

        /// <summary>
        ///     序列化字段时
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            writer.WriteValue(value);
        }

        /// <summary>
        ///     反序列化字段时
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer) {
            if (objectType != typeof(ObjectId)) {
                if (objectType == typeof(Guid)) {
                    try {
                        return Guid.Parse(reader.Value.ToString());
                    } catch {
                        return Guid.Empty;
                    }
                }

                if (objectType == typeof(short) || objectType == typeof(int) || objectType == typeof(long)) {
                    return reader.Value.ConvertToLong(0);
                }
            }

            if (reader.ToString().IsNullOrEmpty()) {
                return null;
            }

            try {
                return ObjectId.Parse(reader.Value.ToString());
            } catch {
                return ObjectId.Empty;
            }
        }

        public override bool CanConvert(Type objectType) {
            return true;
        }
    }
}