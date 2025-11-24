// See https://aka.ms/new-console-template for more information
using Utilities;
using System.IO;

List<string> guidList = new List<string>();

for (int i = 0; i < 100; i++)
{
    guidList.Add(GuidMaintenance.GenerateGuid());
}

File.WriteAllLines(@"C:\Temp\TestGuids.txt", guidList.ToArray());

