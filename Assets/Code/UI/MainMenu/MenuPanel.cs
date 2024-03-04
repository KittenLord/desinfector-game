using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    public void Open()
    {
        this.gameObject.SetActive(true);
        StartCoroutine(OpenCoroutine());
    }

    protected virtual IEnumerator OpenCoroutine()
    {
        yield return null;
    }



    public void Close()
    {
        StartCoroutine(CloseCoroutine());
    }

    protected virtual IEnumerator CloseCoroutine()
    {
        yield return null;
        this.gameObject.SetActive(false);
    }
}
