using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RedPencilKata.Domain;


namespace RedPencilKata.Tests.Domain
{
    [TestFixture]
    public class MarkdownEngineTests
    {
        private MarkdownEngine _engine; 
        
        [SetUp]
        public void setup()
        {
            _engine = new MarkdownEngine();
        }
        
        

        [Test]
        public void can_change_price()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            item = _engine.ChangePrice(item, 90.00m);
            Assert.AreEqual(90.00m, item.MarkedDownPrice.Value);

        }

        [Test]
        public void price_change_should_trigger_promotion()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            item = _engine.ChangePrice(item, 90.00m);
            Assert.IsTrue(item.PromotionStartDate.HasValue);
            Assert.IsTrue(item.PromotionEndDate.HasValue);
        }

        [Test]
        public void promotion_start_and_end_date_should_be_30_days_apart()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            item = _engine.ChangePrice(item, 90.00m);
            TimeSpan dateDiff = item.PromotionEndDate.Value.Subtract(item.PromotionStartDate.Value);
            Assert.AreEqual(30, dateDiff.Days);
        }

        [Test]
        public void price_change_must_be_five_percent_or_greater_of_original()
        {
            RedPencilItem item  = new RedPencilItem(100.00m);
            item = _engine.ChangePrice(item, 96.00m);
            Assert.IsFalse(item.MarkedDownPrice.HasValue);
            
            RedPencilItem item2 = new RedPencilItem(100.00m);
            item2 = _engine.ChangePrice(item2, 95.01m);
            Assert.IsFalse(item2.MarkedDownPrice.HasValue);

            RedPencilItem item3 = new RedPencilItem(100.00m);
            item3 = _engine.ChangePrice(item3, 94.99m);
            Assert.IsTrue(item3.MarkedDownPrice.HasValue);
            
        }

        [Test]
        public void price_change_must_be_equal_to_or_less_than_thirty_percent_of_original()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            item = _engine.ChangePrice(item, 70.00m);
            Assert.IsTrue(item.MarkedDownPrice.HasValue);

            RedPencilItem item2 = new RedPencilItem(100.00m);
            item2 = _engine.ChangePrice(item2, 69.99m);
            Assert.IsFalse(item2.MarkedDownPrice.HasValue);
        }

        [Test]
        public void valid_price_reduction_during_promotion_does_not_extend_promotion()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            item = _engine.ChangePrice(item, 95.00m);
            DateTime origEndDate = item.PromotionEndDate.Value;

            item = _engine.ChangePrice(item, 90.00m);
            Assert.AreEqual(origEndDate, item.PromotionEndDate.Value);
            
        }

        [Test]
        public void invalid_price_reduction_during_promotion_ends_promotion()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            item = _engine.ChangePrice(item, 91.00m);

            item = _engine.ChangePrice(item, 69.99m);

            Assert.IsFalse(item.MarkedDownPrice.HasValue);
            Assert.IsTrue(item.PromotionEndDate.HasValue);

        }

    }
}
