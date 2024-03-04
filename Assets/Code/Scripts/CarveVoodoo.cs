using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarveVoodoo : MonoBehaviour
{
    [SerializeField] private Transform Basement;
    [SerializeField] private Vector2 Spawn;
    [SerializeField] private Vector2 TotemOriginalPosition;

    [SerializeField] private Transform[] Blocks;
    [SerializeField] private VoodooScript Prefab;
    [SerializeField] private Rigidbody2D Totem;

    private VoodooScript CurrentVoodoo;

    private void Start()
    {
        Shuffle();
    }

    public void Open()
    {
        Shuffle();
    }

    public void Submit()
    {
        if(CurrentVoodoo is not null)
        {
            Destroy(CurrentVoodoo.gameObject);
            Totem.transform.localPosition = TotemOriginalPosition;
        }

        var voodoo = Instantiate(Prefab, Spawn, Quaternion.identity, Basement);
        voodoo.totem = Totem;
        voodoo.transform.localPosition = Spawn;
        voodoo.Working = Validate();

        CurrentVoodoo = voodoo;
        gameObject.SetActive(false);
    }

    public bool Validate()
    {
        foreach (var b in Blocks) Debug.Log(b.rotation.eulerAngles.z);
        return Blocks.All(block => Mathf.RoundToInt(block.rotation.eulerAngles.z) == 0);
    }

    public void Shuffle()
    {
        foreach (var block in Blocks) block.rotation *= Quaternion.Euler(0, 0, -90 * Random.Range((int)0, (int)4));
    }

    public void RotateBlock(int block)
    {
        Blocks[block].rotation *= Quaternion.Euler(0, 0, -90);
    }
}
