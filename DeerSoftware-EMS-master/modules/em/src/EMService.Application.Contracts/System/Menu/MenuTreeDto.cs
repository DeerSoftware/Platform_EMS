using System;
using System.Collections.Generic;
using System.Text;

namespace EMService
{
    /// <summary>
    /// 菜单树Dto
    /// </summary>
    public class MenuTreeDto
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 菜单地址
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 菜单展示对象
        /// </summary>
        public MetaDto meta { get; set; }
        /// <summary>
        /// 菜单子级
        /// </summary>
        public List<MenuTreeDto> children { get; set; }
    }
    /// <summary>
    /// 菜单展示对象
    /// </summary>
    public class MetaDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string type { get { return "menu"; } }
    }
}
