using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Checkout.Core;
using Checkout.Core.Items;

namespace Checkout.Core.Test
{
    [TestClass]
    public class CheckoutTest
    {
        private Checkout _checkOut;
        private const double iPadPrice = 549.99;
        private const double macbookProPrice = 1399.99;
        private const double appleTvPrice = 109.50;
        private const double vgaAdapterPrice = 30.00;

        [TestInitialize]
        public void Initialize()
        {
            var pricingRules = new PricingConfiguration().PricingRules;
            _checkOut = new Checkout(pricingRules);
        }

        [TestMethod]
        public void Total_WithoutScan_TotalPriceIsZero()
        {
            //Act and Assert
            Assert.AreEqual(0.0, _checkOut.Total());
        }

        #region "Ipad"

        [TestMethod]
        public void Total_AnIpad_TotalPriceForAnIpad()
        {
            //Arrange
            var item1 = new Ipad();

            //Act
            _checkOut.Scan(item1);

            //Assert
            Assert.AreEqual(iPadPrice, _checkOut.Total());
        }

        [TestMethod]
        public void Total_ThreeIpad_TotalPriceForThreeIpad()
        {
            //Arrange
            var item1 = new Ipad();
            var item2 = new Ipad();
            var item3 = new Ipad();

            //Act
            _checkOut.Scan(item1);
            _checkOut.Scan(item2);
            _checkOut.Scan(item3);

            //Assert
            Assert.AreEqual(iPadPrice * 3, _checkOut.Total());
        }

        [TestMethod]
        public void Total_FourIpad_TotalPriceWithDiscount()
        {
            //Arrange
            var item1 = new Ipad();
            var item2 = new Ipad();
            var item3 = new Ipad();
            var item4 = new Ipad();

            //Act
            _checkOut.Scan(item1);
            _checkOut.Scan(item2);
            _checkOut.Scan(item3);
            _checkOut.Scan(item4);

            //Assert
            Assert.AreEqual(1999.96, _checkOut.Total());
        }

        #endregion

        #region "Apple Tv"

        [TestMethod]
        public void Total_AnAppleTv_TotalPriceForAnAppleTv()
        {
            //Arrange
            var item1 = new AppleTv();

            //Act
            _checkOut.Scan(item1);

            //Assert
            Assert.AreEqual(appleTvPrice, _checkOut.Total());
        }

        [TestMethod]
        public void Total_ThreeAppleTv_TotalPriceForTwoAppleTv()
        {
            //Arrange
            var item1 = new AppleTv();
            var item2 = new AppleTv();
            var item3 = new AppleTv();

            //Act
            _checkOut.Scan(item1);
            _checkOut.Scan(item2);
            _checkOut.Scan(item3);

            //Assert
            Assert.AreEqual(appleTvPrice * 2, _checkOut.Total());
        }

        [TestMethod]
        public void Total_FourAppleTv_TotalPriceForThreeAppleTv()
        {
            //Arrange
            var item1 = new AppleTv();
            var item2 = new AppleTv();
            var item3 = new AppleTv();
            var item4 = new AppleTv();

            //Act
            _checkOut.Scan(item1);
            _checkOut.Scan(item2);
            _checkOut.Scan(item3);
            _checkOut.Scan(item4);

            //Assert
            Assert.AreEqual(appleTvPrice * 3, _checkOut.Total());
        }

        [TestMethod]
        public void Total_SixAppleTv_TotalPriceForFourAppleTv()
        {
            //Arrange
            var item1 = new AppleTv();
            var item2 = new AppleTv();
            var item3 = new AppleTv();
            var item4 = new AppleTv();
            var item5 = new AppleTv();
            var item6 = new AppleTv();

            //Act
            _checkOut.Scan(item1);
            _checkOut.Scan(item2);
            _checkOut.Scan(item3);
            _checkOut.Scan(item4);
            _checkOut.Scan(item5);
            _checkOut.Scan(item6);

            //Assert
            Assert.AreEqual(appleTvPrice * 4, _checkOut.Total());
        }

        #endregion

        #region "Macbook Pro & VGA Adapter"

