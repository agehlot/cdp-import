using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchUpdateStatus.Helpers
{
    public static class BatchHelper
    {
        public static void Decompress(string currentDirectory, string batchid, byte[] bytes)
        {
            try
            {
                var path = Path.Combine(currentDirectory, "ErrorBatches", batchid);
                FileInfo errorBatchFile = new FileInfo(path);
                using (FileStream errorBatchStream = errorBatchFile.Create())
                {
                    using (var memoryStream = new MemoryStream(bytes))
                    {
                        using (var decompressStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                        {
                            decompressStream.CopyTo(errorBatchStream);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static byte[] ReadFully(Stream input)
        {
            try
            {
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }

                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
