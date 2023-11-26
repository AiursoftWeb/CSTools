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
}