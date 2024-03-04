using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ItemRegistry
{
    private Dictionary<string, Func<Item>> items = new Dictionary<string, Func<Item>>();

    public void Add(Func<Item> constructor)
    {
        var test = constructor();
        items[test.Id] = constructor;
    }

    public Item Get(string id)
    {
        if (!items.ContainsKey(id)) return null;
        return items[id]();
    }
}