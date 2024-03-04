using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("setState")]
public class SetEntityStateInteraction : InteractionElement
{
    [JsonProperty("state")] public string State { get; private set; }

    public override InteractionElement Copy() => new SetEntityStateInteraction(State);

    public override async Task Execute(InteractionContext context)
    {
        context.Other.InteractionState = State;
        await Task.Delay(1);
    }

    public override void Finish() { }

    public override void ForceStop() { }

    public SetEntityStateInteraction(string state)
    {
        State = state;
    }
}
