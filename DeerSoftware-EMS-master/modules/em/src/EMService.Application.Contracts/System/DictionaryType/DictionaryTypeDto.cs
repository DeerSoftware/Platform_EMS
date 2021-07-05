using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EMService
{
    /// <summary>
    /// 字典类型Dto
    /// </summary>
    public class DictionaryTypeDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }
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
    }
}
