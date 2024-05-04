using System;

namespace HMIWeb;

public class ErrorException : Exception
{
    private readonly int Line;

    private readonly int Column;

    private readonly string msg;

    public override string Message => msg + " Line: " + Line + " Columu: " + Column;

    public ErrorException(string message, int line, int colume)
    {
        Line = line;
        Column = colume;
        msg = message;
    }
}
