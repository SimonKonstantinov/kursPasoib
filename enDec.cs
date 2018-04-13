using System;
using System.IO;
using System.Security.Cryptography;

namespace Aes_Example
{
    class AesExample
    {
        public static void Main(string[] args)
        {
            try
            {

                

                // Create a new instance of the Aes
                // class.  This generates a new key and initialization 
                // vector (IV).
                using (Aes myAes = Aes.Create())
                {

                    // Encrypt the string to an array of bytes.
                    byte[] encrypted = EncryptStringToBytes_Aes(ReadFile(args[0]),
myAes.Key, myAes.IV);

                    // Decrypt the bytes to a string.
                    byte[] roundtrip = DecryptStringFromBytes_Aes(encrypted,
myAes.Key, myAes.IV);

                    //Display the original data and the decrypted data.
                       Console.WriteLine("Original:  ");

                    foreach (var value in ReadFile(args[0]))
                    {
                        Console.Write(value);
                    }
                    Console.WriteLine();
                    Console.WriteLine("\n\n\nencripted:  ");

                    foreach (var value in encrypted)
                    {
                        Console.Write(value);
                    }
                    Console.WriteLine("\n\n\n\nRound Trip: ");

                    foreach (var value in roundtrip) {
                        Console.Write(value);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }
        }

        static byte[] EncryptStringToBytes_Aes(byte[] plainText, byte[] Key,
byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key
, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt
, encryptor, CryptoStreamMode.Write))
                    {
                        using (BinaryWriter swEncrypt = new BinaryWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        static byte[] DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key
, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            byte[] plaintext;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key
, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt
, decryptor, CryptoStreamMode.Read))
                    {
                        using (BinaryReader srDecrypt = new BinaryReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting 

                            // and place them in a string.

                            return srDecrypt.ReadBytes(cipherText.Length);
                          

                        }
                    }
                }

            }

            

        }
        private static byte[] ReadFile(string path)
        {
            using (FileStream fstream = File.OpenRead(path))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);


                return array;
            }
        }
        public static byte[] WriteFile(string path, byte[] fileContent)
        {
            using (FileStream fstream = new FileStream(@path, FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты

                // запись массива байтов в файл
                fstream.Write(fileContent, 0, fileContent.Length);
            }
            return fileContent;
        }
    }
}
