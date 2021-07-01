using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EMService
{
    /// <summary>
    /// 序列号管理器
    /// </summary>
    public class SequenceManager
    {
        private readonly IRepository<Sequence, Guid> _sequnceRepository;
        public SequenceManager(IRepository<Sequence, Guid> sequnceRepository)
        {
            _sequnceRepository = sequnceRepository;
        }
        /// <summary>
        /// 创建序号
        /// </summary>
        /// <returns></returns>
        private async Task<Sequence> CreateAsync(string tableName)
        {
            return await _sequnceRepository.InsertAsync(new Sequence() { TableName = tableName, Value = 1 });
        }

        /// <summary>
        /// 查询序号
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<Sequence> GetSequenceAsync<T>()
        {
            string tableName = typeof(T).Name;
            Sequence sequence = await _sequnceRepository.FindAsync(p => p.TableName.Equals(tableName));
            if (sequence == null)
            {
                sequence = await CreateAsync(tableName);
            }
            else
            {
                sequence.Value++;
                await _sequnceRepository.UpdateAsync(sequence);
            }
            return sequence;
        }



    }
}
