using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnounceBugsCount : MonoBehaviour
{
    [SerializeField] private BugCounter bugCounter;
    [SerializeField] private CanvasGroup BugAdvice;

    private bool Activated = false;

    private void Update()
    {
        if (Activated) return;

        if (!BugNestScript.IsOver) return;

        Activated = true;
        StartCoroutine(Hide());
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(1.5f);

        float lerp = 0;
        while (lerp < 1)
        {
            lerp += Time.deltaTime * 0.4f;
            bugCounter.SetOpacity(1 - lerp);

            yield return null;
        }
    }

    private bool a = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!a && collision.tag == "Player")
        {
            a = true;
            StartCoroutine(Advice());
        }
    }

    private IEnumerator Advice()
    {
        float lerp = 0;
        while (lerp < 1)
        {
            lerp += Time.deltaTime * 3;
            BugAdvice.alpha = Mathf.Clamp01(lerp);

            yield return null;
        }

        yield return new WaitForSeconds(6f);

        while (lerp > 0)
        {
            lerp -= Time.deltaTime * 3;
            BugAdvice.alpha = Mathf.Clamp01(lerp);

            yield return null;
        }
    }

    public void AnnounceAll()
    {
        StartCoroutine(Announce());
    }

    private IEnumerator Announce()
    {
        bugCounter.gameObject.SetActive(true);
        bugCounter.transform.localScale = Vector3.one * 2;
        bugCounter.SetOpacity(0);
        bugCounter.Activate();

        var rect = bugCounter.GetComponent<RectTransform>();
        Debug.Log(rect is null);

        yield return new WaitForSeconds(1.2f);

        float lerp = 0;
        while(lerp < 1)
        {
            lerp += 3f * Time.deltaTime;
            bugCounter.SetOpacity(lerp);

            yield return null;
        }

        yield return new WaitForSeconds(2);

        var originalPos = rect.position;

        while(lerp > 0)
        {
            lerp -= 3f * Time.deltaTime;

            bugCounter.transform.localScale = Vector3.one * Mathf.Lerp(0.6f, 2, Mathf.Clamp01(lerp));

            rect.anchorMax = Vector3.one * Mathf.Lerp(0.5f, 1, 1 - lerp);
            rect.anchorMin = Vector3.one * Mathf.Lerp(0.5f, 1, 1 - lerp);
            rect.pivot = Vector3.one * Mathf.Lerp(0.5f, 1, 1 - lerp);

            //rect.localPosition = Vector2.Lerp(originalPos, new Vector2(Screen.width - rect.sizeDelta.x, Screen.height - rect.sizeDelta.y), 1 - lerp);
            //Debug.Log(new Vector2(Screen.width - rect.sizeDelta.x, Screen.height - rect.sizeDelta.y));

            yield return null;
        }
    }
}
