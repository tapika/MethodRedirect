using System;

namespace Scenarios_UT
{
    class Scenario6
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
        }

        public virtual string PublicVirtualInstanceMethod()
        {
            return "Scenarios_UT.Scenario6.PublicVirtualInstanceMethod";
        }

        public virtual string PublicVirtualInstanceMethodWithParameter(int x)
        {
            return "Scenarios_UT.Scenario6.PublicVirtualInstanceMethodWithParameter." + x;
        }

        public virtual int AnotherPublicInstanceMethodWithParameter(int x)
        {
            return x + 1;
        }
    }

    class Scenario6Ext
    {
        private string PrivateInstanceMethodWithParameter(int x)
        {
            return "Scenarios_UT.Scenario6Ext.PrivateInstanceMethodWithParameter." + x;
        }
    }
}