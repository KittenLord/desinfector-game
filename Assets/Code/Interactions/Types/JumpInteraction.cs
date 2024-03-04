using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("jump")]
public class JumpInteraction : InteractionElement
{
    [JsonProperty("state")] public string State { get; private set; }
    [JsonProperty("flags")] public List<string> Flags { get; private set; }

    public override InteractionElement Copy() => new JumpInteraction(State, Flags);

    public override async Task Execute(InteractionContext context)
    {
        if (Flags.All(flag => TempFlags.Check(flag))) context.Performer.ChangeSection(State);
        await Task.Delay(1);
    }

    public override void Finish() { }

    public override void ForceStop() { }

    public JumpInteraction(string state, List<string> flags)
    {
        State = state;
        Flags = flags;
    }
}
