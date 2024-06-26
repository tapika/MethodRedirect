﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MethodRedirect;
using System;
using System.Reflection;
using System.Diagnostics;

namespace Scenarios_UT
{
    [TestClass]
    public class Scenario6_UnitTests
    {
        [TestMethod]
        public void Redirect_PublicVirtualInstanceMethodWithParameter_To_PrivateInstanceMethodWithParameter_DifferentInstance()
        {
            Type Scenario_Type = typeof(Scenario6);

            var token = MethodUtil.HookMethod(
                ClassMemberInfo.FromMethod(Scenario_Type, "PublicVirtualInstanceMethodWithParameter"),
                ClassMemberInfo.FromMethod(typeof(Scenario6Ext), "PrivateInstanceMethodWithParameter")
            );

            var scenario = (Scenario6)Activator.CreateInstance(Scenario_Type);

            int parameter = 123;
            string methodName = scenario.PublicVirtualInstanceMethodWithParameter(parameter);

            Assert.IsTrue(methodName == "Scenarios_UT.Scenario6Ext.PrivateInstanceMethodWithParameter." + parameter);

            token.Restore();

            methodName = scenario.PublicVirtualInstanceMethodWithParameter(parameter);

            Assert.IsTrue(methodName == "Scenarios_UT.Scenario6.PublicVirtualInstanceMethodWithParameter." + parameter);
        }

        [TestMethod]
        public void Redirect_PublicVirtualInstanceMethod_To_Lambda_NoParameter()
        {
            Type Scenario_Type = typeof(Scenario6);

            var token = MethodUtil.HookMethod(
                ClassMemberInfo.FromMethod(Scenario_Type, "PublicVirtualInstanceMethod"),
                ClassMemberInfo.FromFunc(() =>
                {
                    return "MethodRedirect.LambdaExpression.NoParameter";
                })
            );

            var scenario = (Scenario6)Activator.CreateInstance(Scenario_Type);

            string methodName = scenario.PublicVirtualInstanceMethod();

            Assert.IsTrue(methodName == "MethodRedirect.LambdaExpression.NoParameter");

            token.Restore();

            methodName = scenario.PublicVirtualInstanceMethod();

            Assert.IsTrue(methodName == "Scenarios_UT.Scenario6.PublicVirtualInstanceMethod");
        }

        [TestMethod]
        public void Redirect_PublicVirtualInstanceMethod_To_Lambda_WithParameter()
        {
            Type Scenario_Type = typeof(Scenario6);

            var token = MethodUtil.HookMethod(
                ClassMemberInfo.FromMethod(Scenario_Type, "PublicVirtualInstanceMethodWithParameter"),
                ClassMemberInfo.FromFunc((int x) =>
                {
                    return "MethodRedirect.LambdaExpression.WithParameter." + x;
                })
            );

            var scenario = (Scenario6)Activator.CreateInstance(Scenario_Type);

            int parameter = 456;
            string methodName = scenario.PublicVirtualInstanceMethodWithParameter(parameter);

            Assert.IsTrue(methodName == "MethodRedirect.LambdaExpression.WithParameter." + parameter);

            token.Restore();

            methodName = scenario.PublicVirtualInstanceMethodWithParameter(parameter);

            Assert.IsTrue(methodName == "Scenarios_UT.Scenario6.PublicVirtualInstanceMethodWithParameter." + parameter);
        }

        [TestMethod]
        public void Redirect_AnotherPublicInstanceMethod_To_Lambda_WithParameter()
        {
            Type Scenario_Type = typeof(Scenario6);

            var token = MethodUtil.HookMethod(
                ClassMemberInfo.FromMethod(Scenario_Type, "AnotherPublicInstanceMethodWithParameter"),
                ClassMemberInfo.FromFunc((int x) =>
                {
                    Debug.WriteLine("Lambda Expression Parameter = " + x.ToString());
                    return x + 10;
                })
            );

            var scenario = (Scenario6)Activator.CreateInstance(Scenario_Type);

            int parameter = 1;
            int value = scenario.AnotherPublicInstanceMethodWithParameter(parameter);

            Assert.IsTrue(value == parameter + 10);

            token.Restore();

            value = scenario.AnotherPublicInstanceMethodWithParameter(parameter);

            Assert.IsTrue(value == parameter + 1);
        }
    }
}
