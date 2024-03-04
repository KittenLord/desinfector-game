using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Interaction
{
    [JsonProperty("groups")] Dictionary<string, List<string>> Groups = new Dictionary<string, List<string>>();
    [JsonProperty("sections")] Dictionary<string, List<InteractionElement>> Sections = new Dictionary<string, List<InteractionElement>>();

    public string ValidSectionOrDefault(string section)
    {
        return Sections.ContainsKey(section ?? "") ? section : (Sections.Keys.FirstOrDefault() ?? "");
    }

    public string GetSectionName(string group, int index)
    {
        if (!Groups.ContainsKey(group)) return Sections.Keys.FirstOrDefault() ?? "";

        var g = Groups[group];
        if(index >= g.Count) return Sections.Keys.FirstOrDefault() ?? "";
        return g[index];
    }

    public int GetSectionLength(string sectionName)
    {
        if(!Sections.ContainsKey(sectionName)) return 0;
        var section = Sections[sectionName];

        return section.Count;
    }

    public InteractionElement GetElement(string sectionName, int index)
    {
        if(!Sections.ContainsKey(sectionName)) return null;
        var section = Sections[sectionName];

        if(section.Count <= index) return null;
        return section[index].Copy();
    }

    [JsonConstructor] public Interaction() {}

    public Interaction(Dictionary<string, List<string>> groups, Dictionary<string, List<InteractionElement>> sections)
    {
        Groups = groups;
        Sections = sections;
    }
}