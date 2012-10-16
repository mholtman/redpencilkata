using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RedPencilKata.Domain;

namespace RedPencilKata.Tests.Domain
{
    [TestFixture]
    public class MarkdownRuleTests
    {
        [Test]
        public void upperbound_rule_test()
        {
            UpperBoundRule rule = new UpperBoundRule();
            RedPencilItem item = new RedPencilItem(100.00m);
            

            Assert.IsTrue(rule.Process(item, 70.00m ));
        }


      
        
    }
}
