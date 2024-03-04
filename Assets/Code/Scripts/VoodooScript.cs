using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoodooScript : MonoBehaviour
{
    public Rigidbody2D totem;
    public bool Working;

    void Start()
    {
        previousVector2 = transform.position;
    }

    // Update is called once per frame

    private Vector2 previousVector2;
    void FixedUpdate()
    {
        if (!Working) return;

        var pos = (Vector2)transform.position;
        if (pos != previousVector2)
        {
            var vector = pos - previousVector2;
            totem.MovePosition((Vector2)totem.transform.position + vector * 3f);

            previousVector2 = pos;
        }
    }
}
