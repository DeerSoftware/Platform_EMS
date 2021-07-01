using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EMService.System.Sequence
{
    /// <summary>
    /// 序列更新Dto
    /// </summary>
    public class UpdateSequenceDto
    {
        /// <summary>
        /// 表名
        /// </summary>
        [Required]
        public string TableName { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        public int Value { get; set; }
    }
}
