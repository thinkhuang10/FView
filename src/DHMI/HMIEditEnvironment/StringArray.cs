using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace HMIEditEnvironment;

[Guid("0D79CA3E-EA06-4f37-88F0-6117502DC8DE")]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class StringArray
{
    public List<string> DATA = new();

    public void Add(string str)
    {
        DATA.Add(str);
    }

    public void Remove(string str)
    {
        DATA.Remove(str);
    }

    public void Clear()
    {
        DATA.Clear();
    }

    public string GetItem(int i)
    {
        return DATA[i];
    }

    public string[] GetArray()
    {
        return DATA.ToArray();
    }
}
