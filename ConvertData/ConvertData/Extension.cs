using System;
using System.Collections.Generic;
using System.Text;

namespace ConvertData
{
    public static class Extension
    {
        public static string FormatData(this decimal value)
        {
            return value.ToString().Replace(",", ".").PadRight(8, '0');
        }
    }
}
