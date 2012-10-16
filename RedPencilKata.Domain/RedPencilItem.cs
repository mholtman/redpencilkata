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

        public decimal OriginalPrice { get; private set; }
        public decimal? MarkedDownPrice { get; private set; }
        public DateTime? PromotionStartDate { get; private set; }
        public DateTime? PromotionEndDate { get; private set; }


        public void ChangePrice(decimal newPrice)
        {
            MarkedDownPrice = newPrice;
            PromotionStartDate = DateTime.Now;
            PromotionEndDate = DateTime.Now.AddDays(30);
        }
    }
}
