using System;
using DSED05_GoldDiggers;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;


namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGoldMinerRunMethod()
        {
            GoldMiner jimMiner=new Jim();
            Assert.AreEqual(0,jimMiner.Location);
            jimMiner.Run();
            Assert.AreNotEqual(0,jimMiner.Location);
        }
    }
}
