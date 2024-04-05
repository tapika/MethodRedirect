using Microsoft.VisualStudio.TestTools.UnitTesting;
using MethodRedirect;
using System;
using System.Reflection;

namespace Scenarios_UT
{
    [TestClass]
    public class Scenario3_UnitTests
    {
        [TestMethod]
        public void Redirect_InternalVirtualInstanceMethod_To_InternalStaticMethod_DifferentInstance()
        {
            Type Scenario_Type = typeof(Scenario3);

            var token = MethodUtil.HookMethod(
                ClassMemberInfo.FromMethod(Scenario_Type, "InternalVirtualInstanceMethod"),
                ClassMemberInfo.FromMethod(typeof(Scenario3Ext), "InternalStaticMethod", true)
            );

            var scenario = (Scenario3)Activator.CreateInstance(Scenario_Type);
                       
            string methodName = scenario.InternalVirtualInstanceMethod();

            Assert.IsTrue(methodName == "Scenarios_UT.Scenario3Ext.InternalStaticMethod");

            token.Restore();

            methodName = scenario.InternalVirtualInstanceMethod();

            Assert.IsTrue(methodName == "Scenarios_UT.Scenario3.InternalVirtualInstanceMethod");
        }
    }
}
