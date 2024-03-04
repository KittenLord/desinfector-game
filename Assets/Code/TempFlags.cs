using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class TempFlags
{
    private static Dictionary<string, bool> flags = new Dictionary<string, bool>();

    public static void ResetAll()
    {
        flags.Clear();
    }

    public static bool Check(string flag)
    {
        return flags.ContainsKey(flag) && flags[flag];
    }

    public static void Set(string flag, bool value = true)
    {
        flags[flag] = value;
    }
}