using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoodooPressurePlate : MonoBehaviour
{
    [SerializeField] private ColorSprites cs;
    [SerializeField] private CanvasGroup notif;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated) return;

        if(collision.tag == "Voodoo")
        {
            activated = true;
            TempFlags.Set("voodooDone");

            cs.SetColor(new Color(0.8f, 0.8f, 0.8f));
            Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound.Get("plate"));
            StartCoroutine(c());
        }
    }

    private IEnumerator c()
    {
        yield return new WaitForSeconds(0.8f);

        float lerp = 0;
        while(lerp < 1)
        {
            lerp += 3 * Time.deltaTime;
            notif.alpha = Mathf.Clamp01(lerp);
            yield return null;
        }

        yield return new WaitForSeconds(3);

        while (lerp > 0)
        {
            lerp -= 3 * Time.deltaTime;
            notif.alpha = Mathf.Clamp01(lerp);
            yield return null;
        }
    }
}
