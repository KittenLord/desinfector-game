using LocalizationLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[JsonType("options")]
public class OptionsInteraction : InteractionElement
{
    [JsonProperty("options")] public List<string> Options { get; private set; }
    [JsonProperty("response")] public string ResponseId { get; private set; }

    [JsonIgnore] private bool stop = false;
    [JsonIgnore] private InteractionContext c;

    public override InteractionElement Copy() => new OptionsInteraction(Options, ResponseId);

    public override async Task Execute(InteractionContext context)
    {
        var options = Options.Select(o => Localization.GetSafe(o));
        context.Bar.SetOptions(options.ToArray());

        if (c is null) c = context;
        while(!stop)
        {
            await Task.Delay(30);
        }
    }

    public override void Finish() { }

    public override void ForceStop()
    {
        stop = true;
    }

    public override void Input(int index)
    {
        c?.Performer?.SetIntResponse(ResponseId, index);
        stop = true;
        c?.Bar?.DisableOptions();
    }

    public OptionsInteraction(List<string> options, string responseId)
    {
        Options = options;
        ResponseId = responseId;
    }
}
