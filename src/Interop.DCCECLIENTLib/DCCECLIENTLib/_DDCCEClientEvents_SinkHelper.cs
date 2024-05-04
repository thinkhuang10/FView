using System.Runtime.InteropServices;

namespace DCCECLIENTLib;

[TypeLibType(TypeLibTypeFlags.FHidden)]
[ClassInterface(ClassInterfaceType.None)]
public sealed class _DDCCEClientEvents_SinkHelper : _DDCCEClientEvents
{
    public _DDCCEClientEvents_FireOnDataReadyEventHandler m_FireOnDataReadyDelegate;

    public _DDCCEClientEvents_FireOnVariableAlarmEventHandler m_FireOnVariableAlarmDelegate;

    public _DDCCEClientEvents_FireOnVariableLagEventHandler m_FireOnVariableLagDelegate;

    public _DDCCEClientEvents_FireOnLoadOverEventHandler m_FireOnLoadOverDelegate;

    public _DDCCEClientEvents_FireOnDeviceStatusEventHandler m_FireOnDeviceStatusDelegate;

    public _DDCCEClientEvents_FireOnBehaviorEventHandler m_FireOnBehaviorDelegate;

    public _DDCCEClientEvents_FireOnScanEventHandler m_FireOnScanDelegate;

    public _DDCCEClientEvents_FireOnScanBehaviorEventHandler m_FireOnScanBehaviorDelegate;

    public _DDCCEClientEvents_FireOnSyncTimeEventHandler m_FireOnSyncTimeDelegate;

    public int m_dwCookie;

    public void FireOnDataReady(int P_0)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        if (m_FireOnDataReadyDelegate != null)
        {
            m_FireOnDataReadyDelegate(P_0);
        }
    }

    public void FireOnVariableAlarm(int P_0, int P_1, ref object P_2)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        if (m_FireOnVariableAlarmDelegate != null)
        {
            m_FireOnVariableAlarmDelegate(P_0, P_1, ref P_2);
        }
    }

    public void FireOnVariableLag(int P_0, int P_1, ref object P_2)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        if (m_FireOnVariableLagDelegate != null)
        {
            m_FireOnVariableLagDelegate(P_0, P_1, ref P_2);
        }
    }

    public void FireOnLoadOver(int P_0, int P_1)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        if (m_FireOnLoadOverDelegate != null)
        {
            m_FireOnLoadOverDelegate(P_0, P_1);
        }
    }

    public void FireOnDeviceStatus(int P_0, int P_1, int P_2)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        if (m_FireOnDeviceStatusDelegate != null)
        {
            m_FireOnDeviceStatusDelegate(P_0, P_1, P_2);
        }
    }

    public void FireOnBehavior(int P_0, int P_1, object P_2, int P_3)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        if (m_FireOnBehaviorDelegate != null)
        {
            m_FireOnBehaviorDelegate(P_0, P_1, P_2, P_3);
        }
    }

    public void FireOnScan(object P_0, int P_1)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        if (m_FireOnScanDelegate != null)
        {
            m_FireOnScanDelegate(P_0, P_1);
        }
    }

    public void FireOnScanBehavior(int P_0, int P_1, int P_2, int P_3, ref int P_4, object P_5, int P_6)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        if (m_FireOnScanBehaviorDelegate != null)
        {
            m_FireOnScanBehaviorDelegate(P_0, P_1, P_2, P_3, ref P_4, P_5, P_6);
        }
    }

    public void FireOnSyncTime(int P_0, int P_1, int P_2, int P_3, int P_4, int P_5, int P_6)
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        if (m_FireOnSyncTimeDelegate != null)
        {
            m_FireOnSyncTimeDelegate(P_0, P_1, P_2, P_3, P_4, P_5, P_6);
        }
    }

    internal _DDCCEClientEvents_SinkHelper()
    {
        //Error decoding local variables: Signature type sequence must have at least one element.
        m_dwCookie = 0;
        m_FireOnDataReadyDelegate = null;
        m_FireOnVariableAlarmDelegate = null;
        m_FireOnVariableLagDelegate = null;
        m_FireOnLoadOverDelegate = null;
        m_FireOnDeviceStatusDelegate = null;
        m_FireOnBehaviorDelegate = null;
        m_FireOnScanDelegate = null;
        m_FireOnScanBehaviorDelegate = null;
        m_FireOnSyncTimeDelegate = null;
    }
}
