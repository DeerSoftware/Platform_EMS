using System;
using System.Collections.Generic;
using System.Text;

namespace EMService.AssetTree.Dto
{
    public class PopMenuDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public int DeviceType { get; set; }

        /// <summary>
        /// 所有上级树
        /// </summary>
        public string TreeArea { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
