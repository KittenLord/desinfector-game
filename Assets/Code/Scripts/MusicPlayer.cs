using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource Source;

    public static MusicPlayer Main { get; private set; }


    private float MaxVolume = 1;

    public float GetMaxVolume() => MaxVolume;

    public void ChangeMaxVolume(float value)
    {
        value = Mathf.Clamp01(value);

        //var reverseLerp = Mathf.InverseLerp(0, MaxVolume, IntendedVolume);
        //var pr = IntendedVolume;
        MaxVolume = value;
        SetRelativeVolume(IntendedVolume);

        //IntendedVolume = pr;
    }

    public float GetRelativeVolume()
    {
        return Mathf.InverseLerp(0, MaxVolume, Source.volume);
    }

    private float IntendedVolume = 1;
    public void SetRelativeVolume(float volume)
    {
        IntendedVolume = volume;
        Source.volume = Mathf.Lerp(0, MaxVolume, Mathf.Clamp01(volume));
    }

    public void Play(AudioClip clip)
    {
        Source.Stop();
        Source.clip = clip;
        Source.Play();
    }

    public void Stop()
    {
        Source.Stop();
    }

    public void LerpValue(float targetVolume, float delay = 0.2f)
    {
        StartCoroutine(LerpAnimation(delay, targetVolume));
    }

    public void LerpMute(float delay = 0.2f)
    {
        StartCoroutine(LerpAnimation(delay, 0));
    }

    public void LerpUnmute(float delay = 0.2f)
    {
        StartCoroutine(LerpAnimation(delay, 1));
    }

    private IEnumerator LerpAnimation(float delay, float target)
    {
        var current = GetRelativeVolume();

        float lerp = 0;
        while(lerp < 1)
        {
            lerp += delay;
            var value = Mathf.Lerp(current, target, lerp);

            SetRelativeVolume(value);

            yield return null;
        }
    }

    private void Awake()
    {
        if (MusicPlayer.Main is not null)
        {
            Destroy(gameObject); 
            return;
        }

        Main = this;

        if (PlayerPrefs.HasKey("_MUSICVOLUME"))
        {
            ChangeMaxVolume(Mathf.Clamp01(PlayerPrefs.GetFloat("_MUSICVOLUME")));
        }
        else ChangeMaxVolume(0.5f);
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
