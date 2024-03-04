using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("musicVolume")]
public class SetMusicVolumeInteraction : InteractionElement
{
    [JsonProperty("volume")] public float Volume { get; private set; }
    public override InteractionElement Copy() => new SetMusicVolumeInteraction(Volume);

    public override async Task Execute(InteractionContext context)
    {
        MusicPlayer.Main.LerpValue(Volume, 0.05f);

        await Task.Delay(1);
    }

    public override void Finish() { }

    public override void ForceStop() { }

    public SetMusicVolumeInteraction(float volume)
    {
        Volume = volume;
    }
}