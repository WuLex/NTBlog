using System.Data.Entity.ModelConfiguration;

namespace NTBlog.Core
{
    /// <summary>
    /// Mapping base
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseEntityTypeConfiguration<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : class
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