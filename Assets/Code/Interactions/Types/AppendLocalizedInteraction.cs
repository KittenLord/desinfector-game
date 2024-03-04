using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LocalizationLib;
using Newtonsoft.Json;

[JsonType("appendl")]
public class AppendLocalizedInteraction : InteractionElement
{
    [JsonProperty("path")] public string Path { get; private set; }
    [JsonProperty("delay")] public int Delay { get; private set; }
    [JsonProperty("nonlocal")] public bool NonLocalLine { get; private set; } = false;
    [JsonProperty("unskippable")] public bool Unskippable {  get; private set; } = false;


    [JsonIgnore] private ControlledDelay cd;
    [JsonIgnore] private bool skip;

    public override InteractionElement Copy() => new AppendLocalizedInteraction(Path, Delay, NonLocalLine, Unskippable);

    public override async Task Execute(InteractionContext context)
    {
        var text = !NonLocalLine ? (context.Other?.GetLocalizedLine(Path) ?? Path) : Localization.GetSafe(Path);
        
        for(int i = 0; i < text.Length; i++)
        {
            context.Bar.Append(text[i].ToString());

            if (!skip)
            {
                cd = new ControlledDelay(Delay);
                await cd.Start();
            }
        }
    }

    public override void Finish()
    {
        if (Unskippable) return;

        skip = true;
        cd?.Stop();
    }

    public override void ForceStop()
    {
        skip = true;
        cd?.Stop();
    }

    public AppendLocalizedInteraction(string path, int delay, bool nonlocal, bool unskippable)
    {
        Path = path;
        Delay = delay;
        NonLocalLine = nonlocal;
        Unskippable = unskippable;
    }
}