using System;
using System.Reflection;

namespace MethodRedirect
{
    public class ClassMemberInfo
    {
        MethodInfo[] _methods;
        public MethodInfo[] Methods
        {
            get { return _methods; }
        }

        private ClassMemberInfo(Type type, string name, bool isMethod, bool isStatic = false)
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

        private ClassMemberInfo(params MethodInfo[] methods )
        {
            _methods = methods;
        }

        static public ClassMemberInfo FromFunc<T>(Func<T> target)
        {
            return new ClassMemberInfo(target.Method);
        }
        static public ClassMemberInfo FromFunc<T, R>(Func<T, R> target)
        {
            return new ClassMemberInfo(target.Method);
        }

        static public ClassMemberInfo FromMethod(Type type, string methodName, bool isStatic = false)
        {
            return new ClassMemberInfo(type, methodName, true, isStatic);
        }

        static public ClassMemberInfo FromProperty(Type type, string methodName, bool isStatic = false)
        {
            return new ClassMemberInfo(type, methodName, false, isStatic);
        }
    }
}
