using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("altar")]
public class AltarInteraction : InteractionElement
{
    public override InteractionElement Copy() => new AltarInteraction();

    public override async Task Execute(InteractionContext context)
    {
        context.Other.GetComponent<AltarController>().OnAltar();

        await Task.Delay(1);
    }

    public override void Finish() { }

    public override void ForceStop() { }
}
