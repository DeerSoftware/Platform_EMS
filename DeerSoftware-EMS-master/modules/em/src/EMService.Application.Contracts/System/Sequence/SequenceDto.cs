using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace EMService
{
    /// <summary>
    /// 序列Dto
    /// </summary>
    public class SequenceDto : AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 序列号
        /// </summary>
        public int Value { get; set; }
    }
}
