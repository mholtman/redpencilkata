using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RedPencilKata.Domain;

namespace RedPencilKata.Tests.Domain
{
    [TestFixture]
    public class RedPencilItemTests
    {
        [Test]
        public void can_create_entity_with_price()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            Assert.AreEqual(100.00m, item.OriginalPrice);
        }

        [Test]
        public void newly_created_item_has_nulled_markdown_price()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            Assert.IsFalse(item.MarkedDownPrice.HasValue);
        }

        [Test]
        public void item_should_be_stable_if_there_is_no_promotion_end_date()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            Assert.IsTrue(item.IsPriceStable());
        }

        [Test]
        public void item_should_be_stable_if_end_date_older_than_30_days()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            item.PromotionEndDate = DateTime.Now.AddDays(-31);
            Assert.IsTrue(item.IsPriceStable());
        }

        [Test]
        public void item_should_be_stable_if_end_date_equal_to_30_days_ago()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            item.PromotionEndDate = DateTime.Now.AddDays(-30);
            Assert.IsTrue(item.IsPriceStable());
        }

        [Test]
        public void item_should_be_unstable_if_end_date_less_then_30_days_ago()
        {
            RedPencilItem item = new RedPencilItem(100.00m);
            item.PromotionEndDate = DateTime.Now.AddDays(-30);
            Assert.IsTrue(item.IsPriceStable());
        }
    }
}
