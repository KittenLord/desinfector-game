using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[JsonType("destroy")]
public class DestroySelfInteraction : InteractionElement
{
    public override InteractionElement Copy() => new DestroySelfInteraction();

    public override async Task Execute(InteractionContext context)
    {
        GameObject.Destroy(context.Other.gameObject);
        await Task.Delay(1);
    }

    public override void Finish() { }

    public override void ForceStop() { }
}