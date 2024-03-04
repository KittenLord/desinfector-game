using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("toggle")]
public class ToggleBarInteraction : InteractionElement
{
    [JsonProperty("enable")] public bool Enable { get; private set; }
    public override InteractionElement Copy() => new ToggleBarInteraction(Enable);

    public override async Task Execute(InteractionContext context)
    {
        UI.Main.InteractionBar.gameObject.SetActive(Enable);

        await Task.Delay(1);
    }

    public override void Finish() { }

    public override void ForceStop() { }

    public ToggleBarInteraction(bool enable)
    {
        Enable = enable;
    }
}