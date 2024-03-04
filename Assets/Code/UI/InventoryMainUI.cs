using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMainUI : MonoBehaviour
{
    [SerializeField] private InventoryUI PlayerInventory;
    [SerializeField] private Transform OtherInventories;

    [SerializeField] private InventoryUI InventoryPrefab;


    public void Display(Inventory player, params Inventory[] other)
    {
        PlayerInventory.Display(player);

        foreach (Transform inv in OtherInventories) Destroy(inv.gameObject);

        foreach(var inventory in other)
        {
            var iui = Instantiate(InventoryPrefab, OtherInventories);
            iui.Display(inventory);
        }

        StartCoroutine(Fix());
    }

    private IEnumerator Fix()
    {
        var v = OtherInventories.GetComponent<VerticalLayoutGroup>();
        v.spacing += 0.01f;
        LayoutRebuilder.ForceRebuildLayoutImmediate(v.GetComponent<RectTransform>());

        yield return null; yield return null; yield return null; yield return null; yield return null;
        //yield return new WaitForSeconds(0.5f);

        v.spacing -= 0.01f;
    }
}
