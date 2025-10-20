using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.Text;

namespace Aiursoft.CSTools.Services;

/// <summary>
/// Provides extension methods for retrieving detailed operating system information.
/// </summary>
public static class OperatingSystemExtensions
{
    /// <summary>
    /// Attempts to get the full, detailed OS version string for the current platform.
    /// This method provides more specific information than Environment.OSVersion.VersionString.
    /// </summary>
    /// <returns>A detailed string representing the OS version, or a fallback from Environment.OSVersion.</returns>
    public static string TryGetFullOsVersion()
    {
        try
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return GetWindowsVersion();
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return GetLinuxVersion();
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return GetMacOsVersion();
            }
        }
        catch (Exception)
        {
            // In case of any unexpected error in the platform-specific methods,
            // we suppress the exception and fall back to the default implementation.
        }

        // Generic fallback for any other OS or if specific methods fail.
        return Environment.OSVersion.VersionString;
    }

    private static string GetWindowsVersion()
    {
        // The primary source for detailed Windows version information is the registry.
#pragma warning disable CA1416
        using var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
        if (key == null)
        {
            return Environment.OSVersion.VersionString;
        }
        // Retrieve ProductName, e.g., "Windows 11 Pro".
        var productName = key.GetValue("ProductName", "Microsoft Windows NT") as string ?? "Microsoft Windows NT";

        // Retrieve display version, e.g., "22H2". Available on newer Windows 10/11.
        var displayVersion = key.GetValue("DisplayVersion", "") as string ?? "";

        // Retrieve the full build number including the UBR (Update Build Revision).
        var currentBuild = key.GetValue("CurrentBuild", "") as string ?? "";
        var ubr = key.GetValue("UBR", "").ToString() ?? "";

        var versionString = new StringBuilder(productName);
        if (!string.IsNullOrWhiteSpace(displayVersion))
        {
            versionString.Append($" {displayVersion}");
        }

        if (string.IsNullOrWhiteSpace(currentBuild))
        {
            return versionString.ToString();
        }
        versionString.Append($" (Build {currentBuild}");
        if (!string.IsNullOrWhiteSpace(ubr))
        {
            versionString.Append($".{ubr}");
        }
        versionString.Append(')');

        return versionString.ToString();

        // Fallback for Windows if registry reading fails.
#pragma warning restore CA1416
    }

    private static string GetLinuxVersion()
    {
        // The standard file for OS release information on modern Linux distributions.
        if (File.Exists("/etc/os-release"))
        {
            var lines = File.ReadAllLines("/etc/os-release");
            // The PRETTY_NAME field gives a user-friendly string, e.g., "Ubuntu 22.04.1 LTS".
            var prettyName = lines
                .FirstOrDefault(l => l.StartsWith("PRETTY_NAME="))
                ?.Split('=', 2)[1]
                .Trim('"');

            if (!string.IsNullOrEmpty(prettyName))
            {
                return prettyName;
            }
        }

        // Fallback for older Debian/Ubuntu-based systems.
        if (File.Exists("/etc/lsb-release"))
        {
            var lines = File.ReadAllLines("/etc/lsb-release");
            var description = lines
                .FirstOrDefault(l => l.StartsWith("DISTRIB_DESCRIPTION="))
                ?.Split('=', 2)[1]
                .Trim('"');

            if (!string.IsNullOrEmpty(description))
            {
                return description;
            }
        }

        // Fallback for Red Hat-based systems (CentOS, Fedora, RHEL).
        if (File.Exists("/etc/redhat-release"))
        {
            return File.ReadAllText("/etc/redhat-release").Trim();
        }

        // If all file-based checks fail, try to execute `uname` as a last resort.
        var uname = ExecuteProcess("uname", "-sr");
        if (!string.IsNullOrWhiteSpace(uname))
        {
            return uname.Trim();
        }

        // Generic fallback for Linux.
        return Environment.OSVersion.VersionString;
    }

    private static string GetMacOsVersion()
    {
        // `sw_vers` is the standard command-line tool to get macOS version info.
        var productName = ExecuteProcess("sw_vers", "-productName")?.Trim() ?? "macOS";
        var productVersion = ExecuteProcess("sw_vers", "-productVersion")?.Trim();
        var buildVersion = ExecuteProcess("sw_vers", "-buildVersion")?.Trim();

        if (!string.IsNullOrWhiteSpace(productVersion) && !string.IsNullOrWhiteSpace(buildVersion))
        {
            return $"{productName} {productVersion} (Build {buildVersion})";
        }

        // Fallback for macOS if `sw_vers` fails.
        return Environment.OSVersion.VersionString;
    }

    private static string? ExecuteProcess(string fileName, string arguments)
    {
        try
        {
            var processStartInfo = new ProcessStartInfo
            {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8
            };

            using var process = Process.Start(processStartInfo);
            if (process == null) return null;

            var output = process.StandardOutput.ReadToEnd();
            // Wait for a maximum of 2 seconds for the process to exit.
            process.WaitForExit(2000);
            return output;
        }
        catch
        {
            // The process could not be started, was not found, or another error occurred.
            return null;
        }
    }
}
