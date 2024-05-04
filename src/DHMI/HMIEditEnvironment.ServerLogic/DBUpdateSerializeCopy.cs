using System;
using System.Collections.Generic;
using System.Data;

namespace HMIEditEnvironment.ServerLogic;

[Serializable]
public class DBUpdateSerializeCopy
{
    public bool ansync;

    public string ResultString = "";

    public string HeadString = "";

    public string JoinString = "";

    public string NowTable = "";

    public Dictionary<string, string[]> ResultStrings = new();

    public List<string> ListboxContentstr = new();

    public DataSet DgDatatables = new();

    public DataSet WhereDatatables = new();
}
