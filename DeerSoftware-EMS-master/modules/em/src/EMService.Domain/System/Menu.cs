using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMService
{
    /// <summary>
    /// 菜单对象实体
    /// </summary>
    public class Menu : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 菜单地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 所属上级
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 菜单昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status  { get; set; }
    }
}
