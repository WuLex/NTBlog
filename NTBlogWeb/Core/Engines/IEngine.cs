using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTBlogWeb.Core.Engines
{
    /// <summary>
    /// Ioc 引擎接口
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Get object instance
        /// </summary>
        /// <returns></returns>
        T GetInstance<T>() where T : class;

        /// <summary>
        /// Get object instance
        /// </summary>
        /// <param name="type">object type</param>
        /// <returns></returns>
        object GetInstance(Type type);

        /// <summary>
        /// Get all object instances
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable<T> GetAllInstances<T>();
    }

    /// <summary>
    /// [扩展]DI容器
    /// </summary>
    public static class EngineExtensions
    {
        /// <summary>
        /// 获取对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <returns></returns>
        public static T GetInstance<T>(this IEngine container)
        {
            return (T)container.GetInstance(typeof(T));
        }

        /// <summary>
        /// 获取对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetAllInstances<T>(this IEngine container)
        {
            return container.GetAllInstances<T>();
        }
    }
}