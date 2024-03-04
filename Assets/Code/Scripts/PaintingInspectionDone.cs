using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingInspectionDone : MonoBehaviour
{
    [SerializeField] private GameObject PaintingUI;
    [SerializeField] private Transform Painting;

    private bool activated = false;

    public void Activate()
    {
        if (activated) return;
        activated = true;

        PaintingUI.SetActive(false);
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        yield return new WaitForSeconds(1);

        float lerp = 0;

        var op = (Vector2)Painting.position;
        var n = op + new Vector2(2.5f, 0);

        while(lerp < 1)
        {
            lerp += 0.13f * Time.deltaTime;
            Painting.position = Vector2.Lerp(op, n, lerp);
            yield return null;
        }
    }

    private void Start()
    {
        
    }
}
