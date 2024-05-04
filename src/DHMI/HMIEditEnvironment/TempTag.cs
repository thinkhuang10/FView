using System.Collections.Generic;

namespace HMIEditEnvironment;

public class TempTag
{
    public object temp;

    public object value;

    public object obj;

    public object oldObj;

    public object t;

    public List<int> SafeRegion;

    public bool isDeleted;

    public static Dictionary<object, object> newOldDict = new();

    public object WhenOutThenDo;

    public TempTag(object temp, object value, object obj)
    {
        this.temp = temp;
        this.value = value;
        this.obj = obj;
    }
}
