using Chromass.ChroZenPump.PacketWrappers;
using ChromassProtocol;
using Communicator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynchronizationContextTestHelper;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

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

        [TestMethod]
        public void PacketWrapperToBytesWhereOffsetIsNotZero()
        {
            var information = new InformationWrapper();
            information.Packet.nVersion = 800;

            var arr = information.SendPacket(offset:32, size:4);
            Assert.AreEqual(28, arr.Length);
            Assert.AreEqual(800, BitConverter.ToInt32(arr.AsSpan(24)));

            information.Packet.nVersion = 0;
            information.Assemble(this, arr.AsSpan(24), 0, 32);
            Assert.AreEqual(800, information.Packet.nVersion);
        }
    }
}