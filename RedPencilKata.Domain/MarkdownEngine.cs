using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedPencilKata.Domain
{
    public class MarkdownEngine
    {
        private readonly IMarkdownRule[] _startRules;
        private readonly IMarkdownRule[] _endRules;
        private readonly IMarkdownRule[] _continueRules;
        
        public MarkdownEngine(IMarkdownRule[] startRules, IMarkdownRule[] continueRules, IMarkdownRule[] endRules)
        {
            _startRules = startRules;
            _continueRules = continueRules;
            _endRules = endRules;
        }

        public MarkdownEngine()
        {
            _startRules = new IMarkdownRule[] {new LowerBoundRule(), new UpperBoundRule(), new StablePriceRule()};
            _continueRules = new IMarkdownRule[] {new LowerBoundRule(), new UpperBoundRule()};
            _endRules = new IMarkdownRule[] {new NoIncreaseRule() };
        }

        public RedPencilItem ChangePrice(RedPencilItem item, decimal newPrice)
        {

            if (_endRules.All(x => x.Process(item, newPrice)))
            {
                return endPromotion(item);
            }
            
            if (_startRules.All(x => x.Process(item, newPrice)))
            {
                return startPromotion(item, newPrice);
            }
            
            if (_continueRules.All(x => x.Process(item, newPrice)))
            {
                return continuePromotion(item, newPrice);
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

        private RedPencilItem continuePromotion(RedPencilItem item, decimal newPrice)
        {
            item.MarkedDownPrice = newPrice;

            return item;
        }

        private RedPencilItem endPromotion(RedPencilItem item)
        {
            item.PromotionEndDate = DateTime.Now;
            item.MarkedDownPrice = null;

            return item;
        }

    }
}
