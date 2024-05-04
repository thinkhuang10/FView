using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace DCCECLIENTLib;

internal sealed class _DDCCEClientEvents_EventProvider : _DDCCEClientEvents_Event, IDisposable
{
    private readonly IConnectionPointContainer m_ConnectionPointContainer;

    private ArrayList m_aEventSinkHelpers;

    private IConnectionPoint m_ConnectionPoint;

    private void Init()
    {
        Guid riid = new(new byte[16]
        {
            24, 115, 116, 185, 1, 246, 109, 70, 155, 24,
            139, 53, 58, 16, 49, 118
        });
        IConnectionPoint ppCP;
        m_ConnectionPointContainer.FindConnectionPoint(ref riid, out ppCP);
        m_ConnectionPoint = ppCP;
        m_aEventSinkHelpers = new ArrayList();
    }

    public event _DDCCEClientEvents_FireOnDataReadyEventHandler FireOnDataReady
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
                _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = new();
                int pdwCookie;
                m_ConnectionPoint.Advise(dDCCEClientEvents_SinkHelper, out pdwCookie);
                dDCCEClientEvents_SinkHelper.m_dwCookie = pdwCookie;
                dDCCEClientEvents_SinkHelper.m_FireOnDataReadyDelegate = value;
                m_aEventSinkHelpers.Add(dDCCEClientEvents_SinkHelper);
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
                    _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = (_DDCCEClientEvents_SinkHelper)m_aEventSinkHelpers[num];
                    if (dDCCEClientEvents_SinkHelper.m_FireOnDataReadyDelegate != null && ((dDCCEClientEvents_SinkHelper.m_FireOnDataReadyDelegate.Equals(value) ? 1u : 0u) & 0xFFu) != 0)
                    {
                        m_aEventSinkHelpers.RemoveAt(num);
                        m_ConnectionPoint.Unadvise(dDCCEClientEvents_SinkHelper.m_dwCookie);
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

    public event _DDCCEClientEvents_FireOnVariableAlarmEventHandler FireOnVariableAlarm
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
                _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = new();
                int pdwCookie;
                m_ConnectionPoint.Advise(dDCCEClientEvents_SinkHelper, out pdwCookie);
                dDCCEClientEvents_SinkHelper.m_dwCookie = pdwCookie;
                dDCCEClientEvents_SinkHelper.m_FireOnVariableAlarmDelegate = value;
                m_aEventSinkHelpers.Add(dDCCEClientEvents_SinkHelper);
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
                    _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = (_DDCCEClientEvents_SinkHelper)m_aEventSinkHelpers[num];
                    if (dDCCEClientEvents_SinkHelper.m_FireOnVariableAlarmDelegate != null && ((dDCCEClientEvents_SinkHelper.m_FireOnVariableAlarmDelegate.Equals(value) ? 1u : 0u) & 0xFFu) != 0)
                    {
                        m_aEventSinkHelpers.RemoveAt(num);
                        m_ConnectionPoint.Unadvise(dDCCEClientEvents_SinkHelper.m_dwCookie);
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

    public event _DDCCEClientEvents_FireOnVariableLagEventHandler FireOnVariableLag
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
                _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = new();
                int pdwCookie;
                m_ConnectionPoint.Advise(dDCCEClientEvents_SinkHelper, out pdwCookie);
                dDCCEClientEvents_SinkHelper.m_dwCookie = pdwCookie;
                dDCCEClientEvents_SinkHelper.m_FireOnVariableLagDelegate = value;
                m_aEventSinkHelpers.Add(dDCCEClientEvents_SinkHelper);
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
                    _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = (_DDCCEClientEvents_SinkHelper)m_aEventSinkHelpers[num];
                    if (dDCCEClientEvents_SinkHelper.m_FireOnVariableLagDelegate != null && ((dDCCEClientEvents_SinkHelper.m_FireOnVariableLagDelegate.Equals(value) ? 1u : 0u) & 0xFFu) != 0)
                    {
                        m_aEventSinkHelpers.RemoveAt(num);
                        m_ConnectionPoint.Unadvise(dDCCEClientEvents_SinkHelper.m_dwCookie);
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

    public event _DDCCEClientEvents_FireOnLoadOverEventHandler FireOnLoadOver
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
                _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = new();
                int pdwCookie;
                m_ConnectionPoint.Advise(dDCCEClientEvents_SinkHelper, out pdwCookie);
                dDCCEClientEvents_SinkHelper.m_dwCookie = pdwCookie;
                dDCCEClientEvents_SinkHelper.m_FireOnLoadOverDelegate = value;
                m_aEventSinkHelpers.Add(dDCCEClientEvents_SinkHelper);
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
                    _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = (_DDCCEClientEvents_SinkHelper)m_aEventSinkHelpers[num];
                    if (dDCCEClientEvents_SinkHelper.m_FireOnLoadOverDelegate != null && ((dDCCEClientEvents_SinkHelper.m_FireOnLoadOverDelegate.Equals(value) ? 1u : 0u) & 0xFFu) != 0)
                    {
                        m_aEventSinkHelpers.RemoveAt(num);
                        m_ConnectionPoint.Unadvise(dDCCEClientEvents_SinkHelper.m_dwCookie);
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

    public event _DDCCEClientEvents_FireOnDeviceStatusEventHandler FireOnDeviceStatus
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
                _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = new();
                int pdwCookie;
                m_ConnectionPoint.Advise(dDCCEClientEvents_SinkHelper, out pdwCookie);
                dDCCEClientEvents_SinkHelper.m_dwCookie = pdwCookie;
                dDCCEClientEvents_SinkHelper.m_FireOnDeviceStatusDelegate = value;
                m_aEventSinkHelpers.Add(dDCCEClientEvents_SinkHelper);
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
                    _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = (_DDCCEClientEvents_SinkHelper)m_aEventSinkHelpers[num];
                    if (dDCCEClientEvents_SinkHelper.m_FireOnDeviceStatusDelegate != null && ((dDCCEClientEvents_SinkHelper.m_FireOnDeviceStatusDelegate.Equals(value) ? 1u : 0u) & 0xFFu) != 0)
                    {
                        m_aEventSinkHelpers.RemoveAt(num);
                        m_ConnectionPoint.Unadvise(dDCCEClientEvents_SinkHelper.m_dwCookie);
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

    public event _DDCCEClientEvents_FireOnBehaviorEventHandler FireOnBehavior
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
                _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = new();
                int pdwCookie;
                m_ConnectionPoint.Advise(dDCCEClientEvents_SinkHelper, out pdwCookie);
                dDCCEClientEvents_SinkHelper.m_dwCookie = pdwCookie;
                dDCCEClientEvents_SinkHelper.m_FireOnBehaviorDelegate = value;
                m_aEventSinkHelpers.Add(dDCCEClientEvents_SinkHelper);
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
                    _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = (_DDCCEClientEvents_SinkHelper)m_aEventSinkHelpers[num];
                    if (dDCCEClientEvents_SinkHelper.m_FireOnBehaviorDelegate != null && ((dDCCEClientEvents_SinkHelper.m_FireOnBehaviorDelegate.Equals(value) ? 1u : 0u) & 0xFFu) != 0)
                    {
                        m_aEventSinkHelpers.RemoveAt(num);
                        m_ConnectionPoint.Unadvise(dDCCEClientEvents_SinkHelper.m_dwCookie);
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

    public event _DDCCEClientEvents_FireOnScanEventHandler FireOnScan
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
                _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = new();
                int pdwCookie;
                m_ConnectionPoint.Advise(dDCCEClientEvents_SinkHelper, out pdwCookie);
                dDCCEClientEvents_SinkHelper.m_dwCookie = pdwCookie;
                dDCCEClientEvents_SinkHelper.m_FireOnScanDelegate = value;
                m_aEventSinkHelpers.Add(dDCCEClientEvents_SinkHelper);
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
                    _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = (_DDCCEClientEvents_SinkHelper)m_aEventSinkHelpers[num];
                    if (dDCCEClientEvents_SinkHelper.m_FireOnScanDelegate != null && ((dDCCEClientEvents_SinkHelper.m_FireOnScanDelegate.Equals(value) ? 1u : 0u) & 0xFFu) != 0)
                    {
                        m_aEventSinkHelpers.RemoveAt(num);
                        m_ConnectionPoint.Unadvise(dDCCEClientEvents_SinkHelper.m_dwCookie);
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

    public event _DDCCEClientEvents_FireOnScanBehaviorEventHandler FireOnScanBehavior
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
                _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = new();
                int pdwCookie;
                m_ConnectionPoint.Advise(dDCCEClientEvents_SinkHelper, out pdwCookie);
                dDCCEClientEvents_SinkHelper.m_dwCookie = pdwCookie;
                dDCCEClientEvents_SinkHelper.m_FireOnScanBehaviorDelegate = value;
                m_aEventSinkHelpers.Add(dDCCEClientEvents_SinkHelper);
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
                    _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = (_DDCCEClientEvents_SinkHelper)m_aEventSinkHelpers[num];
                    if (dDCCEClientEvents_SinkHelper.m_FireOnScanBehaviorDelegate != null && ((dDCCEClientEvents_SinkHelper.m_FireOnScanBehaviorDelegate.Equals(value) ? 1u : 0u) & 0xFFu) != 0)
                    {
                        m_aEventSinkHelpers.RemoveAt(num);
                        m_ConnectionPoint.Unadvise(dDCCEClientEvents_SinkHelper.m_dwCookie);
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

    public event _DDCCEClientEvents_FireOnSyncTimeEventHandler FireOnSyncTime
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
                _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = new();
                int pdwCookie;
                m_ConnectionPoint.Advise(dDCCEClientEvents_SinkHelper, out pdwCookie);
                dDCCEClientEvents_SinkHelper.m_dwCookie = pdwCookie;
                dDCCEClientEvents_SinkHelper.m_FireOnSyncTimeDelegate = value;
                m_aEventSinkHelpers.Add(dDCCEClientEvents_SinkHelper);
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
                    _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = (_DDCCEClientEvents_SinkHelper)m_aEventSinkHelpers[num];
                    if (dDCCEClientEvents_SinkHelper.m_FireOnSyncTimeDelegate != null && ((dDCCEClientEvents_SinkHelper.m_FireOnSyncTimeDelegate.Equals(value) ? 1u : 0u) & 0xFFu) != 0)
                    {
                        m_aEventSinkHelpers.RemoveAt(num);
                        m_ConnectionPoint.Unadvise(dDCCEClientEvents_SinkHelper.m_dwCookie);
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

    public _DDCCEClientEvents_EventProvider(object P_0)
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
                    _DDCCEClientEvents_SinkHelper dDCCEClientEvents_SinkHelper = (_DDCCEClientEvents_SinkHelper)m_aEventSinkHelpers[num];
                    m_ConnectionPoint.Unadvise(dDCCEClientEvents_SinkHelper.m_dwCookie);
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
