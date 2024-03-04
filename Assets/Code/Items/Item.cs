using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using LocalizationLib;

public class Item
{
    public string Id { get; private set; }
    public string OverrideModel { get; private set; } = "";
    public bool AimWhenHolding { get; private set; }

    public LocalizedString Name => new LocalizedString(LocalizationPath.Combine("item", Id, "name").Path);

    public Item SetModel(string model)
    {
        OverrideModel = model;
        return this;
    }

    public virtual void OnUse(ItemScript item, Entity player) { }
    public virtual void OnUseStart(ItemScript item, Entity player) { }
    public virtual void OnUseEnd(ItemScript item, Entity player) { }


    public Item(string id, bool aimWhenHolding = false)
    {
        Id = id;
        AimWhenHolding = aimWhenHolding;
    }
}