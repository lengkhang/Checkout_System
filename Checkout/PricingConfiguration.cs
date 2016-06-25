using System.Collections.Generic;
using Checkout.Core.Items;

namespace Checkout.Core
{
    public class PricingConfiguration
    {
        public readonly List<PricingRule> PricingRules;

        public PricingConfiguration()
        {
            var ipadPricingRule = new PricingRule() { Item = new Ipad(), Quantity = 4, Price = 499.99, Mode = PriceMode.EachItem };
            var appleTvPricingRule = new PricingRule() { Item = new AppleTv(), Quantity = 3, Mode = PriceMode.BatchItem };
            appleTvPricingRule.Price = appleTvPricingRule.Item.Price * 2;
            var macbookPricingRule = new PricingRule() { Item = new MacBookPro(), Quantity = 1, Mode = PriceMode.EachItem, FreeItem = new Vga() };
            macbookPricingRule.Price = macbookPricingRule.Item.Price;

            PricingRules = new List<PricingRule>();
            PricingRules.Add(ipadPricingRule);
            PricingRules.Add(appleTvPricingRule);
            PricingRules.Add(macbookPricingRule);
        }
    }
}
