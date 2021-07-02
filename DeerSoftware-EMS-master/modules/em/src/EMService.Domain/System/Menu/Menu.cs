using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace EMService
{
    /// <summary>
    /// 菜单对象实体
    /// </summary>
    [Table("EMS_Sys_Menu")]
    public class Menu : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [NotNull]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 菜单地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 所属上级
        /// </summary>
        [NotNull]
        [Required]
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
        public Status Status { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public Menu() { }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name">菜单名称</param>
        /// <param name="url">菜单地址</param>
        /// <param name="parentId">所属上级</param>
        /// <param name="sort">排序</param>
        /// <param name="nickName">菜单昵称</param>
        /// <param name="icon">Icon</param>
        /// <param name="status">Status</param>
        public Menu(Guid id,[NotNull] string name, [NotNull] string url, [NotNull] Guid parentId, int sort, string nickName, string icon, Status status)
        {
            this.Id = id;
            this.Name = name;
            this.Url = url;
            this.ParentId = parentId;
            this.Sort = sort;
            this.NickName = nickName;
            this.Icon = icon;
            this.Status = status;
        }
    }
}
