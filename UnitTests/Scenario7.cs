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
    }
}
