using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedPencilKata.Domain
{
    public class RedPencilItem
    {
       
       
        public RedPencilItem(decimal originalPrice)
        {
            OriginalPrice = originalPrice;
            
        }

        public decimal OriginalPrice { get;  set; }
        public decimal? MarkedDownPrice { get;  set; }
        public DateTime? PromotionStartDate { get; set; }
        public DateTime? PromotionEndDate { get;  set; }

    }
}
