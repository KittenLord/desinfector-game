using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Cached<T>
{
    public T Value
    {
        get
        {
            if (cachedValue == null) cachedValue = Getter();
            return cachedValue;
        }
    }
    private T cachedValue;

    private Func<T> Getter;

    public Cached(Func<T> getter)
    {
        Getter = getter;
    }
}