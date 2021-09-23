using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ModernConcurrency
{
    public class TaskDelayAsATimeout
    {
        async Task<int> DownloadWithinSeconds(int timer, int taskDuration)
        {
            var cancel = new CancellationTokenSource(TimeSpan.FromMilliseconds(timer));
            Task download = Task.Delay(taskDuration);
            Task timeoutTask = Task.Delay(Timeout.Infinite, cancel.Token);
            Task complete = await Task.WhenAny(timeoutTask, download);
            if (complete == timeoutTask)
            {
                return -1;
            }
            
            return taskDuration;
        }

        [Test]
        public async Task TimeoutShouldComplete()
        {
            var t = DownloadWithinSeconds(1000, 1);
            await t;
            Assert.AreEqual(1, t.Result);
        }
        [Test]
        public async Task TimeoutShouldTimeout()
        {
            var t = DownloadWithinSeconds(1, 1000);
            await t;
            Assert.AreEqual(-1, t.Result);
        }
    }
}