using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BookshelfDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] TopShelf;
    [SerializeField] private GameObject[] BottomShelf;

    private Inventory Top;
    private Inventory Bottom; 

    void Start()
    {
        var inventories = this.GetComponents<Inventory>().ToList();
        Top = inventories.Find(i => i.Id == "good");
        Bottom = inventories.Find(i => i.Id == "bad");
    }

    // Update is called once per frame
    void Update()
    {
        var topBooks = Top.Items.Where(i => i is not null).Count();
        var bottomBooks = Bottom.Items.Where(i => i is not null).Count();

        for (int i = 0; i < TopShelf.Length; i++) TopShelf[i].SetActive(i < topBooks);
        for (int i = 0; i < BottomShelf.Length; i++) BottomShelf[i].SetActive(i < bottomBooks);
    }
}
