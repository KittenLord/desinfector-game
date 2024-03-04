using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UnityEngine;

public class InteractionPerformer
{
    private Interaction Interaction;
    private InteractionContext Context;

    private Action OnStart;
    private Action OnEnd;



    private InteractionElement CurrentElement;

    private int CurrentIndex = 0;
    private string SectionName;

    private Dictionary<string, int> IntResponses = new Dictionary<string, int>();

    public void Start()
    {
        OnStart?.Invoke();
        ChangeSection(Context.Other?.InteractionState);
        Run();
    }

    private bool Stopped = false;
    public void Stop()
    {
        Stopped = true;
        CurrentElement?.ForceStop();
    }

    public void SetIntResponse(string id, int o)
    {
        IntResponses[id] = o;
    }

    public void ChangeSection(string sectionName)
    {
        SectionName = Interaction.ValidSectionOrDefault(sectionName);
        CurrentIndex = 0;
    }

    public void ChangeSectionResponse(string sectionGroup, string response)
    {
        var index = IntResponses.ContainsKey(response) ? IntResponses[response] : 0;
        var section = Interaction.GetSectionName(sectionGroup, index);
        ChangeSection(section);
    }

    private async void Run()
    {
        try { while(await ExecuteCurrent()) {} } catch(Exception ex) { Debug.Log(ex); }

        if(!Stopped)
            OnEnd?.Invoke();
    }

    private async Task<bool> ExecuteCurrent()
    {
        CurrentElement = Interaction.GetElement(SectionName, CurrentIndex);
        if(CurrentElement is null || Stopped) return false;

        CurrentIndex++;
        await CurrentElement.Execute(Context);
        return true;
    }

    public void FinishCurrent()
    {
        CurrentElement?.Finish();
    }

    public void InputOptionUI(int option)
    {
        CurrentElement?.Input(option);
    }

    public InteractionPerformer(Interaction interaction, InteractionBar bar, InteractableEntity player, InteractableEntity other, Action onStart, Action onEnd)
    {
        Interaction = interaction;
        Context = new InteractionContext(bar, this, player, other);

        OnStart = onStart;
        OnEnd = onEnd;
    }
}