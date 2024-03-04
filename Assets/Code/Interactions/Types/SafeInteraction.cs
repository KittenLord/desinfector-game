using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("safe")]
public class SafeInteraction : InteractionElement
{
    public override InteractionElement Copy() => new SafeInteraction();
    [JsonIgnore] private bool stop = false;

    public override async Task Execute(InteractionContext context)
    {
        var cg = UI.Main.ToggleSafe(true);
        var pc = context.Player.GetComponent<PlayerController>();

        pc.canInteract = false;
        pc.canMove = false;
        pc.canUseItems = false;
        pc.canOpenInventory = false;

        while (cg.alpha > 0 && !stop) await Task.Delay(50);

        pc.canInteract = true;
        pc.canMove = true;
        pc.canUseItems = true;
        pc.canOpenInventory = true;
    }

    public override void Finish()
    {
        UI.Main.ToggleSafe(false);
    }

    public override void ForceStop()
    {
        stop = true;
    }
}