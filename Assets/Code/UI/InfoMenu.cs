using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoMenu : MenuPanel
{
    [SerializeField] private TMP_Text Text;

    protected override IEnumerator OpenCoroutine()
    {
        var bugsKilled = PlayerPrefs.GetInt("BUGSKILLED", 0);
        Text.text = "Bugs killed: " + bugsKilled;
        yield return null;
    }
}
