using System;
using Volo.Abp.Domain.Entities;

namespace EMService.AssetTree
{
    /// <summary>
    /// 测点表
    /// </summary>
    public class Point : AggregateRoot<Guid>
    {
        public Point()
        {

        }

        public Point(Guid key)
            : base(key)
        {

        }

        /// <summary>
        /// 固定编码
        /// </summary>
        public string FixedCode { get; set; }

        /// <summary>
        /// 设备重要等级(对应数据字典的编码)
        /// </summary>
        public string ControlLevel { get; set; }

        /// <summary>
        /// 专项类别(对应数据字典的编码)
        /// </summary>
        public string Specialty { get; set; }

        /// <summary>
        /// 测点工艺位号
        /// </summary>
        public string ProcessCode { get; set; }

        /// <summary>
        /// 电子标签
        /// </summary>
        public string ElecTag { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        public string AccessMode { get; set; }

        /// <summary>
        /// 单位类型(对应数据字典的编码)
        /// </summary>
        public string UnitType { get; set; }

        /// <summary>
        /// 工程单位(对应数据字典的编码)
        /// </summary>
        public string EngineeringUnit { get; set; }

        /// <summary>
        /// 采集方式(对应数据字典的编码)
        /// </summary>
        public string MeasWay { get; set; }

        /// <summary>
        /// 测量方向(对应数据字典的编码)
        /// </summary>
        public string MeasureDirect { get; set; }

        /// <summary>
        /// 电源(对应数据字典的编码)
        /// </summary>
        public string Power { get; set; }

        /// <summary>
        /// 是否生成停机
        /// </summary>
        public bool IsStoppingSignal { get; set; }

        /// <summary>
        /// 最大值 
        /// </summary>
        public decimal MaxValue { get; set; }

        /// <summary>
        /// 最小值 
        /// </summary>
        public decimal MinValue { get; set; }

        /// <summary>
        /// 参考值 
        /// </summary>
        public decimal ReferenceValue { get; set; }
    }
}
