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
            decimal priceDiff = item.OriginalPrice - newPrice;

            return (priceDiff/item.OriginalPrice) >= 0.05m;
            

            return true; 
        }
    }
}
