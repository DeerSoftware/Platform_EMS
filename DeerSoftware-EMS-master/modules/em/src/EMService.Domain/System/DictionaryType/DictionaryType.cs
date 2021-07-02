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
    /// 字典类型
    /// </summary>
    [Table("EMS_Sys_DictionaryType")]
    public class DictionaryType : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        [NotNull]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 类型编码
        /// </summary>
        [NotNull]
        [Required]
        public string Code { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 上级
        /// </summary>
        public Guid ParentId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public DictionaryType() { }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="name">类型名称</param>
        /// <param name="code">类型编码</param>
        /// <param name="sort">排序</param>
        /// <param name="parentId">上级</param>
        /// <param name="remark">备注</param>
        public DictionaryType(Guid id, string name, string code, int sort, Guid parentId, string remark)
        {
            this.Id = id;
            this.Name = name;
            this.Code = code;
            this.Sort = sort;
            this.ParentId = parentId;
            this.Remark = remark;
        }
    }
}
