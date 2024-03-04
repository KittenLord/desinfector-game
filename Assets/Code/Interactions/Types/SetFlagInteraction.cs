using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("flag")]
public class SetFlagInteraction : InteractionElement
{
    [JsonProperty("flag")] public string Flag { get; private set; }
    [JsonProperty("value")] public bool Value { get; private set; }
    public override InteractionElement Copy() => new SetFlagInteraction(Flag, Value);


    public override async Task Execute(InteractionContext context)
    {
        TempFlags.Set(Flag, Value);
        await Task.Delay(1);
    }

    public override void Finish() { }

    public override void ForceStop() { }

    public SetFlagInteraction(string flag, bool value)
    {
        Flag = flag;
        Value = value;
    }
}