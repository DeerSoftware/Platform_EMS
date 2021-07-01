using System;
using System.Collections.Generic;
using System.Text;

namespace EMService.AssetTree.Dto
{
    public class PointDto : FoundationDto
    {
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
        /// 工艺参数标签
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
    }
}
