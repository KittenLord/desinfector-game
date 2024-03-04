using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SuperSecretAnimate : MonoBehaviour
{
    [SerializeField] private PlayerController Player;
    [SerializeField] private Transform Door;
    [SerializeField] private RendererOpacity Eyes;

    void Start()
    {
        
    }

    private bool active = false;

    // Update is called once per frame
    void Update()
    {
        if(!active && TempFlags.Check("bigbigsecret"))
        {
            active = true;

            StartCoroutine(Animate());
        }
    }

    private IEnumerator Animate()
    {
        Player.canMove = false;
        Player.canInteract = false;
        Player.canOpenInventory = false;
        Player.canUseItems = false;
        Player.AimAtMouse = false;
        Player.OverrideIdle = true;

        Player.Human.SetDirection(new Vector2(0, 1));
        Player.Human.Idle();
        Player.Human.SetDirection(new Vector2(0, 1));

        Player.transform.position = (Vector2)Door.transform.position + new Vector2(0, -2);

        yield return new WaitForSeconds(1);
        StartCoroutine(MovePlayer());

        Vector2 op = Door.transform.position;
        Vector2 np = op + new Vector2(-2f, 0);

        float lerp = 0;
        while(lerp < 1)
        {
            lerp += Time.deltaTime * 0.2f;
            Door.transform.position = Vector2.Lerp(op, np, lerp);

            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        lerp = 0;
        while(lerp < 1)
        {
            lerp += Time.deltaTime * 6;
            Eyes.Set(lerp);

            yield return null;
        }

        yield return new WaitForSeconds(0.3f);
        Player.EndGame(true);
    }

    private IEnumerator MovePlayer()
    {
        Player.Human.SetDirection(new Vector2(0, 1));
        yield return new WaitForSeconds(1);
        Player.Human.Move();
        Player.Human.SetDirection(new Vector2(0, 1));

        var op = (Vector2)Player.transform.position;
        var np = op + new Vector2(0, -0.7f);

        float lerp = 0;
        while (lerp < 1)
        {
            lerp += 0.5f * Time.deltaTime;
            Player.transform.position = Vector2.Lerp(op, np, lerp);

            yield return null;
        }

        Player.Human.Idle();
    }
}
