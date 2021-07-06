using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Linq.Expressions;

namespace EMService.Core.Extensions
{
    /// <summary>
    /// 实体查询扩展类
    /// </summary>
    public static class AsyncIQueryableExtensions
    {
        /// <summary>
        /// 异步分页功能
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="repository"></param>
        /// <param name="func"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<List<TEntity>> GetAllPagedAsync<TEntity>(this IQueryable<TEntity> repository,
                                                                        Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func = null,
                                                                        int pageIndex = 1, int pageSize = int.MaxValue)
        {
            var query = func != null ? await func(repository) : repository;


            return await query?.ToPagedListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 异步分页功能
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="repository"></param>
        /// <param name="func"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<List<TEntity>> GetAllPagedAsync<TEntity>(this IQueryable<TEntity> repository,
                                                                        Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
                                                                        int pageIndex = 1, int pageSize = int.MaxValue)

        {
            var query = func != null ? func(repository) : repository;

            return await query?.ToPagedListAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 异步分页扩展功能
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<List<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> source, int pageIndex = 1, int pageSize = int.MaxValue)
        {
            if (source == null)
                return null;

            //min allowed page size is 1
            pageSize = Math.Max(pageSize, 1);

            var totalCount = await source.CountAsync();

            var data = new List<TEntity>();

            data.AddRange(await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());

            return data;
        }

        /// <summary>
        /// 异步分页功能(带直接转DTO功能)
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="repository"></param>
        /// <param name="func"></param>
        /// <param name="mapFunc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<IPagedResult<TDestination>> GetAllPagedAsync<TSource, TDestination>(this IQueryable<TSource> repository,
                                                                                        Func<IQueryable<TSource>, Task<IQueryable<TSource>>> func = null,
                                                                                        Func<List<TSource>, List<TDestination>> mapFunc = null,
                                                                                        int pageIndex = 1, int pageSize = int.MaxValue)
        {
            var query = func != null ? await func(repository) : repository;

            return await query?.ToPagedListAsync<TSource, TDestination>(mapFunc, pageIndex, pageSize);
        }

        /// <summary>
        /// 异步分页功能(带直接转DTO功能)
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="repository"></param>
        /// <param name="func"></param>
        /// <param name="mapFunc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<IPagedResult<TDestination>> GetAllPagedAsync<TSource, TDestination>(this IQueryable<TSource> repository,
                                                                                Func<IQueryable<TSource>, IQueryable<TSource>> func = null,
                                                                                Func<List<TSource>, List<TDestination>> mapFunc = null,
                                                                                int pageIndex = 1, int pageSize = int.MaxValue)
        {
            var query = func != null ? func(repository) : repository;

            return await query?.ToPagedListAsync<TSource, TDestination>(mapFunc, pageIndex, pageSize);
        }

        /// <summary>
        /// 异步分页功能(带直接转DTO功能)
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="func"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<IPagedResult<TDestination>> ToPagedListAsync<TSource, TDestination>(this IQueryable<TSource> source, Func<List<TSource>, List<TDestination>> func,
                                                int pageIndex = 1, int pageSize = int.MaxValue)
        {
            if (source == null)
                return null;

            //min allowed page size is 1
            pageSize = Math.Max(pageSize, 1);

            var totalCount = await source.CountAsync();

            var data = new List<TSource>();

            data.AddRange(await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync());

            List<TDestination> destinations = func(data);

            var pagedResultData = new PagedResultDto<TDestination>(totalCount, destinations);

            return pagedResultData;
        }

        /// <summary>
        /// 排序,可传多个排序字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="sorts"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderByOrThenBy<T>(this IQueryable<T> query, IDictionary<string, object> sorts)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T));

            int index = 0;

            foreach (var sort in sorts)
            {
                //根据属性名获取属性
                var property = typeof(T).GetProperty(sort.Key);

                if (property != null)
                {
                    //创建一个访问属性的表达式
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);

                    string OrderName = "";
                    if (index > 0)
                        OrderName = sort.Value.Equals("desc") ? "ThenByDescending" : "ThenBy";
                    else
                        OrderName = sort.Value.Equals("desc") ? "OrderByDescending" : "OrderBy";

                    index++;

                    MethodCallExpression resultExp = Expression.Call(typeof(IQueryable<T>), OrderName, new Type[] { typeof(T), property.PropertyType }, 
                        query.Expression, Expression.Quote(orderByExp));

                    query = query.Provider.CreateQuery<T>(resultExp);
                }
            }
    
            return query;
        }

    }
}
