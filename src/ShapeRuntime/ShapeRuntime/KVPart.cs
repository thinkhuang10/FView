using System;

namespace ShapeRuntime;

[Serializable]
public class KVPart<TKey, TValue>
{
    public TKey key;

    public TValue val;

    public TKey Key
    {
        get
        {
            return key;
        }
        set
        {
            key = value;
        }
    }

    public TValue Value
    {
        get
        {
            return val;
        }
        set
        {
            val = value;
        }
    }

    public KVPart(TKey key, TValue value)
    {
        this.key = key;
        val = value;
    }

    public override string ToString()
    {
        if (key != null)
        {
            return key.ToString();
        }
        return base.ToString();
    }
}
