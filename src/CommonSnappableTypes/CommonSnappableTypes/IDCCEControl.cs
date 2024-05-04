using System.Drawing;

namespace CommonSnappableTypes;

public interface IDCCEControl
{
    bool isRuning { get; set; }

    event GetValue GetValueEvent;

    event SetValue SetValueEvent;

    event GetDataBase GetDataBaseEvent;

    event GetVarTable GetVarTableEvent;

    event GetValue GetSystemItemEvent;

    Bitmap GetLogo();

    byte[] Serialize();

    void DeSerialize(byte[] bytes);

    void Stop();
}
