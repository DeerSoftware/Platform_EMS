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
        /// 观察量
        /// </summary>
        Observe = 100,

        /// <summary>
        /// 转速量
        /// </summary>
        Speed = 110,

        /// <summary>
        /// 温度量
        /// </summary>
        Temperature = 120,

        /// <summary>
        /// 工艺量
        /// </summary>
        Technology = 130,

        /// <summary>
        /// 开关量
        /// </summary>
        Switch = 140,   

        /// <summary>
        /// 动态量
        /// </summary>
        Dynamic = 150,

    }
}
