using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ContinuePanel : MenuPanel
{ 
    [SerializeField] private GameObject Text;
    [SerializeField] private GameObject Loading;
    [SerializeField] private GameObject Button;

    [SerializeField] private Transform Semi1;
    [SerializeField] private Transform Semi2;

    protected override IEnumerator OpenCoroutine()
    {
        Loading.SetActive(true);
        Text.SetActive(false);
        Button.SetActive(false);

        float time = 0;
        while (time < 5)
        {
            time += Time.deltaTime;

            Semi1.rotation = Quaternion.Euler(0, 0, time * 5 * 30);
            Semi2.rotation = Quaternion.Euler(0, 0, time * -8 * 30);

            yield return null;
        }

        Loading.SetActive(false);
        Text.SetActive(true);
        Button.SetActive(true);
    }
}