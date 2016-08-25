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
            //Instantiate a new Jim : Goldminer for testing
            GoldMiner jimMiner=new Jim();
            // Check Location starts at 0
            Assert.AreEqual(0,jimMiner.Location);
            // run the Run method
            jimMiner.Run();
            //Check it has changed the location
            Assert.AreNotEqual(0,jimMiner.Location);
        }
    }
}
