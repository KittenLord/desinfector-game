using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("carvingTable")]
public class CarvingTableInteraction : InteractionElement
{
    public override InteractionElement Copy() => new CarvingTableInteraction();
    [JsonIgnore] private bool stop;
    public override async Task Execute(InteractionContext context)
    {
        var voodoo = UI.Main.OpenVoodoo();

        while (voodoo.activeSelf && !stop) await Task.Delay(50);
    }

    public override void Finish() { }

    public override void ForceStop() { stop = true; }
}
