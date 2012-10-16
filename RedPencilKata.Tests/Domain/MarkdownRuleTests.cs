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
        public void upperbound_rule_should_pass_for_30_percent_or_less()
        {
            UpperBoundRule rule = new UpperBoundRule();
            RedPencilItem item = new RedPencilItem(100.00m);
            

            Assert.IsTrue(rule.Process(item, 70.00m ));
        }


        [Test]
        public void uppoerbound_rule_should_fail_for_more_than_30_percent()
        {
            RedPencilItem item = new RedPencilItem(100.00m);

            Assert.IsFalse(new UpperBoundRule().Process(item, 69.99m));
        }

        [Test]
        public void lowerbound_rule_should_pass_for_5_percent_or_greater()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            
            Assert.IsTrue(new LowerBoundRule().Process(item, 95.00m));
        }

        [Test]
        public void lowerbound_rule_shoud_fail_for_less_than_5_percent()
        {
            RedPencilItem item = new RedPencilItem(100.00m);

            Assert.IsFalse(new LowerBoundRule().Process(item, 96.66m));
        }
        
    }
}
