using System.Diagnostics;
using System.Text;
using Aiursoft.Scanner.Abstractions;

namespace Aiursoft.CSTools.Services;

public class CommandService : ITransientDependency
{
    public virtual async Task<(int code, string output, string error)> RunCommandAsync(
        string bin, 
        string arg, 
        string path,
        TimeSpan? timeout = null,
        bool killTimeoutProcess = true,
        IDictionary<string, string?>? environmentVariables = null)
    {
        if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        timeout ??= TimeSpan.FromMinutes(25);

        using var process = new Process();
        process.StartInfo = new ProcessStartInfo
        {
            FileName = bin,
            Arguments = arg,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden,
            WorkingDirectory = path,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };

        if (environmentVariables != null)
        {
            foreach (var (key, value) in environmentVariables)
            {
                process.StartInfo.Environment[key] = value;
            }
        }

        process.Start();

        var outputMemoryStream = new MemoryStream();
        var errorMemoryStream = new MemoryStream();
        var outputTask = process.StandardOutput.BaseStream.CopyToAsync(outputMemoryStream);
        var errorTask = process.StandardError.BaseStream.CopyToAsync(errorMemoryStream);
        var exitTask = process.WaitForExitAsync();
        var programTask = Task.WhenAll(outputTask, errorTask, exitTask);

        try
        {
            await programTask.WaitAsync(timeout.Value);
        }
        catch (TimeoutException)
        {
            Exception? terminationException = null;
            try
            {
                if (killTimeoutProcess && process.Id != 0)
                {
                    process.Kill(entireProcessTree: true);
                    await programTask;
                }
            }
            catch (Exception e)
            {
                terminationException = e;
            }

            var message = $@"Execute command: {bin} {arg} at {path} timed out! Timeout is {timeout}.";
            if (terminationException != null)
            {
                message += $" Failed to terminate the process tree cleanly: '{terminationException.Message}'.";
            }

            throw new TimeoutException(message, terminationException);
        }

        var output = Encoding.UTF8.GetString(outputMemoryStream.ToArray());
        var error = Encoding.UTF8.GetString(errorMemoryStream.ToArray());
        return (process.ExitCode, output, error);
    }
}
