using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("icon")]
public class SetIconInteraction : InteractionElement
{
    [JsonProperty("name")] public string IconName { get; private set; }
    [JsonProperty("disable")] public bool Disable { get; private set; }
    public override InteractionElement Copy() => new SetIconInteraction(IconName, Disable);

    public override async Task Execute(InteractionContext context)
    {
        if (!Disable) context.Bar.SetIcon(IconName);
        else context.Bar.DisableIcon();

        await Task.Delay(1);
    }

    public override void Finish() { }

    public override void ForceStop() { }

    public SetIconInteraction(string iconName, bool disable)
    {
        IconName = iconName;
        Disable = disable;
    }
}