using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations.Schema;


namespace EMService
{
    /// <summary>
    /// 角色
    /// </summary>
    [Table("EMS_Sys_Role")]
    public class Role : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }


        public Role() { }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">角色名称</param>
        /// <param name="code">角色编码</param>
        /// <param name="status">状态</param>
        /// <param name="remark">备注</param>
        public Role(Guid id, string name, string code, Status status, string remark)
        {
            this.Id = id;
            this.Name = name;
            this.Code = code;
            this.Status = status;
            this.Remark = remark;
        }
    }
}
