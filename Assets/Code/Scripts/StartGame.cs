using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField] private CanvasGroup BlackScreen;
    [SerializeField] private CanvasGroup ControlsText;
    [SerializeField] private AudioSource Phone;
    [SerializeField] private GameObject PlayerBlocker;
    [SerializeField] private Transform Player;

    void Start()
    {
        TempFlags.ResetAll();

        if(!Game.CheckFlag(Flag.AvailableSave) || !Game.CheckFlag(Flag.ArrivedToHouse))
        {
            Game.SetFlag(Flag.AvailableSave);
            BlackScreen.alpha = 1;
            ControlsText.alpha = 0;

            StartCoroutine(StartPhase());
            return;
        }
    }


    private IEnumerator StartPhase()
    {
        Player.position = Game.WorkLocation;
        PlayerBlocker.SetActive(true);
        var player = GameObject.FindGameObjectWithTag("Player");
        var p = player.GetComponent<PlayerController>();
        p.canMove = false;
        p.canInteract = false;

        yield return new WaitForSeconds(3f);

        float lerp = 0;

        while(lerp < 1)
        {
            lerp += 0.2f * Time.deltaTime;
            ControlsText.alpha = Mathf.Lerp(0, 1, lerp);

            yield return null;
        }

        yield return new WaitForSeconds(3f);

        while (lerp > 0)
        {
            lerp -= 0.2f * Time.deltaTime;
            ControlsText.alpha = Mathf.Lerp(0, 1, lerp);

            yield return null;
        }

        yield return new WaitForSeconds(3f);

        StartCoroutine(Uncover());

        p.canMove = true;
        p.canInteract = true;

        var sound = Sound.Get("phone");
        while(!TempFlags.Check("phone"))
        {
            Phone.PlayOneShot(sound);

            while (Phone.isPlaying && !TempFlags.Check("phone")) yield return null;
        }
        PlayerBlocker.SetActive(false);
        Phone.Stop();
        Phone.PlayOneShot(Sound.Get("phonePickup"));

        yield return new WaitForSeconds(0.5f);
        MusicPlayer.Main.SetRelativeVolume(1);
        MusicPlayer.Main.Play(Sound.Get("ost/work"));
    }

    private IEnumerator Uncover()
    {
        yield return new WaitForSeconds(0.7f);

        BlackScreen.alpha = 0;
        ControlsText.alpha = 0;
    }
}
