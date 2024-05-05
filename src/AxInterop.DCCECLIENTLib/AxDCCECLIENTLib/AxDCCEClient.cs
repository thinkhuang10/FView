using System;
using System.ComponentModel;
using System.Windows.Forms;
using DCCECLIENTLib;

namespace AxDCCECLIENTLib;

[DefaultEvent("FireOnDataReady")]
[DesignTimeVisible(true)]
[Clsid("{6301125a-4b83-402e-9361-4829c6591b31}")]
public class AxDCCEClient : AxHost
{
    private _DDCCEClient ocx;

    private AxDCCEClientEventMulticaster eventMulticaster;

    private ConnectionPointCookie cookie;

    public event _DDCCEClientEvents_FireOnDataReadyEventHandler FireOnDataReady;

    public event _DDCCEClientEvents_FireOnVariableAlarmEventHandler FireOnVariableAlarm;

    public event _DDCCEClientEvents_FireOnVariableLagEventHandler FireOnVariableLag;

    public event _DDCCEClientEvents_FireOnLoadOverEventHandler FireOnLoadOver;

    public event _DDCCEClientEvents_FireOnDeviceStatusEventHandler FireOnDeviceStatus;

    public event _DDCCEClientEvents_FireOnBehaviorEventHandler FireOnBehavior;

    public event _DDCCEClientEvents_FireOnScanEventHandler FireOnScan;

    public event _DDCCEClientEvents_FireOnScanBehaviorEventHandler FireOnScanBehavior;

    public event _DDCCEClientEvents_FireOnSyncTimeEventHandler FireOnSyncTime;

    public AxDCCEClient()
        : base("6301125a-4b83-402e-9361-4829c6591b31")
    {
    }

    public virtual int DataInit(int lShakeInterval)
    {
        if (ocx == null)
        {
            throw new InvalidActiveXStateException("DataInit", ActiveXInvokeKind.MethodInvoke);
        }
        return ocx.DataInit(lShakeInterval);
    }

    public virtual object Read(int lID)
    {
        if (ocx == null)
        {
            throw new InvalidActiveXStateException("Read", ActiveXInvokeKind.MethodInvoke);
        }
        return ocx.Read(lID);
    }

    public virtual int Write(int lID, object varValue)
    {
        if (ocx == null)
        {
            throw new InvalidActiveXStateException("Write", ActiveXInvokeKind.MethodInvoke);
        }
        return ocx.Write(lID, varValue);
    }

    public virtual object ReadEx(int lStartID, int lEndID)
    {
        if (ocx == null)
        {
            throw new InvalidActiveXStateException("ReadEx", ActiveXInvokeKind.MethodInvoke);
        }
        return ocx.ReadEx(lStartID, lEndID);
    }

    public virtual int WriteEx(int lStartID, int lWriteCount, object varValue)
    {
        if (ocx == null)
        {
            throw new InvalidActiveXStateException("WriteEx", ActiveXInvokeKind.MethodInvoke);
        }
        return ocx.WriteEx(lStartID, lWriteCount, varValue);
    }

    public virtual object Execute(int lMethordType, object varValue, int lDeviceID, int lStartID, int lRWCount)
    {
        if (ocx == null)
        {
            throw new InvalidActiveXStateException("Execute", ActiveXInvokeKind.MethodInvoke);
        }
        return ocx.Execute(lMethordType, varValue, lDeviceID, lStartID, lRWCount);
    }

    public virtual int ExecBehavior(int lBehaviorID, object varValue, int lDeviceID)
    {
        if (ocx == null)
        {
            throw new InvalidActiveXStateException("ExecBehavior", ActiveXInvokeKind.MethodInvoke);
        }
        return ocx.ExecBehavior(lBehaviorID, varValue, lDeviceID);
    }

    public virtual int ExecScanBehavior(int lDevType, int lInfType, int lLevel, int lBevID, ref int pAddr, object varValue)
    {
        if (ocx == null)
        {
            throw new InvalidActiveXStateException("ExecScanBehavior", ActiveXInvokeKind.MethodInvoke);
        }
        return ocx.ExecScanBehavior(lDevType, lInfType, lLevel, lBevID, ref pAddr, varValue);
    }

    public virtual int SyncTime()
    {
        if (ocx == null)
        {
            throw new InvalidActiveXStateException("SyncTime", ActiveXInvokeKind.MethodInvoke);
        }
        return ocx.SyncTime();
    }

    public virtual int ChangeDeviceCommAddr(int lDeviceID, int lDeviceAddrNew)
    {
        if (ocx == null)
        {
            throw new InvalidActiveXStateException("ChangeDeviceCommAddr", ActiveXInvokeKind.MethodInvoke);
        }
        return ocx.ChangeDeviceCommAddr(lDeviceID, lDeviceAddrNew);
    }

    protected override void CreateSink()
    {
        try
        {
            eventMulticaster = new AxDCCEClientEventMulticaster(this);
            cookie = new ConnectionPointCookie(ocx, eventMulticaster, typeof(_DDCCEClientEvents));
        }
        catch (Exception)
        {
        }
    }

    protected override void DetachSink()
    {
        try
        {
            cookie.Disconnect();
        }
        catch (Exception)
        {
        }
    }

    protected override void AttachInterfaces()
    {
        try
        {
            ocx = (_DDCCEClient)GetOcx();
        }
        catch (Exception)
        {
        }
    }

    internal void RaiseOnFireOnDataReady(object sender, _DDCCEClientEvents_FireOnDataReadyEvent e)
    {
        FireOnDataReady?.Invoke(sender, e);
    }

    internal void RaiseOnFireOnVariableAlarm(object sender, _DDCCEClientEvents_FireOnVariableAlarmEvent e)
    {
        FireOnVariableAlarm?.Invoke(sender, e);
    }

    internal void RaiseOnFireOnVariableLag(object sender, _DDCCEClientEvents_FireOnVariableLagEvent e)
    {
        FireOnVariableLag?.Invoke(sender, e);
    }

    internal void RaiseOnFireOnLoadOver(object sender, _DDCCEClientEvents_FireOnLoadOverEvent e)
    {
        FireOnLoadOver?.Invoke(sender, e);
    }

    internal void RaiseOnFireOnDeviceStatus(object sender, _DDCCEClientEvents_FireOnDeviceStatusEvent e)
    {
        FireOnDeviceStatus?.Invoke(sender, e);
    }

    internal void RaiseOnFireOnBehavior(object sender, _DDCCEClientEvents_FireOnBehaviorEvent e)
    {
        FireOnBehavior?.Invoke(sender, e);
    }

    internal void RaiseOnFireOnScan(object sender, _DDCCEClientEvents_FireOnScanEvent e)
    {
        FireOnScan?.Invoke(sender, e);
    }

    internal void RaiseOnFireOnScanBehavior(object sender, _DDCCEClientEvents_FireOnScanBehaviorEvent e)
    {
        FireOnScanBehavior?.Invoke(sender, e);
    }

    internal void RaiseOnFireOnSyncTime(object sender, _DDCCEClientEvents_FireOnSyncTimeEvent e)
    {
        FireOnSyncTime?.Invoke(sender, e);
    }
}
