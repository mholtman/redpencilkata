using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedPencilKata.Domain
{
    public interface IMarkdownRule
    {
        bool Process(RedPencilItem item, decimal newPrice);
    }
    
    
    public class UpperBoundRule : IMarkdownRule
    {
        public bool Process(RedPencilItem item, decimal newPrice)
        {
            return honorsUpperBound(item.OriginalPrice, newPrice);
        }

        private bool honorsUpperBound(decimal originalPrice, decimal newPrice)
        {
            decimal priceDiff = originalPrice - newPrice;

            return (priceDiff / originalPrice) <= 0.30m;
        }
    }

    public class LowerBoundRule : IMarkdownRule
    {
        public bool Process(RedPencilItem item, decimal newPrice)
        {
            return honorsLowerBound(item.OriginalPrice, newPrice);
        }


        private bool honorsLowerBound(decimal originalPrice, decimal newPrice)
        {
            decimal priceDiff = originalPrice - newPrice;

            return (priceDiff / originalPrice) >= 0.05m;
        }
    }

    public class NoIncreaseRule : IMarkdownRule
    {
        public bool Process(RedPencilItem item, decimal newPrice)
        {
            if (item.MarkedDownPrice.HasValue)
            {
                return item.MarkedDownPrice.Value <= newPrice;
            }

            return item.OriginalPrice <= newPrice;
        }


    }


    public class StablePriceRule : IMarkdownRule
    {
        public bool Process(RedPencilItem item, decimal newPrice)
        {
            
            if (!item.PromotionEndDate.HasValue)
                return true;
            
            return DateTime.Now.Subtract(item.PromotionEndDate.Value).Days >= 30;
            
        
        }
    }
}
