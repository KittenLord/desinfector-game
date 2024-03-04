using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;

[JsonType("inspectPainting")]
public class InspectPaintingInteraction : InteractionElement
{
    public override InteractionElement Copy() => new InspectPaintingInteraction();

    private bool stop = false;
    public override async Task Execute(InteractionContext context)
    {
        var p = UI.Main.OpenPaintingInspection();

        while (p.activeSelf && !stop) await Task.Delay(50);
    }

    public override void Finish() { }

    public override void ForceStop() { stop = true; }
}
