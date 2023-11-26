namespace Aiursoft.CSTools.Tools;

public static class FolderDeleter
{
    private static void SetAttributesNormal(DirectoryInfo directoryInfo)
    {
        foreach (var info in directoryInfo.GetFileSystemInfos("*", SearchOption.AllDirectories))
        {
            info.Attributes = FileAttributes.Normal;
        }
    }
    
    public static void DeleteByForce(string path, bool keepFolder = false)
    {
        if (!Directory.Exists(path)) return;
        var di = new DirectoryInfo(path);
        SetAttributesNormal(di);
        if (keepFolder)
        {
            foreach (var file in di.GetFiles()) file.Delete();
            foreach (var dir in di.GetDirectories()) dir.Delete(true);
        }
        else
        {
            di.Delete(true);
        }
    }
}