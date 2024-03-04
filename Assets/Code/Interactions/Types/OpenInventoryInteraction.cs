using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("inventory")]
public class OpenInventoryInteraction : InteractionElement
{
    [JsonProperty("inventories")] public string[] Inventories { get; private set; }
    [JsonIgnore] private bool stop = false;

    public override InteractionElement Copy() => new OpenInventoryInteraction(Inventories);

    public override async Task Execute(InteractionContext context)
    {
        var playerInventory = context.Player.GetComponent<PlayerController>().Inventory;

        var inventories = context.Other.GetComponents<Inventory>();
        var selectedInventories = inventories.Where(inv => Inventories.Contains(inv.Id));

        UI.Main.OpenInventory(playerInventory, selectedInventories.ToArray());

        UI.Main.InteractionBar.gameObject.SetActive(false);
        while (UI.Main.InventoryOpened && !stop) await Task.Delay(50);
        UI.Main.InteractionBar.gameObject.SetActive(true);
    }

    public override void Finish()
    {
        stop = true;
        UI.Main.HideInventory();
    }

    public override void ForceStop()
    {
        stop = true;
    }

    public OpenInventoryInteraction(string[] inventories)
    {
        Inventories = inventories;
    }
}