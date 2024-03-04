using LocalizationLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [field: SerializeField] public string Id { get; private set; }

    public LocalizedString Name => new LocalizedString("inventory." + Id);

    [SerializeField] private string[] items;
    [SerializeField] private string[] allowedItems;

    public Item[] Items;

    private void Start()
    {
        Items = items.Select((itemName) => Data.Loaded.Items.Get(itemName)).ToArray();
    }

    public event Action<Item> OnSetSlot = (i) => { };

    public bool Allows(Item item)
    {
        return allowedItems == null || allowedItems.Length <= 0 || allowedItems.Contains(item?.Id ?? "AAAAAAAAAAA");
    }

    public bool IsEmpty() => Items.All(item => item is null);

    public void SetSlot(int slot, Item item)
    {
        if (!Allows(item) && item is not null) return;

        Items[Math.Clamp(slot, 0, Items.Length)] = item;
        OnSetSlot.Invoke(item);
    }

    public void AddNext(Item item)
    {
        var index = Items.ToList().IndexOf(null);
        Debug.Log(index);
        if (index >= 0) SetSlot(index, item);
    }

    public bool Contains(Item item, int amount)
    {
        if (item is null) return false;
        return Items.Where(i => i?.Id == item.Id).Count() >= amount;
    }

    public bool HasEmptySlots(int slots)
    {
        return Items.Where(item => item is null).Count() >= slots;
    }

    public bool RemoveAny(Item item, int amount)
    {
        if(!Contains(item, amount)) return false;

        int alreadyRemoved = 0;
        for(int i = 0; i < Items.Length; i++)
        {
            if (alreadyRemoved >= amount) break;

            if (Items[i]?.Id == item.Id)
            {
                Items[i] = null;
                alreadyRemoved++;
            }
        }

        return true;
    }
}