using System;
using System.Runtime.InteropServices;

namespace MethodRedirect
{
    public struct MethodToken : IDisposable
    {
        public IntPtr Address { get; private set; }
        public IntPtr OriginalValue { get; private set; }
        public IntPtr TargetValue { get; private set; }

        public MethodToken(IntPtr address, IntPtr targetValue)
        {
            // On token creation, preserve the address and the current value at this address
            Address = address;
            OriginalValue = Marshal.ReadIntPtr(address);
            TargetValue = targetValue;
        }

        public void Restore()
        {
            // Restore the value at the address            
            Marshal.Copy(new IntPtr[] { OriginalValue }, 0, Address, 1);
        }

        public override string ToString()
        {
            IntPtr met = Address;
            IntPtr tar = Marshal.ReadIntPtr(Address);
            IntPtr ori = OriginalValue;

            return "Method address = " + met.ToString("x").PadLeft(8, '0') + "\n" +
                   "Target address = " + tar.ToString("x").PadLeft(8, '0') + "\n" +
                   "Origin address = " + ori.ToString("x").PadLeft(8, '0');
        }

        public void Dispose()
        {
            Restore();
        }
    }
}
