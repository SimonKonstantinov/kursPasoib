using System;
using System.Collections.Generic;
using System.IO;

namespace AES
{
    class GettFiles
    {
       internal static List<string> LookIn(string path)
        {
            List<string> files = new List<string>();
            List<string> dirs = new List<string>();

            try
            {
                files.AddRange(Directory.GetFiles(path));
                dirs.AddRange(Directory.GetDirectories(path));
            }
            catch (UnauthorizedAccessException e)
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
}
