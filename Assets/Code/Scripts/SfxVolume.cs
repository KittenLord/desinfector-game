using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SfxVolume : MonoBehaviour
{
    private static float Volume = 1;

    [SerializeField] private bool IsSource = false;
    public void SetVolume(float f)
    {
        Volume = Mathf.Clamp01(f);
    }

    private void Awake()
    {
        if (PlayerPrefs.HasKey("_SFXVOLUME"))
        {
            Volume = Mathf.Clamp01(PlayerPrefs.GetFloat("_SFXVOLUME"));
        }
        else Volume = 0.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!IsSource)
        {
            GetComponent<Slider>().onValueChanged.AddListener((f) =>
            {
                Volume = f;
                PlayerPrefs.SetFloat("_SFXVOLUME", Volume);
                PlayerPrefs.Save();
            });
        }
    }

    private void OnEnable()
    {
        if (!IsSource)
        {
            GetComponent<Slider>().value = Volume;
        }
    }

    private AudioSource source;
    // Update is called once per frame
    void Update()
    {
        if(IsSource && Time.frameCount % 10 == 0)
        {
            if (source is null) source = GetComponent<AudioSource>();
            source.volume = Volume;
        }
    }
}
