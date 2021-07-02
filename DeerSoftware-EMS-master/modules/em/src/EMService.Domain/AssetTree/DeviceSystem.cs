using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace EMService.AssetTree
{
   public class DeviceSystem:AggregateRoot<Guid>
    {
        public DeviceSystem()
        {

        }
        public DeviceSystem(Guid key) : base(key)
        {

        }

        [DisplayName("系统分组")]
        public string SystemGroup { get; set; }
        [DisplayName("系统分类")]
        public string SystemClass { get; set; }
        [DisplayName("描述")]
        public string Description { get; set; }
    }
}
