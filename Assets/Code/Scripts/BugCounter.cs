using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics.Contracts;
using UnityEngine.UI;

public class BugCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text Total;
    [SerializeField] private TMP_Text Remaining;
    [SerializeField] private Slider Slider;
    [SerializeField] private CanvasGroup cg;

    private bool Run = false;
    public void Activate(bool run = true)
    {
        Run = run;
    }

    public void SetOpacity(float value)
    {
        cg.alpha = Mathf.Clamp01(value);
    }

    void Update()
    {
        if (!Run) return;

        var total = BugNestScript.TotalBugs;
        var remaining = total - BugNestScript.RemainingBugs; // this is actually killed bugs, but im too lazy to rename it

        Total.text = total.ToString();
        Remaining.text = remaining.ToString();

        Slider.value = Mathf.Clamp01((float)remaining / (float)total);
    }
}
