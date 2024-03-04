using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("hault")]
public class HaultInteraction : InteractionElement
{
    [JsonIgnore] private bool stop = false;
    public override InteractionElement Copy() => new HaultInteraction();

    public override async Task Execute(InteractionContext context)
    {
        while (!stop) await Task.Delay(10);
    }

    public override void Finish()
    {
        stop = true;
    }

    public override void ForceStop() => Finish();
}