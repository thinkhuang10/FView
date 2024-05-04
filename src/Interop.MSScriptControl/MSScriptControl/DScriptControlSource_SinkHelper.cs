using System.Runtime.InteropServices;

namespace MSScriptControl;

[ClassInterface(ClassInterfaceType.None)]
[TypeLibType(TypeLibTypeFlags.FHidden)]
public sealed class DScriptControlSource_SinkHelper : DScriptControlSource
{
    public DScriptControlSource_ErrorEventHandler m_ErrorDelegate;

    public DScriptControlSource_TimeoutEventHandler m_TimeoutDelegate;

    public int m_dwCookie;

    public void Error()
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        if (m_ErrorDelegate != null)
        {
            m_ErrorDelegate();
        }
    }

    public void Timeout()
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        if (m_TimeoutDelegate != null)
        {
            m_TimeoutDelegate();
        }
    }

    internal DScriptControlSource_SinkHelper()
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        m_dwCookie = 0;
        m_ErrorDelegate = null;
        m_TimeoutDelegate = null;
    }
}
