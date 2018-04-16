using System;
using System.Collections.Generic;
using System.IO;

public class StackBasedIteration
{
    static void Main(string[] args)
    {
       
        foreach(var i in LookIn(args[0]))
        {
            Console.WriteLine(i);
            foreach (var value in ReadFile(i))
            {
                Console.Write(value);
            }
            Console.WriteLine();
        }

    }
    private static byte[] ReadFile(string path)
    {
        using (FileStream fstream = File.OpenRead(path))
        {

            byte[] array = new byte[fstream.Length];
            // считываем данные
            fstream.Read(array, 0, array.Length);


            return array;
        }
    }
        static List<string> LookIn(string path)
    {
        List<string> files = new List<string>();
        List<string> dirs = new List<string>();

        try
        {
            files.AddRange(Directory.GetFiles(path));
            dirs.AddRange(Directory.GetDirectories(path));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        foreach (string dir in dirs)
        {
            files.AddRange(LookIn(dir));
        }
        return files;
    }
}