using System;
using System.IO;
using SevenZip.Compression.LZMA;

namespace SevenZip.Compression;

public class LZMACoder
{
    private static readonly int dictionary = 2097152;

    private static readonly int posStateBits = 2;

    private static readonly int litContextBits = 3;

    private static readonly int litPosBits = 0;

    private static readonly int algorithm = 2;

    private static readonly int numFastBytes = 128;

    private static readonly bool eos = false;

    private static readonly string mf = "bt4";

    private static readonly CoderPropID[] propIDs = new CoderPropID[8]
    {
        CoderPropID.DictionarySize,
        CoderPropID.PosStateBits,
        CoderPropID.LitContextBits,
        CoderPropID.LitPosBits,
        CoderPropID.Algorithm,
        CoderPropID.NumFastBytes,
        CoderPropID.MatchFinder,
        CoderPropID.EndMarker
    };

    private static readonly object[] properties = new object[8] { dictionary, posStateBits, litContextBits, litPosBits, algorithm, numFastBytes, mf, eos };

    public LZMACoder()
    {
        if (!BitConverter.IsLittleEndian)
        {
            throw new Exception("Not implemented");
        }
    }

    public MemoryStream decompress(MemoryStream inStream)
    {
        return decompress(inStream, closeInStream: false);
    }

    public MemoryStream decompress(MemoryStream inStream, bool closeInStream)
    {
        inStream.Position = 0L;
        MemoryStream memoryStream = new();
        byte[] array = new byte[5];
        if (inStream.Read(array, 0, 5) != 5)
        {
            throw new Exception("input .lzma is too short");
        }
        Decoder decoder = new();
        decoder.SetDecoderProperties(array);
        long num = 0L;
        if (BitConverter.IsLittleEndian)
        {
            for (int i = 0; i < 8; i++)
            {
                int num2 = inStream.ReadByte();
                if (num2 < 0)
                {
                    throw new Exception("Can't Read 1");
                }
                num |= (long)((ulong)(byte)num2 << 8 * i);
            }
        }
        long inSize = inStream.Length - inStream.Position;
        decoder.Code(inStream, memoryStream, inSize, num, null);
        if (closeInStream)
        {
            inStream.Close();
        }
        return memoryStream;
    }

    public MemoryStream compress(MemoryStream inStream)
    {
        return compress(inStream, closeInStream: false);
    }

    public MemoryStream compress(MemoryStream inStream, bool closeInStream)
    {
        inStream.Position = 0L;
        long length = inStream.Length;
        MemoryStream memoryStream = new();
        Encoder encoder = new();
        encoder.SetCoderProperties(propIDs, properties);
        encoder.WriteCoderProperties(memoryStream);
        if (BitConverter.IsLittleEndian)
        {
            byte[] bytes = BitConverter.GetBytes(length);
            memoryStream.Write(bytes, 0, bytes.Length);
        }
        encoder.Code(inStream, memoryStream, -1L, -1L, null);
        if (closeInStream)
        {
            inStream.Close();
        }
        return memoryStream;
    }
}
