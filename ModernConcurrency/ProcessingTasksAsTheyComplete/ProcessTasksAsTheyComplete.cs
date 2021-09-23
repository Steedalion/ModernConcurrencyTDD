using System.Threading.Tasks;
using NUnit.Framework;

[TestFixture]
public class ProcessTasksAsTheyComplete
{
    [Test]
    public void ProcessTest()
    {
        int[] results = new int[3];
        Task<int> taskA = DelayAndReturnAsync(2);
        Task<int> taskB = DelayAndReturnAsync(3);
        Task<int> taskC = DelayAndReturnAsync(1);

        Task<int>[] tasks = new[] {taskA, taskB, taskC};
        for (int index = 0; index < tasks.Length; index++)
        {
            results[index] = AwaitAndProcess(tasks[index]).Result;
        }
    }

    private async Task<int> AwaitAndProcess(Task<int> task)
    {
        return task.Result;
    }

    private async Task<int> DelayAndReturnAsync(int value)
    {
        await Task.Delay(value);
        return value;
    }
}