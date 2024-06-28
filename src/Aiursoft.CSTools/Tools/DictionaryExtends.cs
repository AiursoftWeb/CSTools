namespace Aiursoft.CSTools.Tools;

public static class DictionaryExtends
{
    public static T1 GetOrAdd<T1, T2>(this Dictionary<T2, T1> dict, T2 key, Func<T1> factory) where T2 : notnull
    {
        if (dict.TryGetValue(key, out var value))
        {
            return value;
        }

        var newValue = factory();
        dict.Add(key, newValue);
        return newValue;
    }

    /// <summary>
    /// Copies all files and directories recursively from the source path to the target path.
    /// </summary>
    /// <param name="sourcePath">The path to the source directory.</param>
    /// <param name="targetPath">The path to the target directory.</param>
    public static void CopyFilesRecursively(this string sourcePath, string targetPath)
    {
        // Now Create all the directories
        foreach (var dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
        {
            Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
        }

        // Copy all the files & Replaces any files with the same name
        foreach (var newPath in Directory.GetFiles(sourcePath, "*.*",SearchOption.AllDirectories))
        {
            File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
        }
    }
}