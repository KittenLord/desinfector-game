using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform Player;
    private Vector3 velocity;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player is null) return;

        var tp = transform.position;
        tp.z = Player.position.z;

        var pos = Vector3.SmoothDamp(tp, Player.position, ref velocity, 0.3f);
        pos.z = transform.position.z;

        transform.position = pos;
    }
}
