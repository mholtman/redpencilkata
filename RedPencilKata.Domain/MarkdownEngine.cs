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
        private readonly IMarkdownRule[] _endRules2;
        
        public MarkdownEngine(IMarkdownRule[] startRules, IMarkdownRule[] continueRules, IMarkdownRule[] endRules, IMarkdownRule[] endRules2)
        {
            _startRules = startRules;
            _continueRules = continueRules;
            _endRules = endRules;
            _endRules2 = endRules2;
        }

        public MarkdownEngine()
        {
            _startRules = new IMarkdownRule[] {new LowerBoundRule(), new UpperBoundRule(), new StablePriceRule()};
            _continueRules = new IMarkdownRule[] {new LowerBoundRule(), new UpperBoundRule()};
            _endRules = new IMarkdownRule[] {new PriceIncreaseRule()};
            _endRules2 = new IMarkdownRule[] {new UpperBoundRule(), new LowerBoundRule()};
        }

        public RedPencilItem ChangePrice(RedPencilItem item, decimal newPrice)
        {

            
            
            if (_startRules.All(x => x.Process(item, newPrice)))
            {
                return startPromotion(item, newPrice);
            }
            
            if (_endRules.All(x => x.Process(item, newPrice)))
            {
                return endPromotion(item);
            }

            if (_endRules2.Any(x => !x.Process(item, newPrice)))
            {
                return endPromotion(item);
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
