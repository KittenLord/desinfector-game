using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfx : MonoBehaviour
{
    [SerializeField] private AudioSource Source;

    public static Sfx Main { get; private set; }


    private float MaxVolume = 1;

    public void ChangeMaxVolume(float value)
    {
        value = Mathf.Clamp01(value);

        var reverseLerp = Mathf.InverseLerp(0, MaxVolume, Source.volume);
        MaxVolume = value;
        SetRelativeVolume(reverseLerp);
    }

    public float GetRelativeVolume()
    {
        return Mathf.InverseLerp(0, MaxVolume, Source.volume);
    }

    public void SetRelativeVolume(float volume)
    {
        Source.volume = Mathf.Lerp(0, MaxVolume, Mathf.Clamp01(volume));
    }

    public void Play(AudioClip clip)
    {
        Source.Stop();
        Source.PlayOneShot(clip);
    }

    public void Stop()
    {
        Source.Stop();
    }

    private void Awake()
    {
        Main = this;
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
