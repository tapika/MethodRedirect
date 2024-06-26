﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MethodRedirect;
using System;
using System.Reflection;

namespace Scenarios_UT
{
    [TestClass]
    public class Scenario4_UnitTests
    {
        [TestMethod]
        public void Redirect_PublicVirtualInstanceMethod_To_PrivateInstanceMethod_DifferentInstance()
        {
            Type Scenario_Type = typeof(Scenario4);

            var token = MethodUtil.HookMethod(
                ClassMemberInfo.FromMethod(Scenario_Type, "PublicVirtualInstanceMethod"),
                ClassMemberInfo.FromMethod(typeof(Scenario4Ext), "PrivateInstanceMethod")
            );

            var scenario = (Scenario4)Activator.CreateInstance(Scenario_Type);
                       
            string methodName = scenario.PublicVirtualInstanceMethod();

            Assert.IsTrue(methodName == "Scenarios_UT.Scenario4Ext.PrivateInstanceMethod");

            token.Restore();

            methodName = scenario.PublicVirtualInstanceMethod();

            Assert.IsTrue(methodName == "Scenarios_UT.Scenario4.PublicVirtualInstanceMethod");
        }
    }
}
