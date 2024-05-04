using System.IO;
using System.IO.Compression;

namespace HMIWeb;

public class DeflateCompress
{
    public static byte[] Compress(byte[] bytes)
    {
        using MemoryStream memoryStream = new();
        using (DeflateStream deflateStream = new(memoryStream, CompressionMode.Compress))
        {
            deflateStream.Write(bytes, 0, bytes.Length);
            deflateStream.Close();
        }
        return memoryStream.ToArray();
    }

    public static byte[] Decompress(byte[] bytes)
    {
        using MemoryStream stream = new(bytes);
        using DeflateStream deflateStream = new(stream, CompressionMode.Decompress);
        byte[] array = new byte[4096];
        int num = 0;
        using MemoryStream memoryStream = new();
        while ((num = deflateStream.Read(array, 0, array.Length)) != 0)
        {
            memoryStream.Write(array, 0, num);
        }
        return memoryStream.ToArray();
    }
}
