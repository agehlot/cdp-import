using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace ImportTrigger.Helpers
{
    public static class BatchImportHelper
    {
        public static void GzipFile(string currentDirectory)
        {
            var path = Path.Combine(currentDirectory, "Json", "UpdateUsers.json");
            FileInfo fileToBeGZipped = new FileInfo(path);
            FileInfo gzipFileName = new FileInfo(string.Concat(fileToBeGZipped.FullName, ".gz"));
            using (FileStream fileToBeZippedAsStream = fileToBeGZipped.OpenRead())
            {
                using (FileStream gzipTargetAsStream = gzipFileName.Create())
                {
                    using (GZipStream gzipStream = new GZipStream(gzipTargetAsStream, CompressionMode.Compress))
                    {
                        try
                        {
                            fileToBeZippedAsStream.CopyTo(gzipStream);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        public static void GZipFile(string currentDirectory, byte[] bytes, string batchID)
        {
            var path = Path.Combine(currentDirectory, "Batches", batchID);
            FileInfo gzipFileName = new FileInfo(string.Concat(path, ".gz"));
            using (FileStream gzipTargetAsStream = gzipFileName.Create())
            {
                using (var gzipStream = new GZipStream(gzipTargetAsStream, CompressionLevel.Optimal))
                {
                    gzipStream.Write(bytes, 0, bytes.Length);
                }
            }
        }
        public static string GetMD5Checksum(string currentDirectory, string batchID)
        {
            try
            {
                var path = Path.Combine(currentDirectory, "Batches", batchID);
                FileInfo gzipFileName = new FileInfo(string.Concat(path, ".gz"));
                using (var md5 = MD5.Create())
                {
                    using (FileStream fileStream = gzipFileName.OpenRead())
                    {
                        var hash = md5.ComputeHash(fileStream);
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public static long GetFileSize(string currentDirectory, string batchID)
        {
            try
            {
                var path = Path.Combine(currentDirectory, "Batches", batchID);
                FileInfo gzipFileName = new FileInfo(string.Concat(path, ".gz"));
                return gzipFileName.Length;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }
        public static Guid GetGuid()
        {
            try
            {
                return Guid.NewGuid();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return default;
        }
        public static byte[] HexStringToBytes(this string inputHex)
        {
            var resultantArray = new byte[inputHex.Length / 2];
            for (var i = 0; i < resultantArray.Length; i++)
            {
                resultantArray[i] = System.Convert.ToByte(inputHex.Substring(i * 2, 2), 16);
            }
            return resultantArray;
        }
        public static bool CheckFileSize(long filesize)
        {
            try
            {
                long maxFileSize = 50 * 1024 * 1024;
                if (filesize < maxFileSize)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
