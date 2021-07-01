using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace EMService.AssetTree
{
    [DisplayName("设备系统")]
   public class DevSystem: AggregateRoot<Guid>
    {
        public DevSystem()
        {

        }
        public DevSystem(Guid Key):base(Key)
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
