using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MethodRedirect
{
    public class MethodHook
    {
        MethodInfo[] _methods;
        public MethodInfo[] Methods
        {
            get { return _methods; }
        }

        private MethodHook(Type type, string name, bool isMethod, bool isStatic = false)
        {
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public;
            if (isStatic)
            {
                flags |= BindingFlags.Static;
            }
            else
            {
                flags |= BindingFlags.Instance;
            }

            if (isMethod)
            {
                _methods = new MethodInfo[] { type.GetMethod(name, flags) };
            }
            else
            {
                var propInfo = type.GetProperty(name, flags);
                _methods = new MethodInfo[] { propInfo.GetGetMethod(true), propInfo.GetSetMethod(true) };
            }
        }

        private MethodHook(params MethodInfo[] methods )
        {
            _methods = methods;
        }

        static public MethodHook FromFunc<T>(Func<T> target)
        {
            return new MethodHook(target.Method);
        }
        static public MethodHook FromFunc<T, R>(Func<T, R> target)
        {
            return new MethodHook(target.Method);
        }

        static public MethodHook FromMethod(Type type, string methodName, bool isStatic = false)
        {
            return new MethodHook(type, methodName, true, isStatic);
        }

        static public MethodHook FromProperty(Type type, string methodName, bool isStatic = false)
        {
            return new MethodHook(type, methodName, false, isStatic);
        }
    }
}
