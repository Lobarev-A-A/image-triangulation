using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ImageTriangulation.Applicaton;

namespace ImageTriangulation.Tests
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

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                    "Geometry/PointRelativelyTriangleTestData.xml",
                    "Set",
                    DataAccessMethod.Sequential)]
        [TestMethod]
        public void PointRelativelyTriangle_Check_FromXML()
        {
            // arrange
            Pixel p = new Pixel(Convert.ToInt32(TestContext.DataRow["px"]), Convert.ToInt32(TestContext.DataRow["py"]));
            Pixel t0 = new Pixel(Convert.ToInt32(TestContext.DataRow["x0"]), Convert.ToInt32(TestContext.DataRow["y0"]));
            Pixel t1 = new Pixel(Convert.ToInt32(TestContext.DataRow["x1"]), Convert.ToInt32(TestContext.DataRow["y1"]));
            Pixel t2 = new Pixel(Convert.ToInt32(TestContext.DataRow["x2"]), Convert.ToInt32(TestContext.DataRow["y2"]));
            int expected = Convert.ToInt32(TestContext.DataRow["result"]);

            // act
            int actual = Geometry.PointRelativelyTriangle(p, t0, t1, t2);

            // assert
            Assert.AreEqual(expected, actual, $"Expected {expected}, but returned {actual}." +
                                              $" Test Data: p[{p.X},{p.Y}], t0[{t0.X},{t0.Y}], t1[{t1.X},{t1.Y}], t2[{t2.X},{t2.Y}].");
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                    "Geometry/PointsRelativelyStraightTestData.xml",
                    "Set",
                    DataAccessMethod.Sequential)]
        [TestMethod]
        public void PointsRelativelyStraight_Check_FromXML()
        {
            // arrange
            Pixel p1 = new Pixel(Convert.ToInt32(TestContext.DataRow["p1x"]), Convert.ToInt32(TestContext.DataRow["p1y"]));
            Pixel p2 = new Pixel(Convert.ToInt32(TestContext.DataRow["p2x"]), Convert.ToInt32(TestContext.DataRow["p2y"]));
            Section s = new Section(new Pixel(Convert.ToInt32(TestContext.DataRow["s1x"]), Convert.ToInt32(TestContext.DataRow["s1y"])),
                                    new Pixel(Convert.ToInt32(TestContext.DataRow["s2x"]), Convert.ToInt32(TestContext.DataRow["s2y"])));
            int expected = Convert.ToInt32(TestContext.DataRow["result"]);

            // act
            int actual = Geometry.PointsRelativelyStraight(p1, p2, s);

            // assert
            Assert.AreEqual(expected, actual, $"Expected {expected}, but returned {actual}." +
                                              $" Test Data: p1[{p1.X},{p1.Y}], p2[{p2.X},{p2.Y}], s[{s.a.X},{s.a.Y}], [{s.b.X},{s.b.Y}].");
        }
    }
}
