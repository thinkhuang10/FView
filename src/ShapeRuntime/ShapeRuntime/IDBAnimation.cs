using System.Collections.Generic;
using ShapeRuntime.DBAnimation;

namespace ShapeRuntime;

public interface IDBAnimation
{
    int DBResult { get; set; }

    bool Dbmultoperation { get; set; }

    List<ShapeRuntime.DBAnimation.DBAnimation> DBAnimationList { get; set; }

    bool Newtable { get; set; }

    string NewtableSQL { get; set; }

    bool Ansyncnewtable { get; set; }

    byte[] NewtableOtherData { get; set; }

    bool Dbselect { get; set; }

    bool Ansyncselect { get; set; }

    string DbselectSQL { get; set; }

    string DbselectTO { get; set; }

    byte[] DbselectOtherData { get; set; }

    bool Dbinsert { get; set; }

    bool Ansyncinsert { get; set; }

    string DbinsertSQL { get; set; }

    byte[] DbinsertOtherData { get; set; }

    bool Dbupdate { get; set; }

    bool Ansyncupdate { get; set; }

    string DbupdateSQL { get; set; }

    byte[] DbupdateOtherData { get; set; }

    bool Dbdelete { get; set; }

    bool Ansyncdelete { get; set; }

    string DbdeleteSQL { get; set; }

    byte[] DbdeleteOtherData { get; set; }

    void FireDBOperationOK();

    void FireDBOperationErr();
}
