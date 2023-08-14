using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketCheckout.Tests
{
    [TestFixture]
    public class CheckoutTests
    {
        [Test]
        public void CalculateTotal_NoSpecialPricing_CorrectTotal()
        {
            // Setting up pricing rules for items
            var pricingRules = new PricingRule[]
            {
                new PricingRule(50, new SpecialPrice(0, 0)),
                new PricingRule(30, new SpecialPrice(0, 0)),
                new PricingRule(20, new SpecialPrice(0, 0)),
                new PricingRule(15, new SpecialPrice(0, 0))
            };

            var checkout = new Checkout(pricingRules);

            // Adding items to the checkout
            checkout.AddItem(0);  // Item 0: 50
            checkout.AddItem(1);  // Item 1: 30
            checkout.AddItem(2);  // Item 2: 20
            checkout.AddItem(3);  // Item 3: 15

            // Checking if the calculated total matches the expected total
            Assert.AreEqual(115, checkout.CalculateTotalPrice());
        }

        [Test]
        public void CalculateTotal_WithSpecialPricing_CorrectTotal()
        {
            var pricingRules = new PricingRule[]
            {
                new PricingRule(50, new SpecialPrice(3, 130)),
                new PricingRule(30, new SpecialPrice(2, 45)),
                new PricingRule(20, new SpecialPrice(0, 0)),
                new PricingRule(15, new SpecialPrice(0, 0))
            };

            var checkout = new Checkout(pricingRules);

            // Adding items that have special pricing
            checkout.AddItem(0);  // Item 0: 50 (3 for 130)
            checkout.AddItem(0);  // Item 0: 50 (3 for 130)
            checkout.AddItem(0);  // Item 0: 50 (3 for 130)
            checkout.AddItem(1);  // Item 1: 30 (2 for 45)
            checkout.AddItem(1);  // Item 1: 30 (2 for 45)

            // Checking if the calculated total matches the expected total
            Assert.AreEqual(175, checkout.CalculateTotalPrice());
        }

        [Test]
        public void CalculateTotal_NoItems_ReturnsZero()
        {
            var pricingRules = new PricingRule[]
            {
                new PricingRule(50, new SpecialPrice(0, 0)),
                new PricingRule(30, new SpecialPrice(0, 0))
            };

            var checkout = new Checkout(pricingRules);

            // Checking if the total is zero when no items are added
            Assert.AreEqual(0, checkout.CalculateTotalPrice());
        }

        [Test]
        public void AddItem_InvalidItemIndex_ThrowsException()
        {
            var pricingRules = new PricingRule[]
            {
                new PricingRule(50, new SpecialPrice(0, 0)),
                new PricingRule(30, new SpecialPrice(0, 0))
            };

            var checkout = new Checkout(pricingRules);

            // Adding an item with an invalid index should throw an exception
            Assert.Throws<IndexOutOfRangeException>(() => checkout.AddItem(2));
        }
    }
}