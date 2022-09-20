namespace Chromass.ChroZenPump.Tests
{
    [TestClass]
    public class ControllerTest
    {
        [TestMethod]
        public void Connect()
        {
            var pump = new ChroZenPump.Controller();
            pump.ConnectAsync("localhost", 4242, CancellationToken.None).Wait();

            Assert.IsTrue(pump.IsConnected);

            pump.Close();
        }
    }
}