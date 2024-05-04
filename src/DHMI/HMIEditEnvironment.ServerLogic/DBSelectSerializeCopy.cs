using System;
using System.Collections.Generic;
using System.Data;

namespace HMIEditEnvironment.ServerLogic;

[Serializable]
public class DBSelectSerializeCopy
{
    public bool ansync;

    public string ResultString = "";

    public string HeadString = "";

    public string JoinString = "";

    public string Wherestr = "";

    public string Orderstr = "";

    public DataTable DgDatatable = new();

    public List<string> ListboxContentstr = new();

    public DataTable WhereDatatable = new();

    public DataTable OrderDatatable = new();
}
