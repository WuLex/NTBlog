//using System.Data.Entity.ModelConfiguration;

using NTBlogWeb.Core.Extension;

namespace NTBlogWeb.Core
{
    /// <summary>
    /// Mapping base
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseEntityTypeConfiguration<TEntity> : EfExt.EntityTypeConfiguration<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseEntityTypeConfiguration()
        {
            PostInitialize();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void PostInitialize()
        {
        }
    }
}