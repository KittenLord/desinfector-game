using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image Background;
    [SerializeField] private float BackgroundSpeed;

    [SerializeField] private MenuPanel NewGameConfirm;
    [SerializeField] private MenuPanel ContinueNoSave;
    [SerializeField] private MenuPanel SettingsMenu;
    [SerializeField] private MenuPanel CreditsMenu;
    [SerializeField] private MenuPanel ExitConfirm;
    [SerializeField] private MenuPanel InfoPanel;
    [SerializeField] private MenuPanel Changelog;

    private bool SecondaryMenuOpened()
    {
        var o = GameObject.FindGameObjectsWithTag("SecondaryMenu");
        return o.Count() > 0;
    }

    public void NewGame()
    {
        if (SecondaryMenuOpened()) return;

        var previousSave = Game.CheckFlag(Flag.AvailableSave);

        if(previousSave)
        {
            NewGameConfirm.Open();
            return;
        }

        ForceNewGame();
    }

    public void ForceNewGame()
    {
        MusicPlayer.Main.LerpMute();
        TempFlags.ResetAll();

        Flag.ResetAll();
        BugNestScript.RemainingBugs = 0;
        BugNestScript.TotalBugs = 0;
        Scenes.Load("GameScene");
    }

    public void Continue()
    {
        if (SecondaryMenuOpened()) return;

        ContinueNoSave.Open();
    }

    public void Settings()
    {
        if (SecondaryMenuOpened()) return;

        SettingsMenu.Open();
    }

    public void Credits()
    {
        if (SecondaryMenuOpened()) return;

        CreditsMenu.Open();
    }

    public void OpenChangelog()
    {
        if (SecondaryMenuOpened()) return;

        Changelog.Open();
    }

    public void Exit()
    {
        if (SecondaryMenuOpened()) return;

        ExitConfirm.Open();
    }

    public void ForceExit()
    {
        Debug.Log("exit");
        Application.Quit();
    }

    public void Info()
    {
        if (SecondaryMenuOpened()) return;

        InfoPanel.Open();
    }



    private Vector3 originalBgPosition;
    private void Start()
    {
        MusicPlayer.Main.SetRelativeVolume(1);
        MusicPlayer.Main.Play(Sound.Get("ost/menu"));
        originalBgPosition = Background.transform.position;
        MoveBg();
    }

    private void Update()
    {
        if (Time.frameCount % 500 == 0) MoveBg();
    }

    private void MoveBg()
    {
        var direction = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)).normalized;
        if ((originalBgPosition - Background.transform.position).magnitude > 120) direction = (originalBgPosition - Background.transform.position).normalized;
        StartCoroutine(MoveBackground(direction, BackgroundSpeed * Time.deltaTime * Screen.width / 1000, 500));
    }
    private IEnumerator MoveBackground(Vector2 direction, float speed, int frames)
    {
        direction = direction.normalized;
        for (int i = 0; i < frames; i++)
        {
            var delta = direction * speed;
            Background.transform.position += (Vector3)delta;

            yield return null;
        }
    }
}
