using Microsoft.VisualStudio.TestTools.UnitTesting;
using video_rental_Assign_N;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace video_rental_Assign_N.Tests
{
    [TestClass()]
    public class Form1Tests
    {
        [TestMethod()]
        public void Form1Test()
        {

            RentVideo obj = new RentVideo();
            int price=obj.getCharges(1);
            if (price > 0)
            {
                Assert.IsTrue(true);
            }
            else {
                Assert.IsTrue(false);
            }
        }

        [TestMethod()]
        public void CusotmerTest()
        {

            RentVideo obj = new RentVideo();
            int price = obj.registerCustomer("as","123","NZ");
            if (price > 0)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

    }
}