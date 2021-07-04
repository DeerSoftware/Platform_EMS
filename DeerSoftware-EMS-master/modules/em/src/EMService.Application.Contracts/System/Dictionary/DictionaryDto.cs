using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EMService
{
    /// <summary>
    /// 字典Dto
    /// </summary>
    public class DictionaryDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }
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
    }
}
