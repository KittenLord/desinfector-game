using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookshelfSecret : MonoBehaviour
{
    private bool active = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TempFlags.Check("bookshelfSecret") && !active)
        {
            active = true;
            GetComponent<SpriteRenderer>().enabled = true; 
        }
    }
}
