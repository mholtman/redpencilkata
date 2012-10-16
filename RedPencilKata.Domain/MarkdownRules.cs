using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedPencilKata.Domain
{
    public interface IMarkdownRules
    {
        bool Process(RedPencilItem item, decimal newPrice);
    }
    
    
    public class DefaultMarkdownRules : IMarkdownRules
    {
        public bool Process(RedPencilItem item, decimal newPrice)
        {
            return honorsLowerBound(item.OriginalPrice, newPrice) && honorsUpperBound(item.OriginalPrice, newPrice);

        }

        private bool honorsLowerBound(decimal originalPrice, decimal newPrice)
        {
            decimal priceDiff = originalPrice - newPrice;

            return (priceDiff / originalPrice) >= 0.05m;
        }

        private bool honorsUpperBound(decimal originalPrice, decimal newPrice)
        {
            decimal priceDiff = originalPrice - newPrice;

            return (priceDiff/originalPrice) <= 0.30m;
        }
    }
}
