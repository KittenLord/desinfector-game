using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("giveOrJump")]
public class GiveOrJumpInteraction : InteractionElement
{
    [JsonProperty("inventory")] public string InventoryId { get; private set; }
    [JsonProperty("items")] public string[] Items { get; private set; }
    [JsonProperty("elseState")] public string State { get; private set; }


    public override InteractionElement Copy() => new GiveOrJumpInteraction(InventoryId, Items, State);

    public override async Task Execute(InteractionContext context)
    {
        var inventories = context.Player.GetComponents<Inventory>();
        var inventory = inventories.ToList().Find(i => i.Id == InventoryId);

        if (inventory.HasEmptySlots(Items.Length)) foreach (var item in Items) inventory.AddNext(Data.Loaded.Items.Get(item));
        else { UnityEngine.Debug.Log("changestate");  context.Performer.ChangeSection(State); }

        await Task.Delay(1);
    }

    public override void Finish() { }

    public override void ForceStop() { }

    public GiveOrJumpInteraction(string inventory, string[] items, string elseState)
    {
        InventoryId = inventory;
        Items = items;
        State = elseState;
    }
}
