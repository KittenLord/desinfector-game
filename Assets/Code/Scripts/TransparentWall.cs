using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentWall : Trigger
{
    private RendererOpacity r;

    void Start()
    {
        r = GetComponent<RendererOpacity>();
    }

    private IEnumerator Fade(bool forward)
    {
        float lerp = 0;
        while(lerp < 1)
        {
            lerp += 3 * Time.deltaTime;

            if (forward) r.Set(lerp);
            else r.Set(1 - lerp);

            yield return null;
        }
    }

    public override void On()  
    {
        if (!this.enabled) return;

        StartCoroutine(Fade(true));
    }

    public override void Off()
    {
        StartCoroutine(Fade(false));
    }
}
