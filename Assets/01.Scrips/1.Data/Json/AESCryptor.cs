using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class AESCryptor
{
    private byte[] key;
    private byte[] iv;
    private readonly string keyPath = Application.dataPath.ToString() + "/save/aeskey.dat";
    private readonly string ivPath = Application.dataPath.ToString() + "/save/aesiv.dat";

    public AESCryptor()
    {
        if (File.Exists(keyPath) && File.Exists(ivPath))
        {
            key = File.ReadAllBytes(keyPath);
            iv = File.ReadAllBytes(ivPath);
        }
        else
        {
            key = GenerateRandomBytes(32);
            iv = GenerateRandomBytes(16);
            File.WriteAllBytes(keyPath, key);
            File.WriteAllBytes(ivPath, iv);
        }
    }

    private byte[] GenerateRandomBytes(int length)
    {
        byte[] randomBytes = new byte[length];

        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        return randomBytes;
    }

    public string Encryptor(string plainText)
    {
        byte[] encrypted;
        using (Rijndael aesAlg = Rijndael.Create())
        {
            aesAlg.KeySize = 256;
            aesAlg.BlockSize = 128;
            aesAlg.Key = key;
            aesAlg.IV = iv;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream ms = new())
            {
                using (CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new(cs))
                    {
                        sw.Write(plainText);
                    }
                    encrypted = ms.ToArray();
                }
            }
        }

        return Convert.ToBase64String(encrypted);
    }

    public string Decryptor(string encryptedText)
    {
        string decrypted;
        byte[] cipher = Convert.FromBase64String(encryptedText);
        using (Rijndael aesAlg = Rijndael.Create())
        {
            aesAlg.KeySize = 256;
            aesAlg.BlockSize = 128;
            aesAlg.Key = key;
            aesAlg.IV = iv;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using (MemoryStream ms = new(cipher))
            {
                using (CryptoStream cs = new(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new(cs))
                    {
                        decrypted = sr.ReadToEnd();
                    }
                }
            }
        }
        
        return decrypted;
    }
}
