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

        public decimal OriginalPrice { get; private set; }
        public decimal? MarkedDownPrice { get; private set; }
        public DateTime? PromotionStartDate { get; private set; }
        public DateTime? PromotionEndDate { get; private set; }


        public void ChangePrice(decimal newPrice)
        {
            if(_rules.Process(this, newPrice))
            {
                MarkedDownPrice = newPrice;
                PromotionStartDate = DateTime.Now;
                PromotionEndDate = DateTime.Now.AddDays(30);
            }
            
            
        }
    }
}
