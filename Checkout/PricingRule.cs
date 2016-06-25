using System;
using Checkout.Core.Items;

namespace Checkout.Core
{
    public class PricingRule
    {       
        public Item Item { get; set; }

        public int Quantity { get; set; }

        public Double Price { get; set; }

        public PriceMode Mode { get; set; }

        public Item FreeItem { get; set; }
    }

    public enum PriceMode
    {
        EachItem = 1,
        BatchItem = 2
    }

}
