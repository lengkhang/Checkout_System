namespace Checkout.Core.Items
{
    public abstract class Item
    {
        public string SKU { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }
    }
}
