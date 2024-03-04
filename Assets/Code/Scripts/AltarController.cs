using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AltarController : MonoBehaviour
{
    [SerializeField] private GameObject CandlePrefab;

    [SerializeField] private InteractableEntity[] Candles;
    [SerializeField] private InteractableEntity Altar;

    [SerializeField] private CanvasGroup Notification;

    private int inserted = 0;
    private bool AllInserted = false;
    private bool RitualActive = false;

    public void OnCandle(int candleIndex)
    {
        if (candleIndex > 4) return; // 5 candles

        var candle = Candles[candleIndex];

        if(candle.transform.childCount == 0)
        {
            Instantiate(CandlePrefab, candle.transform);
            candle.enabled = false;
            inserted++;

            if(inserted >= 5)
            {
                AllInserted = true;
                Altar.InteractionState = "preritual";
            }

            return;
        }

        if(RitualActive)
        {
            if (Pattern[RecordedIndex] == candleIndex)
            {
                if(RecordedIndex >= Pattern.Length - 1)
                {
                    Candles.ToList().ForEach(candle => candle.transform.GetChild(0).GetComponent<CandleController>().Play(new Color(0.3f, 1, 0.3f)));
                    CleanUpAfterRitual();

                    RitualSuccess();
                }
                else candle.transform.GetChild(0).GetComponent<CandleController>().Play(new Color(0.3f, 1, 0.3f));
            }
            else
            {
                Candles.ToList().ForEach(candle => candle.transform.GetChild(0).GetComponent<CandleController>().Play(new Color(1, 0, 0)));
                StopCoroutine("AnimateCandles");
                CleanUpAfterRitual();
            }

            RecordedIndex++;
        }
    }

    private int[] Pattern;
    private int RecordedIndex = 0;

    private int PatternLength = 7;

    public void OnAltar()
    {
        if (!AllInserted) return;

        Pattern = new int[PatternLength];
        RecordedIndex = 0;

        Pattern = Pattern.Select(_ => Random.Range(0, Candles.Length)).ToArray();
        Candles.ToList().ForEach(candle => candle.enabled = true);

        RitualActive = true;
        Altar.enabled = false;

        StartCoroutine(AnimateCandles());

        //Candles.ToList().ForEach(candle => candle.transform.GetChild(0).GetComponent<CandleController>().Play());
    }

    private void CleanUpAfterRitual()
    {
        Candles.ToList().ForEach(candle => candle.enabled = false);
        RitualActive = false;
        Altar.enabled = true;
    }

    private void RitualSuccess()
    {
        var inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (!inventory.Inventory.Contains(new Item("notePart1"), 1) ||
            !inventory.Inventory.Contains(new Item("notePart2"), 1) ||
            !inventory.Inventory.Contains(new Item("notePart3"), 1))
        {
            // ritual failure interaction
            return;
        }

        inventory.Inventory.RemoveAny(new Item("notePart1"), 1);
        inventory.Inventory.RemoveAny(new Item("notePart2"), 1);
        inventory.Inventory.RemoveAny(new Item("notePart3"), 1);

        var note = Data.Loaded.Items.Get("note");
        Debug.Log(note.Id);
        inventory.Inventory.AddNext(note);

        TempFlags.Set("altar");

        // ritual success interaction

        StartCoroutine(Notify());
    }

    private IEnumerator Notify()
    {
        yield return new WaitForSeconds(0.8f);

        float lerp = 0;
        while(lerp < 1)
        {
            lerp += 3 * Time.deltaTime;
            Notification.alpha = Mathf.Clamp01(lerp);
            yield return null;
        }

        yield return new WaitForSeconds(3);

        while (lerp > 0)
        {
            lerp -= 3 * Time.deltaTime;
            Notification.alpha = Mathf.Clamp01(lerp);
            yield return null;
        }
    }

    public IEnumerator AnimateCandles()
    {
        yield return new WaitForSeconds(1);

        for(int i = 0; i < PatternLength; i++) 
        {
            var candle = Candles[Pattern[i]];

            candle.transform.GetChild(0).GetComponent<CandleController>().Play();

            yield return new WaitForSeconds(1);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
