using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ToxinThrowerItem : Item
{
    private static bool DamagedBug = false;

    private PlayerController playerController;
    private ToxinThrowerItemScript toxinThrowerItem;

    public override void OnUse(ItemScript item, Entity player)
    {
        if(playerController == null) playerController = player.GetComponent<PlayerController>();
        if(toxinThrowerItem == null) toxinThrowerItem = item as ToxinThrowerItemScript;

        if (!playerController.bodyInventory.Contains(Data.Loaded.Items.Get("armor"), 1)) playerController.Damage();

        var mouseDirection = (StaticInput.Instance.MousePosition - (Vector2)player.transform.position).normalized;
        var center = (Vector2)player.transform.position;

        var result = Physics2D.OverlapCircleAll(center, 5);
        var bugs = result.Select(entity => entity.GetComponent<BugNestScript>())
                         .Where(bug => bug is not null)
                         .Where(bug => Vector2.Dot(((Vector2)bug.transform.position - center).normalized, mouseDirection) > 0.95f &&
                                       (((Vector2)bug.transform.position - center).magnitude <= 4f));

        foreach (var bug in bugs) bug.Damage();

        if (bugs.Count() > 0) Debug.Log(bugs.Count());

        if (bugs.Count() > 0 && !DamagedBug)
        {
            DamagedBug = true;
            OnFirstUse();
        }
    }

    private async void OnFirstUse()
    {
        GameObject.FindObjectOfType<AnnounceBugsCount>().AnnounceAll();

        await Task.Delay(500);

        if (MusicPlayer.Main is not null)
        {
            MusicPlayer.Main.Play(Sound.Get("ost/house"));
            MusicPlayer.Main.LerpUnmute();
        }
    }

    public override void OnUseStart(ItemScript item, Entity player)
    {
        var t = item as ToxinThrowerItemScript;
        t.ParticleSystem.Play();

        playerController = player.GetComponent<PlayerController>();
        playerController.AimAtMouse = true;
        playerController.SpeedMultiplier = 0.5f;
    }
    public override void OnUseEnd(ItemScript item, Entity player)
    {
        var t = item as ToxinThrowerItemScript;
        t.ParticleSystem.Stop(true, UnityEngine.ParticleSystemStopBehavior.StopEmitting);

        var c = player.GetComponent<PlayerController>();
        c.AimAtMouse = false;
        c.SpeedMultiplier = 1;
    }


    public ToxinThrowerItem(string id) : base(id, true)
    {

    }
}