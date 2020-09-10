using System;
using System.Threading;
using System.Threading.Tasks;

public class Example
{
    static void Main(string[] args)
    {
        WriteOutput("main/Program Begin");
        DoAsTask();
        //   DoAsAsync();
        WriteOutput("main/Program End");
        Console.ReadLine();
    }

    static void DoAsTask()
    {
        WriteOutput("1 - Starting");
        var t = Task.Factory.StartNew<int>(DoSomethingThatTakesTime);
        WriteOutput("2 - Task started");
        t.Wait();
        WriteOutput("3 - Task completed with result: " + t.Result);
    }

    static Task DoAsTask2()
    {
        WriteOutput("1 - Starting");
        var t = Task.Factory.StartNew<int>(DoSomethingThatTakesTime, TaskCreationOptions.HideScheduler); //<-- HideScheduler do the magic

        TaskCompletionSource<int> tsc = new TaskCompletionSource<int>();
        t.ContinueWith(tsk => tsc.TrySetResult(tsk.Result)); //<-- Set the result to the created Task

        WriteOutput("2 - Task started");

        tsc.Task.ContinueWith(tsk => WriteOutput("3 - Task completed with result: " + tsk.Result)); //<-- Complete the Task
        return tsc.Task;
    }

    static async Task DoAsAsync()
    {
        WriteOutput("1 - Starting");
        var t = Task.Factory.StartNew<int>(DoSomethingThatTakesTime);
        WriteOutput("2 - Task started");
        var result = await t;
        WriteOutput("3 - Task completed with result: " + result);
    }

    static int DoSomethingThatTakesTime()
    {
        WriteOutput("A - Started something");
        Thread.Sleep(5000);
        WriteOutput("B - Completed something");
        return 123;
    }

    static void WriteOutput(string message)
    {
        Console.WriteLine("[{0}] {1}", Thread.CurrentThread.ManagedThreadId, message);
    }
}