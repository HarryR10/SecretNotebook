using System;
using System.IO;
using System.Security.Cryptography;

public class DataProtectionSample
{
    // Create byte array for additional entropy when using Protect method.
    static byte[] s_aditionalEntropy = { 9, 8, 7, 6, 5, 1, 4, 48, 145 };

    public static void Main()
    {
        #region MsExample
        //// Create a simple byte array containing data to be encrypted.
        //byte[] secret = { 0, 1, 2, 3, 4, 1, 2, 3, 4 };

        ////Encrypt the data.
        //byte[] encryptedSecret = Protect(secret);
        //Console.WriteLine("The encrypted byte array is:");
        //PrintValues(encryptedSecret);

        //// Decrypt the data and store in a byte array.
        //byte[] originalData = Unprotect(encryptedSecret);
        //Console.WriteLine("{0}The original data is:", Environment.NewLine);
        //PrintValues(originalData);
        #endregion

        string path = @"ItsFile.txt";

        var result = Conversion(path, Protect);
        Console.WriteLine("The encrypted byte array is:");
        PrintValues(result);

        result = Conversion(path, Unprotect);
        Console.WriteLine("{0}The original data is:", Environment.NewLine);
        PrintValues(result);
    }

    public static byte[] Protect(byte[] data)
    {
        try
        {
            // Encrypt the data using DataProtectionScope.CurrentUser. The result can be decrypted
            // only by the same current user.
            return ProtectedData.Protect(data, s_aditionalEntropy, DataProtectionScope.CurrentUser);
        }
        catch (CryptographicException e)
        {
            Console.WriteLine("Data was not encrypted. An error occurred.");
            Console.WriteLine(e.ToString());
            return null;
        }
    }

    public static byte[] Unprotect(byte[] data)
    {
        try
        {
            //Decrypt the data using DataProtectionScope.CurrentUser.
            return ProtectedData.Unprotect(data, s_aditionalEntropy, DataProtectionScope.CurrentUser);
        }
        catch (CryptographicException e)
        {
            Console.WriteLine("Data was not decrypted. An error occurred.");
            Console.WriteLine(e.ToString());
            return null;
        }
    }

    public static void PrintValues(Byte[] myArr)
    {
        foreach (Byte i in myArr)
        {
            Console.Write("\t{0}", i);
        }
        Console.WriteLine();
    }

    public static byte[] Conversion(string path, Func<byte[], byte[]> doIt)
    {
        if (!File.Exists(path))
            throw new Exception();

        byte[] bytes;

        using (FileStream fsSource = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            bytes = new byte[fsSource.Length];
            int numBytesToRead = (int)fsSource.Length;
            int numBytesRead = 0;

            while (numBytesToRead > 0)
            {
                int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

                if (n == 0)
                    break;

                numBytesRead += n;
                numBytesToRead -= n;
            }
        }

        using (FileStream newSource = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            bytes = doIt(bytes);
            newSource.Write(bytes, 0, bytes.Length);
        }

        return bytes;
    }
}