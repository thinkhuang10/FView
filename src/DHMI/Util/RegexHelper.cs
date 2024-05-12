using System.Text.RegularExpressions;

namespace Util
{
    public class RegexHelper
    {
        /// <summary>
        /// 校验正整数（包含0）
        /// </summary>
        public static bool IsInterger(string str)
        {
            return !string.IsNullOrWhiteSpace(str) && Regex.IsMatch(str, @"^[1-9]\d*|0$");
        }

        /// <summary>
        /// 校验非零正整数
        /// </summary>
        public static bool IsIntergerNonZero(string str)
        {
            return !string.IsNullOrWhiteSpace(str) && Regex.IsMatch(str, @"^[1-9]\d*$");
        }

        public static bool IsIPAddress(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
                return false;

            return Regex.IsMatch(ip.Trim(), @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }
}
