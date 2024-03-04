using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("end")]
public class EndGameInteraction : InteractionElement
{
    public override InteractionElement Copy() => new EndGameInteraction();

    public override async Task Execute(InteractionContext context)
    {
        await Task.Delay(500);

        context.Player.GetComponent<PlayerController>().EndGame();
    }

    public override void Finish() { }

    public override void ForceStop() { }
}