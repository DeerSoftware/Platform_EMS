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
    /// 字典实体
    /// </summary>
    [Table("EMS_Sys_Dictionary")]
    public class Dictionary : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 字典名称
        /// </summary>
        [NotNull]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 字典编码
        /// </summary>
        [NotNull]
        [Required]
        public string Code { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 字典类型
        /// </summary>
        [NotNull]
        [Required]
        public Guid DictionaryType { get; set; }

        public Dictionary() { }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">字典名称</param>
        /// <param name="code">字典编码</param>
        /// <param name="sort">排序</param>
        /// <param name="dictionaryType">字典类型</param>
        public Dictionary(Guid id, string name, string code, int sort, Guid dictionaryType)
        {
            this.Id = id;
            this.Name = name;
            this.Code = code;
            this.Sort = sort;
            this.DictionaryType = dictionaryType;
        }


    }
}
