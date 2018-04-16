

using System;
using System.IO;

namespace AES
{
    class CheckPath
    {
        internal static string СhekedPathToFile(ref string pathToKey)
        {
            while (File.Exists(pathToKey) != true)
            {
                Console.WriteLine("Файла " + pathToKey + " не существует");
                Console.WriteLine("Укажите существующий файл");
                pathToKey = Console.ReadLine();

            }
            return pathToKey;
        }
        internal static string СhekedPathToDirectory(ref string path)
        {
            while (Directory.Exists(path) != true)
            {
                Console.WriteLine("Директории " + path + " не существует");
                Console.WriteLine("Укажите существующее имя директории");
                path = Console.ReadLine();

            }
            return path;
        }
    }
}
