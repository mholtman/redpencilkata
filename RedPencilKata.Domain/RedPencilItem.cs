using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedPencilKata.Domain
{
    public class RedPencilItem
    {
        private readonly IMarkdownRules _rules;
        
        public RedPencilItem(decimal originalPrice, IMarkdownRules rules)
        {
            OriginalPrice = originalPrice;
            _rules = rules;
        }

        public RedPencilItem(decimal originalPrice)
        {
            OriginalPrice = originalPrice;
            _rules = new DefaultMarkdownRules();
        }

        public decimal OriginalPrice { get;  set; }
        public decimal? MarkedDownPrice { get;  set; }
        public DateTime? PromotionStartDate { get; set; }
        public DateTime? PromotionEndDate { get;  set; }

        public bool IsPriceStable()
        {
            if (!PromotionEndDate.HasValue)
                return true;
            
            return DateTime.Now.Subtract(PromotionEndDate.Value).Days >= 30;
            
        }

    }
}
