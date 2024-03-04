using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInput : MonoBehaviour
{
    public static StaticInput Instance { get; private set; }
    public Vector2 MousePosition { get; private set; }
    private Camera main;
    void Awake()
    {
        Instance = this;
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MousePosition = main.ScreenToWorldPoint(Input.mousePosition);
    }
}
