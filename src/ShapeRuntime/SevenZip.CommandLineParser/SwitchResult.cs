using System.Collections;

namespace SevenZip.CommandLineParser;

public class SwitchResult
{
    public bool ThereIs;

    public bool WithMinus;

    public ArrayList PostStrings = new();

    public int PostCharIndex;

    public SwitchResult()
    {
        ThereIs = false;
    }
}
