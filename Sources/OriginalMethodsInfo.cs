using System;
using System.Collections.Generic;
using System.Linq;

namespace MethodRedirect
{
    /// <summary>
    /// Result of a method redirection (Origin => *)
    /// </summary>
    public class OriginalMethodsInfo : IDisposable
    {
        List<MethodToken> Origins = new List<MethodToken>();
        
        public void Dispose()
        {
            Restore();
        }

        /// <summary>
        /// Release method hook without patching back original code
        /// </summary>
        public void Release()
        {
            Origins.Clear();
        }

        public void Restore()
        { 
            Origins.ForEach(x => x.Restore());
            Origins.Clear();
        }

        public void AddOrigin(IntPtr address, IntPtr targetValue)
        {
            Origins.Add(new MethodToken(address, targetValue));
        }

        public override string ToString()
        {
            return Origins.First().ToString();
        }

        /// <summary>
        /// Restores original method values so it can be called successfully without recursive crash, when disposing
        /// return value - values will be patched to original values.
        /// </summary>
        public OriginalMethodsInfo RestoreOriginal()
        {
            OriginalMethodsInfo methodsInfo = new OriginalMethodsInfo();
            foreach( MethodToken token in Origins)
            {
                token.Restore();
                methodsInfo.Origins.Add(new MethodToken(token.Address, token.TargetValue));
            }

            return methodsInfo;
        }
    }
}
