using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiniCutscene : MonoBehaviour
{
    [SerializeField] private Transform Car;
    [SerializeField] private GameObject Player;
    [SerializeField] private CanvasGroup BlackScreen;
    [SerializeField] private Camera Camera;

    private bool Activated = false;

    private void Update()
    {
        if(TempFlags.Check("cameraMove") && !Activated)
        {
            Activated = true;
            StartCoroutine(Animation());
        }
    }

    private IEnumerator Animation()
    {
        Player.SetActive(false);
        Player.transform.position = Car.transform.position;
        Player.GetComponent<PlayerController>().canUseItems = false;

        Game.SetFlag(Flag.ArrivedToHouse);

        MusicPlayer.Main.LerpMute(0.002f);
        StartCoroutine(CarSound());

        yield return new WaitForSeconds(1);

        StartCoroutine(ZoomoutCamera());

        yield return new WaitForSeconds(2.5f);

        StartCoroutine(MoveCar());

        yield return new WaitForSeconds(4);

        StartCoroutine(BlackOut());

        yield return new WaitForSeconds(3f);

        StartCoroutine(Finish());

        yield return new WaitForSeconds(2);

        StartCoroutine(Reappear());
        Player.GetComponent<PlayerController>().canUseItems = true;
    }

    private void Start()
    {

    }

    private IEnumerator CarSound()
    {
        var carSound = Camera.main.GetComponent<AudioSource>();


        carSound.PlayOneShot(Sound.Get("car"));

        yield return new WaitForSeconds(1f);

        carSound.PlayOneShot(Sound.Get("carDoor"));
    }

    private IEnumerator ZoomoutCamera()
    {
        float lerp = 0;

        while(lerp < 1)
        {
            lerp += 0.1f * Time.deltaTime;
            Camera.orthographicSize = Mathf.Lerp(5, 9, lerp);

            yield return null;
        }
    }

    private IEnumerator MoveCar()
    {
        float lerp = 0;
        var rb = Car.GetComponent<Rigidbody2D>();

        while (lerp < 1)
        {
            lerp += 0.5f * Time.deltaTime;
            rb.velocity = new Vector2(Mathf.Lerp(0, -30, lerp), 0);

            yield return null;
        }
    }

    private IEnumerator BlackOut()
    {
        float value = 0;

        while(value < 1)
        {
            value += 0.7f * Time.deltaTime;

            BlackScreen.alpha = Mathf.Clamp01(value);
            yield return null;
        }
    }

    private IEnumerator Reappear()
    {
        float value = 0;

        while (value < 1)
        {
            value += 0.7f * Time.deltaTime;

            BlackScreen.alpha = Mathf.Clamp01(1 - value);
            yield return null;
        }
    }

    public IEnumerator Finish()
    {
        StopCoroutine("MoveCar");

        yield return new WaitForSeconds(0.5f);

        Player.SetActive(true);
        Camera.orthographicSize = 5;
        Car.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Player.transform.position = Game.MansionLocation; // mansion location
        Player.GetComponent<PlayerController>().UseItemEnd();
    }
}
