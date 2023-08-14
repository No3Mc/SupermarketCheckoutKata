using System;

namespace SupermarketCheckout
{
    // Represents a supermarket checkout system
    public class Checkout
    {
        private readonly PricingRule[] _pricingRules;
        private readonly int[] _itemCounts;

        // Constructor to initialize the checkout with pricing rules
        public Checkout(PricingRule[] pricingRules)
        {
            // Store the pricing rules for items and initialize item counts
            _pricingRules = pricingRules ?? throw new ArgumentNullException(nameof(pricingRules));
            _itemCounts = new int[pricingRules.Length];
        }

        // Add an item to the checkout
        public void AddItem(int itemIndex)
        {
            // Check if the item index is valid
            if (IsValidIndex(itemIndex))
            {
                // Increment the item count for the specified item index
                _itemCounts[itemIndex]++;
            }
            else
            {
                // Throw an exception if the item index is out of range
                throw new IndexOutOfRangeException($"Item index {itemIndex} is out of range.");
            }
        }

        // Calculate the total price of all items in the checkout
        public int CalculateTotalPrice()
        {
            int total = 0;

            // Iterate through each item and its count
            for (int index = 0; index < _itemCounts.Length; index++)
            {
                int itemCount = _itemCounts[index];
                var rule = _pricingRules[index];

                // Calculate the price based on the pricing rule for the item
                total += rule.CalculatePrice(itemCount);
            }

            return total;
        }

        // Check if an item index is valid
        private bool IsValidIndex(int index) => index >= 0 && index < _pricingRules.Length;
    }

    // Represents pricing rules for an item
    public class PricingRule
    {
        // The regular unit price of the item
        public int UnitPrice { get; }
        // Special pricing details for the item
        public SpecialPrice SpecialPrice { get; }

        // Constructor to set unit price and special pricing
        public PricingRule(int unitPrice, SpecialPrice specialPrice)
        {
            UnitPrice = unitPrice;
            SpecialPrice = specialPrice;
        }

        // Calculate the total price based on quantity and special pricing
        public int CalculatePrice(int quantity)
        {
            if (HasSpecialPrice)
            {
                // Calculate price considering special pricing and remaining items
                int specialPriceCount = quantity / SpecialPrice.Quantity;
                int remainingItems = quantity % SpecialPrice.Quantity;

                return specialPriceCount * SpecialPrice.Price + remainingItems * UnitPrice;
            }
            else
            {
                // Calculate price using regular unit price
                return quantity * UnitPrice;
            }
        }

        // Check if the item has special pricing
        public bool HasSpecialPrice => SpecialPrice.Quantity > 0;
    }

    // Represents special pricing details for an item
    public class SpecialPrice
    {
        // Quantity required for the special price
        public int Quantity { get; }
        // Special price for the given quantity
        public int Price { get; }

        // Constructor to set quantity and special price
        public SpecialPrice(int quantity, int price)
        {
            Quantity = quantity;
            Price = price;
        }
    }
}
