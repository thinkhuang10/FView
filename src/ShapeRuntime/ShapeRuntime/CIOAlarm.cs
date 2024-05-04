using System;

namespace ShapeRuntime;

[Serializable]
public class CIOAlarm
{
    public string name = "";

    public string MsgID = "";

    public string ScriptName = "";

    public string[] script = new string[12]
    {
        "", "", "", "", "", "", "", "", "", "",
        "", ""
    };

    public string[] boolAlarmScript = new string[10] { "", "", "", "", "", "", "", "", "", "" };
}
