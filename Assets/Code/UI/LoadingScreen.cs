using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private float MaxSliderDelta = 0.01f;
    [SerializeField] private float FadeSpeed = 3;

    public static LoadingScreen Main { get; private set; }

    private CanvasGroup cg;
    [SerializeField] private Slider slider;

    void Start()
    {
        if(Main is not null && Main != null)
        {
            Destroy(gameObject);
            return;
        }

        Main = this;
        DontDestroyOnLoad(gameObject);
        this.gameObject.SetActive(false);

        cg = GetComponent<CanvasGroup>();
    }

    public void TrackProgress(AsyncOperation operation)
    {
        cg.blocksRaycasts = true;

        this.gameObject.SetActive(true);
        StartCoroutine(TrackProgressCoroutine(operation));
    }

    private void SetProgress(float value)
    {
        slider.value = Mathf.Clamp01(value);
    }

    private float GetProgress()
    {
        return slider.value;
    }

    private IEnumerator TrackProgressCoroutine(AsyncOperation operation)
    {
        //operation.allowSceneActivation = false; // no idea what this does *precisely*
        SetProgress(0);
        while(cg.alpha < 1)
        {
            cg.alpha = Mathf.Clamp01(cg.alpha + FadeSpeed * Time.deltaTime);
            yield return null;
        }

        var previousProgress = 0f;
        var currentProgress = 0f;

        while(GetProgress() < 1)
        {
            var progress = operation.progress / 0.9f;
            currentProgress = Mathf.Max(progress, currentProgress);

            var delta = Mathf.Min(currentProgress - previousProgress, MaxSliderDelta);

            SetProgress(Mathf.Clamp01(GetProgress() + delta));
            previousProgress = GetProgress();

            //if (GetProgress() > 0.85f) operation.allowSceneActivation = true; 

            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        while(cg.alpha > 0)
        {
            cg.alpha = Mathf.Clamp01(cg.alpha - FadeSpeed * Time.deltaTime);
            yield return null;
        }

        cg.blocksRaycasts = false;
        this.gameObject.SetActive(false);
    }
}
