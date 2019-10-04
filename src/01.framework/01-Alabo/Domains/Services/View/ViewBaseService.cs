using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Update;
using Alabo.Extensions;
using System;
using System.Collections.Generic;

namespace Alabo.Domains.Services.View {

    public abstract class ViewBaseService<TEntity, TKey> : UpdateBase<TEntity, TKey>, IViewBase<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey> {

        protected ViewBaseService(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store) {
        }

        public TEntity GetViewById(object id) {
            if (!id.IsNullOrEmpty()) {
                var model = GetSingle(id);
                if (model != null) {
                    return model;
                }
            }

            return GetDefaultModel();
        }

        private TEntity GetDefaultModel() {
            var model = Activator.CreateInstance<TEntity>();
            // model.Id ;
            CreateSubInstance(model);

            return model;
        }

        /// <summary>
        ///     创建对象
        /// </summary>
        private void CreateSubInstance(object obj) {
            //loop properties.
            var type = obj.GetType();
            foreach (var p in type.GetProperties()) {
                if (!p.CanWrite) {
                    continue;
                }

                var propertyType = p.PropertyType;
                if (IsValueType(propertyType) || propertyType.IsAbstract || propertyType.IsArray) {
                    continue;
                }

                //reference type
                if (propertyType.IsGenericType) {
                    var subItemType = propertyType.GetGenericArguments()[0];
                    //解决循环依赖创建
                    if (subItemType == type || IsValueType(subItemType)) {
                        continue;
                    }

                    var subItem = Activator.CreateInstance(subItemType);
                    CreateSubInstance(subItem);
                    var value = GetGenericValue(subItemType, subItem);
                    p.SetValue(obj, value, null);
                } else {
                    //解决循环依赖创建
                    if (propertyType == type) {
                        continue;
                    }

                    var subItem = Activator.CreateInstance(propertyType);
                    CreateSubInstance(subItem);
                    p.SetValue(obj, subItem, null);
                }
            }
        }

        /// <summary>
        ///     创建泛型集合对象
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private object GetGenericValue(Type type, object value) {
            var genericType = typeof(List<>).MakeGenericType(type);
            var result = Activator.CreateInstance(genericType);
            var addMethod = genericType.GetMethod("Add");
            addMethod.Invoke(result, new[] { value });
            return result;
        }

        /// <summary>
        ///     判断是否是值类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsValueType(Type type) {
            return type == typeof(string) ||
                   type == typeof(string) ||
                   type.IsPrimitive ||
                   type.IsValueType ||
                   type == typeof(DateTime) ||
                   type == typeof(bool);
        }
    }
}