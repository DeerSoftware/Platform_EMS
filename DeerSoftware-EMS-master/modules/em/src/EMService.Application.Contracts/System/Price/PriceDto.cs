using System;
using System.Collections.Generic;
using System.Text;

namespace EMService.System.Price
{
   public class PriceDto
    {
        public Guid Id { get; set; }
        // 价格类型
        public string PriceType { get; set; }
        // 类型名称
        public string PriceTypeName { get; set; }
        //类型编码
        public string TypeCode { get; set; }
        //单价
        public string UnitPrice { get; set; }
        //基地 Base
        public Guid BaseId { get; set; }
        //基地名称
        public string BaseName { get; set; }

    }
}
