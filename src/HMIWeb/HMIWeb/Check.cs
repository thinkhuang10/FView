using System;

namespace HMIWeb;

public static class Check
{
    public static bool CheckZero(object target)
    {
        if (Convert.ToDouble(target) == 0.0)
            return false;

        return true;
    }
}
