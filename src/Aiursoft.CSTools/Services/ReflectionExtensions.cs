using System.Reflection;

namespace Aiursoft.CSTools.Services;

/// <summary>
/// Provides extension methods for reflection-related operations.
/// </summary>
public static class ReflectionExtensions
{
    /// <summary>
    /// Sets a _private_ Property Value from a given Object. Uses Reflection.
    /// Throws a ArgumentOutOfRangeException if the Property is not found.
    /// </summary>
    /// <typeparam name="TObject">Type of the Object</typeparam>
    /// <typeparam name="TValue">Type of the Value</typeparam>
    /// <param name="obj">Object from where the Property Value is set</param>
    /// <param name="propName">Property name as string.</param>
    /// <param name="val">Value to set.</param>
    /// <returns>PropertyValue</returns>
    public static void SetPrivatePropertyValue<TObject, TValue>(this TObject obj, string propName, TValue val)
        where TObject : class
    {
        var t = typeof(TObject);
        var prop = t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        if (prop == null)
        {
            throw new ArgumentOutOfRangeException(nameof(propName),
                $"Property {propName} was not found in Type {t.FullName}");
        }

        prop.SetValue(obj, val, null);
    }
}