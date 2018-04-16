using System;
using System.IO;

namespace AES
{
    class ReadWriteFile
    {

        internal static byte[] ReadFile(string path)
        {
            using (FileStream fstream = File.OpenRead(path))
            {

                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);

                return array;
            }
        }
        internal static byte[] WriteFile(string path, byte[] fileContent)
        {
            using (FileStream fstream = File.Create(@path))
            {
               
                // запись массива байтов в файл
                fstream.Write(fileContent, 0, fileContent.Length);
            }
            return fileContent;
        }
    }
}
