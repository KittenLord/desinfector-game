using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("jumpIfItems")]
public class JumpIfContainsItemsInteraction : InteractionElement
{
    [JsonProperty("state")] public string State { get; private set;  }
    [JsonProperty("inventory")] public string InventoryId { get; private set; }
    [JsonProperty("items")] public ItemPair[] Items { get; private set; }

    public override InteractionElement Copy() => new JumpIfContainsItemsInteraction(State, InventoryId, Items);

    public override async Task Execute(InteractionContext context)
    {
        var inventories = context.Other.GetComponents<Inventory>();
        var inventory = inventories.ToList().Find(i => i.Id ==  InventoryId);

        if (Items.All(item => inventory.Contains(new Item(item.Id), item.Amount))) context.Performer.ChangeSection(State);

        await Task.Delay(1);
    }

    public override void Finish() { }

    public override void ForceStop() { }

    public JumpIfContainsItemsInteraction(string state, string inventory, ItemPair[] items)
    {
        State = state;
        InventoryId = inventory;
        Items = items;
    }
}

public struct ItemPair
{
    [JsonProperty("item")] public string Id { get; private set; }
    [JsonProperty("amount")] public int Amount { get; private set; }

    [JsonConstructor] public ItemPair(string item, int amount)
    {
        Id = item;
        Amount = amount;
    }
}
