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
    /// 组织管理器
    /// </summary>
    public class DictionaryTypeManager : DomainService
    {
        private readonly IRepository<DictionaryType, Guid> _dictionaryRepository;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="dictionaryRepository"></param>
        public DictionaryTypeManager(IRepository<DictionaryType, Guid> dictionaryTypeRepository)
        {
            _dictionaryRepository = dictionaryTypeRepository;
        }
        /// <summary>
        /// 创建新字典
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public async Task<DictionaryType> CreateAsync(DictionaryType dictionary)
        {
            try
            {
                var existingProduct = await _dictionaryRepository.FirstOrDefaultAsync(p => p.Name == dictionary.Name);
                if (existingProduct != null)
                {
                    throw new OrganizationCodeAlreadyExistsException(dictionary.Name);
                }
                return await _dictionaryRepository.InsertAsync(new DictionaryType(
                    GuidGenerator.Create(),
                    dictionary.Name,
                    dictionary.Code,
                    dictionary.Sort,
                    dictionary.ParentId,
                    dictionary.Remark
                    ));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
