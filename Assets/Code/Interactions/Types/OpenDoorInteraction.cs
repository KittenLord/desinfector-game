using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("door")]
public class OpenDoorInteraction : InteractionElement
{
    public override InteractionElement Copy() => new OpenDoorInteraction();

    public override async Task Execute(InteractionContext context)
    {
        var entity = context.Other;
        var door = entity.GetComponent<DoorsController>();
        door.Toggle();

        await Task.Delay(1);
    }

    public override void Finish()
    {

    }

    public override void ForceStop()
    {

    }
}