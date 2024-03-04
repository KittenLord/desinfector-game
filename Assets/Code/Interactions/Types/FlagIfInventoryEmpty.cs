using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("flagIfEmpty")]
public class FlagIfInventoryEmptyInteraction : InteractionElement
{
    [JsonProperty("flag")] public string Flag { get; private set; }

    public override InteractionElement Copy() => new FlagIfInventoryEmptyInteraction(Flag);

    public override async Task Execute(InteractionContext context)
    {
        var inv = context.Other.GetComponent<Inventory>();
        if (inv.IsEmpty()) TempFlags.Set(Flag);

        await Task.Delay(1);
    }

    public override void Finish() { }

    public override void ForceStop() { }

    public FlagIfInventoryEmptyInteraction(string flag)
    {
        Flag = flag;
    }
}
