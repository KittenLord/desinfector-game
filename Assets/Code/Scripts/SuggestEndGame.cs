using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuggestEndGame : MonoBehaviour
{
    [SerializeField] private CanvasGroup Suggestion;
    private bool Activate = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!Activate && TempFlags.Check("person1") && TempFlags.Check("person2") && TempFlags.Check("person3") && TempFlags.Check("person4") && TempFlags.Check("person5") && TempFlags.Check("person6"))
        {
            Activate = true;
            StartCoroutine(Animate());
        }
    }

    private IEnumerator Animate()
    {
        yield return new WaitForSeconds(1.5f);

        float lerp = 0;
        while (lerp < 1)
        {
            lerp += Time.deltaTime * 3;
            Suggestion.alpha = Mathf.Clamp01(lerp);

            yield return null;
        }

        yield return new WaitForSeconds(3f);

        while (lerp > 0)
        {
            lerp -= Time.deltaTime * 3;
            Suggestion.alpha = Mathf.Clamp01(lerp);

            yield return null;
        }
    }
}
