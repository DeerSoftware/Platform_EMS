using System;
using System.Collections.Generic;
using System.Text;

namespace EMService.AssetTree.Dto
{
    /// <summary>
    /// 设备树基础数据
    /// </summary>
    public class FoundationDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 节点Id
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public int DeviceType { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所有上级树
        /// </summary>
        public string TreeArea { get; set; }

        /// <summary>
        /// 简拼
        /// </summary>
        public string JianPin { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
