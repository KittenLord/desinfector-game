using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[JsonType("clear")]
public class ClearInteraction : InteractionElement
{
    public override InteractionElement Copy() => new ClearInteraction();

    public override async Task Execute(InteractionContext context)
    {
        await Task.Delay(1);
        context.Bar.ClearText();
    }

    public override void Finish() {}
    public override void ForceStop() {}
}
