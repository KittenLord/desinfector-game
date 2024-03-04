using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

[JsonType("append")]
public class AppendInteraction : InteractionElement
{
    [JsonProperty("text")] public string Text { get; private set; }
    [JsonProperty("delay")] public int Delay { get; private set; }
    [JsonProperty("skippable")] public bool Skippable { get; private set; }

    [JsonIgnore] private ControlledDelay cd;
    [JsonIgnore] private bool skip;

    public override InteractionElement Copy() => new AppendInteraction(Text, Delay, Skippable);

    public override async Task Execute(InteractionContext context)
    {
        var text = Text;
        
        for(int i = 0; i < text.Length; i++)
        {
            context.Bar.Append(text[i].ToString());

            if(!skip)
            {
                cd = new ControlledDelay(Delay);
                await cd.Start();
            }
        }
    }

    public override void Finish()
    {
        if (!Skippable) return;

        skip = true;
        cd?.Stop();
    }

    public override void ForceStop()
    {
        skip = true;
        cd?.Stop();
    }

    public AppendInteraction(string text, int delay, bool skippable)
    {
        Text = text;
        Delay = delay;
        Skippable = Skippable;
    }
}