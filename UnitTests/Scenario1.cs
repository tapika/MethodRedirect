﻿using MethodRedirect;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Scenarios_UT
{
    class Scenario1
    {
        static void Main(string[] args)
        {
            Type Scenario_Type = typeof(Scenario1);

            var token = MethodUtil.HookMethod(
                ClassMemberInfo.FromMethod(Scenario_Type, "InternalInstanceMethod"),
                ClassMemberInfo.FromMethod(Scenario_Type, "PrivateInstanceMethod")
            );

            // Using "dynamic" type to resolve the following issue in x64 and Release (with code optimizations) builds.
            //
            // Symptom: the second call to method InternalInstanceMethod() does not return the expected string value
            //
            // Cause: the result from the first call to method InternalInstanceMethod() is cached and so the second
            // call gets the cached value instead of making the call again.
            //
            // Remark: for some reason, without "dynamic" type, only the "Debug x86" build configuration would reevaluate 
            // the second call to InternalInstanceMethod() without using the cached string value.
            //
            // Also, using the [MethodImpl(MethodImplOptions.NoOptimization)] attribute on the method did not work.
            dynamic scenario = (Scenario1)Activator.CreateInstance(Scenario_Type);

            string methodName = scenario.InternalInstanceMethod();

            //Console.WriteLine("Call Scenarios_UT.Scenario1.InternalInstanceMethod => {0}", methodName);

            //Debug.Assert(methodName == "Scenarios_UT.Scenario1.PrivateInstanceMethod");

            if (methodName == "Scenarios_UT.Scenario1.PrivateInstanceMethod")
            {
                //Console.WriteLine("\nRestore...");
                
                token.Restore();

                //Console.WriteLine(token);

                methodName = scenario.InternalInstanceMethod();

                //Console.WriteLine("Call Scenarios_UT.Scenario1.InternalInstanceMethod => {0}", methodName);

                //Debug.Assert(methodName == "Scenarios_UT.Scenario1.InternalInstanceMethod");

                if (methodName == "Scenarios_UT.Scenario1.InternalInstanceMethod")
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

            //Console.ReadKey();
        }
        
        internal string InternalInstanceMethod()
        {
            return "Scenarios_UT.Scenario1.InternalInstanceMethod";
        }
        
        private string PrivateInstanceMethod()
        {
            return "Scenarios_UT.Scenario1.PrivateInstanceMethod";
        }

        public string PublicInstanceMethod()
        {
            return "Scenarios_UT.Scenario1.PublicInstanceMethod";
        }

        public static string PublicStaticMethod()
        {
            return "Scenarios_UT.Scenario1.PublicStaticMethod";
        }
    }
}
