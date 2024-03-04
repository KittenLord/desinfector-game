using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Data
{
    public static Data Loaded { get; private set; }
    public static void Load(Data data)
    {
        Loaded = data;
    }

    public IReadOnlyDictionary<string, Interaction> Interactions { get; private set; }
    public ItemRegistry Items { get; private set; }

    public Data(Dictionary<string, Interaction> interactions, ItemRegistry items)
    {
        Interactions = interactions;
        Items = items;
    }
}
