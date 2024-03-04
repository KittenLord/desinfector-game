using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static List<GameObject> Slots = new List<GameObject>();
    //public static List<InventoryUI> InventoryUIs = new List<InventoryUI>();


    [SerializeField] private TMP_Text Title;
    [SerializeField] private Transform Items;

    [SerializeField] private GameObject SlotPrefab;
    [SerializeField] private InventoryItemUI ItemPrefab;

    public Inventory Inventory;

    private void Start()
    {
        Slots.RemoveAll(slot => slot == null);
        //InventoryUIs.Add(this);
    }

    private void OnDestroy()
    {
        //InventoryUIs.Remove(this);
    }

    public void Display(Inventory inventory)
    {
        Inventory = inventory;

        Title.text = inventory.Name;

        foreach (Transform item in Items) { Slots.Remove(item.gameObject);  Destroy(item.gameObject); }

        foreach(var item in inventory.Items)
        {
            var slot = Instantiate(SlotPrefab, Items);
            Slots.Add(slot);

            if (item is null) continue;

            var itemUI = Instantiate<InventoryItemUI>(ItemPrefab, slot.transform);
            itemUI.SetItem(item);
            itemUI.Slot = slot;

            var r = itemUI.GetComponent<RectTransform>();
            var rs = slot.GetComponent<RectTransform>();

            r.sizeDelta = rs.sizeDelta;
        }

        StartCoroutine(Fix());
    }

    private IEnumerator Fix()
    {
        Items.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        yield return null;
        yield return null;
        yield return null;
        yield return null;

        Items.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.MinSize;
    }
}
