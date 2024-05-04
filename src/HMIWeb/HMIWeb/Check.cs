using System;

namespace HMIWeb;

public static class Check
{
    public static bool CheckNull(object target)
    {
        if (target == null)
        {
            return false;
        }
        return true;
    }

    public static bool CheckZero(object target)
    {
        if (Convert.ToDouble(target) == 0.0)
        {
            return false;
        }
        return true;
    }
}
