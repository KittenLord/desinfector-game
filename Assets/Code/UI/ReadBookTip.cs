using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadBookTip : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;
    private bool activated = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!activated && TempFlags.Check("readBookTip"))
        {
            activated = true;
            StartCoroutine(Animation());
        }
    }

    IEnumerator Animation()
    {
        yield return new WaitForSeconds(0.7f);

        float lerp = 0;
        while(lerp < 1)
        {
            lerp += Time.deltaTime * 3;
            cg.alpha = Mathf.Clamp01(lerp);

            yield return null;
        }

        yield return new WaitForSeconds(2.5f);

        while (lerp > 0)
        {
            lerp -= Time.deltaTime * 3;
            cg.alpha = Mathf.Clamp01(lerp);

            yield return null;
        }
    }
}
