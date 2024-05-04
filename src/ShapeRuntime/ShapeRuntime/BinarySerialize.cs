using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ShapeRuntime;

public class BinarySerialize<T>
{
    public void Serialize(T obj, string strFilePath)
    {
        FileInfo fileInfo = new(strFilePath);
        if (fileInfo.Exists)
        {
            fileInfo.Delete();
        }
        using FileStream serializationStream = new(strFilePath, FileMode.Create);
        BinaryFormatter binaryFormatter = new();
        binaryFormatter.Serialize(serializationStream, obj);
    }

    public T DeSerialize(string filePath)
    {
        FileInfo fileInfo = new(filePath);
        if (!fileInfo.Exists)
        {
            return default;
        }
        using FileStream serializationStream = new(filePath, FileMode.Open);
        BinaryFormatter binaryFormatter = new();
        try
        {
            return (T)binaryFormatter.Deserialize(serializationStream);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
