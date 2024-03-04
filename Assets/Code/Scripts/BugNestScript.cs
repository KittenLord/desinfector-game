using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BugNestScript : MonoBehaviour
{
    private ColorSprites cs;
    private RendererOpacity ro;

    public static int TotalBugs { get; set; }
    public static int RemainingBugs { get; set; }
    public static bool IsOver => RemainingBugs <= 0;


    private int Health = 30; // 150
    private bool IsDead = false;

    void Start()
    {
        cs = GetComponent<ColorSprites>();
        ro = GetComponent<RendererOpacity>();

        TotalBugs++;
        RemainingBugs++;
    }

    public void Damage()
    {
        Health--;

        if(!IsDead) StartCoroutine(DamageEffect());
    }

    private bool alreadyStarted = false;
    private IEnumerator DamageEffect()
    {
        if (alreadyStarted) yield break;
        alreadyStarted = true;

        float lerp = 0;
        while(lerp < 1)
        {
            lerp += 5 * Time.deltaTime;
            var v = Color.Lerp(Color.white, Color.red, lerp);

            cs.SetColor(v);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        while(lerp > 0)
        {
            lerp -= 5 * Time.deltaTime;
            var v = Color.Lerp(Color.white, Color.red, lerp);

            cs.SetColor(v);
            yield return null;
        }

        alreadyStarted = false;
    }

    public void Die()
    {
        if (IsDead) return;
        IsDead = true;

        PlayerPrefs.SetInt("BUGSKILLED", PlayerPrefs.GetInt("BUGSKILLED", 0) + 1);

        StartCoroutine(DieAnimation());
    }

    private IEnumerator DieAnimation()
    {
        float lerp = 0;
        while (lerp < 1)
        {
            lerp += 2.5f * Time.deltaTime;
            ro.Set(Mathf.Clamp01(1 - lerp));
            yield return null;
        }

        RemainingBugs--;
        if (IsOver) Over();
        Destroy(gameObject);

        if(RemainingBugs <= 0)
        {
            TempFlags.Set("killedAllBugs");
        }
    }

    private void Over()
    {
        MusicPlayer.Main.LerpMute(0.001f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out var player))
        {
            if (player.bodyInventory.Contains(Data.Loaded.Items.Get("armor"), 1)) return;
            player.Damage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0) Die();
    }
}
