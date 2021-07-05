using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace EMService.System.Price
{
    [Table("EMS_Sys_Price")]
    public class Price:FullAuditedAggregateRoot<Guid>
    {
        //单价类型
        [NotNull]
        [Required]
        public string PriceType { get; set; }
        //类型编码
        public string TypeCode { get; set; }
        //单价
        public string UnitPrice { get; set; }
        //基地 Base
        public Guid BaseId { get; set; }

        public Price()
        {

        }

        public Price(Guid id,string priceType,string typeCode,string unitPrice,Guid baseId)
        {
            this.Id = id;
            this.PriceType = priceType;
            this.TypeCode = typeCode;
            this.UnitPrice = unitPrice;
            this.BaseId = baseId;
        } 


    }
}
