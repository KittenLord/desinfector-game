using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BirthdaySceneController : MonoBehaviour
{
    [SerializeField] private PlayerController Player;
    [SerializeField] private ColorSprites cs;

    private bool activate = false;
    private bool set = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!set)
        {
            set = true;
            cs.SetColor(Color.black);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (activate) return;
            activate = true;

            StartCoroutine(Animate());
        }
    }

    private IEnumerator Animate()
    {
        TempFlags.Set("birthdayActivated");

        Player.canMove = false;
        Player.canInteract = false;
        Player.canOpenInventory = false;
        Player.canUseItems = false;

        Player.Human.SetDirection(new Vector2(0, 1));
        Player.OverrideIdle = true;
        Player.Human.Move();

        var op = (Vector2)Player.transform.position;
        var np = op + new Vector2(0, 3.5f);

        if (MusicPlayer.Main is not null)
        {
            MusicPlayer.Main.LerpMute(0.004f);
        }

        float lerp = 0;
        while (lerp < 1)
        {
            lerp += 0.3f * Time.deltaTime;
            Player.transform.position = Vector2.Lerp(op, np, lerp);

            yield return null;
        }

        Player.Human.Idle();

        yield return new WaitForSeconds(4);

        cs.SetColor(Color.white);

        yield return new WaitForSeconds(0.5f);

        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound.Get("party"));

        yield return new WaitForSeconds(1.5f);

        if (MusicPlayer.Main is not null)
        {
            MusicPlayer.Main.SetRelativeVolume(1);
            MusicPlayer.Main.Play(Sound.Get("ost/birthday"));
        }

        var interaction = new Interaction(new Dictionary<string, List<string>>(), new Dictionary<string, List<InteractionElement>>() { { "start", new List<InteractionElement>
        {
            new SetFlagInteraction("birthday", true),
            new SetIconInteraction("people", false),
            new AppendLocalizedInteraction("interaction.birthday.people1", 50, true, true),
            new WaitInteraction(3000, true),
            new ClearInteraction(),
            new SetIconInteraction("person4", false),
            new AppendLocalizedInteraction("interaction.birthday.person1", 50, true, true),
            new HaultInteraction(),
            new ClearInteraction(),
            new SetIconInteraction("player", false),
            new AppendLocalizedInteraction("interaction.birthday.player1", 50, true, false),
            new HaultInteraction(),
            new AppendLocalizedInteraction("interaction.birthday.player2", 50, true, false),
            new HaultInteraction(),
            new ClearInteraction(),
            new SetIconInteraction("person1", false),
            new AppendLocalizedInteraction("interaction.birthday.person2", 50, true, false),
            new HaultInteraction(),
            new ClearInteraction(),
            new SetIconInteraction("person3", false),
            new AppendLocalizedInteraction("interaction.birthday.person3", 50, true, false),
            new HaultInteraction(),
            new AppendLocalizedInteraction("interaction.birthday.person4", 50, true, false),
            new HaultInteraction(),
            new ClearInteraction(),
            new SetIconInteraction("player", false),
            new AppendLocalizedInteraction("interaction.birthday.player3", 50, true, false),
            new HaultInteraction(),
            new AppendLocalizedInteraction("interaction.birthday.player4", 50, true, false),
            new HaultInteraction(),
            new ClearInteraction(),
            new AppendLocalizedInteraction("interaction.birthday.player5", 50, true, false),
            new HaultInteraction(),
            new ClearInteraction(),
            new SetIconInteraction("person5", false),
            new AppendLocalizedInteraction("interaction.birthday.person5", 50, true, false),
            new HaultInteraction(),
            new ClearInteraction(),
            new SetIconInteraction("player", false),
            new AppendLocalizedInteraction("interaction.birthday.player6", 50, true, false),
            new HaultInteraction(),
            new AppendLocalizedInteraction("interaction.birthday.player7", 50, true, false),
            new HaultInteraction(),
            new ClearInteraction(),
            new SetIconInteraction("person6", false),
            new AppendLocalizedInteraction("interaction.birthday.person6", 50, true, false),
            new HaultInteraction(),
            new ClearInteraction(),
            new SetIconInteraction("people", false),
            new AppendLocalizedInteraction("interaction.birthday.people2", 50, true, false),
            new HaultInteraction(),
            new ClearInteraction(),
            new SetIconInteraction("person2", false),
            new AppendLocalizedInteraction("interaction.birthday.person7", 50, true, false),
            new HaultInteraction(),
            new AppendLocalizedInteraction("interaction.birthday.person8", 50, true, false),
            new HaultInteraction(),
            new ClearInteraction(),
        } } });

        Debug.Log("aboba");
        Player.canInteract = true;
        Player.Human.SetDirection(new Vector2(1, 0));

        Player.StartInteractionOutside(interaction);

        Player.OverrideIdle = false;
    }
}
