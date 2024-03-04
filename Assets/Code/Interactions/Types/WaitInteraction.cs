using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

[JsonType("wait")]
public class WaitInteraction : InteractionElement
{
    [JsonProperty("delay")] public int Delay { get; private set; }
    [JsonProperty("unskippable")] public bool Unskippable { get; private set; } = false;

    [JsonIgnore] private ControlledDelay cd;

    public override InteractionElement Copy() => new WaitInteraction(Delay, Unskippable);

    public override async Task Execute(InteractionContext context)
    {
        cd = new ControlledDelay(Delay);
        await cd.Start();
    }

    public override void Finish()
    {
        if(!Unskippable)
            cd?.Stop();
    }

    public override void ForceStop() => cd?.Stop();

    public WaitInteraction(int delay, bool unskippable)
    {
        Delay = delay;
        Unskippable = unskippable;
    }
}