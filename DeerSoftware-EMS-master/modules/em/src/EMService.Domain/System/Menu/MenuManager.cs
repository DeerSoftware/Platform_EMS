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
    /// 菜单管理器
    /// </summary>
    public class MenuManager : DomainService
    {
        private readonly IRepository<Menu, Guid> _menuRepository;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="productRepository"></param>
        /// <param name="sequenceRepository"></param>
        public MenuManager(IRepository<Menu, Guid> menuRepository)
        {
            _menuRepository = menuRepository;
        }
        /// <summary>
        /// 创建新组织
        /// </summary>
        /// <param name="organization"></param>
        /// <returns></returns>
        public async Task<Menu> CreateAsync(Menu menu)
        {
            try
            {
                var existingProduct = await _menuRepository.FirstOrDefaultAsync(p => p.Name == menu.Name);
                if (existingProduct != null)
                {
                    throw new OrganizationCodeAlreadyExistsException(menu.Name);
                }
                return await _menuRepository.InsertAsync(new Menu(
                    GuidGenerator.Create(),
                    menu.Name,
                    menu.Url,
                    menu.ParentId,
                    menu.Sort,
                    menu.NickName,
                    menu.Icon,
                    menu.Status
                    ));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
