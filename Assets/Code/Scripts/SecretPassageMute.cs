using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretPassageMute : MonoBehaviour
{
    [SerializeField] private Transform Player;

    void Start()
    {
        
    }

    // Update is called once per frame

    private float Radius = 18f;
    void Update()
    {
        var distance = ((Vector2)(Player.position - transform.position)).magnitude;

        if(!BugNestScript.IsOver && distance <= Radius && Player.position.y <= transform.position.y)
        {
            var lerp = Mathf.Pow(Mathf.InverseLerp(5, Radius, distance), 1.5f);
            MusicPlayer.Main.SetRelativeVolume(lerp);
        }
    }
}
