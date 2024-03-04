using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("candle")]
public class CandleInteraction : InteractionElement
{
    public override InteractionElement Copy() => new CandleInteraction();

    public override async Task Execute(InteractionContext context)
    {
        var altar = context.Other.transform.parent.GetComponent<AltarController>();
        altar.OnCandle(context.Other.transform.GetSiblingIndex());

        await Task.Delay(1);
    }

    public override void Finish() { }

    public override void ForceStop() { }
}