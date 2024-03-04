using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("jumpIfPlayerItem")]
public class JumpIfPlayerHasItemInteraction : InteractionElement
{
    [JsonProperty("state")] public string State { get; private set; }
    [JsonProperty("inventory")] public string InventoryId { get; private set; }
    [JsonProperty("items")] public ItemPair[] Items { get; private set; }


    public override InteractionElement Copy() => new JumpIfPlayerHasItemInteraction(State, InventoryId, Items);

    public override async Task Execute(InteractionContext context)
    {
        var inventories = context.Player.GetComponents<Inventory>();
        var inventory = inventories.ToList().Find(i => i.Id == InventoryId);

        if (Items.All(item => inventory.Contains(new Item(item.Id), item.Amount))) context.Performer.ChangeSection(State);

        await Task.Delay(1);
    }

    public override void Finish() { }

    public override void ForceStop() { }

    public JumpIfPlayerHasItemInteraction(string state, string inventoryId, ItemPair[] items)
    {
        State = state;
        InventoryId = inventoryId;
        Items = items;
    }
}
