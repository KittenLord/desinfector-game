using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverAnimate : MonoBehaviour
{
    [SerializeField] private CanvasGroup PointsGroup;
    [SerializeField] private CanvasGroup SecretsGroup;

    [SerializeField] private CanvasGroup GradeLabel;
    [SerializeField] private CanvasGroup GradeText;

    [SerializeField] private TMP_Text Points;
    [SerializeField] private TMP_Text Secrets;
    [SerializeField] private TMP_Text Grade;

    private const int MaxPoints = 70000;
    private const int MaxSecrets = 1;

    public void Activate(int points, int secrets, string grade)
    {
        StartCoroutine(AnimateCoroutine(points, secrets, grade));
    }

    private IEnumerator AnimateCoroutine(int points, int secrets, string grade)
    {
        if (MusicPlayer.Main is not null)
        {
            MusicPlayer.Main.SetRelativeVolume(1);
            MusicPlayer.Main.Play(Sound.Get("ost/menu"));
        }

        yield return new WaitForSeconds(1);

        float lerp1 = 0;
        while(lerp1 < 1)
        {
            lerp1 += 5 * Time.deltaTime;
            PointsGroup.alpha = lerp1;

            yield return null;
        }

        int countedPoints = 0;
        while(countedPoints <= points) 
        {
            Points.text = countedPoints + "/" + MaxPoints;

            countedPoints += Random.Range(Mathf.Max(points / 500 - 15, 20), points / 500);
            countedPoints = Mathf.Clamp(countedPoints, 0, points);

            if(countedPoints == points)
            {
                Points.text = countedPoints + "/" + MaxPoints;
                break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(1);

        float lerp2 = 0;
        while (lerp2 < 1)
        {
            lerp2 += 5 * Time.deltaTime;
            SecretsGroup.alpha = lerp2;

            yield return null;
        }

        int countedSecrets = 0;
        while(countedSecrets <= secrets)
        {
            Secrets.text = countedSecrets + "/" + MaxSecrets;
            countedSecrets++;

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1);

        float lerp3 = 0;
        while(lerp3 < 1)
        {
            lerp3 += Time.deltaTime * 5;
            GradeLabel.alpha = lerp3;

            yield return null;
        }

        yield return new WaitForSeconds(1);

        Grade.text = grade;
        GradeText.alpha = 1;

        yield return new WaitForSeconds(4);

        Scenes.Load("MenuScene");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
