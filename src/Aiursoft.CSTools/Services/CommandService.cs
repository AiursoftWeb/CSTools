using System.Diagnostics;
using System.Text;
using Aiursoft.Scanner.Abstractions;

namespace Aiursoft.CSTools.Services;

public class CommandService : ITransientDependency
{
    public async Task<(int code, string output, string error)> RunCommandAsync(
        string bin, 
        string arg, 
        string path,
        TimeSpan? timeout = null,
        bool killTimeoutProcess = true)
    {
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        timeout ??= TimeSpan.FromMinutes(2);

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = bin,
                Arguments = arg,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                WorkingDirectory = path,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            }
        };
        process.Start();

        var outputMemoryStream = new MemoryStream();
        var errorMemoryStream = new MemoryStream();
        var programTask = Task.WhenAll(
            process.StandardOutput.BaseStream.CopyToAsync(outputMemoryStream),
            process.StandardError.BaseStream.CopyToAsync(errorMemoryStream), 
            process.WaitForExitAsync()
        );
        await Task.WhenAny(
            Task.Delay(timeout.Value),
            programTask);
        if (!programTask.IsCompleted)
        {
            try
            {
                if (killTimeoutProcess && process.Id != 0)
                {
                    process.Kill();
                }
            }
            catch (Exception e)
            {
                throw new TimeoutException($@"Execute command: {bin} {arg} at {path} was time out! Timeout is {timeout}. And we also failed to kill the timeout process because '{e.Message}'!");
            }
            throw new TimeoutException($@"Execute command: {bin} {arg} at {path} was time out! Timeout is {timeout}.");
        }

        var output = Encoding.UTF8.GetString(outputMemoryStream.ToArray());
        var error = Encoding.UTF8.GetString(errorMemoryStream.ToArray());
        return (process.ExitCode, output, error);
    }
}