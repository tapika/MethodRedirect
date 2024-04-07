using MethodRedirect;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Scenarios_UT
{
    [TestClass]
    public class Scenario7_UnitTests
    {
        [TestMethod]
        public void HookOverloadedMethods()
        {
            // You won't be able to intercept OverloadMethod with older API, as there are multiple same named methods.
            var token = MethodUtil.HookMethod(typeof(Scenario7), "OverloadMethod",
                new Func<int, int, int>(
                    (int arg1, int arg2) =>
                    {
                        return arg1 - arg2;
                    }
                )
            );
            Scenario7 s7 = new Scenario7();
            Assert.AreEqual(s7.OverloadMethod(2, 1), 1);
            token.Dispose();
            Assert.AreEqual(s7.OverloadMethod(2, 1), 3);
        }

        delegate void DelegateMethodWithOutParam(out string ret);

        [TestMethod]
        public void HookOutMethod()
        {
            // out and ref parameters can be supported only via delegates.
            DelegateMethodWithOutParam func = (out string s) =>
            {
                s = nameof(HookOutMethod);
            };

            // You won't be able to intercept OverloadMethod with older API, as there are multiple same named methods.
            var token = MethodUtil.HookMethod(typeof(Scenario7), "MethodWithOutParam", func);
            Scenario7 s7 = new Scenario7();
            string ret = "";
            s7.MethodWithOutParam(out ret);
            Assert.AreEqual(ret, nameof(HookOutMethod));
            token.Dispose();
            s7.MethodWithOutParam(out ret);
            Assert.AreEqual(ret, nameof(Scenario7.MethodWithOutParam));
        }

        [TestMethod]
        public void CanCallOriginalMethod()
        {
            Scenario7 s7 = new Scenario7();

            Scenario7Ext.token = MethodUtil.HookMethod(
                ClassMemberInfo.FromMethod(typeof(Scenario7), "M1"),
                ClassMemberInfo.FromMethod(typeof(Scenario7Ext), "M1", true)
            );

            Assert.AreEqual(s7.M1(1,2), 13);
        }

        [TestMethod]
        public void HookMultipleMethods()
        {
            // Can hook multiple methods with same name, only arguments overload.
            var token = MethodUtil.HookMethod(typeof(Scenario7), typeof(Scenario7Ext), "Sum");
            Scenario7 s7 = new Scenario7();
            Assert.AreEqual(s7.Sum(1,2), 0);
            Assert.AreEqual(s7.Sum("1","2"), "");
            token.Dispose();
            Assert.AreEqual(s7.Sum(1, 2), 3);
            Assert.AreEqual(s7.Sum("1", "2"), "12");

            // Hook type must have a complete list of methods that must be hooked.
            // If method not found in original type, exception will be thrown.
            var ex = Assert.ThrowsException<ArgumentException>(() => {
                MethodUtil.HookMethod(typeof(Scenario7), typeof(Scenario7Ext), "Sub");
            }, "yep");

            Assert.AreEqual(ex.Message, "Hook method Sub(System.String,System.String) does not have mapping in original type");
        }
    }

}

