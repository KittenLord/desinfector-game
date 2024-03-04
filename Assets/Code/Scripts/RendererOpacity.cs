using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RendererOpacity : MonoBehaviour
{
    [SerializeField] private float DefaultValue = 1;

    private List<SpriteRenderer> renderers;
    private bool DoesObstruct = false;

    void Start()
    {
        renderers = this.GetComponentsInChildren<SpriteRenderer>(true).ToList();
        Opacity = renderers.FirstOrDefault()?.color.a ?? 1;

        Set(DefaultValue);
    }


    public float Opacity { get; private set; } = 1;
    public void Set(float opacity)
    {
        Opacity = Mathf.Clamp01(opacity);
        renderers.Where(r => r != null).ToList().ForEach(r => r.color = new Color(r.color.r, r.color.g, r.color.b, Opacity));
    }
}
