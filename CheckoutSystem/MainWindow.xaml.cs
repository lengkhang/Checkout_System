using System;
using System.Collections.Generic;
using System.Windows;
using Checkout.Core;
using Checkout.Core.Items;

namespace CheckoutSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Checkout.Core.Checkout _checkOut;

        public MainWindow()
        {
            InitializeComponent();

            var items = new List<Item>() { new Ipad(), new MacBookPro(), new AppleTv(), new Vga() };
            AvailableItems.ItemsSource = items;

            var pricingRules = new PricingConfiguration().PricingRules;
            _checkOut = new Checkout.Core.Checkout(pricingRules);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Item)AvailableItems.SelectedItem;
            if (selectedItem != null)
            {
                ScannedItems.Items.Add(selectedItem);
                _checkOut.Scan(selectedItem);

                TotalPrice.Content = String.Format("{0:0.00}", _checkOut.Total());
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Item)ScannedItems.SelectedItem;
            if (selectedItem != null)
            {
                ScannedItems.Items.Remove(selectedItem);
                _checkOut.Remove(selectedItem);

                TotalPrice.Content = String.Format("{0:0.00}",_checkOut.Total());
            }
        }
    }
}
