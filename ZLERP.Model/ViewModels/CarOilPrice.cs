using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ZLERP.Model.ViewModels
{
    public class CarOilPrice
    {
        [DisplayName("开始时间")]
        [Required]
        public DateTime BeginTime{get;set;}

        [DisplayName("结束时间")]
        [Required]
        public DateTime EndTime{get; set;}

        [DisplayName("油价(元/升)")]
        [Required]
        [Range(0.00001,float.MaxValue,ErrorMessage="油价必须大于0")]
        public Decimal Price { get; set; }
    }
}
