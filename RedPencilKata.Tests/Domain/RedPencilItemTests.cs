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

    }
}
