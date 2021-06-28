using System;
using System.Text;
using System.Collections.Generic;

namespace EMService.AssetTree
{
    /// <summary>
    /// 设备类型
    /// </summary>
    public enum DeviceType
    {
        /// <summary>
        /// 集团(组织)
        /// </summary>
        Organization = 0,

        /// <summary>
        /// 基地
        /// </summary>
        Base = 1,

        /// <summary>
        /// 厂区
        /// </summary>
        Factory = 10,

        /// <summary>
        /// 区域/生产线/车间
        /// </summary>
        Area = 20,

        /// <summary>
        /// 系统
        /// </summary>
        System = 30,

        /// <summary>
        /// 设备
        /// </summary>
        Device = 40,

        /// <summary>
        /// 零部件
        /// </summary>
        Component = 50,

        /// <summary>
        /// 测点
        /// </summary>
        Point = 60
    }
}
