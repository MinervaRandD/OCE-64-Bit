using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

public static class ProcessListGenerator
{
    public sealed class ProcInfo
    {
        public int Pid { get; init; }
        public string Name { get; init; } = "";
        public string Path { get; init; } = "";
        public string Folder => System.IO.Path.GetDirectoryName(Path) ?? "";
    }

    public static List<Process> FindForCurrentUserByNamePrefix(string namePrefix)
    {
        List<Process> rtrnValue = new List<Process>(); 

        string mySid = WindowsIdentity.GetCurrent().User!.Value;
        var procs = Process.GetProcesses();

        foreach (var p in procs)
        {
            // Fast name filter first
            if (!p.ProcessName.StartsWith(namePrefix, StringComparison.OrdinalIgnoreCase))
                continue;

            IntPtr hProc = IntPtr.Zero;
            IntPtr hTok = IntPtr.Zero;
            try
            {
                hProc = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, false, p.Id);
                if (hProc == IntPtr.Zero) continue;

                // Get owner SID from the process token
                if (!OpenProcessToken(hProc, TOKEN_QUERY, out hTok) || hTok == IntPtr.Zero) continue;

                string? sid = GetSidFromToken(hTok);
                if (sid is null || !sid.Equals(mySid, StringComparison.OrdinalIgnoreCase)) continue;

                // Get full executable path
                string? fullPath = QueryImagePath(hProc);
                if (string.IsNullOrEmpty(fullPath)) continue;

                rtrnValue.Add( p );
            }
            catch
            {
                // Process may have exited or access denied—ignore and continue
            }
            finally
            {
                if (hTok != IntPtr.Zero) CloseHandle(hTok);
                if (hProc != IntPtr.Zero) CloseHandle(hProc);
            }
        }

        return rtrnValue;
    }

    // --- Helpers ---

    private static string? QueryImagePath(IntPtr hProcess)
    {
        int size = 1024;
        var sb = new StringBuilder(size);
        if (QueryFullProcessImageName(hProcess, 0, sb, ref size))
            return sb.ToString();
        return null;
    }

    private static string? GetSidFromToken(IntPtr token)
    {
        // First call gets required buffer size
        GetTokenInformation(token, TOKEN_INFORMATION_CLASS.TokenUser, IntPtr.Zero, 0, out uint len);
        if (len == 0) return null;

        IntPtr buf = Marshal.AllocHGlobal((int)len);
        try
        {
            if (!GetTokenInformation(token, TOKEN_INFORMATION_CLASS.TokenUser, buf, len, out _))
                return null;

            var tu = Marshal.PtrToStructure<TOKEN_USER>(buf);
            var sid = new SecurityIdentifier(tu.User.Sid);
            return sid.Value;
        }
        finally
        {
            Marshal.FreeHGlobal(buf);
        }
    }

    // --- Win32 interop ---

    private const uint PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;
    private const uint TOKEN_QUERY = 0x0008;

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(uint access, bool inheritHandle, int processId);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr hObject);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

    [DllImport("advapi32.dll", SetLastError = true)]
    private static extern bool GetTokenInformation(
        IntPtr TokenHandle,
        TOKEN_INFORMATION_CLASS TokenInformationClass,
        IntPtr TokenInformation,
        uint TokenInformationLength,
        out uint ReturnLength);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool QueryFullProcessImageName(
        IntPtr hProcess, int dwFlags, StringBuilder lpExeName, ref int lpdwSize);

    private enum TOKEN_INFORMATION_CLASS { TokenUser = 1 }

    [StructLayout(LayoutKind.Sequential)]
    private struct TOKEN_USER { public SID_AND_ATTRIBUTES User; }

    [StructLayout(LayoutKind.Sequential)]
    private struct SID_AND_ATTRIBUTES { public IntPtr Sid; public uint Attributes; }
}
