using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ModernConcurrency
{
    public class ProgressReporterTest
    {
        async Task DoingSomething(IProgress<int> progress)
        {
            int percentComplete = 0;
            for (int i = 0; i < 100; i++)
            {
                percentComplete = i;
                progress?.Report(percentComplete);
                // await Task.Delay(10);
            }
        }

        async Task CallAndPrintProgress()
        {
            var progress = new Progress<int>();
            progress.ProgressChanged += (sender, i) => { Console.WriteLine("Progress is " + i+ "on "+sender); };

            await DoingSomething(progress);
        }

        [Test]
        public void ProgressReporter()
        {
            Task t = CallAndPrintProgress();
        }
    }
}