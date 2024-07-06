namespace Aiursoft.CSTools.Tools;

public static class CollectionExtends
{
    public static T2 TryFindFirst<T1, T2>(
        this IEnumerable<T1> collection, 
        Func<T1, bool> func,
        Func<T1, T2> onFound,
        Func<T2> onNotFound)
    {
        foreach (var item in collection)
        {
            if (func(item))
            {
                return onFound(item);
            }
        }
        return onNotFound();
    }

    public static void TryFindFirst<T>(
        this IEnumerable<T> collection, 
        Func<T, bool> func,
        Action<T> onFound,
        Action onNotFound)
    {
        foreach (var item in collection)
        {
            if (func(item))
            {
                onFound(item);
                return;
            }
        }
        onNotFound();
    }
}