using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;

    private ParticleSystem.MinMaxGradient defaultColor;

    public void Play() => Play(defaultColor);
    public void Play(Color color) => Play(new ParticleSystem.MinMaxGradient(color, color));
    public void Play(Color colorA, Color colorB) => Play(new ParticleSystem.MinMaxGradient(colorA, colorB));

    private void Play(ParticleSystem.MinMaxGradient gradient)
    {
        var set = ps.main;
        set.startColor = gradient;

        ps.Play();
    }

    void Start()
    {
        defaultColor = ps.main.startColor;
        ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
