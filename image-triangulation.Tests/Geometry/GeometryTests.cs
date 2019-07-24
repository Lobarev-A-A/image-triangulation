using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace image_triangulation.Tests
{
    [TestClass]
    public class GeometryTests
    {
        public TestContext TestContext { get; set; }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                    "Geometry/DelaunayCheckTestData.xml",
                    "Set",
                    DataAccessMethod.Sequential)]
        [TestMethod]
        public void DelaunayCheck_Check_FromXML()
        {
            // arrange
            Pixel p0 = new Pixel(Convert.ToInt32(TestContext.DataRow["x0"]), Convert.ToInt32(TestContext.DataRow["y0"]));
            Pixel p1 = new Pixel(Convert.ToInt32(TestContext.DataRow["x1"]), Convert.ToInt32(TestContext.DataRow["y1"]));
            Pixel p2 = new Pixel(Convert.ToInt32(TestContext.DataRow["x2"]), Convert.ToInt32(TestContext.DataRow["y2"]));
            Pixel p3 = new Pixel(Convert.ToInt32(TestContext.DataRow["x3"]), Convert.ToInt32(TestContext.DataRow["y3"]));
            bool expected = Convert.ToBoolean(TestContext.DataRow["result"]);

            // act
            bool actual = Geometry.DelaunayCheck(p0, p1, p2, p3);

            // assert
            Assert.AreEqual(expected, actual, $"Expected {expected}, but returned {actual}." +
                                              $" Test Data: p0[{p0.X},{p0.Y}], p1[{p1.X},{p1.Y}], p2[{p2.X},{p2.Y}], p3[{p3.X},{p3.Y}].");
        }
    }
}
