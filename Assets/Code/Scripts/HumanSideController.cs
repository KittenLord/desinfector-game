using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HumanSideController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Head;
    [SerializeField] private SpriteRenderer Eyes;
    [SerializeField] private SpriteRenderer RightArm;
    [SerializeField] private SpriteRenderer LeftArm;
    [SerializeField] private SpriteRenderer Body;
    [SerializeField] private SpriteRenderer RightLeg;
    [SerializeField] private SpriteRenderer LeftLeg;

    private Animator animator;

    public void LoadSheet(string sheet, bool vertical)
    {
        string name = sheet + "_" + (vertical ? "v" : "h");
        Sprite[] sprites = Resources.LoadAll<Sprite>("CharacterSheets/" + name);

        Head.sprite = sprites[0];
        Eyes.sprite = sprites[1];
        Body.sprite = sprites[2];
        RightArm.sprite = sprites[3];
        LeftArm.sprite = sprites[4];
        RightLeg.sprite = sprites[5];
        LeftLeg.sprite = sprites[6];
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private string currentAnimation;

    private void PlayAnimation(string animation)
    {
        if (currentAnimation == animation) return;
        currentAnimation = animation;
        animator?.CrossFade(animation, 0.5f);
    }

    public void Idle() => PlayAnimation("Idle");

    public void Move() => PlayAnimation("Walk");

    public void Death() => PlayAnimation("Death");
}
