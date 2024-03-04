using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColorSprites : MonoBehaviour
{
    private List<SpriteRenderer> renderers;

    void Start()
    {
        renderers = this.GetComponentsInChildren<SpriteRenderer>(true).ToList();
    }

    public void SetColor(Color color)
    {
        foreach (var r in renderers) if(r != null) r.color = color;
    }
}
