using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayTrigger : MonoBehaviour
{
    [SerializeField] private Trigger[] FirstGroup;
    [SerializeField] private Trigger[] SecondGroup;

    private bool FirstTrigger;
    private bool SecondTrigger;

    private bool CurrentActivated = false;

    public void SetTrigger(bool firstTrigger, bool value)
    {
        if (firstTrigger) FirstTrigger = value;
        else SecondTrigger = value;
    }

    private void Update()
    {
        if(FirstTrigger && !SecondTrigger && CurrentActivated)
        {
            CurrentActivated = false;

            foreach(var g in FirstGroup) { g?.On(); }
            foreach (var g in SecondGroup) { g?.Off(); }
            return;
        }

        if (!FirstTrigger && SecondTrigger && !CurrentActivated)
        {
            CurrentActivated = true;

            foreach (var g in FirstGroup) { g?.Off(); }
            foreach (var g in SecondGroup) { g?.On(); }
            return;
        }
    }
}
