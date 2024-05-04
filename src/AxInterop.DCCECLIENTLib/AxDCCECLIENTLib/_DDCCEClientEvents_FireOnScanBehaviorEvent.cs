namespace AxDCCECLIENTLib;

public class _DDCCEClientEvents_FireOnScanBehaviorEvent
{
    public int lDevType;

    public int lInfType;

    public int lLevel;

    public int lBevID;

    public int pAddr;

    public object varValue;

    public int lResult;

    public _DDCCEClientEvents_FireOnScanBehaviorEvent(int lDevType, int lInfType, int lLevel, int lBevID, int pAddr, object varValue, int lResult)
    {
        this.lDevType = lDevType;
        this.lInfType = lInfType;
        this.lLevel = lLevel;
        this.lBevID = lBevID;
        this.pAddr = pAddr;
        this.varValue = varValue;
        this.lResult = lResult;
    }
}
