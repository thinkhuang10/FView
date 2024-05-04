using System;
using System.Collections.Generic;

namespace ShapeRuntime;

[Serializable]
public class EventSetItem
{
    private string condition;

    private string eventName;

    private string operationType;

    private KVPart<string, string> toObject;

    private string fromObject;

    private List<KVPart<string, string>> paras;

    private object tag;

    public string Condition
    {
        get
        {
            return condition;
        }
        set
        {
            condition = value;
        }
    }

    public string EventName
    {
        get
        {
            return eventName;
        }
        set
        {
            eventName = value;
        }
    }

    public string OperationType
    {
        get
        {
            return operationType;
        }
        set
        {
            operationType = value;
        }
    }

    public KVPart<string, string> ToObject
    {
        get
        {
            return toObject;
        }
        set
        {
            toObject = value;
        }
    }

    public string FromObject
    {
        get
        {
            return fromObject;
        }
        set
        {
            fromObject = value;
        }
    }

    public List<KVPart<string, string>> Paras
    {
        get
        {
            return paras;
        }
        set
        {
            paras = value;
        }
    }

    public object Tag
    {
        get
        {
            return tag;
        }
        set
        {
            tag = value;
        }
    }
}
