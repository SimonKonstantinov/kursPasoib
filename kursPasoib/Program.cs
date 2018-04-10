using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Simakurs
{
    class Program
    {
        static byte[] ReadFile(string fileName)
        {

            // преобразуем строку в байты
            byte[] array = File.ReadAllBytes(fileName);
                // считываем данные
               
           
                return array;
            
        }
        static List<string> LookIn(string path, string switchExpression)
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


            using (var myAes = Aes.Create())
            {
                
                switch (switchExpression)
                {
                    case "1":
                        {
                            foreach (string dir in dirs)
                            {
                                files.AddRange(LookIn(dir, switchExpression));
                                foreach (string i in files)
                                {
                                    byte[] encript = ReadFile(i);


                                    using (FileStream fstream = File.Create(i))
                                    {


                                     
                                        fstream.Write(EncryptStringToBytesAes(encript, myAes.Key, myAes.IV), 0, EncryptStringToBytesAes(encript, myAes.Key, myAes.IV).Length);
                                        Console.WriteLine("файл " + i + " зашифрован");
                                        Console.WriteLine("\nВаш ключ ");
                                        foreach (var value in myAes.Key)
                                        {

                                            Console.Write(Convert.ToString(value));
                                        }

                                        Console.WriteLine("\nВаш вектор инициализации ");
                                        foreach (var value in myAes.IV)
                                        {

                                            Console.Write(Convert.ToString(value));
                                        }
                                        Console.WriteLine("\n\n\n\n");

                                    }

                                }
                            }

                        }
                        break;
                    case "2":
                        {
                            foreach (string dir in dirs)
                            {
                                files.AddRange(LookIn(dir, switchExpression));
                                foreach (string i in files)
                                {
                                    Console.WriteLine(i);
                                   
                                    byte[] decript = ReadFile(i);
                                    using (FileStream fstream = File.Create(i))
                                    {


                                        EncryptStringToBytesAes(decript, myAes.Key, myAes.IV);
                                        fstream.Write(DecryptStringFromBytesAes(decript, myAes.Key, myAes.IV), 0, DecryptStringFromBytesAes(decript, myAes.Key, myAes.IV).Length);
                                        Console.WriteLine("файл " + i + " зашифрован");
                                        Console.WriteLine("\nВаш ключ ");
                                        foreach (var value in myAes.Key)
                                        {

                                            Console.Write(Convert.ToString(value));
                                        }

                                        Console.WriteLine("\nВаш вектор инициализации ");
                                        foreach (var value in myAes.IV)
                                        {

                                            Console.Write(Convert.ToString(value));
                                        }
                                        Console.WriteLine("\n\n\n\n");

                                    }
                                  
                                }
                            }

                        }
                        break;
                    default:
                        Console.WriteLine("Вы ничего не выбрали - перезапустите програму");
                        // You cannot "fall through" any switch section, including
                        // the last one. 
                        break;


                }
            }
            return files;
        }

        static byte[] DecryptStringFromBytesAes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Проверяем аргументы
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Строка, для хранения расшифрованного текста
            byte[] plaintext;

            // Создаем объект класса AES,
            // Ключ и IV
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Создаем объект, который определяет основные операции преобразований.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Создаем поток для расшифрования.
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Читаем расшифрованное сообщение и записываем в строку
                            plaintext = Encoding.Default.GetBytes(srDecrypt.ReadToEnd());
                        }
                    }
                }

            }

            return plaintext;

        }

        private static byte[] EncryptStringToBytesAes(byte[] plainText, byte[] Key, byte[] IV)
        {


            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Создаем объект класса AES
            // с определенным ключом and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Создаем объект, который определяет основные операции преобразований.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Создаем поток для шифрования.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Записываем в поток все данные.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            //Возвращаем зашифрованные байты из потока памяти.
            return encrypted;

        }

        static void Main(string[] args)
        {

            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine("Название: {0}", drive.Name);
                Console.WriteLine("Тип: {0}", drive.DriveType);

                Console.WriteLine();
            }
            var dirName = @"E:\8 семестр\вторник\testForcurs";
            Console.WriteLine("Если вы хотите зашифровать файлы -нажмите 1\nЕсли вы хотите расшифровать файлы - нажмите 2\n");
            Console.WriteLine("Нажмите 1 или 2");
            string ch = Console.ReadLine();

            //
            LookIn(dirName, ch);

            Console.ReadLine();
        }
    }
}
