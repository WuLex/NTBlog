using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NTBlogWeb.Core
{
    /// <summary>
    /// 资源库接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TId">主键类型</typeparam>
    public interface IRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Insert(TEntity entity);

        /// <summary>
        /// 批量新增数据
        /// </summary>
        /// <param name="entites"></param>
        /// <returns></returns>
        void Insert(IEnumerable<TEntity> entities);

        /// <summary>
        /// [异步]新增数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// [异步]批量新增数据
        /// </summary>
        /// <param name="entites"></param>
        /// <returns></returns>
        Task InsertAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        void Update(Expression<Func<TEntity, TEntity>> updateExpression);

        /// <summary>
        /// [异步]修改数据
        /// </summary>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        Task UpdateAsync(Expression<Func<TEntity, TEntity>> updateExpression);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(TEntity entity);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entities">Entities</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Delete(TEntity entity);

        /// <summary>
        /// [异步]删除数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        void Delete(Expression<Func<TEntity, bool>> predicate);
    }
}