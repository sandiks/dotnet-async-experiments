using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static Random rand = new Random();

    static void Main()
    {
        List<int[]> storage = new List<int[]>(100);
        while (true)
        {
            string input_number = Console.ReadLine();
            int size = 0;
            if (Int32.TryParse(input_number, out size))
            {
            }
            if (size < 1 || size > 1000_000)
                size = 1000_000;

            Console.WriteLine($"Current size: {size} list size {storage.Count}");

            AddToStorage(size, storage);

        }
    }

    static async void AddToStorage(int size, List<int[]> storage)
    {
        var data = await Task.Run(() => Generate(size));
        storage.Add(data);
        System.Console.WriteLine($"generate new array length:{data.Length}");
    }

    static int[] Generate(int size)
    {
        var data = new int[size];
        for (int i = 0; i < size; i++)
        {
            data[i] = rand.Next(10000);
        }
        Thread.Sleep(2000);
        return data;
    }
}