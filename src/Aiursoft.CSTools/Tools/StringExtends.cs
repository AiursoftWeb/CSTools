using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Aiursoft.CSTools.Tools;

public static class StringExtends
{
    private static Random Seed { get; } = new();

    public static string BytesToBase64(this byte[] input)
    {
        return Convert.ToBase64String(input);
    }

    public static byte[] Base64ToBytes(this string input)
    {
        return string.IsNullOrWhiteSpace(input) ? Array.Empty<byte>() : Convert.FromBase64String(input);
    }

    public static byte[] StringToBytes(this string input)
    {
        return Encoding.UTF8.GetBytes(input);
    }

    public static byte[] StringToUtf8WithBom(this string input)
    {
        var preamble = Encoding.UTF8.GetPreamble();
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var result = new byte[preamble.Length + inputBytes.Length];
        preamble.CopyTo(result, 0);
        inputBytes.CopyTo(result, preamble.Length);
        return result;
    }

    public static string BytesToString(this byte[] input)
    {
        return Encoding.UTF8.GetString(input, 0, input.Length);
    }
    
    public static string Utf8WithBomToString(this byte[] input)
    {
        var preamble = Encoding.UTF8.GetPreamble();
        var inputBytes = new byte[input.Length - preamble.Length];
        Array.Copy(input, preamble.Length, inputBytes, 0, inputBytes.Length);
        return Encoding.UTF8.GetString(inputBytes, 0, inputBytes.Length);
    }

    public static string StringToBase64(this string input)
    {
        return BytesToBase64(StringToBytes(input));
    }

    public static string Base64ToString(this string input)
    {
        return BytesToString(Base64ToBytes(input));
    }

    private static string GetMd5Hash(MD5 md5Hash, string input)
    {
        var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        var sBuilder = new StringBuilder();
        foreach (var c in data)
        {
            sBuilder.Append(c.ToString("x2"));
        }

        return sBuilder.ToString();
    }

    public static string GetMd5(this string sourceString)
    {
        var hash = GetMd5Hash(MD5.Create(), sourceString);
        return hash;
    }

    public static string GetMd5(this byte[] data)
    {
        using var md5 = MD5.Create();
        var hash = md5.ComputeHash(data);
        var hex = BitConverter.ToString(hash);
        return hex.Replace("-", "");
    }

    public static string SafeSubstring(this string source, int maxLength)
    {
        return source.Length <= maxLength ? source : $"{source[..(maxLength - 3)]}...";
    }

    public static bool IsInFollowingExtension(this string filename, params string[] extensions)
    {
        var ext = Path.GetExtension(filename);
        return extensions.Any(extension => ext.Trim('.').ToLower() == extension);
    }

    public static bool IsStaticImage(this string filename)
    {
        return filename.IsInFollowingExtension("jpg", "png", "bmp", "jpeg");
    }

    public static string RemoveTags(this string content)
    {
        var regex = new Regex("<.*?>");
        return regex.Replace(content, string.Empty);
    }

    public static string RandomString(int count)
    {
        var checkCode = string.Empty;
        var random = new Random(Seed.Next());
        for (var i = 0; i < count; i++)
        {
            var number = random.Next();
            number %= 36;
            if (number < 10)
            {
                number += 48;
            }
            else
            {
                number += 55;
            }

            checkCode += ((char)number).ToString();
        }

        return checkCode;
    }

    public static string EncodePath(this string input)
    {
        return string.IsNullOrWhiteSpace(input) ? 
            string.Empty : 
            input.ToUrlEncoded().Replace("%2F", "/");
    }

    public static string ToUrlEncoded(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        return Uri.EscapeDataString(input);
    }

    public static string AppendPath(this string? root, string folder)
    {
        return root == null ? folder : root + "/" + folder;
    }

    public static string? DetachPath(this string? path)
    {
        if (path == null || !path.Contains("/"))
        {
            return null;
        }

        return path.Replace("/" + path.Split('/').Last(), "");
    }

    public static IEnumerable<string> SplitInParts(this string input, int partLength)
    {
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        if (partLength <= 0)
        {
            throw new ArgumentException("Part length has to be positive.", nameof(partLength));
        }

        for (var i = 0; i < input.Length; i += partLength)
        {
            yield return input.Substring(i, Math.Min(partLength, input.Length - i));
        }
    }

    public static string HumanReadableSize(this long size)
    {
        double sizeD = size;
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        var order = 0;
        while (sizeD >= 1024 && order < sizes.Length - 1)
        {
            order++;
            sizeD /= 1024;
        }

        return $"{sizeD:0.##} {sizes[order]}";
    }

    public static bool IsValidJson(this string strInput)
    {
        if (string.IsNullOrWhiteSpace(strInput))
        {
            return false;
        }

        strInput = strInput.Trim();
        if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object.
            (strInput.StartsWith("\"") && strInput.EndsWith("\"")) || // For string.
            (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array.
        {
            try
            {
                JsonDocument.Parse(strInput); // throw exception for illegal json format.
                return true;
            }
            catch (JsonException)
            {
            }
        }

        return false;
    }
    
    public static bool IsTrue(this string? input)
    {
        return string.Equals(input?.Trim(), true.ToString(), StringComparison.OrdinalIgnoreCase);
    }
    
    public static bool IsFalse(this string? input)
    {
        return !input.IsTrue();
    }
}