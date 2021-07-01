using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMService.AssetTree
{
    [DisplayName("系统类型分组")]
    public class SystemClass: FullAuditedAggregateRoot<Guid>
    {
        public SystemClass(Guid Key):base(Key)
        {

        }
        [DisplayName("父级")]
        public Guid ParentId { get; set; }
        [DisplayName("名称")]
        public string Name { get; set; }
        [DisplayName("编码")]
        public string Code { get; set; }
    }
}
