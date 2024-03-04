using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UnityEngine;
using LocalizationLib;

public class LocalizationUnityResourcesReader : ILocalizatorReader // Long-ass name, but it is used literally once, so idk idc
{
    public bool CanRead(string localization) => Resources.Load<TextAsset>($"Localization/{localization}") is not null;
    public string  Read(string localization) => Resources.Load<TextAsset>($"Localization/{localization}")?.text ?? "";
}