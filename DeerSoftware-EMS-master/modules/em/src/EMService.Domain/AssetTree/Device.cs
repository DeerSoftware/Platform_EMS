using System;
using Volo.Abp.Domain.Entities;

namespace EMService.AssetTree
{
    /// <summary>
    /// 设备表
    /// </summary>
    public class Device : AggregateRoot<Guid>
    {
        public Device()
        {

        }

        public Device(Guid key)
            : base(key)
        {

        }

        /// <summary>
        /// 位置码
        /// </summary>
        public string LocationCode { get; set; }

        /// <summary>
        /// Erp编码
        /// </summary>
        public string ErpCode { get; set; }

        /// <summary>
        /// 设备重要等级(对应数据字典的编码)
        /// </summary>
        public string ControlLevel { get; set; }

        /// <summary>
        /// 专项类别(对应数据字典的编码)
        /// </summary>
        public string Specialty { get; set; }

        /// <summary>
        /// 设备类别(需要单独表维护)
        /// </summary>
        public string DeviceCategory { get; set; }

        /// <summary>
        /// 设备种类(对应数据字典的编码)
        /// </summary>
        public string DeviceKind { get; set; }

        /// <summary>
        /// 专业(对应数据字典的编码)
        /// </summary>
        public string Profession { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 计量单位(对应数据字典的编码)
        /// </summary>
        public string MeasureUnit { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public Guid ResponsibleUserId { get; set; }

        /// <summary>
        /// 负责工程师
        /// </summary>
        public Guid ResponsibleEngineer { get; set; }

        /// <summary>
        /// 使用状态
        /// </summary>
        public int UsedState { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
