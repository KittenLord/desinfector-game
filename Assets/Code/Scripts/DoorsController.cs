using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    [SerializeField] private Transform Opened;
    [SerializeField] private Transform Closed;
    private BoxCollider2D doorCollider;

    private bool closed = true;

    public void Toggle() => Set(!closed);
    public void Open() => Set(false);
    public void Close() => Set(true);

    private void Set(bool close)
    {
        closed = close;

        Opened.gameObject.SetActive(!closed);
        Closed.gameObject.SetActive(closed);

        doorCollider.isTrigger = !closed;
    }

    private void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>();
    }
}
