using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LocalizationLib;

public class InteractableEntity : Entity
{
    public string GetLocalizedLine(string path)
    {
        return Localization.GetSafe(LocalizationPath.Combine("interaction", InteractionId, path).Path);
    }

    [field: SerializeField] public string InteractionId;
    [field: SerializeField] public string InteractionState { get; set; }


    public LocalizedString Name => new LocalizedString("interaction." + InteractionId + ".state_name." + (InteractionState == "" ? "start" : InteractionState));

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
