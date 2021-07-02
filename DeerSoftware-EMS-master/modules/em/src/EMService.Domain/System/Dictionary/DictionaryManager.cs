using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace EMService
{
    /// <summary>
    ///字典管理器
    /// </summary>
    public class DictionaryManager : DomainService
    {
        private readonly IRepository<Dictionary, Guid> _dictionaryRepository;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="dictionaryRepository"></param>
        public DictionaryManager(IRepository<Dictionary, Guid> dictionaryRepository)
        {
            _dictionaryRepository = dictionaryRepository;
        }
        /// <summary>
        /// 创建新组织
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public async Task<Dictionary> CreateAsync(Dictionary dictionary)
        {
            try
            {
                var existingProduct = await _dictionaryRepository.FirstOrDefaultAsync(p => p.Name == dictionary.Name);
                if (existingProduct != null)
                {
                    throw new OrganizationCodeAlreadyExistsException(dictionary.Name);
                }
                return await _dictionaryRepository.InsertAsync(new Dictionary(
                    GuidGenerator.Create(),
                    dictionary.Name,
                    dictionary.Code,
                    dictionary.Sort,
                    dictionary.DictionaryType
                    ));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
