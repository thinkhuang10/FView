namespace AxDCCECLIENTLib;

public class _DDCCEClientEvents_FireOnDeviceStatusEvent
{
    public int lDeviceID;

    public int lDeviceInterface;

    public int lDeviceStatus;

    public _DDCCEClientEvents_FireOnDeviceStatusEvent(int lDeviceID, int lDeviceInterface, int lDeviceStatus)
    {
        this.lDeviceID = lDeviceID;
        this.lDeviceInterface = lDeviceInterface;
        this.lDeviceStatus = lDeviceStatus;
    }
}
