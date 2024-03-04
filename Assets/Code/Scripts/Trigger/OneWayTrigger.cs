using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OneWayTrigger : MonoBehaviour
{
    [SerializeField] private TwoWayTrigger MainTrigger;


    [SerializeField] private bool FirstTrigger;
    private bool SecondTrigger => !FirstTrigger;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        MainTrigger.SetTrigger(FirstTrigger, true);    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        MainTrigger.SetTrigger(FirstTrigger, false);
    }
}
