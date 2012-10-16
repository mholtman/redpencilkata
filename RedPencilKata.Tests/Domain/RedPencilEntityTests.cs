using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RedPencilKata.Domain;


namespace RedPencilKata.Tests.Domain
{
    [TestFixture]
    public class RedPencilEntityTests
    {

        [Test]
        public void can_create_entity_with_price()
        {
            RedPencilItem item  = new RedPencilItem(100.00m);
            Assert.AreEqual(100.00m, item.OriginalPrice);
        }

        [Test]
        public void newly_created_item_has_nulled_markdown_price()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            Assert.IsFalse(item.MarkedDownPrice.HasValue);
        }

        [Test]
        public void can_change_price()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            item.ChangePrice(90.00m);
            Assert.AreEqual(90.00m, item.MarkedDownPrice.Value);

        }

        [Test]
        public void price_change_should_trigger_promotion()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            item.ChangePrice(90.00m);
            Assert.IsTrue(item.PromotionStartDate.HasValue);
            Assert.IsTrue(item.PromotionEndDate.HasValue);
        }

        [Test]
        public void promotion_start_and_end_date_should_be_30_days_apart()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            item.ChangePrice(90.00m);
            TimeSpan dateDiff = item.PromotionEndDate.Value.Subtract(item.PromotionStartDate.Value);
            Assert.AreEqual(30, dateDiff.Days);
        }

    }
}
