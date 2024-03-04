using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;

public class InteractionBar : MonoBehaviour
{
    [SerializeField] private TMP_Text Text;

    [SerializeField] private Image IconHolder;
    [SerializeField] private Image Icon;

    [SerializeField] private InteractionOption[] Options;

    private InteractionPerformer currentPerformer;

    private void Start()
    {
        Reset();
        //this.gameObject.SetActive(false);
    }

    public async void StartInteraction(Interaction interaction, InteractableEntity player, InteractableEntity other, Action onStart, Action onEnd)
    {
        if (currentPerformer is not null) return;
        Activate();

        Action onEndModified = () => 
        {
            onEnd();

            Deactivate();
        };

        await Task.Delay(50);
        var performer = new InteractionPerformer(interaction, this, player, other, onStart, onEndModified);
        performer.Start();

        currentPerformer = performer;
    }

    public void FinishCurrentElement()
    {
        if (currentPerformer is null) return;
        currentPerformer.FinishCurrent();
    }

    public void InputOption(int o)
    {
        if (currentPerformer is null) return;
        currentPerformer.InputOptionUI(o);
    }

    private void Activate()
    {
        gameObject.SetActive(true);
        Reset();
    }

    private void Deactivate()
    {
        currentPerformer = null;

        if (this == null) return; // Already destroyed, dont care
        gameObject.SetActive(false);
    }

    void OnDestroy() 
    {
        currentPerformer?.Stop(); // also prevents onEnd from activating, though idk what will happen after Alt+F4. Probably nothing, but idk
    }



    public void Append(string text)
    {
        Text.text += text;
    }

    public void ClearText()
    {
        Text.text = "";
    }

    public void Reset()
    {
        ClearText();
        DisableIcon();
        DisableOptions();
    }

    public void SetIcon(string icon)
    {
        var sprite = Resources.Load<Sprite>($"Icons/{icon}");

        IconHolder.gameObject.SetActive(true);
        if (sprite != null) Icon.sprite = sprite;
    }

    public void DisableIcon()
    {
        IconHolder.gameObject.SetActive(false);
    }

    public void DisableOptions()
    {
        Options.ToList().ForEach(o => o.Active(false));
    }
    public void SetOptions(params string[] texts)
    {
        var count = Mathf.Min(texts.Length, Options.Length);

        for(int i = 0; i < count;  i++)
        {
            Options[i].Active(true);
            Options[i].SetText(texts[i]);
        }
    }
}
