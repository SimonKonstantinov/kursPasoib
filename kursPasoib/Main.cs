
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using AES;
namespace Aes_Example
{
    class AesExample
    {
        public static void Main()
        {
            Console.WriteLine("Программа для шифрвации  и дешифрации файлов и дисков\n");
            Console.WriteLine("1 - Работать с папкой или диском\n");
            Console.WriteLine("2 - Работать одним файлом\n");

            string selection = Console.ReadLine();

            switch (selection)
            {
                case "1":
                    WorkWithFolderOrDisk();
                    break;
                case "2":
                    WorkWithOneFile();
                    break;
                default:
                    Console.WriteLine("Вы нажали неизвестную букву");
                    break;
            }

        }

        private static void WorkWithOneFile()
        {
            Console.WriteLine("1 - Зашифровать один файл\n");
            Console.WriteLine("2 - Расшифровать один файл\n");
            string selection = Console.ReadLine();

            string path;
            switch (selection)
            {

                case "1":
                    using (Aes myAes = Aes.Create())
                    {
                        Console.WriteLine("Введите полное имя файла(с указанием пути)");
                        path = Console.ReadLine();
                        CheckPath.СhekedPathToFile(ref path);

                        byte[] encrypted;
                        try
                        {
                            if (ReadWriteFile.ReadFile(path).Length != 0)
                            {
                                // Encrypt the string to an array of bytes.
                                encrypted = CifherAes.EncryptBytes_Aes(ReadWriteFile.ReadFile(path), 
                                                                       myAes.Key,
                                                                       myAes.IV);
                                ReadWriteFile.WriteFile(path, encrypted);
                            }
                            else
                            {
                                Console.WriteLine("файл " + path + "пустой ");
                            }

                            Console.WriteLine("\nВведите полное имя файла для хранения ключа (с указанием пути)");
                            path = Console.ReadLine();
                            CheckPath.СhekedPathToFile(ref path);
                            ReadWriteFile.WriteFile(path, myAes.Key);
                            Console.WriteLine("Ключ  ");
                            foreach (var value in myAes.Key)
                            {
                                Console.Write(value);
                            }
                            Console.WriteLine();

                            Console.WriteLine("Введите полное имя файла для хранения вектора инициаизации  (с указанием пути)");
                            path = Console.ReadLine();
                            CheckPath.СhekedPathToFile(ref path);
                            ReadWriteFile.WriteFile(path, myAes.IV);
                            Console.WriteLine("Вектор инициализации {0}");
                            foreach (var value in myAes.IV)
                            {
                                Console.Write(value);
                            }


                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                    break;
                case "2":
                    Console.WriteLine("Введите полное имя файла(с указанием пути)");
                    path = Console.ReadLine();
                    CheckPath.СhekedPathToFile(ref path);

                    byte[] decripted;
                    try
                    {
                        Console.WriteLine("Введите полное имя ключа(с указанием пути)");
                        string pathToKey = Console.ReadLine();
                        CheckPath.СhekedPathToFile(ref path);
                        Console.WriteLine("Ключ  ");
                        foreach (var value in ReadWriteFile.ReadFile(pathToKey))
                        {
                            Console.Write(value);
                        }

                        Console.WriteLine();

                        Console.WriteLine("Введите полное имя вектора инициализации (с указанием пути)");
                        string pathToIV = Console.ReadLine();
                        CheckPath.СhekedPathToFile(ref path);
                        foreach (var value in ReadWriteFile.ReadFile(pathToIV))
                        {
                            Console.Write(value);
                        }

                        // Encrypt the string to an array of bytes.
                        if (ReadWriteFile.ReadFile(path).Length != 0)
                        {
                            decripted = CifherAes.DecryptBytes_Aes(ReadWriteFile.ReadFile(path),
                                                                   ReadWriteFile.ReadFile(pathToKey),
                                                                   ReadWriteFile.ReadFile(pathToIV));

                            ReadWriteFile.WriteFile(path, decripted);
                        }
                        else
                        {
                            Console.WriteLine("файл " + path + "пустой. с ним нельзя работать");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;
                default:
                    Console.WriteLine("Вы нажали неизвестную букву");
                    break;
            }
        }


        private static void WorkWithFolderOrDisk()
        {
            Console.Clear();

            Console.WriteLine("1 - Зашифровать папку или диск\n");
            Console.WriteLine("Помниет что ключ и вектор инициализации хранить отдельно от пути объекта шифрации");
            Console.WriteLine("2 - Расшифровать папку или диск\n");
            string selection = Console.ReadLine();

            switch (selection)
            {
                case "1":
                    using (Aes myAes = Aes.Create())
                    {
                        Console.WriteLine("Введите полное имя папки(с указанием пути)");
                        string path = Console.ReadLine();
                        CheckPath.СhekedPathToDirectory(ref path);

                        byte[] encrypted;
                        try
                        {
                            foreach (var i in GettFiles.LookIn(path))
                            {
                                Console.WriteLine(i);
                                // Encrypt the string to an array of bytes.
                                if (ReadWriteFile.ReadFile(i).Length != 0)
                                {
                                    encrypted = CifherAes.EncryptBytes_Aes(ReadWriteFile.ReadFile(i), myAes.Key, myAes.IV);
                                    ReadWriteFile.WriteFile(i, encrypted);
                                }
                                else
                                {
                                    Console.WriteLine("файл " + i + " пустой");
                                }
                            }
                            GettFiles.LookIn(path).Clear();

                            Console.WriteLine("\nВведите полное имя файла для хранения ключа (с указанием пути)");
                            path = Console.ReadLine();
                            CheckPath.СhekedPathToFile(ref path);

                            ReadWriteFile.WriteFile(path, myAes.Key);
                            Console.WriteLine("Ключ  ");
                            foreach (var value in myAes.Key)
                            {
                                Console.Write(value);
                            }
                            Console.WriteLine();

                            Console.WriteLine("Введите полное имя файла для хранения вектора инициаизации  (с указанием пути)");
                            path = Console.ReadLine();
                            CheckPath.СhekedPathToFile(ref path);
                            ReadWriteFile.WriteFile(path, myAes.IV);
                            Console.WriteLine("Вектор инициализации {0}");
                            foreach (var value in myAes.IV)
                            {
                                Console.Write(value);
                            }


                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }

                    break;
                case "2":
                    using (Aes myAes = Aes.Create())
                    {
                        Console.WriteLine("Введите полное имя папки или диска (с указанием пути)");
                        string path = Console.ReadLine();
                        CheckPath.СhekedPathToDirectory(ref path);

                        byte[] decripted;
                        try
                        {
                            Console.WriteLine("Введите полное имя ключа(с указанием пути)");
                            string pathToKey = Console.ReadLine();
                            CheckPath.СhekedPathToFile(ref pathToKey);
                            Console.WriteLine("Ключ  ");
                            foreach (var value in ReadWriteFile.ReadFile(pathToKey))
                            {
                                Console.Write(value);
                            }
                            Console.WriteLine();

                            Console.WriteLine("Введите полное имя вектора инициализации (с указанием пути)");
                            string pathToIV = Console.ReadLine();
                            CheckPath.СhekedPathToFile(ref pathToKey);
                            foreach (var value in ReadWriteFile.ReadFile(pathToIV))
                            {
                                Console.Write(value);
                            }

                            // Encrypt the string to an array of bytes.
                            foreach (var i in GettFiles.LookIn(path))
                            {
                                Console.WriteLine(i);
                                // Encrypt the string to an array of bytes.
                                if (ReadWriteFile.ReadFile(i).Length != 0)
                                {
                                    decripted = CifherAes.DecryptBytes_Aes(ReadWriteFile.ReadFile(i),
                                                                           ReadWriteFile.ReadFile(pathToKey), 
                                                                           ReadWriteFile.ReadFile(pathToIV));
                                    ReadWriteFile.WriteFile(i, decripted);
                                }
                                else
                                {
                                    Console.WriteLine("файл пустой,  " + i + " с ним нельзя работать ");
                                }
                            }
                            GettFiles.LookIn(path).Clear();


                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }

                    break;
                default:
                    Console.WriteLine("Вы нажали неизвестную  команду");
                    break;
            }
        }
        
    }
}