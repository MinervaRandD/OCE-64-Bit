using Utilities;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


for (int i = 0; i < 10; i++)
{
    string guid = Guid.NewGuid().ToString();

    Console.WriteLine(guid);
}

Console.ReadLine();