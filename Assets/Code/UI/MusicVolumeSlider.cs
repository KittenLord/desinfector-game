using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = MusicPlayer.Main.GetMaxVolume();
        slider.onValueChanged.AddListener((f) =>
        {
            MusicPlayer.Main.ChangeMaxVolume(f);
            //MusicPlayer.Main.SetRelativeVolume(1);

            PlayerPrefs.SetFloat("_MUSICVOLUME", MusicPlayer.Main.GetMaxVolume());
            PlayerPrefs.Save();
        });
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
