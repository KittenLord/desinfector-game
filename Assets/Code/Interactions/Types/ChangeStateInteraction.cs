using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

[JsonType("state")]
public class ChangeStateInteraction : InteractionElement
{
    [JsonProperty("state")] public string StateName { get; private set; }

    [JsonProperty("group")] public string GroupName { get; private set; }
    [JsonProperty("response")] public string ResponseId { get; private set; }

    public override InteractionElement Copy() => new ChangeStateInteraction(StateName, ResponseId, GroupName);

    public override async Task Execute(InteractionContext context)
    {
        if(ResponseId is not null && ResponseId != "")
        {
            context.Performer.ChangeSectionResponse(GroupName, ResponseId);
            return;
        }

        await Task.Delay(1);
        context.Performer.ChangeSection(StateName);
    }

    public override void Finish() {}
    public override void ForceStop() {}

    public ChangeStateInteraction(string stateName, string response, string group)
    {
        StateName = stateName;
        GroupName = group;
        ResponseId = response;
    }
}