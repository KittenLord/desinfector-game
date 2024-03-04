using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("jumpIfRemove")]
public class JumpIfCanRemoveItemInteraction : InteractionElement
{
    [JsonProperty("inventory")] public string PlayerInventoryId { get; private set; }
    [JsonProperty("item")] public string ItemId { get; private set; }
    [JsonProperty("amount")] public int Amount { get; private set; }
    [JsonProperty("state")] public string State { get; private set; }

    public override InteractionElement Copy() => new JumpIfCanRemoveItemInteraction(PlayerInventoryId, ItemId, Amount, State);

    public override async Task Execute(InteractionContext context)
    {
        var inventories = context.Player.GetComponents<Inventory>();
        var inventory = inventories.ToList().Find(i => i.Id == PlayerInventoryId);

        if (inventory.RemoveAny(new Item(ItemId), Amount)) context.Performer.ChangeSection(State);

        await Task.Delay(1);
    }

    public override void Finish() { }

    public override void ForceStop() { }

    public JumpIfCanRemoveItemInteraction(string inventory, string item, int amount, string state)
    {
        PlayerInventoryId = inventory;
        ItemId = item;
        Amount = amount;
        State = state;
    }
}
