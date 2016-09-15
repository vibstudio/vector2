#region Namespaces

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;

#endregion

namespace Eng.Vector.Domain
{
    public class NetworkShareAccessProvider
    {
        private const int LOGON32_LOGON_INTERACTIVE = 9;
        private const int LOGON32_PROVIDER_DEFAULT = 0;

        private static WindowsImpersonationContext impersonationContext;

        [DllImport("advapi32.dll")]
        private static extern int LogonUserA(string lpszUserName, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);
        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool RevertToSelf();
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool CloseHandle(IntPtr handle);

        public static bool ImpersonateUser(string userName, string domain, string password)
        {
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (RevertToSelf())
            {
                if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                {
                    if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                    {
                        var tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                        impersonationContext = tempWindowsIdentity.Impersonate();
                        if (impersonationContext != null)
                        {
                            CloseHandle(token);
                            CloseHandle(tokenDuplicate);
                            return true;
                        }
                    }
                }
            }

            if (token != IntPtr.Zero)
            {
                CloseHandle(token);
            }
            
            if (tokenDuplicate != IntPtr.Zero)
            {
                CloseHandle(tokenDuplicate);
            }
            
            return false;
        }

        public static void UndoImpersonation()
        {
            impersonationContext.Undo();
            impersonationContext.Dispose();
        }

        public static string GetLastError()
        {
            return new Win32Exception(Marshal.GetLastWin32Error()).Message;
        }
    }
}