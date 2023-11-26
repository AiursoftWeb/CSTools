using System.Diagnostics;
using System.Text;
using Aiursoft.Scanner.Abstractions;

namespace Aiursoft.CSTools.Services;

public class CommandService : ITransientDependency
{
    public async Task<(int code, string output, string error)> RunCommandAsync(string bin, string arg, string path,
        TimeSpan? timeout = null)
    {
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        timeout ??= TimeSpan.FromMinutes(2);

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = bin,
                Arguments = arg,
                WorkingDirectory = path,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            }
        };
        process.Start();

        var outputMemoryStream = new MemoryStream();
        var errorMemoryStream = new MemoryStream();
        var readOutputTask = Task.Run(() => process.StandardOutput.BaseStream.CopyToAsync(outputMemoryStream));
        var readErrorTask = Task.Run(() => process.StandardError.BaseStream.CopyToAsync(errorMemoryStream));

        var programTask = Task.WhenAll(readOutputTask, readErrorTask, process.WaitForExitAsync());
        await Task.WhenAny(
            Task.Delay(timeout.Value),
            programTask);
        if (!programTask.IsCompleted)
        {
            throw new TimeoutException($@"Execute command: {bin} {arg} at {path} was time out! Timeout is {timeout}.");
        }

        await process.WaitForExitAsync();
        var output = Encoding.UTF8.GetString(outputMemoryStream.ToArray());
        var error = Encoding.UTF8.GetString(errorMemoryStream.ToArray());
        return (process.ExitCode, output, error);
    }
}