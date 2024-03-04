using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirthdayMusicScript : MonoBehaviour
{
    private bool Activated = false;
    [SerializeField] private Transform Player;
    void Start()
    {
        
    }

    private float MinRadius = 10;

    private float MaxRadius = 30;

    // Update is called once per frame
    void Update()
    {
        if (TempFlags.Check("enableMusic")) return;

        if(!Activated && TempFlags.Check("birthday"))
        {
            Activated = true;
        }

        if(Activated)
        {
            var d = ((Vector2)Player.position - (Vector2)transform.position).magnitude;

            if (d < MinRadius)
            {
                MusicPlayer.Main.SetRelativeVolume(1);
                return;
            }

            var inverseLerp = 1 - Mathf.Clamp01(Mathf.InverseLerp(MinRadius, MaxRadius, d));
            MusicPlayer.Main.SetRelativeVolume(inverseLerp);
        }
    }
}
