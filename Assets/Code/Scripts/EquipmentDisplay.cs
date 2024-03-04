using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentDisplay : MonoBehaviour
{
    [SerializeField] private GameObject Gun;
    [SerializeField] private GameObject Armor;

    [SerializeField] private Inventory Inventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Gun.SetActive(Inventory.Contains(Data.Loaded.Items.Get("toxin_thrower"), 1));
        Armor.SetActive(Inventory.Contains(Data.Loaded.Items.Get("armor"), 1));
    }
}
