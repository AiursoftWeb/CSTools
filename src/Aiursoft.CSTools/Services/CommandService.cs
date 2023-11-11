using System.Diagnostics;
using Aiursoft.Scanner.Abstractions;

namespace Aiursoft.CSTools.Services;

public class CommandService : ITransientDependency
{
    public async Task<(int code, string output, string error)> RunCommandAsync(string bin, string arg, string path, bool getOutput = true)
    {
        var p = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = bin,
                Arguments = arg,
                WorkingDirectory = path,
                UseShellExecute = !getOutput,
                RedirectStandardOutput = getOutput,
                RedirectStandardError = getOutput,
            }
        };
        p.Start();
        await p.WaitForExitAsync();
        if (getOutput)
        {
            var output = await p.StandardOutput.ReadToEndAsync();
            var error = await p.StandardError.ReadToEndAsync();
            return (p.ExitCode, output, error);
        }
        else
        {
            return (p.ExitCode, string.Empty, string.Empty);
        }
    }
}