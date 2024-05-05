using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace MSScriptControl;

internal sealed class DScriptControlSource_EventProvider : DScriptControlSource_Event, IDisposable
{
    private readonly IConnectionPointContainer m_ConnectionPointContainer;

    private ArrayList m_aEventSinkHelpers;

    private IConnectionPoint m_ConnectionPoint;

    private void Init()
    {
        Guid riid = new(new byte[16]
        {
            96, 125, 22, 139, 5, 134, 208, 17, 171, 203,
            0, 160, 201, 15, 255, 192
        });
        m_ConnectionPointContainer.FindConnectionPoint(ref riid, out IConnectionPoint ppCP);
        m_ConnectionPoint = ppCP;
        m_aEventSinkHelpers = new ArrayList();
    }

    public event DScriptControlSource_ErrorEventHandler Error
    {
        add
        {
            Monitor.Enter(this);
            try
            {
                if (m_ConnectionPoint == null)
                {
                    Init();
                }
                DScriptControlSource_SinkHelper dScriptControlSource_SinkHelper = new();
                m_ConnectionPoint.Advise(dScriptControlSource_SinkHelper, out int pdwCookie);
                dScriptControlSource_SinkHelper.m_dwCookie = pdwCookie;
                dScriptControlSource_SinkHelper.m_ErrorDelegate = value;
                m_aEventSinkHelpers.Add(dScriptControlSource_SinkHelper);
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        remove
        {
            Monitor.Enter(this);
            try
            {
                if (m_aEventSinkHelpers == null)
                {
                    return;
                }
                int count = m_aEventSinkHelpers.Count;
                int num = 0;
                if (0 >= count)
                {
                    return;
                }
                do
                {
                    DScriptControlSource_SinkHelper dScriptControlSource_SinkHelper = (DScriptControlSource_SinkHelper)m_aEventSinkHelpers[num];
                    if (dScriptControlSource_SinkHelper.m_ErrorDelegate != null && ((dScriptControlSource_SinkHelper.m_ErrorDelegate.Equals(value) ? 1u : 0u) & 0xFFu) != 0)
                    {
                        m_aEventSinkHelpers.RemoveAt(num);
                        m_ConnectionPoint.Unadvise(dScriptControlSource_SinkHelper.m_dwCookie);
                        if (count <= 1)
                        {
                            Marshal.ReleaseComObject(m_ConnectionPoint);
                            m_ConnectionPoint = null;
                            m_aEventSinkHelpers = null;
                        }
                        break;
                    }
                    num++;
                }
                while (num < count);
            }
            finally
            {
                Monitor.Exit(this);
            }
        }
    }

    public event DScriptControlSource_TimeoutEventHandler Timeout
    {

        add
        {
            Monitor.Enter(this);
            try
            {
                if (m_ConnectionPoint == null)
                {
                    Init();
                }
                DScriptControlSource_SinkHelper dScriptControlSource_SinkHelper = new();
                m_ConnectionPoint.Advise(dScriptControlSource_SinkHelper, out int pdwCookie);
                dScriptControlSource_SinkHelper.m_dwCookie = pdwCookie;
                dScriptControlSource_SinkHelper.m_TimeoutDelegate = value;
                m_aEventSinkHelpers.Add(dScriptControlSource_SinkHelper);
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

        remove
        {
            Monitor.Enter(this);
            try
            {
                if (m_aEventSinkHelpers == null)
                {
                    return;
                }
                int count = m_aEventSinkHelpers.Count;
                int num = 0;
                if (0 >= count)
                {
                    return;
                }
                do
                {
                    DScriptControlSource_SinkHelper dScriptControlSource_SinkHelper = (DScriptControlSource_SinkHelper)m_aEventSinkHelpers[num];
                    if (dScriptControlSource_SinkHelper.m_TimeoutDelegate != null && ((dScriptControlSource_SinkHelper.m_TimeoutDelegate.Equals(value) ? 1u : 0u) & 0xFFu) != 0)
                    {
                        m_aEventSinkHelpers.RemoveAt(num);
                        m_ConnectionPoint.Unadvise(dScriptControlSource_SinkHelper.m_dwCookie);
                        if (count <= 1)
                        {
                            Marshal.ReleaseComObject(m_ConnectionPoint);
                            m_ConnectionPoint = null;
                            m_aEventSinkHelpers = null;
                        }
                        break;
                    }
                    num++;
                }
                while (num < count);
            }
            finally
            {
                Monitor.Exit(this);
            }
        }

    }

    public DScriptControlSource_EventProvider(object P_0)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        m_ConnectionPointContainer = (IConnectionPointContainer)P_0;
    }

    public void Finalize()
    {
        Monitor.Enter(this);
        try
        {
            if (m_ConnectionPoint == null)
            {
                return;
            }
            int count = m_aEventSinkHelpers.Count;
            int num = 0;
            if (0 < count)
            {
                do
                {
                    DScriptControlSource_SinkHelper dScriptControlSource_SinkHelper = (DScriptControlSource_SinkHelper)m_aEventSinkHelpers[num];
                    m_ConnectionPoint.Unadvise(dScriptControlSource_SinkHelper.m_dwCookie);
                    num++;
                }
                while (num < count);
            }
            Marshal.ReleaseComObject(m_ConnectionPoint);
        }
        catch (Exception)
        {
        }
        finally
        {
            Monitor.Exit(this);
        }
    }

    public void Dispose()
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        Finalize();
        GC.SuppressFinalize(this);
    }
}
