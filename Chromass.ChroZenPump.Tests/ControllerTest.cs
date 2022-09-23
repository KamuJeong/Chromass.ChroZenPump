using Communicator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynchronizationContextTestHelper;
using System.Threading;
using System.Threading.Tasks;

namespace Chromass.ChroZenPump.Tests
{
    [TestClass]
    public class ControllerTest
    {
        [SynchronizationContextTestMethod]
        [TestMethod]
        public async Task Connect()
        {
            var pump = new ChroZenPump.Controller(new Tcp());
            var thread = Thread.CurrentThread;

            await pump.ConnectAsync("localhost", 4242, CancellationToken.None);

            Assert.AreEqual(Thread.CurrentThread, thread);
            Assert.IsTrue(pump.IsConnected);

            pump.Close();
        }
    }
}