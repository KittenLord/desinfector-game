using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[JsonType("warpBasement")]
public class BasementWarpInteraction : InteractionElement
{
    [JsonProperty("outOfBasement")] public bool OutOfBasement { get; private set; }

    private static bool firstUse = true;

    public override InteractionElement Copy() => new BasementWarpInteraction(OutOfBasement);

    public override async Task Execute(InteractionContext context)
    {
        var player = GameObject.FindGameObjectWithTag("Player").transform;

        UI.Main.FadeOut();

        await Task.Delay(1000);
        player.position = OutOfBasement ? Game.PaintingLocation : Game.BasementLocation;
        await Task.Delay(1000);

        if(firstUse) 
        {
            firstUse = false;

            MusicPlayer.Main.Play(Sound.Get("ost/basement"));
            MusicPlayer.Main.LerpUnmute();
        }

        UI.Main.FadeIn();
    }

    public override void Finish() { }

    public override void ForceStop() { }

    public BasementWarpInteraction(bool outOfBasement)
    {
        OutOfBasement = outOfBasement;
    }
}
