using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemUI : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private TMP_Text Text;
    [SerializeField] private RectTransform This;

    private Item Item;

    private Vector2 ap;

    public GameObject Slot;

    private float divisor;

    private void Start()
    {
        ap = This.anchoredPosition;
        divisor = transform.parent.parent.parent.parent.localScale.x;
    }

    public void OnDrag(PointerEventData eventData)
    {
        This.anchoredPosition += eventData.delta / UI.Main.Canvas.scaleFactor / divisor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.parent.parent.parent.parent.SetAsLastSibling();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        InventoryUI.Slots.RemoveAll(s => s == null);

        var slot = InventoryUI.Slots.Select(s => s.GetComponent<RectTransform>()).Where(r => r.Overlaps(This)).FirstOrDefault();
        if(slot == null)
        {
            This.anchoredPosition = ap;
            return;
        }

        if (slot.childCount != 0)
        {
            TrySwapItems(slot);
            return;
        }

        var inventory = GetInventoryFromSlot(slot);
        if (!inventory.Inventory.Allows(Item))
        {
            This.anchoredPosition = ap;
            return;
        }
        var oldInventory = GetInventoryFromSlot(Slot.transform);

        var index = slot.GetSiblingIndex();
        var oldIndex = Slot.transform.GetSiblingIndex();

        oldInventory.Inventory.SetSlot(oldIndex, null);
        inventory.Inventory.SetSlot(index, Item);

        transform.parent = slot;
        This.anchoredPosition = ap;

        Slot = slot.gameObject;
    }

    private void TrySwapItems(RectTransform otherSlot)
    {
        var thisInventory = GetInventoryFromSlot(Slot.transform);
        var otherInventory = GetInventoryFromSlot(otherSlot);

        var thisItem = this;
        var otherItem = otherSlot.GetChild(0).GetComponent<InventoryItemUI>();

        if (otherItem == null) { This.anchoredPosition = ap; return; }
        if (!thisInventory.Inventory.Allows(otherItem.Item) || !otherInventory.Inventory.Allows(thisItem.Item)) { This.anchoredPosition = ap; return; }

        var thisIndex = Slot.transform.GetSiblingIndex();
        var otherIndex = otherSlot.GetSiblingIndex();

        otherInventory.Inventory.SetSlot(otherIndex, thisItem.Item);
        thisInventory.Inventory.SetSlot(thisIndex, otherItem.Item);

        var _slot = Slot;
        thisItem.transform.parent = otherSlot;
        otherItem.transform.parent = Slot.transform;

        thisItem.Slot = otherSlot.gameObject;
        otherItem.Slot = _slot;

        thisItem.This.anchoredPosition = ap;
        otherItem.This.anchoredPosition = ap;
    }

    private InventoryUI GetInventoryFromSlot(Transform slot)
    {
        return slot.parent.parent.GetComponent<InventoryUI>();
    }

    public void SetItem(Item item)
    {
        Item = item;
        Text.text = item.Name;
    }
}
