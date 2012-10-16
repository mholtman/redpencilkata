using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedPencilKata.Domain
{
    public class MarkdownEngine
    {
        private readonly IMarkdownRules _rules;
        
        public MarkdownEngine(IMarkdownRules rules)
        {
            _rules = rules;
        }

        public MarkdownEngine()
        {
            _rules = new DefaultMarkdownRules();
        }

        public RedPencilItem ChangePrice(RedPencilItem item, decimal newPrice)
        {
            if (_rules.Process(item, newPrice))
            {
                return startPromotion(item, newPrice);

            }

            return item;
        }

        private RedPencilItem startPromotion(RedPencilItem item, decimal newPrice)
        {
            item.MarkedDownPrice = newPrice;
            item.PromotionStartDate = DateTime.Now;
            item.PromotionEndDate = item.PromotionStartDate.Value.AddDays(30);

            return item;
        }

    }
}
