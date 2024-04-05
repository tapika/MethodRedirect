using System;

namespace Scenarios_UT
{
    class Scenario4
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
        }

        public virtual string PublicVirtualInstanceMethod()
        {
            return "Scenarios_UT.Scenario4.PublicVirtualInstanceMethod";
        }
    }

    class Scenario4Ext
    {
        private string PrivateInstanceMethod()
        {
            return "Scenarios_UT.Scenario4Ext.PrivateInstanceMethod";
        }
    }
}
