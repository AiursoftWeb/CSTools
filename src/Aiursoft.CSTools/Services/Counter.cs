﻿using Aiursoft.Scanner.Abstractions;

namespace Aiursoft.CSTools.Services;

public class Counter : ISingletonDependency
{
    private int _current;

    /// <summary>
    ///     Last returned counter value. If a initial counter, will be 0.
    /// </summary>
    public int GetCurrent => _current;

    /// <summary>
    ///     Get a new scope unique number which is one larger than current. First one is 1
    /// </summary>
    /// <returns></returns>
    public int GetUniqueNo()
    {
        return Interlocked.Increment(ref _current);
    }
}