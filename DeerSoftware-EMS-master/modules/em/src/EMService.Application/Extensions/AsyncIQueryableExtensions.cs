using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace EMService.Extensions
{
    public static class AsyncIQueryableExtensions
    {
        public static async Task<List<TEntity>> GetAllPagedAsync<TEntity>(this IQueryable<TEntity> repository,
                                                                        Func<IQueryable<TEntity>, Task<IQueryable<TEntity>>> func = null,
                                                                        int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = func != null ? await func(repository) : repository;

            return await query?.ToPagedListAsync(pageIndex, pageSize);
        }

        public static async Task<List<TEntity>> GetAllPagedAsync<TEntity>(this IQueryable<TEntity> repository,
                                                                        Func<IQueryable<TEntity>, IQueryable<TEntity>> func = null,
                                                                        int pageIndex = 0, int pageSize = int.MaxValue)

        {
            var query = func != null ? func(repository) : repository;

            return await query?.ToPagedListAsync(pageIndex, pageSize);
        }

        public static async Task<List<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> source, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            if (source == null)
                return null;

            //min allowed page size is 1
            pageSize = Math.Max(pageSize, 1);

            var totalCount = await source.CountAsync();

            var data = new List<TEntity>();

            data.AddRange(await source.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync());

            return data;
        }

        public static async Task<IPagedResult<TDestination>> GetAllPagedAsync<TSource, TDestination>(this IQueryable<TSource> repository,
                                                                                        Func<IQueryable<TSource>, Task<IQueryable<TSource>>> func = null,
                                                                                        Func<List<TSource>, List<TDestination>> mapFunc = null,
                                                                                        int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = func != null ? await func(repository) : repository;

            return await query?.ToPagedListAsync<TSource, TDestination>(mapFunc, pageIndex, pageSize);
        }

        public static async Task<IPagedResult<TDestination>> GetAllPagedAsync<TSource, TDestination>(this IQueryable<TSource> repository,
                                                                                Func<IQueryable<TSource>, IQueryable<TSource>> func = null,
                                                                                Func<List<TSource>, List<TDestination>> mapFunc = null,
                                                                                int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = func != null ? func(repository) : repository;

            return await query?.ToPagedListAsync<TSource, TDestination>(mapFunc, pageIndex, pageSize);
        }

        public static async Task<IPagedResult<TDestination>> ToPagedListAsync<TSource, TDestination>(this IQueryable<TSource> source, Func<List<TSource>, List<TDestination>> func,
                                                int pageIndex = 0, int pageSize = int.MaxValue)
        {
            if (source == null)
                return null;

            //min allowed page size is 1
            pageSize = Math.Max(pageSize, 1);

            var totalCount = await source.CountAsync();

            var data = new List<TSource>();

            data.AddRange(await source.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync());

            List<TDestination> destinations = func(data);

            var pagedResultData = new PagedResultDto<TDestination>(totalCount, destinations);

            return pagedResultData;
        }
    }
}
