using MethodRedirect;

namespace Scenarios_UT
{
    public class Scenario7
    {
        // Hook will not work without 'public virtual'
        public virtual int OverloadMethod(int a1, int a2)
        { 
            return a1 + a2;
        }
        public virtual string OverloadMethod(string a1, string a2)
        {
            return a1 + a2;
        }

        public virtual void MethodWithOutParam(out string ret)
        {
            ret = nameof(MethodWithOutParam);
        }

        public int delta2add = 5;
        
        public virtual int M1(int a1, int a2)
        {
            return a1 + a2 + delta2add;
        }

        public virtual int Sum(int a1, int a2)
        {
            return a1 + a2;
        }
        public virtual string Sum(string s1, string s2)
        {
            return s1 + s2;
        }

        public virtual int Sub(int a1, int a2)
        {
            return a1 - a2;
        }

    }

    class Scenario7Ext
    {
        // can be only static, as M1 does not know context of Scenario7Ext class, as it can follow only original class (Scenario7)
        public static OriginalMethodsInfo token = null;

        static int M1(Scenario7 pthis, int a1, int a2)
        {
            // if you call pthis.M1 without token.RestoreOriginal() - you will get stack overflow as function just tries to 
            // call itself infinitely.
            using (token.RestoreOriginal())
            {
                pthis.delta2add = 0;    // Have access to original class being hooked.
                int r = pthis.M1(a1, a2) + 10;
                pthis.delta2add = 5;
                return r;
            }
        }

        public virtual int Sum(int a1, int a2)
        {
            return 0;
        }

        // can use object instead of original class in case if class is non-public.
        public static string Sum(object scenario7, string s1, string s2)
        {
            return "";
        }

        public virtual int Sub(int a1, int a2)
        {
            return 0;
        }
        public virtual string Sub(string s1, string s2)
        {
            return "";
        }

    }

}
