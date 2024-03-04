using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using LocalizationLib;

public class InitializeGame : MonoBehaviour
{
    private static bool Initialized = false;

    private Dictionary<string, Interaction> LoadInteractions()
    {
        var interactionAssets = Resources.LoadAll<TextAsset>("Interactions");

        // TODO: Add search in some mods folder
        Dictionary<string, Interaction> interactions = new Dictionary<string, Interaction>();
        foreach (var asset in interactionAssets)
        {
            var id = asset.name;
            var json = asset.text;

            var interaction = JsonConvert.DeserializeObject<Interaction>(json, new JsonTypeConverter<InteractionElement>());
            if (interaction is null) continue;

            interactions[id] = interaction;
        }

        return interactions;
    }

    void Awake()
    {
        if (Initialized) return;

        Initialized = true;
        Localizator loc = new Localizator(new LocalizationUnityResourcesReader());
        Localization.InitializeLocalizator(loc);
        Localization.SetLocalization("eng");

        var interactions = LoadInteractions();
        var items = new ItemRegistry();

        items.Add(() => new ToxinThrowerItem("toxin_thrower"));
        items.Add(() => new Item("armor"));
        items.Add(() => new Item("candle"));
        items.Add(() => new Item("rightKey"));
        items.Add(() => new Item("bigKey"));

        items.Add(() => new BookItem("goodBook1").SetModel("DefaultBook"));
        items.Add(() => new BookItem("goodBook2").SetModel("DefaultBook"));
        items.Add(() => new BookItem("goodBook3").SetModel("DefaultBook"));
        items.Add(() => new BookItem("goodBook4").SetModel("DefaultBook"));
        items.Add(() => new BookItem("goodBook5").SetModel("DefaultBook"));
        items.Add(() => new BookItem("goodBook6").SetModel("DefaultBook"));

        items.Add(() => new BookItem("badBook1").SetModel("DefaultBook"));
        items.Add(() => new BookItem("badBook2").SetModel("DefaultBook"));
        items.Add(() => new BookItem("badBook3").SetModel("DefaultBook"));
        items.Add(() => new BookItem("badBook4").SetModel("DefaultBook"));
        items.Add(() => new BookItem("badBook5").SetModel("DefaultBook"));
        items.Add(() => new BookItem("badBook6").SetModel("DefaultBook"));
        items.Add(() => new BookItem("badBook7").SetModel("DefaultBook"));

        items.Add(() => new Item("notePart1"));
        items.Add(() => new Item("notePart2"));
        items.Add(() => new Item("notePart3"));

        items.Add(() => new NoteItem("note"));

        var data = new Data(interactions, items);
        Data.Load(data);
    }
}
