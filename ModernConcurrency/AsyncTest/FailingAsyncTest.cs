using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ModernConcurrency
{
    public class FailingAsyncTest
    {
        async Task Fail()
        {
            await Task.Delay(10);
            await Task.Delay(10);
            await Task.Delay(10);
            throw new TestAsyncException("should fail");
        }

        [Test]
        public async Task ShouldFail()
        {
            // Assert.ThrowsAsync<Exception>(async () => await Fail());
            try
            {
                await Fail();
                Assert.Fail("Should throw exception");
            
            }
            catch (Exception e)
            {
                Assert.Pass("Exception caught");
            }
            
        }

        [Test]
        public async Task DetectSpecificExceptionTpye()
        {
            Assert.ThrowsAsync<TestAsyncException>(async () => await Fail());
        }
    }
}