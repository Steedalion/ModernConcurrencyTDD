using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ModernConcurrency
{
    [TestFixture]
    public class MockingAsyncs
    {
        private IMyAsync sync = new MockSync();
        private IMyAsync asyncPass = new MockASync();
        private IMyAsync asyncFail = new MockASyncFail();

        [Test]
        public async Task SyncPassTest()
        {
            Task<int> result = sync.DoSomething();
            await result;
            Assert.AreEqual(10, result.Result);
        }

        [Test]
        public async Task ASyncTest()
        {
            Task<int> result = asyncPass.DoSomething();
            await result;
            Assert.AreEqual(10, result.Result);
        }

        [Test]
        public async Task AsyncShouldFail()
        {
            Task<int> result = asyncFail.DoSomething();
            Assert.ThrowsAsync<Exception>(async () => await result);
            Exception innerException = result.Exception.InnerExceptions[0];
            Assert.AreEqual("Should fail for testing", innerException.Message);
        }
    }

    internal class MockASyncFail : IMyAsync
    {
        public async Task<int> DoSomething()
        {
            await Task.Delay(10);
            throw new Exception("Should fail for testing");
        }
    }

    internal class MockASync : IMyAsync
    {
        public async Task<int> DoSomething()
        {
            await Task.Delay(5);
            return 10;
        }
    }

    public class MockSync : IMyAsync
    {
        public Task<int> DoSomething()
        {
            return Task<int>.FromResult(10);
        }
    }

    interface IMyAsync
    {
        Task<int> DoSomething();
    }
}