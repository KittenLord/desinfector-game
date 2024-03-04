using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionOption : MonoBehaviour
{
    [SerializeField] private TMP_Text Text;
    [SerializeField] private int Index;

    public void Press()
    {
        UI.Main.InteractionBar.InputOption(Index);
    }

    public void SetText(string text)
    {
        Text.text = text;
    }

    public void Active(bool a)
    {
        gameObject.SetActive(a);
    }
}
