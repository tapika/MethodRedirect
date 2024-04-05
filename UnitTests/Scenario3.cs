using System;

namespace Scenarios_UT
{
    class Scenario3
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
        }

        internal virtual string InternalVirtualInstanceMethod()
        {
            return "Scenarios_UT.Scenario3.InternalVirtualInstanceMethod";
        }
    }

    class Scenario3Ext
    {
        internal static string InternalStaticMethod()
        {
            return "Scenarios_UT.Scenario3Ext.InternalStaticMethod";
        }
    }
}
