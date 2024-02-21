using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NTBlogWeb.Core.Engines;

namespace NTBlogWeb.Core
{
    /// <summary>
    /// 只读资源库接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TId">主键类型</typeparam>
    public interface IReadOnlyRepository<TEntity> : IDependency where TEntity : BaseEntity
    {
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns>实体</returns>
        TEntity FindById(object id);

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns>所有数据</returns>
        IList<TEntity> FindAll();

        /// <summary>
        /// Queryable查询对象
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// Queryable查询对象
        /// </summary>
        IQueryable<TEntity> TableNoTracking { get; }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>结果列表</returns>
        IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>分页结果集</returns>
        PagedList<TEntity> FindPage(int pageIndex, int pageSize);

        /// <summary>
        /// [异步]分页查询数据
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>分页结果集</returns>
        Task<PagedList<TEntity>> FindPageAsync(int pageIndex, int pageSize);

        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="predicate">查询条件</param>
        /// <returns>分页结果集</returns>
        PagedList<TEntity> FindPage(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// [异步]分页查询数据
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="predicate">查询条件</param>
        /// <returns>分页结果集</returns>
        Task<PagedList<TEntity>> FindPageAsync(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate);
    }
}