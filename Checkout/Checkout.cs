using System.Collections.Generic;
using System.Linq;
using Checkout.Core.Items;

namespace Checkout.Core
{
    public class Checkout
    {
        private List<Item> _scannedItems;
        private IEnumerable<PricingRule> _pricingRules;

        public Checkout(IEnumerable<PricingRule> pricingRules)
        {
            _scannedItems = new List<Item>();
            _pricingRules = pricingRules;
        }

        public void Scan(Item item)
        {
            _scannedItems.Add(item);
        }

        public void Remove(Item item)
        {
            _scannedItems.Remove(item);
        }

        public double Total()
        {
            double totalPrice = 0;
            var itemGroupsBySKU = GetItemGroupBySKU();
            var freeItems = new Dictionary<string, int>();

            foreach (var itemGroup in itemGroupsBySKU)
            {
                var remainder = itemGroup.Count();

                foreach (var rule in _pricingRules)
                {
                    if (AreItemGroupEligibleForDiscount(itemGroup, rule, remainder))
                    {
                        switch (rule.Mode) {
                            case PriceMode.EachItem:

                                totalPrice += rule.Price * remainder;
                                remainder = 0;
                                break;

                            case PriceMode.BatchItem:

                                var batchQuantity = remainder / rule.Quantity;
                                totalPrice += rule.Price * batchQuantity;
                                remainder %= rule.Quantity;
                                break;
                        }

                        if (rule.FreeItem != null)
                            freeItems.Add(rule.FreeItem.SKU, itemGroup.Count());
                    }
                }

                totalPrice += remainder * itemGroup.Select(item => item.Price).FirstOrDefault();
            }

            return GetExcludedPriceForFreeItems(freeItems, itemGroupsBySKU, totalPrice);
        }

        private IEnumerable<IGrouping<string, Item>> GetItemGroupBySKU()
        {
            return _scannedItems
                .GroupBy(item => item.SKU);
        }

        private bool AreItemGroupEligibleForDiscount(IGrouping<string, Item> itemGroup, PricingRule rule, int itemGroupQuantity)
        {
            return (itemGroup.Key.ToLower() == rule.Item.SKU.ToLower()
                        && itemGroupQuantity >= rule.Quantity);
        }

        private double GetExcludedPriceForFreeItems(Dictionary<string, int> freeItems, IEnumerable<IGrouping<string, Item>> itemGroupsBySKU, double totalPrice)
        {
            foreach (var freeItem in freeItems)
            {
                var itemGroupForFreeItem = itemGroupsBySKU
                    .Where(item => item.Key.ToLower() == freeItem.Key.ToLower())
                    .FirstOrDefault();

                if (itemGroupForFreeItem != null)
                {
                    var freeItemQuantity = (itemGroupForFreeItem.Count() < freeItem.Value) ? itemGroupForFreeItem.Count() : freeItem.Value;
                    totalPrice -= freeItemQuantity * itemGroupForFreeItem
                        .Select(item => item.Price)
                        .FirstOrDefault();
                }
            }

            return totalPrice;
        }
    }
}
