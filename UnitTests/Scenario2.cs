using MethodRedirect;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Scenarios_UT
{
    class Scenario2
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Redirect : Scenarios_UT.Scenario2.InternalVirtualInstanceMethod()");
            Console.WriteLine("To       : Scenarios_UT.Scenario2.PrivateInstanceMethod()");            

            Type Scenario_Type = typeof(Scenario2);

            var token = MethodUtil.HookMethod(
                ClassMemberInfo.FromMethod(Scenario_Type, "InternalVirtualInstanceMethod"),
                ClassMemberInfo.FromMethod(Scenario_Type, "PrivateInstanceMethod")
            );

            var scenario = (Scenario2)Activator.CreateInstance(Scenario_Type);

            string methodName = scenario.InternalVirtualInstanceMethod();

            Console.WriteLine("Call Scenarios_UT.Scenario2.InternalVirtualInstanceMethod => {0}", methodName);

            Debug.Assert(methodName == "Scenarios_UT.Scenario2.PrivateInstanceMethod");

            if (methodName == "Scenarios_UT.Scenario2.PrivateInstanceMethod")
            {
                Console.WriteLine("\nRestore...");

                token.Restore();

                methodName = scenario.InternalVirtualInstanceMethod();

                Console.WriteLine("Call Scenarios_UT.Scenario2.InternalVirtualInstanceMethod => {0}", methodName);

                Debug.Assert(methodName == "Scenarios_UT.Scenario2.InternalVirtualInstanceMethod");

                if (methodName == "Scenarios_UT.Scenario2.InternalVirtualInstanceMethod")
                {
                    Console.WriteLine("\nSUCCESS!");
                }
                else
                {
                    Console.WriteLine("\nRestore FAILED");
                }
            }
            else
            {
                Console.WriteLine("\nRedirection FAILED");
            }

            Console.ReadKey();
        }

        internal virtual string AnotherInternalVirtualInstanceMethod()
        {
            return "Scenarios_UT.Scenario2.AnotherInternalVirtualInstanceMethod";
        }

        internal virtual string InternalVirtualInstanceMethod()
        {
            return "Scenarios_UT.Scenario2.InternalVirtualInstanceMethod";
        }

        private string PrivateInstanceMethod()
        {
            return "Scenarios_UT.Scenario2.PrivateInstanceMethod";
        }
    }
}