        [TestMethod]
        public void Total_AMacbook_TotalPriceForAMacbook()
        {
            //Arrange
            var item1 = new MacBookPro();

            //Act
            _checkOut.Scan(item1);

            //Assert
            Assert.AreEqual(macbookProPrice, _checkOut.Total());
        }

        [TestMethod]
        public void Total_AMacbookWithAVgaAdapter_TotalPriceExcludeVgaAdapter()
        {
            //Arrange
            var item1 = new MacBookPro();
            var item2 = new Vga();

            //Act
            _checkOut.Scan(item1);
            _checkOut.Scan(item2);

            //Assert
            Assert.AreEqual(macbookProPrice, _checkOut.Total());
        }

        [TestMethod]
        public void Total_TwoMacbookWithAVgaAdapter_TotalPriceExcludeVgaAdapter()
        {
            //Arrange
            var item1 = new MacBookPro();
            var item2 = new MacBookPro();
            var item3 = new Vga();

            //Act
            _checkOut.Scan(item1);
            _checkOut.Scan(item2);
            _checkOut.Scan(item3);

            //Assert
            Assert.AreEqual(macbookProPrice * 2, _checkOut.Total());
        }

        [TestMethod]
        public void Total_TwoMacbookWithThreeVgaAdapter_TotalPriceIncludeAVgaAdapter()
        {
            //Arrange
            var item1 = new MacBookPro();
            var item2 = new MacBookPro();
            var item3 = new Vga();
            var item4 = new Vga();
            var item5 = new Vga();

            //Act
            _checkOut.Scan(item1);
            _checkOut.Scan(item2);
            _checkOut.Scan(item3);
            _checkOut.Scan(item4);
            _checkOut.Scan(item5);

            //Assert
            Assert.AreEqual(macbookProPrice * 2 + vgaAdapterPrice, _checkOut.Total());
        }

        [TestMethod]
        public void Total_AVgaAdapterWithAMacbook_TotalPriceExcludeVgaAdapter()
        {
            //Arrange
            var item1 = new Vga();
            var item2 = new MacBookPro();

            //Act
            _checkOut.Scan(item1);
            _checkOut.Scan(item2);

            //Assert
            Assert.AreEqual(macbookProPrice, _checkOut.Total());
        }

        #endregion

        #region "Assignment Examples"
        [TestMethod]
        public void Total_ThreeAppleTvAndAVgaAdapter_DiscountedPriceForAppleTv()
        {
            //Arrange
            var item1 = new AppleTv();
            var item2 = new AppleTv();
            var item3 = new AppleTv();
            var item4 = new Vga();

            //Act
            _checkOut.Scan(item1);
            _checkOut.Scan(item2);
            _checkOut.Scan(item3);
            _checkOut.Scan(item4);

            //Assert
            Assert.AreEqual(249.0, _checkOut.Total());
        }

        [TestMethod]
        public void Total_FiveIpadWithTwoAppleTv_DiscountedPriceForIpad()
        {
            //Arrange
            var item1 = new AppleTv();
            var item2 = new Ipad();
            var item3 = new Ipad();
            var item4 = new AppleTv();
            var item5 = new Ipad();
            var item6 = new Ipad();
            var item7 = new Ipad();

            //Act
            _checkOut.Scan(item1);
            _checkOut.Scan(item2);
            _checkOut.Scan(item3);
            _checkOut.Scan(item4);
            _checkOut.Scan(item5);
            _checkOut.Scan(item6);
            _checkOut.Scan(item7);

            //Assert
            Assert.AreEqual(2718.95, _checkOut.Total());
        }

        [TestMethod]
        public void Total_AMacbookWithVgaAdapterAndIpad_VgaAdapterPriceIsExcluded()
        {
            //Arrange
            var item1 = new MacBookPro();
            var item2 = new Vga();
            var item3 = new Ipad();

            //Act
            _checkOut.Scan(item1);
            _checkOut.Scan(item2);
            _checkOut.Scan(item3);

            //Assert
            Assert.AreEqual(1949.98, _checkOut.Total());
        }
        #endregion
    }
}
